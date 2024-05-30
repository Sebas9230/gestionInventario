using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIpetshop.Migrations
{
    /// <inheritdoc />
    public partial class initia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "productos",
                columns: new[] { "id", "name", "price", "quantity" },
                values: new object[,]
                {
                    { 1001, "Cool Watch", "99", "44" },
                    { 1002, "Diving Watch", "89", "89" },
                    { 1003, "Dress Watch", "85", "66" },
                    { 1004, "Military Watch", "99", "45" },
                    { 1005, "Wrist Watch", "85", "75" },
                    { 1006, "Army Watch", "99", "50" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productos");
        }
    }
}
