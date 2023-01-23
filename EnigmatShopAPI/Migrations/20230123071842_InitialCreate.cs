using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnigmatShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ES_Customer_TM",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customername = table.Column<string>(name: "customer_name", type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nomorhp = table.Column<string>(name: "nomor_hp", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ES_Customer_TM", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ES_Product_TM",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    productname = table.Column<string>(name: "product_name", type: "nvarchar(max)", nullable: false),
                    productprice = table.Column<string>(name: "product_price", type: "nvarchar(max)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ES_Product_TM", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ES_Purchase_TM",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customerid = table.Column<Guid>(name: "customer_id", type: "uniqueidentifier", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ES_Purchase_TM", x => x.id);
                    table.ForeignKey(
                        name: "FK_ES_Purchase_TM_ES_Customer_TM_customer_id",
                        column: x => x.customerid,
                        principalTable: "ES_Customer_TM",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ES_PurchaseDetail_TM",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    purchaseid = table.Column<Guid>(name: "purchase_id", type: "uniqueidentifier", nullable: false),
                    productid = table.Column<Guid>(name: "product_id", type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ES_PurchaseDetail_TM", x => x.id);
                    table.ForeignKey(
                        name: "FK_ES_PurchaseDetail_TM_ES_Product_TM_product_id",
                        column: x => x.productid,
                        principalTable: "ES_Product_TM",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ES_PurchaseDetail_TM_ES_Purchase_TM_purchase_id",
                        column: x => x.purchaseid,
                        principalTable: "ES_Purchase_TM",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ES_Purchase_TM_customer_id",
                table: "ES_Purchase_TM",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_ES_PurchaseDetail_TM_product_id",
                table: "ES_PurchaseDetail_TM",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_ES_PurchaseDetail_TM_purchase_id",
                table: "ES_PurchaseDetail_TM",
                column: "purchase_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ES_PurchaseDetail_TM");

            migrationBuilder.DropTable(
                name: "ES_Product_TM");

            migrationBuilder.DropTable(
                name: "ES_Purchase_TM");

            migrationBuilder.DropTable(
                name: "ES_Customer_TM");
        }
    }
}
