using System;
using backend.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegisterDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Credibility = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            var random = new Random();

            var customer1Date = DateTime.UtcNow.AddDays(random.Next(-365, 0)); // Son bir yıl içinde rastgele tarih
            var customer2Date = DateTime.UtcNow.AddDays(random.Next(-365, 0));

            var customer1Credibility = (byte)random.Next(0, 11); // 0-10 arasında rastgele byte
            var customer2Credibility = (byte)random.Next(0, 11);

            migrationBuilder.InsertData("Customers",
                columns: new[] { nameof(Customer.RegisterDate), nameof(Customer.Credibility), "UserId" },
                values: new object[,]
                {
                    { customer1Date, customer1Credibility, 1 },
                    { customer2Date, customer2Credibility, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
