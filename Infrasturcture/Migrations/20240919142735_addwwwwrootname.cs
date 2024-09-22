using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrasturcture.Migrations
{
    /// <inheritdoc />
    public partial class addwwwwrootname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WWWRootName",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WWWRootName",
                table: "Documents");
        }
    }
}
