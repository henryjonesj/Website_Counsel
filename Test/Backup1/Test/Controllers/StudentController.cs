using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEntityLogin.Models;
using Test.Models;
using System.Web.Security;
using System.Web.Configuration;
using System.Globalization;

namespace MVCEntityLogin.Controllers
{
    public class StudentController : Controller
    {
    
        public ActionResult Student()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Student(StudentModel student)
        {
            ModelState.Clear();

            var date = student.Date;
            var time = student.Time;

            if (date == null || time == null) return View(student);
           
           var dateTime= DateTime.ParseExact(date + " " + time, "MM/dd/yyyy hh:mm:ss tt",CultureInfo.InvariantCulture);

            AppointmentEntity db = new AppointmentEntity();
            AllAppointment db2 = new AllAppointment();

            TrackerEntities studentdb = new TrackerEntities();
            var username = (string)Session["UserName"];
            var stud= studentdb.Students.FirstOrDefault(i => i.PUID.ToString() == username);

            db.ScheduledAppointments.Add(new ScheduledAppointment()
            {
                DateTime = dateTime,
                StudentID = stud.PUID,
                StudentName = stud.Name
            });

            db.SaveChanges();

            ViewBag.Message = "Appointment Scheduled Successfully";

            return RedirectToAction("Login", "Home");
        }

        public ActionResult WelcomePage()
        {
            return View();
        }
    }
}