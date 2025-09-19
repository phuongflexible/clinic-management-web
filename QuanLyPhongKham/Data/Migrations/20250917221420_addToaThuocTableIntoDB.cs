using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyPhongKham.Data.Migrations
{
    /// <inheritdoc />
    public partial class addToaThuocTableIntoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToaThuoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoSoKhamId = table.Column<int>(type: "int", nullable: false),
                    NgayKe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuocSiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToaThuoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToaThuoc_DuocSi_DuocSiId",
                        column: x => x.DuocSiId,
                        principalTable: "DuocSi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToaThuoc_HoSoKham_HoSoKhamId",
                        column: x => x.HoSoKhamId,
                        principalTable: "HoSoKham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToaThuoc_DuocSiId",
                table: "ToaThuoc",
                column: "DuocSiId");

            migrationBuilder.CreateIndex(
                name: "IX_ToaThuoc_HoSoKhamId",
                table: "ToaThuoc",
                column: "HoSoKhamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToaThuoc");
        }
    }
}
