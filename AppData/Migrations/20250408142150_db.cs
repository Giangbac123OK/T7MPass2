using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chatLieus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenchatlieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trangthai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatLieus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenmau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mamau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Trangthai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "giamgias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Donvi = table.Column<int>(type: "int", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: false),
                    Giatri = table.Column<int>(type: "int", nullable: false),
                    Ngaybatdau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayketthuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Trangthai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giamgias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "nhanviens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hovaten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ngaysinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diachi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gioitinh = table.Column<bool>(type: "bit", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Trangthai = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Ngaytaotaikhoan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nhanviens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "phuongthucthanhtoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenpttt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Trangthai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phuongthucthanhtoans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ranks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenrank = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MinMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Trangthai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ranks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Trangthai = table.Column<int>(type: "int", nullable: false),
                    Ngaybatdau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayketthuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sosize = table.Column<int>(type: "int", nullable: false),
                    Trangthai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "thuonghieus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenthuonghieu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tinhtrang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thuonghieus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "giamgia_Ranks",
                columns: table => new
                {
                    IDgiamgia = table.Column<int>(type: "int", nullable: false),
                    Idrank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giamgia_Ranks", x => new { x.IDgiamgia, x.Idrank });
                    table.ForeignKey(
                        name: "FK_giamgia_Ranks_giamgias_IDgiamgia",
                        column: x => x.IDgiamgia,
                        principalTable: "giamgias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_giamgia_Ranks_ranks_Idrank",
                        column: x => x.Idrank,
                        principalTable: "ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "khachhangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ngaysinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tichdiem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaytaotaikhoan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Diemsudung = table.Column<int>(type: "int", nullable: false),
                    Trangthai = table.Column<int>(type: "int", nullable: false),
                    Idrank = table.Column<int>(type: "int", nullable: false),
                    Gioitinh = table.Column<bool>(type: "bit", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khachhangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_khachhangs_ranks_Idrank",
                        column: x => x.Idrank,
                        principalTable: "ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sanphams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSanpham = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Trangthai = table.Column<int>(type: "int", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayThemMoi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Chieudai = table.Column<int>(type: "int", nullable: false),
                    Chieurong = table.Column<int>(type: "int", nullable: false),
                    Chieucao = table.Column<int>(type: "int", nullable: false),
                    Trongluong = table.Column<int>(type: "int", nullable: false),
                    Idth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanphams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sanphams_thuonghieus_Idth",
                        column: x => x.Idth,
                        principalTable: "thuonghieus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "diachis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idkh = table.Column<int>(type: "int", nullable: false),
                    Tennguoinhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sdtnguoinhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Thanhpho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quanhuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phuongxa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diachicuthe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diachis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_diachis_khachhangs_Idkh",
                        column: x => x.Idkh,
                        principalTable: "khachhangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "giohangs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Soluong = table.Column<int>(type: "int", nullable: false),
                    Idkh = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giohangs", x => x.id);
                    table.ForeignKey(
                        name: "FK_giohangs_khachhangs_Idkh",
                        column: x => x.Idkh,
                        principalTable: "khachhangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hoadons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idnv = table.Column<int>(type: "int", nullable: true),
                    Idkh = table.Column<int>(type: "int", nullable: true),
                    Trangthaithanhtoan = table.Column<int>(type: "int", nullable: false),
                    Trangthaidonhang = table.Column<int>(type: "int", nullable: false),
                    Thoigiandathang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diachiship = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Ngaygiaothucte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tongtiencantra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tongtiensanpham = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tonggiamgia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Idgg = table.Column<int>(type: "int", nullable: true),
                    Trangthai = table.Column<int>(type: "int", nullable: false),
                    Phivanchuyen = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Idpttt = table.Column<int>(type: "int", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoadons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hoadons_giamgias_Idgg",
                        column: x => x.Idgg,
                        principalTable: "giamgias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hoadons_khachhangs_Idkh",
                        column: x => x.Idkh,
                        principalTable: "khachhangs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hoadons_nhanviens_Idnv",
                        column: x => x.Idnv,
                        principalTable: "nhanviens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hoadons_phuongthucthanhtoans_Idpttt",
                        column: x => x.Idpttt,
                        principalTable: "phuongthucthanhtoans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trahangs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tenkhachhang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diachiship = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Ngaytrahangthucte = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Idnv = table.Column<int>(type: "int", nullable: true),
                    Idkh = table.Column<int>(type: "int", nullable: false),
                    Sotienhoan = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Lydotrahang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<int>(type: "int", nullable: false),
                    Phuongthuchoantien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chuthich = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hinhthucxuly = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennganhang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sotaikhoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tentaikhoan = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trahangs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trahangs_khachhangs_Idkh",
                        column: x => x.Idkh,
                        principalTable: "khachhangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trahangs_nhanviens_Idnv",
                        column: x => x.Idnv,
                        principalTable: "nhanviens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sanphamchitiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Trangthai = table.Column<int>(type: "int", nullable: false),
                    Giathoidiemhientai = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: false),
                    UrlHinhanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idsp = table.Column<int>(type: "int", nullable: false),
                    IdSize = table.Column<int>(type: "int", nullable: false),
                    IdChatLieu = table.Column<int>(type: "int", nullable: false),
                    IdMau = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sanphamchitiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sanphamchitiets_chatLieus_IdChatLieu",
                        column: x => x.IdChatLieu,
                        principalTable: "chatLieus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sanphamchitiets_colors_IdMau",
                        column: x => x.IdMau,
                        principalTable: "colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sanphamchitiets_sanphams_Idsp",
                        column: x => x.Idsp,
                        principalTable: "sanphams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sanphamchitiets_sizes_IdSize",
                        column: x => x.IdSize,
                        principalTable: "sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "giohangchitiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idgh = table.Column<int>(type: "int", nullable: false),
                    Idspct = table.Column<int>(type: "int", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giohangchitiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_giohangchitiets_giohangs_Idgh",
                        column: x => x.Idgh,
                        principalTable: "giohangs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_giohangchitiets_Sanphamchitiets_Idspct",
                        column: x => x.Idspct,
                        principalTable: "Sanphamchitiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hoadonchitiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idhd = table.Column<int>(type: "int", nullable: false),
                    Idspct = table.Column<int>(type: "int", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: false),
                    Giasp = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Giamgia = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoadonchitiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hoadonchitiets_hoadons_Idhd",
                        column: x => x.Idhd,
                        principalTable: "hoadons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hoadonchitiets_Sanphamchitiets_Idspct",
                        column: x => x.Idspct,
                        principalTable: "Sanphamchitiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "salechitiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idspct = table.Column<int>(type: "int", nullable: false),
                    Idsale = table.Column<int>(type: "int", nullable: false),
                    Donvi = table.Column<int>(type: "int", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: false),
                    Giatrigiam = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salechitiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_salechitiets_sales_Idsale",
                        column: x => x.Idsale,
                        principalTable: "sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_salechitiets_Sanphamchitiets_Idspct",
                        column: x => x.Idspct,
                        principalTable: "Sanphamchitiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "danhgias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idkh = table.Column<int>(type: "int", nullable: false),
                    Noidungdanhgia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaydanhgia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Idhdct = table.Column<int>(type: "int", nullable: false),
                    Sosao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_danhgias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_danhgias_hoadonchitiets_Idhdct",
                        column: x => x.Idhdct,
                        principalTable: "hoadonchitiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_danhgias_khachhangs_Idkh",
                        column: x => x.Idkh,
                        principalTable: "khachhangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "trahangchitiets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Idth = table.Column<int>(type: "int", nullable: false),
                    Soluong = table.Column<int>(type: "int", nullable: false),
                    Tinhtrang = table.Column<int>(type: "int", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idhdct = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trahangchitiets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trahangchitiets_hoadonchitiets_Idhdct",
                        column: x => x.Idhdct,
                        principalTable: "hoadonchitiets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trahangchitiets_trahangs_Idth",
                        column: x => x.Idth,
                        principalTable: "trahangs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "hinhanhs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Urlhinhanh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idtrahang = table.Column<int>(type: "int", nullable: true),
                    Iddanhgia = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hinhanhs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hinhanhs_danhgias_Iddanhgia",
                        column: x => x.Iddanhgia,
                        principalTable: "danhgias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hinhanhs_trahangs_Idtrahang",
                        column: x => x.Idtrahang,
                        principalTable: "trahangs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_danhgias_Idhdct",
                table: "danhgias",
                column: "Idhdct",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_danhgias_Idkh",
                table: "danhgias",
                column: "Idkh");

            migrationBuilder.CreateIndex(
                name: "IX_diachis_Idkh",
                table: "diachis",
                column: "Idkh");

            migrationBuilder.CreateIndex(
                name: "IX_giamgia_Ranks_Idrank",
                table: "giamgia_Ranks",
                column: "Idrank");

            migrationBuilder.CreateIndex(
                name: "IX_giohangchitiets_Idgh",
                table: "giohangchitiets",
                column: "Idgh");

            migrationBuilder.CreateIndex(
                name: "IX_giohangchitiets_Idspct",
                table: "giohangchitiets",
                column: "Idspct");

            migrationBuilder.CreateIndex(
                name: "IX_giohangs_Idkh",
                table: "giohangs",
                column: "Idkh",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hinhanhs_Iddanhgia",
                table: "hinhanhs",
                column: "Iddanhgia");

            migrationBuilder.CreateIndex(
                name: "IX_hinhanhs_Idtrahang",
                table: "hinhanhs",
                column: "Idtrahang");

            migrationBuilder.CreateIndex(
                name: "IX_hoadonchitiets_Idhd",
                table: "hoadonchitiets",
                column: "Idhd");

            migrationBuilder.CreateIndex(
                name: "IX_hoadonchitiets_Idspct",
                table: "hoadonchitiets",
                column: "Idspct");

            migrationBuilder.CreateIndex(
                name: "IX_hoadons_Idgg",
                table: "hoadons",
                column: "Idgg");

            migrationBuilder.CreateIndex(
                name: "IX_hoadons_Idkh",
                table: "hoadons",
                column: "Idkh");

            migrationBuilder.CreateIndex(
                name: "IX_hoadons_Idnv",
                table: "hoadons",
                column: "Idnv");

            migrationBuilder.CreateIndex(
                name: "IX_hoadons_Idpttt",
                table: "hoadons",
                column: "Idpttt");

            migrationBuilder.CreateIndex(
                name: "IX_khachhangs_Idrank",
                table: "khachhangs",
                column: "Idrank");

            migrationBuilder.CreateIndex(
                name: "IX_salechitiets_Idsale",
                table: "salechitiets",
                column: "Idsale");

            migrationBuilder.CreateIndex(
                name: "IX_salechitiets_Idspct",
                table: "salechitiets",
                column: "Idspct");

            migrationBuilder.CreateIndex(
                name: "IX_Sanphamchitiets_IdChatLieu",
                table: "Sanphamchitiets",
                column: "IdChatLieu");

            migrationBuilder.CreateIndex(
                name: "IX_Sanphamchitiets_IdMau",
                table: "Sanphamchitiets",
                column: "IdMau");

            migrationBuilder.CreateIndex(
                name: "IX_Sanphamchitiets_IdSize",
                table: "Sanphamchitiets",
                column: "IdSize");

            migrationBuilder.CreateIndex(
                name: "IX_Sanphamchitiets_Idsp",
                table: "Sanphamchitiets",
                column: "Idsp");

            migrationBuilder.CreateIndex(
                name: "IX_sanphams_Idth",
                table: "sanphams",
                column: "Idth");

            migrationBuilder.CreateIndex(
                name: "IX_trahangchitiets_Idhdct",
                table: "trahangchitiets",
                column: "Idhdct",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trahangchitiets_Idth",
                table: "trahangchitiets",
                column: "Idth");

            migrationBuilder.CreateIndex(
                name: "IX_trahangs_Idkh",
                table: "trahangs",
                column: "Idkh");

            migrationBuilder.CreateIndex(
                name: "IX_trahangs_Idnv",
                table: "trahangs",
                column: "Idnv");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "diachis");

            migrationBuilder.DropTable(
                name: "giamgia_Ranks");

            migrationBuilder.DropTable(
                name: "giohangchitiets");

            migrationBuilder.DropTable(
                name: "hinhanhs");

            migrationBuilder.DropTable(
                name: "salechitiets");

            migrationBuilder.DropTable(
                name: "trahangchitiets");

            migrationBuilder.DropTable(
                name: "giohangs");

            migrationBuilder.DropTable(
                name: "danhgias");

            migrationBuilder.DropTable(
                name: "sales");

            migrationBuilder.DropTable(
                name: "trahangs");

            migrationBuilder.DropTable(
                name: "hoadonchitiets");

            migrationBuilder.DropTable(
                name: "hoadons");

            migrationBuilder.DropTable(
                name: "Sanphamchitiets");

            migrationBuilder.DropTable(
                name: "giamgias");

            migrationBuilder.DropTable(
                name: "khachhangs");

            migrationBuilder.DropTable(
                name: "nhanviens");

            migrationBuilder.DropTable(
                name: "phuongthucthanhtoans");

            migrationBuilder.DropTable(
                name: "chatLieus");

            migrationBuilder.DropTable(
                name: "colors");

            migrationBuilder.DropTable(
                name: "sanphams");

            migrationBuilder.DropTable(
                name: "sizes");

            migrationBuilder.DropTable(
                name: "ranks");

            migrationBuilder.DropTable(
                name: "thuonghieus");
        }
    }
}
