using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capanna.alessandro._5H.prenota.Migrations
{
    /// <inheritdoc />
    public partial class Latestversione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Oggetti",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Oggetti");
        }
    }
}
