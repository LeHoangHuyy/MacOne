using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Macone.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tLoai",
                columns: table => new
                {
                    MaLoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tLoai", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "tUser",
                columns: table => new
                {
                    MaUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiKhoan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ViTri = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tUser", x => x.MaUser);
                });

            migrationBuilder.CreateTable(
                name: "tSanPham",
                columns: table => new
                {
                    MaSp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenSp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gia = table.Column<long>(type: "BIGINT", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MoTa = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    ThongTin = table.Column<string>(type: "NVARCHAR(MAX)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    MaLoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tSanPham", x => x.MaSp);
                    table.ForeignKey(
                        name: "FK_tSanPham_tLoai_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "tLoai",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tAnh",
                columns: table => new
                {
                    MaAnh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenFileAnh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tAnh", x => x.MaAnh);
                    table.ForeignKey(
                        name: "FK_tAnh_tSanPham_MaSp",
                        column: x => x.MaSp,
                        principalTable: "tSanPham",
                        principalColumn: "MaSp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tAnh_MaSp",
                table: "tAnh",
                column: "MaSp");

            migrationBuilder.CreateIndex(
                name: "IX_tSanPham_MaLoai",
                table: "tSanPham",
                column: "MaLoai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tAnh");

            migrationBuilder.DropTable(
                name: "tUser");

            migrationBuilder.DropTable(
                name: "tSanPham");

            migrationBuilder.DropTable(
                name: "tLoai");
        }
    }
}
