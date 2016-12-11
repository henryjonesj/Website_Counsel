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
    public class AppointmentListModel
    {
        public List<ScheduledAppointment> AppointmentListCollction { get; set; }
        public ScheduledAppointment AppointmentListDetail { get; set; }

        public List<AppointmentDetail> AllAppointmentCollection { get; set; }

       // public 
    }


    public class AppointmentDetail
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public System.DateTime DateTime { get; set; }

        public string ConfirmationStatus { get; set; }
    }
}