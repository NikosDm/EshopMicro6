using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EshopMicro6.Services.ProductApi.Data.Migrations
{
    public partial class ProductImageURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Appetizer", "Details about the product", "https://nthomadakis.blob.core.windows.net/eshop/14.jpg", "Samosa", 15.0 },
                    { 2, "Appetizer", "Details about the product 2", "https://nthomadakis.blob.core.windows.net/eshop/12.jpg", "Mango", 13.99 },
                    { 3, "Dessert", "Details about the product 3", "https://nthomadakis.blob.core.windows.net/eshop/11.jpg", "Sweet Pie", 10.99 },
                    { 4, "Dessert", "Details about the product 4", "https://nthomadakis.blob.core.windows.net/eshop/13.jpg", "Pav Bhaji", 10.99 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);
        }
    }
}
