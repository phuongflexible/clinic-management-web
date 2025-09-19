using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongKham.Data.Migrations
{
    /// <inheritdoc />
    public partial class addBacSiColumnInLichHenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BacSiId",
                table: "LichHen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LichHen_BacSiId",
                table: "LichHen",
                column: "BacSiId");

            migrationBuilder.AddForeignKey(
                name: "FK_LichHen_Bacsi_BacSiId",
                table: "LichHen",
                column: "BacSiId",
                principalTable: "Bacsi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LichHen_Bacsi_BacSiId",
                table: "LichHen");

            migrationBuilder.DropIndex(
                name: "IX_LichHen_BacSiId",
                table: "LichHen");

            migrationBuilder.DropColumn(
                name: "BacSiId",
                table: "LichHen");
        }
    }
}
