using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEntityLogin.Models;

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
			if (ModelState.IsValid)
			{
				/*DBEntity db = new DBEntity();
				var user = (from userlist in db.tblUsers
							where userlist.UserName == login.UserName && userlist.Password == login.Password
							select new
							{
								userlist.UserID,
								userlist.UserName



							}).ToList();*/

				var user = new List<string>();
				if (login.UserName.ToString() =="s")
				{
					//Session["UserName"] = user.FirstOrDefault().UserName;
					//Session["UserID"] = user.FirstOrDefault().UserID;
					return RedirectToAction("Student", "Home");
				}
				if (login.UserName.ToString() == "c")
				{
					return RedirectToAction("Counselor", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Invalid login credentials.");
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