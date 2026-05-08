using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event_Saver.Migrations
{
    /// <inheritdoc />
    public partial class fixTableName2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationParameters_DictParameterType_ParameterTypeId",
                table: "ApplicationParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DictParameterType",
                table: "DictParameterType");

            migrationBuilder.RenameTable(
                name: "DictParameterType",
                newName: "DictParameterTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DictParameterTypes",
                table: "DictParameterTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationParameters_DictParameterTypes_ParameterTypeId",
                table: "ApplicationParameters",
                column: "ParameterTypeId",
                principalTable: "DictParameterTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationParameters_DictParameterTypes_ParameterTypeId",
                table: "ApplicationParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DictParameterTypes",
                table: "DictParameterTypes");

            migrationBuilder.RenameTable(
                name: "DictParameterTypes",
                newName: "DictParameterType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DictParameterType",
                table: "DictParameterType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationParameters_DictParameterType_ParameterTypeId",
                table: "ApplicationParameters",
                column: "ParameterTypeId",
                principalTable: "DictParameterType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
