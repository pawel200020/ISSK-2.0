using Data.DatabaseEnums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalLoginProvidersConfigurationGoogle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("ApplicationParameters", ["Name", "TsInsert", "TsUpdate", "Value", "ParameterTypeId"],
                [Enum.GetName(ApplicationParameter.ExternalLoginProviders),
                    DateTime.Now,
                    DateTime.Now, 
                    """
                    {
                      [
                        {
                            "ClientId": "",
                            "ClientSecret": "",
                            "IsEnabled": false,
                            "ProviderName": "Google"
                        }
                      ]
                    }
                    """,
                    (int)ParameterTypeEnum.String]);

        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("ApplicationParameters", ["Name"], [Enum.GetName(ApplicationParameter.ExternalLoginProviders)]);

        }
    }
}
