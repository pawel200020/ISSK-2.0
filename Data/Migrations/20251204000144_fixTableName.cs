using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event_Saver.Migrations
{
    /// <inheritdoc />
    public partial class fixTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationParameters_ParameterTypesDictionary_ParameterTypeId",
                table: "ApplicationParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParameterTypesDictionary",
                table: "ParameterTypesDictionary");

            migrationBuilder.RenameTable(
                name: "ParameterTypesDictionary",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationParameters_DictParameterType_ParameterTypeId",
                table: "ApplicationParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DictParameterType",
                table: "DictParameterType");

            migrationBuilder.RenameTable(
                name: "DictParameterType",
                newName: "ParameterTypesDictionary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParameterTypesDictionary",
                table: "ParameterTypesDictionary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationParameters_ParameterTypesDictionary_ParameterTypeId",
                table: "ApplicationParameters",
                column: "ParameterTypeId",
                principalTable: "ParameterTypesDictionary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
