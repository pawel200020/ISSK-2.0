using Data.DatabaseEnums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationParameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("ApplicationParameters", ["Name", "TsInsert", "TsUpdate", "Value", "ParameterTypeId"], [Enum.GetName(ApplicationParameter.IsAnonymousRegisterEnabled), DateTime.Now,DateTime.Now, false.ToString(), (int)ParameterTypeEnum.Boolean]);
            migrationBuilder.InsertData("ApplicationParameters", ["Name", "TsInsert", "TsUpdate", "Value", "ParameterTypeId"], [Enum.GetName(ApplicationParameter.EmailLogin), DateTime.Now,DateTime.Now, null, (int)ParameterTypeEnum.String]);
            migrationBuilder.InsertData("ApplicationParameters", ["Name", "TsInsert", "TsUpdate", "Value", "ParameterTypeId"], [Enum.GetName(ApplicationParameter.EmailPassword), DateTime.Now,DateTime.Now, null, (int)ParameterTypeEnum.String]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("ApplicationParameters", ["Name"], [Enum.GetName(ApplicationParameter.IsAnonymousRegisterEnabled)]);
            migrationBuilder.DeleteData("ApplicationParameters", ["Name"], [Enum.GetName(ApplicationParameter.EmailLogin)]);
            migrationBuilder.DeleteData("ApplicationParameters", ["Name"], [Enum.GetName(ApplicationParameter.EmailPassword)]);
        }
    }
}
