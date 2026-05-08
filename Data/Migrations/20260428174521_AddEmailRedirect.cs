using Microsoft.EntityFrameworkCore.Migrations;
using Data.DatabaseEnums;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailRedirect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("ApplicationParameters", ["Name", "TsInsert", "TsUpdate", "Value", "ParameterTypeId"], [Enum.GetName(ApplicationParameter.IsRedirectEmailEnabled), DateTime.Now, DateTime.Now, false.ToString(), (int)ParameterTypeEnum.Boolean]);
            migrationBuilder.InsertData("ApplicationParameters", ["Name", "TsInsert", "TsUpdate", "Value", "ParameterTypeId"], [Enum.GetName(ApplicationParameter.EmailRedirectAddress), DateTime.Now, DateTime.Now, string.Empty, (int)ParameterTypeEnum.String]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("ApplicationParameters", ["Name"], [Enum.GetName(ApplicationParameter.IsRedirectEmailEnabled)]);
            migrationBuilder.DeleteData("ApplicationParameters", ["Name"], [Enum.GetName(ApplicationParameter.EmailRedirectAddress)]);

        }
    }
}
