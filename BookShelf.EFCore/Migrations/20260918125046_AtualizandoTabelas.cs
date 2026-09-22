using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShelf.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_CEP",
                table: "Addresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CEP",
                table: "Addresses",
                column: "CEP",
                unique: true);
        }
    }
}
