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

            CounsellingEntities db = new CounsellingEntities();

            if (ModelState.IsValid)
            {

                var example = db.loginauthentications;


                var user = (from userlist in db.loginauthentications
                            where userlist.Username == login.UserName && userlist.Password == hashedPassword
                            select new
                            { userlist }).ToList();

                if (user==null || user.Count()==0)
                {
                    try
                    {
                        db.loginauthentications.Add(new loginauthentication() { Username = login.UserName, Password = hashedPassword });
                        db.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        TempData["Message"] = "Unexpected Error Encountered!";
                    }

                    return View(login);
                }

                if (Convert.ToInt32(login.UserName) == 0)
                {
                    return RedirectToAction("Admin", "Admin");
                }

                int userId = Convert.ToInt32(login.UserName);

                var student = (from studentList in db.Students
                               where studentList.PUID== userId
                               select studentList);

                if (student.Count()!=0 &&  student.FirstOrDefault() != null)
                {
                    return RedirectToAction("Student", "Student");
                }

                var counselorData = (from counselorList in db.counselors
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