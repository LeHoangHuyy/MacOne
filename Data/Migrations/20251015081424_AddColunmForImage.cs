using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Macone.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColunmForImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "tImage",
                type: "bit",
                nullable: true,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "tImage");
        }
    }
}
