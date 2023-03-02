using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHelper.Migrations
{
    public partial class schedulle2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Schedulles_SchedulleId",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "SchedulleId",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Schedulles_SchedulleId",
                table: "Doctors",
                column: "SchedulleId",
                principalTable: "Schedulles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Schedulles_SchedulleId",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "SchedulleId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Schedulles_SchedulleId",
                table: "Doctors",
                column: "SchedulleId",
                principalTable: "Schedulles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
