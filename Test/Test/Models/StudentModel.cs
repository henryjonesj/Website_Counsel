using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MVCEntityLogin.Models
{
	public class StudentModel
	{
        [Required(ErrorMessage = "Please enter a date.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        [StringLength(30)]
        public string Date { get; set; }


        public string Time { get; set; }

	}
}