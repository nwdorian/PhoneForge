using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber_Value",
                table: "Contacts",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "LastName_Value",
                table: "Contacts",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FirstName_Value",
                table: "Contacts",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Email_Value",
                table: "Contacts",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Contacts",
                newName: "PhoneNumber_Value");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Contacts",
                newName: "LastName_Value");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Contacts",
                newName: "FirstName_Value");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Contacts",
                newName: "Email_Value");
        }
    }
}
