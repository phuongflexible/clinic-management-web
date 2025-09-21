using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongKham.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdColumnInBacSiTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bacsi",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bacsi_UserId",
                table: "Bacsi",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bacsi_AspNetUsers_UserId",
                table: "Bacsi",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bacsi_AspNetUsers_UserId",
                table: "Bacsi");

            migrationBuilder.DropIndex(
                name: "IX_Bacsi_UserId",
                table: "Bacsi");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bacsi");
        }
    }
}
