using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnigmatShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class UsersTableCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ES_User_TM",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "NVarchar(50)", nullable: false),
                    email = table.Column<string>(type: "NVarchar(50)", nullable: false),
                    password = table.Column<string>(type: "NVarchar(255)", nullable: false),
                    role = table.Column<string>(type: "NVarchar(10)", nullable: false),
                    refreshtoken = table.Column<string>(name: "refresh_token", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ES_User_TM", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ES_User_TM");
        }
    }
}
