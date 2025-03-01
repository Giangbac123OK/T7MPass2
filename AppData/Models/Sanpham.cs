using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class Sanpham
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required(ErrorMessage = "Tên sản phẩm không được để trống")]
		[MaxLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự")]
		public string Tensp { get; set; }
		[MaxLength(200, ErrorMessage = "Tên sản phẩm không được vượt quá 200 ký tự")]
		public string? Mota { get; set; }
		[Range(0, 3, ErrorMessage = "Trạng thái không hợp lệ")]
		public int Trangthai { get; set; }
		public DateTime NgayThemMoi { get; set; }
		public string? UrlHinhanh { get; set; }
		public int Idth { get; set; }
		[ForeignKey("Idth")]
		public virtual Thuonghieu Thuonghieu { get; set; }
        public virtual ICollection<Sanphamchitiet> Sanphamchitiets { get; set; }
    }
}
