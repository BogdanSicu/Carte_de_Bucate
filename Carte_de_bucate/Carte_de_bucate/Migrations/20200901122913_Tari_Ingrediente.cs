using Microsoft.EntityFrameworkCore.Migrations;

namespace Carte_de_bucate.Migrations
{
    public partial class Tari_Ingrediente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reteta",
                table: "Mancare");

            migrationBuilder.AddColumn<int>(
                name: "TaraID",
                table: "Mancare",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Timp_de_preparare",
                table: "Mancare",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ingrediente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrediente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tari",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire_tara = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tari", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingrediente_In_Retete",
                columns: table => new
                {
                    MancareID = table.Column<int>(nullable: false),
                    IngrediendID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrediente_In_Retete", x => new { x.MancareID, x.IngrediendID });
                    table.ForeignKey(
                        name: "FK_Ingrediente_In_Retete_Ingrediente_IngrediendID",
                        column: x => x.IngrediendID,
                        principalTable: "Ingrediente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingrediente_In_Retete_Mancare_MancareID",
                        column: x => x.MancareID,
                        principalTable: "Mancare",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mancare_TaraID",
                table: "Mancare",
                column: "TaraID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingrediente_In_Retete_IngrediendID",
                table: "Ingrediente_In_Retete",
                column: "IngrediendID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mancare_Tari_TaraID",
                table: "Mancare",
                column: "TaraID",
                principalTable: "Tari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mancare_Tari_TaraID",
                table: "Mancare");

            migrationBuilder.DropTable(
                name: "Ingrediente_In_Retete");

            migrationBuilder.DropTable(
                name: "Tari");

            migrationBuilder.DropTable(
                name: "Ingrediente");

            migrationBuilder.DropIndex(
                name: "IX_Mancare_TaraID",
                table: "Mancare");

            migrationBuilder.DropColumn(
                name: "TaraID",
                table: "Mancare");

            migrationBuilder.DropColumn(
                name: "Timp_de_preparare",
                table: "Mancare");

            migrationBuilder.AddColumn<string>(
                name: "Reteta",
                table: "Mancare",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
