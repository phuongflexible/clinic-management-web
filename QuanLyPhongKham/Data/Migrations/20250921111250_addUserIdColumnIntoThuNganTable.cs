using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongKham.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdColumnIntoThuNganTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ThuNgan",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ThuNgan_UserId",
                table: "ThuNgan",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThuNgan_AspNetUsers_UserId",
                table: "ThuNgan",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThuNgan_AspNetUsers_UserId",
                table: "ThuNgan");

            migrationBuilder.DropIndex(
                name: "IX_ThuNgan_UserId",
                table: "ThuNgan");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ThuNgan");
        }
    }
}
