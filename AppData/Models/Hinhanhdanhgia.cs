using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
	public class Hinhanhdanhgia

    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id {  get; set; }

		public string Urlhinhanh { get; set; }

		public int Iddg { get; set; }
		[ForeignKey("Iddg")]
		public virtual Danhgia Danhgia { get; set; }
	}
}
