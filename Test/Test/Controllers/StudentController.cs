using System;
using System.Linq;
using System.Web.Mvc;
using MVCEntityLogin.Models;
using Test.Models;
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


            if (date == null || time == null)
            {
                TempData["message"] = "Please Enter the details to schedule an appointment";
                return View(student);
            }
           
           var dateTime= DateTime.ParseExact(date + " " + time, "MM/dd/yyyy hh:mm:ss tt",CultureInfo.InvariantCulture);

            if(dateTime==null || DateTime.Compare(dateTime,DateTime.Now)<0)
            {
                TempData["message"] = "Please Enter a Valid Date and Time";
                return View(student);
            }

        
            CounsellingEntities db = new CounsellingEntities();

            var username = (string)Session["UserName"];

            if (db.scheduledappointments.FirstOrDefault(i => i.StudentID.ToString() == username)!=null)
            {
                TempData["message"] = "An Appointment is Scheduled Already!";

                return View(student);
            }

            if (username == null)
                return Student();

            var stud= db.Students.FirstOrDefault(i => i.PUID.ToString() == username);

            try
            {

                db.scheduledappointments.Add(new scheduledappointment()
                {
                    DateTime = dateTime,
                    StudentID = stud.PUID,
                    StudentName = stud.Name
                });

                db.SaveChanges();

                TempData["Message"] = "Appointment Scheduled Successfully!";


               
            }
            catch(Exception e)
            {
                TempData["Message"] = "Unexpected Error Encountered!";

                return RedirectToAction("Login", "Home");
            }

            return View(student);
        }

        public ActionResult WelcomePage()
        {
            return View();
        }
    }
}