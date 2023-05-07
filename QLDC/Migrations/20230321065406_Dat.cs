using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLDC.Migrations
{
    /// <inheritdoc />
    public partial class Dat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiangVien",
                columns: table => new
                {
                    MaGiangVien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenGiangVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    SoDienThoai = table.Column<int>(type: "int", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangVien", x => x.MaGiangVien);
                });

            migrationBuilder.CreateTable(
                name: "MonHoc",
                columns: table => new
                {
                    MaMon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: false),
                    HinhThucThi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonHoc", x => x.MaMon);
                });

            migrationBuilder.CreateTable(
                name: "VanPhongKhoa",
                columns: table => new
                {
                    MaNhanVien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<int>(type: "int", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VanPhongKhoa", x => x.MaNhanVien);
                });

            migrationBuilder.CreateTable(
                name: "GiangVien_MonHoc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGiangVien = table.Column<int>(type: "int", nullable: false),
                    MaMon = table.Column<int>(type: "int", nullable: false),
                    KyHoc = table.Column<int>(type: "int", nullable: false),
                    KhoaHoc = table.Column<int>(type: "int", nullable: false),
                    GiangVienMaGiangVien = table.Column<int>(type: "int", nullable: false),
                    MonHocMaMon = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangVien_MonHoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiangVien_MonHoc_GiangVien_GiangVienMaGiangVien",
                        column: x => x.GiangVienMaGiangVien,
                        principalTable: "GiangVien",
                        principalColumn: "MaGiangVien",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiangVien_MonHoc_MonHoc_MonHocMaMon",
                        column: x => x.MonHocMaMon,
                        principalTable: "MonHoc",
                        principalColumn: "MaMon",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeCuong",
                columns: table => new
                {
                    MaDeCuong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDeCuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TomTat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayPheDuyet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    MonHocMaMon = table.Column<int>(type: "int", nullable: false),
                    VanPhongKhoaMaNhanVien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeCuong", x => x.MaDeCuong);
                    table.ForeignKey(
                        name: "FK_DeCuong_MonHoc_MonHocMaMon",
                        column: x => x.MonHocMaMon,
                        principalTable: "MonHoc",
                        principalColumn: "MaMon",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeCuong_VanPhongKhoa_VanPhongKhoaMaNhanVien",
                        column: x => x.VanPhongKhoaMaNhanVien,
                        principalTable: "VanPhongKhoa",
                        principalColumn: "MaNhanVien",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeCuong_MonHocMaMon",
                table: "DeCuong",
                column: "MonHocMaMon");

            migrationBuilder.CreateIndex(
                name: "IX_DeCuong_VanPhongKhoaMaNhanVien",
                table: "DeCuong",
                column: "VanPhongKhoaMaNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_GiangVien_MonHoc_GiangVienMaGiangVien",
                table: "GiangVien_MonHoc",
                column: "GiangVienMaGiangVien");

            migrationBuilder.CreateIndex(
                name: "IX_GiangVien_MonHoc_MonHocMaMon",
                table: "GiangVien_MonHoc",
                column: "MonHocMaMon");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeCuong");

            migrationBuilder.DropTable(
                name: "GiangVien_MonHoc");

            migrationBuilder.DropTable(
                name: "VanPhongKhoa");

            migrationBuilder.DropTable(
                name: "GiangVien");

            migrationBuilder.DropTable(
                name: "MonHoc");
        }
    }
}
