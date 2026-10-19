using Authentication.Infrastructure.Entities.ExternalLoginProviders;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGoogleProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("ExternalLoginProviders", ["Id","Name", "IsEnabled", "TsUpdate", "TsInsert", "ProviderType"],
            [ 
                Guid.NewGuid(),
                Enum.GetName(ProviderType.Google),
                false,
                DateTime.Now,
                DateTime.Now, 
                (int)ProviderType.Google]
                ); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("ExternalLoginProviders", ["Name"], [Enum.GetName(ProviderType.Google)]);
        }
    }
}
