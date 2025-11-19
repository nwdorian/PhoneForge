#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(
                        type: "nvarchar(100)",
                        maxLength: 100,
                        nullable: false
                    ),
                    LastName = table.Column<string>(
                        type: "nvarchar(100)",
                        maxLength: 100,
                        nullable: false
                    ),
                    Email = table.Column<string>(
                        type: "nvarchar(100)",
                        maxLength: 100,
                        nullable: false
                    ),
                    PhoneNumber = table.Column<string>(
                        type: "nvarchar(20)",
                        maxLength: 20,
                        nullable: false
                    ),
                    DeletedOnUtc = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true
                    ),
                    Deleted = table.Column<bool>(
                        type: "bit",
                        nullable: false,
                        defaultValue: false
                    ),
                    CreatedOnUtc = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false
                    ),
                    ModifiedOnUtc = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Contacts");
        }
    }
}
