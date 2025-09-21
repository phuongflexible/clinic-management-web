using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongKham.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdColumnInLeTanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LeTan",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_LeTan_UserId",
                table: "LeTan",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeTan_AspNetUsers_UserId",
                table: "LeTan",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeTan_AspNetUsers_UserId",
                table: "LeTan");

            migrationBuilder.DropIndex(
                name: "IX_LeTan_UserId",
                table: "LeTan");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LeTan");
        }
    }
}
