using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AssingnLineDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lines_AspNetUsers_SupervisorId",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Seasons_seasonId",
                table: "Lines");

            migrationBuilder.RenameColumn(
                name: "seasonId",
                table: "Lines",
                newName: "SeasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Lines_seasonId",
                table: "Lines",
                newName: "IX_Lines_SeasonId");

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorId",
                table: "Lines",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_AspNetUsers_SupervisorId",
                table: "Lines",
                column: "SupervisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Seasons_SeasonId",
                table: "Lines",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lines_AspNetUsers_SupervisorId",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Seasons_SeasonId",
                table: "Lines");

            migrationBuilder.RenameColumn(
                name: "SeasonId",
                table: "Lines",
                newName: "seasonId");

            migrationBuilder.RenameIndex(
                name: "IX_Lines_SeasonId",
                table: "Lines",
                newName: "IX_Lines_seasonId");

            migrationBuilder.AlterColumn<string>(
                name: "SupervisorId",
                table: "Lines",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_AspNetUsers_SupervisorId",
                table: "Lines",
                column: "SupervisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Seasons_seasonId",
                table: "Lines",
                column: "seasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
