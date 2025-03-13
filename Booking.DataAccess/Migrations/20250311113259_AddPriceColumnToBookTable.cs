using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceColumnToBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PriceAfterDiscount",
                table: "Books",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriceBeforeDiscount",
                table: "Books",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAfterDiscount",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PriceBeforeDiscount",
                table: "Books");
        }
    }
}
