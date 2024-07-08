using backend.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(72)", maxLength: 72, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData("Users",
                columns: new[] { nameof(User.Name), nameof(User.Surname), nameof(User.Email), nameof(User.Password) },
                values: new object[,]
                {
                    {"Hava", "Aydın", "hava.aydin@gmail.com", "$2a$10$RipXJuXJ8a28Vx6lw0c8ke5OD6gWHkY1SKGUNeApER9JVOOdizJQS"},
                    {"Furkan", "Acar", "furkan.acar@outlook.com", "$2a$11$iStbUVsLEt3Iu4kTq1iNaeBjYVHAxy5X.So6Dvydn8uX4TPL8XK7G" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
