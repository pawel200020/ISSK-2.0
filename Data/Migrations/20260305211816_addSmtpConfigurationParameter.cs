using Data.DatabaseEnums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addSmtpConfigurationParameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("ApplicationParameters", ["Name", "TsInsert", "TsUpdate", "Value", "ParameterTypeId"], [Enum.GetName(ApplicationParameter.SmtpConfiguration), DateTime.Now,DateTime.Now, string.Empty, (int)ParameterTypeEnum.String]);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("ApplicationParameters", ["Name"], [Enum.GetName(ApplicationParameter.SmtpConfiguration)]);

        }
    }
}
