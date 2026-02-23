using Data.DatabaseEnums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmaliModeParameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("ApplicationParameters", ["Name", "TsInsert", "TsUpdate", "Value", "ParameterTypeId"], [Enum.GetName(ApplicationParameter.EmailMode), DateTime.Now,DateTime.Now, 0.ToString(), (int)ParameterTypeEnum.Integer]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("ApplicationParameters", ["Name"], [Enum.GetName(ApplicationParameter.EmailMode)]);
        }
    }
}
