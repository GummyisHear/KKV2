using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KeelteKoolV2.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedPackagesa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FilesToDatabase",
                table: "FilesToDatabase");

            migrationBuilder.RenameTable(
                name: "FilesToDatabase",
                newName: "Files");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "ImageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "FilesToDatabase");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilesToDatabase",
                table: "FilesToDatabase",
                column: "ImageID");
        }
    }
}
