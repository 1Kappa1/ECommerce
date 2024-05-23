using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capanna.alessandro._5H.prenota.Migrations
{
    /// <inheritdoc />
    public partial class Latestvwithcart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroDiOggetti = table.Column<int>(type: "INTEGER", nullable: true),
                    Username_Utente = table.Column<string>(type: "TEXT", nullable: false),
                    Nome_Prodotto = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");
        }
    }
}
