﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.DTO
{
	public class ThuonghieuDTO
	{
		public int Id {  get; set; }
		public string Tenthuonghieu { get; set; }
        public int Tinhtrang { get; set; }
        public bool IsUsedInProduct { get; set; }
    }
}
