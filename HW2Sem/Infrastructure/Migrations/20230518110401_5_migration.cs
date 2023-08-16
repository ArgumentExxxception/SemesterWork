using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HW2Sem.Migrations
{
    /// <inheritdoc />
    public partial class _5_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CoinAmount",
                table: "Purchases",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoinAmount",
                table: "Purchases");
        }
    }
}
