using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalonBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixBookingDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Bookings",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "CustomerEmail",
                table: "Bookings",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "AppointmentDateTime",
                table: "Bookings",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Bookings",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Bookings",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Bookings",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Bookings",
                newName: "CustomerEmail");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Bookings",
                newName: "AppointmentDateTime");
        }
    }
}
