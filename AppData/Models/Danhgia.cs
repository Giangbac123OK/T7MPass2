﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class Danhgia
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id {  get; set; }
		public int Idkh { get; set; }
		public int soSao { get; set; }
		[ForeignKey("Idkh")]
		public virtual Khachhang Khachhang {  get; set; }
		public int Trangthai {  get; set; }
		public string? Noidungdanhgia {  get; set; }
		public DateTime Ngaydanhgia { get; set; }
		public int	Idhdct { get; set; }
		public  Hoadonchitiet Hoadonchitiet {  get; set; }

		public string? UrlHinhanh {  get; set; }
	}
}
