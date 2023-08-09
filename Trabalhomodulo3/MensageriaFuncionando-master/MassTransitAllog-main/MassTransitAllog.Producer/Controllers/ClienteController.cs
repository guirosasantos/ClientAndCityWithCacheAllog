using MassTransit;
using MassTransitAllog.Shared;
using Microsoft.AspNetCore.Mvc;
using MassTransitAllog.Producer.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using StackExchange.Redis;
using System.Text.Json;
using MassTransitAllog.Producer.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MassTransitAllog.Producer.Controllers;



[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    public readonly IPublishEndpoint _publishEndpoint;

    private readonly IDatabase _redis;

    private readonly ClienteContext _context;

    public ClienteController(
        IPublishEndpoint publishEndpoint,
        IConnectionMultiplexer iConnectionMultiplexer,
        ClienteContext context
    )
    {
        _publishEndpoint = publishEndpoint;
        _redis = iConnectionMultiplexer.GetDatabase();
        _context = context;
    }

//Criar dois Endpoints GET:
//Retornar o nome dos clientes filtrando pelo ID da cidade (parâmetro).
//Retornar a quantidade de clientes por UF (parâmetro).

    [HttpGet("{id_cidade}")]
    public async Task<ActionResult<IEnumerable<ClienteDtoNome>>> GetNomes(int id_cidade)
    {
        string keyname = $"cidade_{id_cidade}";

        var resultRedis = await _redis.StringGetAsync(keyname);

        if (resultRedis.IsNull == false) {
            Console.WriteLine("Busca na cache...");
            return Ok(
                JsonSerializer.Deserialize<IEnumerable<ClienteDtoNome>>(resultRedis)
            );
        }

        Console.WriteLine("Busca no banco...");
        var clientes = await _context.Clientes.Include(c => c.Cidade).Where(c => c.Id_cidade == id_cidade).ToListAsync();

        List<ClienteDtoNome> nomesDto = new();

        foreach (var cliente in clientes)
        {
            nomesDto.Add(new ClienteDtoNome{Nome = cliente.Nome});
        }

        string json = JsonSerializer.Serialize(nomesDto);
        await _redis.StringSetAsync(keyname, json);
        await _redis.KeyExpireAsync(keyname, TimeSpan.FromSeconds(120));

        return Ok(nomesDto);
    }

    [HttpGet("cidade/{uf}")]
    public async Task<ActionResult<int>> GetQuantidade(string uf)
    {
        List<Cliente> listClienteEntity;
        List<NotificationDto>? listClientes = new();

        string keyname = $"cidade_{uf}";

        var resultRedis = await _redis.StringGetAsync(keyname); //Pega os valores dentro da cache caso exista.

        if (resultRedis.IsNull == false) {
            Console.WriteLine("Busca na cache...");

            listClientes = JsonSerializer.Deserialize<List<NotificationDto>>(resultRedis);
        } else {
            Console.WriteLine("Busca no banco...");

            listClienteEntity = await _context.Clientes
                .Include(c => c.Cidade)
                .Where(c => c.Cidade!.UF.ToLower() == uf.ToLower())
                .ToListAsync();
                
            foreach(Cliente cliente in listClienteEntity) {
                listClientes.Add(new NotificationDto{Nome = cliente.Nome, Telefone = cliente.Telefone});
            }

            string listaClientesForCache = JsonSerializer.Serialize(listClientes);
            await _redis.StringSetAsync(keyname, listaClientesForCache);
            await _redis.KeyExpireAsync(keyname, TimeSpan.FromSeconds(120));
        }

        int countResult = listClientes!.Count;
        if (countResult > 10)
        {
            List<NotificationDto> list = new();

            foreach(var cliente in listClientes)
            {
                list.Add(
                    new NotificationDto {
                        Nome = cliente.Nome,
                        Telefone = cliente.Telefone
                    }
                );
            }

            await _publishEndpoint.Publish<INotificationCreated>(new {Mensagem = list});
        }

        return Ok(countResult);
    }

/*
    [HttpPost("notification")]
    public async Task<IActionResult> Notify(NotificationDto notificationDto)
    {
        await _publishEndpoint.Publish<INotificationCreated>(
            new {
                notificationDto.Date, notificationDto.Message, notificationDto.Author
            }
        );

        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> CreateCotacao(CotacaoDtoCreate cotacaoDtoCreate) 
    {
        await _redis.StringGetDeleteAsync($"cotacao_{cotacaoDtoCreate.Data}");

        var dia = cotacaoDtoCreate.Data.Substring(0, 2);
        var mes = cotacaoDtoCreate.Data.Substring(3, 2);
        var ano = cotacaoDtoCreate.Data.Substring(6, 4);

        DateTime varDate = new DateTime(
            int.Parse(ano),
            int.Parse(mes),
            int.Parse(dia)
        ).ToUniversalTime();

        _context.Cotacoes.Add(
            new Cotacao {
                Sigla = cotacaoDtoCreate.Sigla,
                Nome_moeda = cotacaoDtoCreate.Nome_moeda,
                Valor = cotacaoDtoCreate.Valor,
                Data = varDate,
            }
        );

        await _context.SaveChangesAsync();

        if (cotacaoDtoCreate.Sigla == "USD" && cotacaoDtoCreate.Valor < 3.0m)
        {
            await _publishEndpoint.Publish<INotificationCreated>(
                new {
                    Message = "Dolar abaixo de 3 reais",
                    Author = "PlaceHolder",
                    Date = varDate
                }
            );
        }

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CotacaoDto>>> GetCotacao() 
    {
        var cotacoesToReturn = await _context.Cotacoes.ToListAsync();

        return Ok(cotacoesToReturn);
    }

    [HttpGet("bydate")]
    public async Task<ActionResult<IEnumerable<CotacaoDto>>> GetCotacao(string date, bool limparCache = false) {
        string keyname = $"cotacao_{date}";

        Regex rg = new Regex(@"^\d{2}\/\d{2}\/\d{4}$");

        if (rg.IsMatch(date) == false) { 
            return BadRequest(); 
        }

        var dia = date.Substring(0, 2);
        var mes = date.Substring(3, 2);
        var ano = date.Substring(6, 4);

        DateTime varDate;

        try {
            varDate = new DateTime(
                int.Parse(ano),
                int.Parse(mes),
                int.Parse(dia)
            );
        }
        catch (Exception e) {
            return BadRequest(e);
        }

        // Verifica se a keyword est� relacionada na cache
        var resultRedis = await _redis.StringGetAsync(keyname);

        
        if (resultRedis.IsNull == false && limparCache == false) {
            Console.WriteLine("Busca na cache...");
            return Ok(
                JsonSerializer.Deserialize<IEnumerable<Cotacao>>(resultRedis)
            );
        }

        // Caso contr�rio, busca no banco de dados
        var cotacoesToReturn = await _context.Cotacoes.Where(c => 
            c.Data.Day == varDate.Day &&
            c.Data.Month == varDate.Month &&
            c.Data.Year == varDate.Year
        ).ToListAsync();

        if (cotacoesToReturn == null || cotacoesToReturn.Count == 0) 
        {
            return NotFound("Nenhuma cota��o registrada para essa data.");
        }

        // Mensagem enviada quando consulta retornar dolar abaixo de 3 reais
        var dolar = cotacoesToReturn.FirstOrDefault(line => line.Sigla == "USD");

        if (dolar != null && dolar.Valor < 3.0m) 
        {
            await _publishEndpoint.Publish<INotificationCreated>(
                new {
                    Message = "Dolar abaixo de 3 reais",
                    Author = "PlaceHolder",
                    Date = DateTime.Now
                }
            );
        }

        // Adiciona o json na cache e define o tempo de expira��o
        string json = JsonSerializer.Serialize(cotacoesToReturn);
        await _redis.StringSetAsync(keyname, json);
        // Muito cuidado ao aumentar o valor abaixo. Ele n�o � resetado a run
        await _redis.KeyExpireAsync(keyname, TimeSpan.FromSeconds(10));

        Console.WriteLine("Busca no banco...");
        return Ok(cotacoesToReturn);
    }
    */
}