using MassTransitAllog.Producer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MassTransitAllog.Producer.Context 
{
    public class ClienteContext : DbContext 
    {
        public DbSet<Cidade> Cidades { get; set; } = null!;

        public DbSet<Cliente> Clientes { get; set; } = null!;

        public ClienteContext(DbContextOptions<ClienteContext> options) :
            base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            var cidade = modelBuilder.Entity<Cidade>();

            cidade
                .HasMany(cidade => cidade.Clientes)
                .WithOne(Cliente => Cliente.Cidade)
                .HasForeignKey(Cliente => Cliente.Id_cidade);

            cidade.HasData(
            new Cidade
            {
                Id = 1,
                Nome = "Itajaí",
                UF = "SC",
            },
            new Cidade
            {
                Id = 2,
                Nome = "Navegantes",
                UF = "SC",
            },
            new Cidade
            {
                Id = 3,
                Nome = "São Paulo",
                UF = "SP",
            },
            new Cidade
            {
                Id = 4,
                Nome = "Rio de Janeiro",
                UF = "RJ",
            }
        );

        var cliente = modelBuilder.Entity<Cliente>();

        cliente.HasData(
            new Cliente
            {
                Id = 1,
                Id_cidade = 2,
                Nome = "Paulo",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 2,
                Id_cidade = 2,
                Nome = "Carlos",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 3,
                Id_cidade = 2,
                Nome = "Saulo",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 4,
                Id_cidade = 1,
                Nome = "Marco",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 5,
                Id_cidade = 1,
                Nome = "João",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 6,
                Id_cidade = 3,
                Nome = "Pedro",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 7,
                Id_cidade = 4,
                Nome = "Luís",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente{
                Id = 8,
                Id_cidade = 4,
                Nome = "Guilherme",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 9,
                Id_cidade = 4,
                Nome = "Pedro",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 10,
                Id_cidade = 4,
                Nome = "Jorge",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 11,
                Id_cidade = 4,
                Nome = "Matheus",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 12,
                Id_cidade = 4,
                Nome = "Guilherme Rosa",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 13,
                Id_cidade = 4,
                Nome = "Lucas Iago",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 14,
                Id_cidade = 4,
                Nome = "Thomas Shelby",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 15,
                Id_cidade = 4,
                Nome = "Superman",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 16,
                Id_cidade = 4,
                Nome = "Batman",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "123679677"
            },
            new Cliente
            {
                Id = 17,
                Id_cidade = 4,
                Nome = "Flash",
                Endereco = "Rua Etc Bairro Teste Numero 121",
                Telefone = "234567899"
            }
        );

            base.OnModelCreating(modelBuilder);
        }
    }
}
