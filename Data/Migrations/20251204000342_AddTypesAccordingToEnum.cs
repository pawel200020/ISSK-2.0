using Data.DatabaseEnums;
using Data.Entites.Configuration;
using Data.Entites.Languages;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event_Saver.Migrations
{
    /// <inheritdoc />
    public partial class AddTypesAccordingToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var values = Enum.GetValues(typeof(ParameterTypeEnum)).Cast<ParameterTypeEnum>();
            foreach (var value in values)
            {
                var enumName = Enum.GetName(typeof(ParameterTypeEnum), value);
                migrationBuilder.InsertData("DictParameterTypes", ["Name"], [enumName]);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var values = Enum.GetValues(typeof(ParameterTypeEnum)).Cast<ParameterTypeEnum>();
            foreach (var value in values)
            {
                var enumName = Enum.GetName(typeof(ParameterTypeEnum), value);
                migrationBuilder.DeleteData("DictParameterTypes", ["Name"], [enumName]);
            }
        }
    }
}
