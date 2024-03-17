using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sorage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductAmount = table.Column<int>(type: "integer", nullable: false),
                    ProductsStorageName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductsStorageId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Deccription = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "CategoryToProduct",
                        column: x => x.Id,
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsStorage",
                columns: table => new
                {
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductsStorageId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsStorage", x => new { x.ProductsId, x.ProductsStorageId });
                    table.ForeignKey(
                        name: "FK_ProductsStorage_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsStorage_Sorage_ProductsStorageId",
                        column: x => x.ProductsStorageId,
                        principalTable: "Sorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryName",
                table: "ProductCategory",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductName",
                table: "Products",
                column: "ProductName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsStorage_ProductsStorageId",
                table: "ProductsStorage",
                column: "ProductsStorageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsStorage");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sorage");

            migrationBuilder.DropTable(
                name: "ProductCategory");
        }
    }
}
