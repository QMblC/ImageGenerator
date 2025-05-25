using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageGenerator.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Images",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "ScaleFactor",
                table: "Images",
                newName: "scaleFactor");

            migrationBuilder.RenameColumn(
                name: "FontSize",
                table: "Images",
                newName: "fontSize");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Images",
                newName: "fileName");

            migrationBuilder.RenameColumn(
                name: "FileData",
                table: "Images",
                newName: "fileData");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Images",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "NameYPos",
                table: "Images",
                newName: "yPos");

            migrationBuilder.RenameColumn(
                name: "NameXPos",
                table: "Images",
                newName: "xPos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Images",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "scaleFactor",
                table: "Images",
                newName: "ScaleFactor");

            migrationBuilder.RenameColumn(
                name: "fontSize",
                table: "Images",
                newName: "FontSize");

            migrationBuilder.RenameColumn(
                name: "fileName",
                table: "Images",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "fileData",
                table: "Images",
                newName: "FileData");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Images",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "yPos",
                table: "Images",
                newName: "NameYPos");

            migrationBuilder.RenameColumn(
                name: "xPos",
                table: "Images",
                newName: "NameXPos");
        }
    }
}
