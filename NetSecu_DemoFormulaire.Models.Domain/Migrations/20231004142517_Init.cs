using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetSecu_DemoFormulaire.Models.Domain.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Nom = table.Column<string>(type: "NVARCHAR(75)", nullable: false),
                    Prenom = table.Column<string>(type: "NVARCHAR(384)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passwd = table.Column<byte[]>(type: "BINARY(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utilisateur");
        }
    }
}
