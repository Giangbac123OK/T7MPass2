﻿// <auto-generated />
using System;
using AppData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppData.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250318025451_add_Avatar_Khachhang")]
    partial class add_Avatar_Khachhang
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AppData.Models.ChatLieu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Tenchatlieu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("chatLieus");
                });

            modelBuilder.Entity("AppData.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Mamau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tenmau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("colors");
                });

            modelBuilder.Entity("AppData.Models.Danhgia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Idhdct")
                        .HasColumnType("int");

                    b.Property<int>("Idkh")
                        .HasColumnType("int");

                    b.Property<DateTime>("Ngaydanhgia")
                        .HasColumnType("datetime2");

                    b.Property<string>("Noidungdanhgia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sosao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Idhdct")
                        .IsUnique();

                    b.HasIndex("Idkh");

                    b.ToTable("danhgias");
                });

            modelBuilder.Entity("AppData.Models.Diachi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Diachicuthe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Idkh")
                        .HasColumnType("int");

                    b.Property<string>("Phuongxa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quanhuyen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tennguoinhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Thanhpho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sdtnguoinhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Idkh");

                    b.ToTable("diachis");
                });

            modelBuilder.Entity("AppData.Models.Giamgia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Donvi")
                        .HasColumnType("int");

                    b.Property<int>("Giatri")
                        .HasColumnType("int");

                    b.Property<string>("Mota")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Ngaybatdau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Ngayketthuc")
                        .HasColumnType("datetime2");

                    b.Property<int>("Soluong")
                        .HasColumnType("int");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("giamgias");
                });

            modelBuilder.Entity("AppData.Models.giamgia_rank", b =>
                {
                    b.Property<int>("IDgiamgia")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("Idrank")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.HasKey("IDgiamgia", "Idrank");

                    b.HasIndex("Idrank");

                    b.ToTable("giamgia_Ranks");
                });

            modelBuilder.Entity("AppData.Models.Giohang", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<int>("Idkh")
                        .HasColumnType("int");

                    b.Property<int>("Soluong")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Idkh")
                        .IsUnique();

                    b.ToTable("giohangs");
                });

            modelBuilder.Entity("AppData.Models.Giohangchitiet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Idgh")
                        .HasColumnType("int");

                    b.Property<int>("Idspct")
                        .HasColumnType("int");

                    b.Property<int>("Soluong")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Idgh");

                    b.HasIndex("Idspct");

                    b.ToTable("giohangchitiets");
                });

            modelBuilder.Entity("AppData.Models.Hinhanh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Iddanhgia")
                        .HasColumnType("int");

                    b.Property<int?>("Idtrahang")
                        .HasColumnType("int");

                    b.Property<string>("Urlhinhanh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Iddanhgia");

                    b.HasIndex("Idtrahang");

                    b.ToTable("hinhanhs");
                });

            modelBuilder.Entity("AppData.Models.Hoadon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Diachiship")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Ghichu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Idgg")
                        .HasColumnType("int");

                    b.Property<int?>("Idkh")
                        .HasColumnType("int");

                    b.Property<int?>("Idnv")
                        .HasColumnType("int");

                    b.Property<int>("Idpttt")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Ngaygiaothucte")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Phivanchuyen")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Thoigiandathang")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Tonggiamgia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Tongtiencantra")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Tongtiensanpham")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.Property<int>("Trangthaidonhang")
                        .HasColumnType("int");

                    b.Property<int>("Trangthaithanhtoan")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Idgg");

                    b.HasIndex("Idkh");

                    b.HasIndex("Idnv");

                    b.HasIndex("Idpttt");

                    b.ToTable("hoadons");
                });

            modelBuilder.Entity("AppData.Models.Hoadonchitiet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal?>("Giamgia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Giasp")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Idhd")
                        .HasColumnType("int");

                    b.Property<int>("Idspct")
                        .HasColumnType("int");

                    b.Property<int>("Soluong")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Idhd");

                    b.HasIndex("Idspct");

                    b.ToTable("hoadonchitiets");
                });

            modelBuilder.Entity("AppData.Models.Khachhang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Diachi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Diemsudung")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Gioitinh")
                        .HasColumnType("bit");

                    b.Property<int>("Idrank")
                        .HasColumnType("int");

                    b.Property<DateTime>("Ngaysinh")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Ngaytaotaikhoan")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Tichdiem")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Idrank");

                    b.ToTable("khachhangs");
                });

            modelBuilder.Entity("AppData.Models.Nhanvien", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Diachi")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Gioitinh")
                        .HasColumnType("bit");

                    b.Property<string>("Hovaten")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Ngaysinh")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Ngaytaotaikhoan")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("nhanviens");
                });

            modelBuilder.Entity("AppData.Models.Phuongthucthanhtoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Tenpttt")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("phuongthucthanhtoans");
                });

            modelBuilder.Entity("AppData.Models.Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("MaxMoney")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MinMoney")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Tenrank")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ranks");
                });

            modelBuilder.Entity("AppData.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Mota")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("Ngaybatdau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Ngayketthuc")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("sales");
                });

            modelBuilder.Entity("AppData.Models.Salechitiet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Donvi")
                        .HasColumnType("int");

                    b.Property<decimal>("Giatrigiam")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Idsale")
                        .HasColumnType("int");

                    b.Property<int>("Idspct")
                        .HasColumnType("int");

                    b.Property<int>("Soluong")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Idsale");

                    b.HasIndex("Idspct");

                    b.ToTable("salechitiets");
                });

            modelBuilder.Entity("AppData.Models.Sanpham", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Chieudai")
                        .HasColumnType("int");

                    b.Property<int>("Chieurong")
                        .HasColumnType("int");

                    b.Property<decimal>("GiaBan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Idth")
                        .HasColumnType("int");

                    b.Property<string>("Mota")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("NgayThemMoi")
                        .HasColumnType("datetime2");

                    b.Property<int>("Soluong")
                        .HasColumnType("int");

                    b.Property<string>("TenSanpham")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.Property<int>("Trongluong")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Idth");

                    b.ToTable("sanphams");
                });

            modelBuilder.Entity("AppData.Models.Sanphamchitiet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Giathoidiemhientai")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdChatLieu")
                        .HasColumnType("int");

                    b.Property<int>("IdMau")
                        .HasColumnType("int");

                    b.Property<int>("IdSize")
                        .HasColumnType("int");

                    b.Property<int>("Idsp")
                        .HasColumnType("int");

                    b.Property<string>("Mota")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Soluong")
                        .HasColumnType("int");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.Property<string>("UrlHinhanh")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdChatLieu");

                    b.HasIndex("IdMau");

                    b.HasIndex("IdSize");

                    b.HasIndex("Idsp");

                    b.ToTable("Sanphamchitiets");
                });

            modelBuilder.Entity("AppData.Models.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Sosize")
                        .HasColumnType("int");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("sizes");
                });

            modelBuilder.Entity("AppData.Models.Thuonghieu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Tenthuonghieu")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Tinhtrang")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("thuonghieus");
                });

            modelBuilder.Entity("AppData.Models.Trahang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Chuthich")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hinhthucxuly")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Idkh")
                        .HasColumnType("int");

                    b.Property<int?>("Idnv")
                        .HasColumnType("int");

                    b.Property<string>("Lydotrahang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Ngaytrahangdukien")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Ngaytrahangthucte")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phuongthuchoantien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Sotienhoan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Tenkhachhang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Trangthai")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Idkh");

                    b.HasIndex("Idnv");

                    b.ToTable("trahangs");
                });

            modelBuilder.Entity("AppData.Models.Trahangchitiet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Ghichu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hinhthucxuly")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Idhdct")
                        .HasColumnType("int");

                    b.Property<int>("Idth")
                        .HasColumnType("int");

                    b.Property<int>("Soluong")
                        .HasColumnType("int");

                    b.Property<int>("Tinhtrang")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Idhdct")
                        .IsUnique();

                    b.HasIndex("Idth");

                    b.ToTable("trahangchitiets");
                });

            modelBuilder.Entity("AppData.Models.Danhgia", b =>
                {
                    b.HasOne("AppData.Models.Hoadonchitiet", "Hoadonchitiet")
                        .WithOne("danhgia")
                        .HasForeignKey("AppData.Models.Danhgia", "Idhdct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Khachhang", "Khachhang")
                        .WithMany("Danhgias")
                        .HasForeignKey("Idkh")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Hoadonchitiet");

                    b.Navigation("Khachhang");
                });

            modelBuilder.Entity("AppData.Models.Diachi", b =>
                {
                    b.HasOne("AppData.Models.Khachhang", "Khachhang")
                        .WithMany("Diachis")
                        .HasForeignKey("Idkh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Khachhang");
                });

            modelBuilder.Entity("AppData.Models.giamgia_rank", b =>
                {
                    b.HasOne("AppData.Models.Giamgia", "Giamgia")
                        .WithMany("Giamgia_Ranks")
                        .HasForeignKey("IDgiamgia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Rank", "Rank")
                        .WithMany("Giamgia_Ranks")
                        .HasForeignKey("Idrank")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Giamgia");

                    b.Navigation("Rank");
                });

            modelBuilder.Entity("AppData.Models.Giohang", b =>
                {
                    b.HasOne("AppData.Models.Khachhang", "Khachhang")
                        .WithOne("Giohang")
                        .HasForeignKey("AppData.Models.Giohang", "Idkh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Khachhang");
                });

            modelBuilder.Entity("AppData.Models.Giohangchitiet", b =>
                {
                    b.HasOne("AppData.Models.Giohang", "Giohang")
                        .WithMany("Giohangchitiets")
                        .HasForeignKey("Idgh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Sanphamchitiet", "Sanphamchitiet")
                        .WithMany("Giohangchitiets")
                        .HasForeignKey("Idspct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Giohang");

                    b.Navigation("Sanphamchitiet");
                });

            modelBuilder.Entity("AppData.Models.Hinhanh", b =>
                {
                    b.HasOne("AppData.Models.Danhgia", "Danhgia")
                        .WithMany("Hinhanhs")
                        .HasForeignKey("Iddanhgia");

                    b.HasOne("AppData.Models.Trahang", "Trahang")
                        .WithMany("Hinhanhs")
                        .HasForeignKey("Idtrahang");

                    b.Navigation("Danhgia");

                    b.Navigation("Trahang");
                });

            modelBuilder.Entity("AppData.Models.Hoadon", b =>
                {
                    b.HasOne("AppData.Models.Giamgia", "Giamgia")
                        .WithMany("Hoadons")
                        .HasForeignKey("Idgg");

                    b.HasOne("AppData.Models.Khachhang", "Khachhang")
                        .WithMany("Hoadons")
                        .HasForeignKey("Idkh");

                    b.HasOne("AppData.Models.Nhanvien", "Nhanvien")
                        .WithMany("Hoadons")
                        .HasForeignKey("Idnv");

                    b.HasOne("AppData.Models.Phuongthucthanhtoan", "Phuongthucthanhtoan")
                        .WithMany()
                        .HasForeignKey("Idpttt")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Giamgia");

                    b.Navigation("Khachhang");

                    b.Navigation("Nhanvien");

                    b.Navigation("Phuongthucthanhtoan");
                });

            modelBuilder.Entity("AppData.Models.Hoadonchitiet", b =>
                {
                    b.HasOne("AppData.Models.Hoadon", "Hoadon")
                        .WithMany("Hoadonchitiets")
                        .HasForeignKey("Idhd")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Sanphamchitiet", "Idspchitiet")
                        .WithMany("Hoadonchitiets")
                        .HasForeignKey("Idspct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hoadon");

                    b.Navigation("Idspchitiet");
                });

            modelBuilder.Entity("AppData.Models.Khachhang", b =>
                {
                    b.HasOne("AppData.Models.Rank", "Rank")
                        .WithMany("Khachhangs")
                        .HasForeignKey("Idrank")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rank");
                });

            modelBuilder.Entity("AppData.Models.Salechitiet", b =>
                {
                    b.HasOne("AppData.Models.Sale", "Sale")
                        .WithMany("Salechitiets")
                        .HasForeignKey("Idsale")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Sanphamchitiet", "spchitiet")
                        .WithMany("Salechitiets")
                        .HasForeignKey("Idspct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sale");

                    b.Navigation("spchitiet");
                });

            modelBuilder.Entity("AppData.Models.Sanpham", b =>
                {
                    b.HasOne("AppData.Models.Thuonghieu", "Thuonghieu")
                        .WithMany("Sanphams")
                        .HasForeignKey("Idth")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Thuonghieu");
                });

            modelBuilder.Entity("AppData.Models.Sanphamchitiet", b =>
                {
                    b.HasOne("AppData.Models.ChatLieu", "ChatLieu")
                        .WithMany("Sanphamchitiets")
                        .HasForeignKey("IdChatLieu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Color", "Color")
                        .WithMany("Sanphamchitiets")
                        .HasForeignKey("IdMau")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Size", "Size")
                        .WithMany("Sanphamchitiets")
                        .HasForeignKey("IdSize")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Sanpham", "Sanpham")
                        .WithMany("Sanphamchitiets")
                        .HasForeignKey("Idsp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatLieu");

                    b.Navigation("Color");

                    b.Navigation("Sanpham");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("AppData.Models.Trahang", b =>
                {
                    b.HasOne("AppData.Models.Khachhang", "Khachhang")
                        .WithMany()
                        .HasForeignKey("Idkh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Nhanvien", "Nhanvien")
                        .WithMany("Trahangs")
                        .HasForeignKey("Idnv");

                    b.Navigation("Khachhang");

                    b.Navigation("Nhanvien");
                });

            modelBuilder.Entity("AppData.Models.Trahangchitiet", b =>
                {
                    b.HasOne("AppData.Models.Hoadonchitiet", "Hoadonchitiet")
                        .WithOne("Trahangchitiet")
                        .HasForeignKey("AppData.Models.Trahangchitiet", "Idhdct")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Trahang", "Trahang")
                        .WithMany("Trahangchitiets")
                        .HasForeignKey("Idth")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Hoadonchitiet");

                    b.Navigation("Trahang");
                });

            modelBuilder.Entity("AppData.Models.ChatLieu", b =>
                {
                    b.Navigation("Sanphamchitiets");
                });

            modelBuilder.Entity("AppData.Models.Color", b =>
                {
                    b.Navigation("Sanphamchitiets");
                });

            modelBuilder.Entity("AppData.Models.Danhgia", b =>
                {
                    b.Navigation("Hinhanhs");
                });

            modelBuilder.Entity("AppData.Models.Giamgia", b =>
                {
                    b.Navigation("Giamgia_Ranks");

                    b.Navigation("Hoadons");
                });

            modelBuilder.Entity("AppData.Models.Giohang", b =>
                {
                    b.Navigation("Giohangchitiets");
                });

            modelBuilder.Entity("AppData.Models.Hoadon", b =>
                {
                    b.Navigation("Hoadonchitiets");
                });

            modelBuilder.Entity("AppData.Models.Hoadonchitiet", b =>
                {
                    b.Navigation("Trahangchitiet")
                        .IsRequired();

                    b.Navigation("danhgia")
                        .IsRequired();
                });

            modelBuilder.Entity("AppData.Models.Khachhang", b =>
                {
                    b.Navigation("Danhgias");

                    b.Navigation("Diachis");

                    b.Navigation("Giohang")
                        .IsRequired();

                    b.Navigation("Hoadons");
                });

            modelBuilder.Entity("AppData.Models.Nhanvien", b =>
                {
                    b.Navigation("Hoadons");

                    b.Navigation("Trahangs");
                });

            modelBuilder.Entity("AppData.Models.Rank", b =>
                {
                    b.Navigation("Giamgia_Ranks");

                    b.Navigation("Khachhangs");
                });

            modelBuilder.Entity("AppData.Models.Sale", b =>
                {
                    b.Navigation("Salechitiets");
                });

            modelBuilder.Entity("AppData.Models.Sanpham", b =>
                {
                    b.Navigation("Sanphamchitiets");
                });

            modelBuilder.Entity("AppData.Models.Sanphamchitiet", b =>
                {
                    b.Navigation("Giohangchitiets");

                    b.Navigation("Hoadonchitiets");

                    b.Navigation("Salechitiets");
                });

            modelBuilder.Entity("AppData.Models.Size", b =>
                {
                    b.Navigation("Sanphamchitiets");
                });

            modelBuilder.Entity("AppData.Models.Thuonghieu", b =>
                {
                    b.Navigation("Sanphams");
                });

            modelBuilder.Entity("AppData.Models.Trahang", b =>
                {
                    b.Navigation("Hinhanhs");

                    b.Navigation("Trahangchitiets");
                });
#pragma warning restore 612, 618
        }
    }
}
