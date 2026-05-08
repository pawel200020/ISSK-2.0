using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event_Saver.Migrations
{
    /// <inheritdoc />
    public partial class ExtendApplicationParameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParameterTypeId",
                table: "ApplicationParameters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ParameterTypesDictionary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterTypesDictionary", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationParameters_ParameterTypeId",
                table: "ApplicationParameters",
                column: "ParameterTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationParameters_ParameterTypesDictionary_ParameterTypeId",
                table: "ApplicationParameters",
                column: "ParameterTypeId",
                principalTable: "ParameterTypesDictionary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationParameters_ParameterTypesDictionary_ParameterTypeId",
                table: "ApplicationParameters");

            migrationBuilder.DropTable(
                name: "ParameterTypesDictionary");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationParameters_ParameterTypeId",
                table: "ApplicationParameters");

            migrationBuilder.DropColumn(
                name: "ParameterTypeId",
                table: "ApplicationParameters");
        }
    }
}
