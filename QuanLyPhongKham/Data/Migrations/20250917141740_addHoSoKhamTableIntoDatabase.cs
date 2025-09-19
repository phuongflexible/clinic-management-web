using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongKham.Data.Migrations
{
    /// <inheritdoc />
    public partial class addHoSoKhamTableIntoDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HoSoKham",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BenhNhanId = table.Column<int>(type: "int", nullable: false),
                    TrieuChung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChanDoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KetLuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayKham = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BacSiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoKham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoSoKham_Bacsi_BacSiId",
                        column: x => x.BacSiId,
                        principalTable: "Bacsi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoSoKham_BenhNhan_BenhNhanId",
                        column: x => x.BenhNhanId,
                        principalTable: "BenhNhan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoSoKham_BacSiId",
                table: "HoSoKham",
                column: "BacSiId");

            migrationBuilder.CreateIndex(
                name: "IX_HoSoKham_BenhNhanId",
                table: "HoSoKham",
                column: "BenhNhanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoSoKham");
        }
    }
}
