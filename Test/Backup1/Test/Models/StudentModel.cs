using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Test.Models;

namespace MVCEntityLogin.Models
{
	public class StudentModel
	{
		public string Date { get; set; }


        public string Time { get; set; }

	}
}