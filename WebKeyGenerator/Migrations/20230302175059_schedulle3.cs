using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHelper.Migrations
{
    public partial class schedulle3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Schedulles_SchedulleId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedulles",
                table: "Schedulles");

            migrationBuilder.RenameTable(
                name: "Schedulles",
                newName: "Schedulle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedulle",
                table: "Schedulle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Schedulle_SchedulleId",
                table: "Doctors",
                column: "SchedulleId",
                principalTable: "Schedulle",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Schedulle_SchedulleId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedulle",
                table: "Schedulle");

            migrationBuilder.RenameTable(
                name: "Schedulle",
                newName: "Schedulles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedulles",
                table: "Schedulles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Schedulles_SchedulleId",
                table: "Doctors",
                column: "SchedulleId",
                principalTable: "Schedulles",
                principalColumn: "Id");
        }
    }
}
