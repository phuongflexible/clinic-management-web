using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongKham.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdColumnInDuocSiTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DuocSi",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DuocSi_UserId",
                table: "DuocSi",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DuocSi_AspNetUsers_UserId",
                table: "DuocSi",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DuocSi_AspNetUsers_UserId",
                table: "DuocSi");

            migrationBuilder.DropIndex(
                name: "IX_DuocSi_UserId",
                table: "DuocSi");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DuocSi");
        }
    }
}
