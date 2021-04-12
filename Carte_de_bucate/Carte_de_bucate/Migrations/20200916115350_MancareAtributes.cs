using Microsoft.EntityFrameworkCore.Migrations;

namespace Carte_de_bucate.Migrations
{
    public partial class MancareAtributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(table:"Mancare", name:"Name_of_the_food", newName:"NameFood");
            migrationBuilder.RenameColumn(table: "Mancare", name: "Mod_de_preparare", newName: "ModPreparare");
            migrationBuilder.RenameColumn(table: "Mancare", name: "Timp_de_preparare", newName: "TimpPreparare");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(table: "Mancare", name: "TimpPreparare", newName: "Timp_de_preparare");
            migrationBuilder.RenameColumn(table: "Mancare", name: "NameFood", newName: "Name_of_the_food");
            migrationBuilder.RenameColumn(table: "Mancare", name: "ModPreparare", newName: "Mod_de_preparare");
        }
    }
}
