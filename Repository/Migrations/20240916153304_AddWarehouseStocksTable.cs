using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehouseStocksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseStock_Products_ProductId",
                table: "WarehouseStock");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseStock_Warehouses_WarehouseId",
                table: "WarehouseStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseStock",
                table: "WarehouseStock");

            migrationBuilder.RenameTable(
                name: "WarehouseStock",
                newName: "WarehouseStocks");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseStock_WarehouseId",
                table: "WarehouseStocks",
                newName: "IX_WarehouseStocks_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseStock_ProductId",
                table: "WarehouseStocks",
                newName: "IX_WarehouseStocks_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Warehouses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Warehouses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseStocks",
                table: "WarehouseStocks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseStocks_Products_ProductId",
                table: "WarehouseStocks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseStocks_Warehouses_WarehouseId",
                table: "WarehouseStocks",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseStocks_Products_ProductId",
                table: "WarehouseStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseStocks_Warehouses_WarehouseId",
                table: "WarehouseStocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseStocks",
                table: "WarehouseStocks");

            migrationBuilder.RenameTable(
                name: "WarehouseStocks",
                newName: "WarehouseStock");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseStocks_WarehouseId",
                table: "WarehouseStock",
                newName: "IX_WarehouseStock_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseStocks_ProductId",
                table: "WarehouseStock",
                newName: "IX_WarehouseStock_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Warehouses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseStock",
                table: "WarehouseStock",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseStock_Products_ProductId",
                table: "WarehouseStock",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseStock_Warehouses_WarehouseId",
                table: "WarehouseStock",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
