using Data;
using Microsoft.EntityFrameworkCore.Migrations;
using Users;
using Users.Models.Roles;

#nullable disable

namespace Event_Saver.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoles : Migration
    {
        public static Dictionary<Guid, UserRole> UserRolesWithGuids = new()
        {
            {new Guid("501d9016-6785-4fec-889f-431fd151b4b2"), UserRole.Administrator},
            {new Guid("6c18d691-ea82-4765-b876-60c83837c93f"), UserRole.Manager},
            {new Guid("af3307f8-e217-42c1-bf23-5523feba07fc"), UserRole.Standard},
            {new Guid("cc413efd-1a3c-482f-839c-274f3e63857c"), UserRole.Basic}
        };
        
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var roleWithGuid in UserRolesWithGuids)
                migrationBuilder.InsertData(table:"AspNetRoles",columns:["Id","Name","NormalizedName"], values:[roleWithGuid.Key.ToString(),Enum.GetName(roleWithGuid.Value), Enum.GetName(roleWithGuid.Value).ToString().ToUpper()]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var roleWithGuid in UserRolesWithGuids)
                migrationBuilder.DeleteData(table:"[dbo].[AspNetRoles]",keyColumn:"Id", keyValue:roleWithGuid.Key.ToString());
        }
    }
}
