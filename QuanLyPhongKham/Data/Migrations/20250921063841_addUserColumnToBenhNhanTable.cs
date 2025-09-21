using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongKham.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUserColumnToBenhNhanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "BenhNhan",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BenhNhan_UserId",
                table: "BenhNhan",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BenhNhan_AspNetUsers_UserId",
                table: "BenhNhan",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BenhNhan_AspNetUsers_UserId",
                table: "BenhNhan");

            migrationBuilder.DropIndex(
                name: "IX_BenhNhan_UserId",
                table: "BenhNhan");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BenhNhan");
        }
    }
}
