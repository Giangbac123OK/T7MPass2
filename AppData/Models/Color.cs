using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AppData.Models
{
    public class Color
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NameColor { get; set; }
        public string MaMau {  get; set; }
        public int trangThai { get; set; }
        public virtual ICollection<Sanphamchitiet> Sanphamchitiets { get; set; }

    }
}
