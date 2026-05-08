using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event_Saver.Migrations
{
    /// <inheritdoc />
    public partial class AddNewSupportedLanguagesIntoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("SupportedLanguages", ["Name", "Code"], ["English", "en-US"]);
            migrationBuilder.InsertData("SupportedLanguages", ["Name", "Code"], ["Polish", "pl-PL"]);
            migrationBuilder.InsertData("SupportedLanguages", ["Name", "Code"], ["German", "de-DE"]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("SupportedLanguages", ["Name", "Code"], ["English", "en-US"]);
            migrationBuilder.DeleteData("SupportedLanguages", ["Name", "Code"], ["Polish", "pl-PL"]);
            migrationBuilder.DeleteData("SupportedLanguages", ["Name", "Code"], ["German", "de-DE"]);
        }
    }
}
