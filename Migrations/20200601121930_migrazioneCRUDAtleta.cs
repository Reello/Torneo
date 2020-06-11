using Microsoft.EntityFrameworkCore.Migrations;

namespace TorneoJudo.Migrations
{
    public partial class migrazioneCRUDAtleta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atleta",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    nome = table.Column<string>(nullable: true),
                    cognome = table.Column<string>(nullable: true),
                    foreignKeySquadra = table.Column<string>(nullable: true),
                    sesso = table.Column<string>(nullable: false),
                    peso = table.Column<int>(nullable: false),
                    cintura = table.Column<string>(nullable: true),
                    vittorie = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atleta", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atleta");
        }
    }
}
