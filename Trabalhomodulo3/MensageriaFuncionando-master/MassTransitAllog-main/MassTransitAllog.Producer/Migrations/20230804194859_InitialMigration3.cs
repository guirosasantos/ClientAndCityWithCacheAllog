using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MassTransitAllog.Producer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Endereco", "Id_cidade", "Nome", "Telefone" },
                values: new object[,]
                {
                    { 8, "Rua Etc Bairro Teste Numero 121", 4, "Guilherme", "123679677" },
                    { 9, "Rua Etc Bairro Teste Numero 121", 4, "Pedro", "123679677" },
                    { 10, "Rua Etc Bairro Teste Numero 121", 4, "Jorge", "123679677" },
                    { 11, "Rua Etc Bairro Teste Numero 121", 4, "Matheus", "123679677" },
                    { 12, "Rua Etc Bairro Teste Numero 121", 4, "Guilherme Rosa", "123679677" },
                    { 13, "Rua Etc Bairro Teste Numero 121", 4, "Lucas Iago", "123679677" },
                    { 14, "Rua Etc Bairro Teste Numero 121", 4, "Thomas Shelby", "123679677" },
                    { 15, "Rua Etc Bairro Teste Numero 121", 4, "Superman", "123679677" },
                    { 16, "Rua Etc Bairro Teste Numero 121", 4, "Batman", "123679677" },
                    { 17, "Rua Etc Bairro Teste Numero 121", 4, "Flash", "234567899" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 17);
        }
    }
}
