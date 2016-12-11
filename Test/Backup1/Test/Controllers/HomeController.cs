using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEntityLogin.Models;
using Test.Models;
using System.Web.Security;
using System.Web.Configuration;

namespace MVCEntityLogin.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Login()
		{
			return View();
		}

		public ActionResult Student()
		{
			return View();
		}

		public ActionResult Counselor()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginModel login)
		{
			ModelState.Clear();

            if(login.UserName==null || login.Password==null)
                return View(login);

            Session["UserName"] = login.UserName;

       
            var hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password,
                        FormsAuthPasswordFormat.MD5.ToString());

            TrackerEntities1 db = new TrackerEntities1();

            TrackerEntities students = new TrackerEntities();
            CounselorEntities counselor = new CounselorEntities();

            if (ModelState.IsValid)
            {

                var example = db.LoginAuthentications;


                var user = (from userlist in db.LoginAuthentications
                            where userlist.Username == login.UserName && userlist.Password == hashedPassword
                            select new
                            { userlist }).ToList();

                if (user==null || user.Count()==0)
                {

                    db.LoginAuthentications.Add(new LoginAuthentication() { Username = login.UserName, Password = hashedPassword });
                    db.SaveChanges();

                    return View(login);
                }

                if (Convert.ToInt32(login.UserName) == 0)
                {
                    return RedirectToAction("Admin", "Admin");
                }

                int userId = Convert.ToInt32(login.UserName);

                var student = (from studentList in students.Students
                               where studentList.PUID== userId
                               select studentList);

                if (student.Count()!=0 &&  student.FirstOrDefault() != null)
                {
                    return RedirectToAction("Student", "Student");
                }

                var counselorData = (from counselorList in counselor.Counselors
                                     where counselorList.CounselorID == userId
                                     select counselorList);

                if (counselorData.Count()!=0 && counselorData.FirstOrDefault() != null)
                {
                    return RedirectToAction("Counselor", "Counselor");
                }

            }
			return View(login);
		}

		public ActionResult WelcomePage()
		{
			return View();
		}
	}
}