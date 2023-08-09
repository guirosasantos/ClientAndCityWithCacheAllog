using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MassTransitAllog.Producer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cidades",
                columns: new[] { "Id", "Nome", "UF" },
                values: new object[,]
                {
                    { 1, "Itajaí", "SC" },
                    { 2, "Navegantes", "SC" },
                    { 3, "São Paulo", "SP" },
                    { 4, "Rio de Janeiro", "RJ" }
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Endereco", "Id_cidade", "Nome", "Telefone" },
                values: new object[,]
                {
                    { 1, "Rua Etc Bairro Teste Numero 121", 2, "Paulo", "123679677" },
                    { 2, "Rua Etc Bairro Teste Numero 121", 2, "Carlos", "123679677" },
                    { 3, "Rua Etc Bairro Teste Numero 121", 2, "Saulo", "123679677" },
                    { 4, "Rua Etc Bairro Teste Numero 121", 1, "Marco", "123679677" },
                    { 5, "Rua Etc Bairro Teste Numero 121", 1, "João", "123679677" },
                    { 6, "Rua Etc Bairro Teste Numero 121", 3, "Pedro", "123679677" },
                    { 7, "Rua Etc Bairro Teste Numero 121", 4, "Luís", "123679677" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cidades",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cidades",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cidades",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cidades",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
