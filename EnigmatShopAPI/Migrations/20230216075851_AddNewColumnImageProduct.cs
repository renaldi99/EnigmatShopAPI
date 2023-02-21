using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnigmatShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnImageProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "ES_Product_TM",
                type: "NVarchar(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "ES_Product_TM");
        }
    }
}
