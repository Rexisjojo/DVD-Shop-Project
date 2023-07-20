using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVD_Shop.Data.Migrations
{
    /// <inheritdoc />
    public partial class addNewsContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "News",
                table: "NewsContents",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "NewsTitle",
                table: "NewsContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsTitle",
                table: "NewsContents");

            migrationBuilder.AlterColumn<int>(
                name: "News",
                table: "NewsContents",
                type: "int",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
