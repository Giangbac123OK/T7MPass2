﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AppData.Models
{
    public enum TraHangStatus
    {
        [Description("Đơn hàng chờ trả hàng")]
        DonhangChoTraHang = 0,

        [Description("Trả hàng thành công")]
        TraHangThanhCong = 1,

        [Description("Trả hàng không thành công")]
        TraHangKhongThanhCong = 2,
    }
    public class Trahang
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id {  get; set; }

		public string Tenkhachhang {  get; set; }
        [StringLength(200, ErrorMessage = "Địa chỉ ship không được vượt quá 200 ký tự.")]
        public string? Diachiship { get; set; }
        public DateTime? Ngaytrahangthucte { get; set; }
        public int? Idnv { get; set; }
		[ForeignKey("Idnv")]
		public virtual Nhanvien Nhanvien { get; set; }

		public int Idkh { get; set; }
		[ForeignKey("Idkh")]
		public virtual Khachhang Khachhang { get; set; }

		public decimal? Sotienhoan {  get; set; }

		public string? Lydotrahang {  get; set; }

		public int Trangthai {  get; set; }
        
        public string Phuongthuchoantien {  get; set; }
		public string? Chuthich {  get; set; }
        public int Trangthaihoantien { get; set; } // 0: chưa hoàn tiền, 1: đã hoàn tiền
        public string? Hinhthucxuly { get; set; }
        public string? Tennganhang { get; set; }
        public string? Sotaikhoan { get; set; }
        public string? Tentaikhoan { get; set; }
        public virtual ICollection<Hinhanh> Hinhanhs { get; set; }
		public virtual ICollection<Trahangchitiet> Trahangchitiets { get; set; }

        // Trạng thái hiển thị dưới dạng chuỗiaaaa
        public string TrangthaiStr => GetEnumDescription((TraHangStatus)Trangthai);

        // Phương thức để lấy giá trị mô tả từ enum
        private string GetEnumDescription(TraHangStatus status)
        {
            var field = status.GetType().GetField(status.ToString());
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute == null ? status.ToString() : attribute.Description;
        }

    }
}
