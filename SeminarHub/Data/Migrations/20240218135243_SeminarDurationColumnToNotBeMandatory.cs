using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeminarHub.Data.Migrations
{
    public partial class SeminarDurationColumnToNotBeMandatory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Seminars",
                type: "int",
                nullable: true,
                comment: "Seminar Duration",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Seminar Duration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Seminars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Seminar Duration",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Seminar Duration");
        }
    }
}
