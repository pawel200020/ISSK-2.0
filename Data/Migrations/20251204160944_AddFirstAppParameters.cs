using Data.DatabaseEnums;
using Data.Entites.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;
using ApplicationParameter = Data.DatabaseEnums.ApplicationParameter;

#nullable disable

namespace Event_Saver.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstAppParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("ApplicationParameters", ["Name", "TsInsert", "TsUpdate", "Value", "ParameterTypeId"], [Enum.GetName(ApplicationParameter.ApplicationName), DateTime.Now,DateTime.Now, "New Application *", (int)ParameterTypeEnum.String]);
            migrationBuilder.InsertData("ApplicationParameters", ["Name", "TsInsert", "TsUpdate", "Value", "ParameterTypeId"], [Enum.GetName(ApplicationParameter.IsWeatherEnabled), DateTime.Now,DateTime.Now, true.ToString(), (int)ParameterTypeEnum.Boolean]);
            migrationBuilder.InsertData("ApplicationParameters", ["Name", "TsInsert", "TsUpdate", "Value", "ParameterTypeId"], [Enum.GetName(ApplicationParameter.IsRankEnabled), DateTime.Now,DateTime.Now, true.ToString(), (int)ParameterTypeEnum.Boolean]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("ApplicationParameters", ["Name"], [Enum.GetName(ApplicationParameter.ApplicationName)]);
            migrationBuilder.InsertData("ApplicationParameters", ["Name"], [Enum.GetName(ApplicationParameter.IsWeatherEnabled)]);
            migrationBuilder.InsertData("ApplicationParameters", ["Name"], [Enum.GetName(ApplicationParameter.IsRankEnabled)]);
        }
    }
}
