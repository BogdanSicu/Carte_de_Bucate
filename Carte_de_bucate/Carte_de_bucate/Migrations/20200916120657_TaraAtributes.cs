using Microsoft.EntityFrameworkCore.Migrations;

namespace Carte_de_bucate.Migrations
{
    public partial class TaraAtributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(table: "Tari", name: "Denumire_tara", newName: "DenumireTara ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(table: "Tari", name: "DenumireTara ", newName: "Denumire_tara");
        }
    }
}
