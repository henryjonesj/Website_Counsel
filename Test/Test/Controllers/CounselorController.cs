using MVCEntityLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class CounselorController : Controller
    {
        // GET: Counselor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Counselor()
        {
            AppointmentListModel objAppointmentListModel = new AppointmentListModel();
            objAppointmentListModel.AppointmentListCollction = new List<scheduledappointment>();
            objAppointmentListModel.AppointmentListCollction = GetAppointmentList();
            return View(objAppointmentListModel);
        }
        

        public List<scheduledappointment> GetAppointmentList()
        {
            List<scheduledappointment> objAppointment = new List<scheduledappointment>();
            /*Create instance of entity model*/

            CounsellingEntities objentity = new CounsellingEntities();
            /*Getting data from database for user validation*/

            var _objappointmentdetail = (from data in objentity.scheduledappointments
                                   select data);

            foreach (var item in _objappointmentdetail)
            {
                var appointment = objentity.appointments.Find(item.StudentID);
                if (appointment != null) continue;

                objAppointment.Add(new scheduledappointment
                {
                    StudentID = item.StudentID,
                    StudentName = item.StudentName,
                    DateTime = item.DateTime
                });

            }
           

            return objAppointment;
        }

        public ActionResult Create()
        {
            AppointmentListModel objstudentlistmodel = new AppointmentListModel();
            return View(objstudentlistmodel);
        }

        [HttpPost]
        public ActionResult Create(AppointmentListModel objstudentlistmodel)
        {
            try
            {
                /*HERE WILL BE YOUR CODE TO SAVE THE FILE DETAIL IN DATA BASE*/
                scheduledappointment _objstudentlist = new scheduledappointment();
                _objstudentlist.StudentID = Convert.ToInt32(Guid.NewGuid());
                _objstudentlist.StudentName = objstudentlistmodel.AppointmentListDetail.StudentName;
                _objstudentlist.DateTime = objstudentlistmodel.AppointmentListDetail.DateTime;
   
                Save(_objstudentlist);
                /*Retrived Data*/
                List<scheduledappointment> objfilelist = new List<scheduledappointment>();
                objfilelist = GetAppointmentList();
                objstudentlistmodel.AppointmentListCollction = objfilelist;
                objstudentlistmodel.AppointmentListDetail = new scheduledappointment();
                ViewBag.Message = "Data saved successfully.";
            }
            catch
            {
                ViewBag.Message = "Error while saving data.";
            }
            return View(objstudentlistmodel);
        }

        public int Save(scheduledappointment _objstudentlist)
        {
            CounsellingEntities objentity = new CounsellingEntities();
            scheduledappointment objAppointmentDetail = new scheduledappointment();


            objAppointmentDetail.StudentID = _objstudentlist.StudentID;
            objAppointmentDetail.StudentName = _objstudentlist.StudentName;
            objAppointmentDetail.DateTime = _objstudentlist.DateTime;
            /*Save data to database*/
            objentity.SaveChanges();
            return 1;
        }

        public ActionResult Edit(string studentId)
        {
            
            if (studentId == "" || studentId == null) return View();
            CounsellingEntities objentity = new CounsellingEntities();

            AppointmentListModel model = new AppointmentListModel();

            model.AppointmentListCollction = GetAppointmentList();

            var student= model.AppointmentListCollction.FirstOrDefault(i => i.StudentID.ToString() == studentId);
   
            scheduledappointment objstudentdetail = new scheduledappointment();
            objstudentdetail = (from data in objentity.scheduledappointments
                               where data.StudentID.ToString() == studentId
                               select data).FirstOrDefault();

            scheduledappointment _objstudentlist = new scheduledappointment();
            _objstudentlist.StudentID = Convert.ToInt32(studentId);
            _objstudentlist.StudentName = objstudentdetail.StudentName;
            _objstudentlist.DateTime = objstudentdetail.DateTime;
            AppointmentListModel objstudentlistmodel = new AppointmentListModel();
            objstudentlistmodel.AppointmentListDetail = _objstudentlist;
            return View(objstudentlistmodel);
        }
        ///

        [HttpPost]
        public ActionResult Edit(AppointmentListModel objstudentlistmodel, string Id = "")
        {
            CounsellingEntities objentity = new CounsellingEntities();
            var student = objentity.scheduledappointments.FirstOrDefault(i => i.StudentID.ToString() == Id);

            if(IsInvalidDateTime(objstudentlistmodel.AppointmentListDetail.DateTime))
            {
                ViewBag.Message = "Invalid Date Time";
                return View(objstudentlistmodel);
            }

            if (string.IsNullOrEmpty(objstudentlistmodel.AppointmentListDetail.StudentName)|| 
                string.IsNullOrWhiteSpace(objstudentlistmodel.AppointmentListDetail.StudentName))
            {
                ViewBag.Message = "Invalid Student Name";
                return View(objstudentlistmodel);
            }


            student.StudentName = objstudentlistmodel.AppointmentListDetail.StudentName;
            student.DateTime = objstudentlistmodel.AppointmentListDetail.DateTime;
            
            ///*Save data to database*/
            objentity.SaveChanges();
            return RedirectToAction("/Counselor/");
        }

        public bool IsInvalidDateTime(DateTime txtDate)
        {
            return txtDate!=null ? DateTime.Compare(txtDate, DateTime.Now) < 0 : false;
        }

        public ActionResult SendMail(AppointmentListModel Detail)
        {

            if (Detail == null || Detail.AppointmentListDetail==null) return RedirectToAction("/Counselor/");

            if (string.IsNullOrEmpty(Detail.MailId) || string.IsNullOrWhiteSpace(Detail.MailId))
                return RedirectToAction("/Accept", new { studentID = Detail.AppointmentListDetail.StudentID.ToString() });

            var studentId = Detail.AppointmentListDetail.StudentID;
            var studentName = Detail.AppointmentListDetail.StudentName;
            var dateTime = Detail.AppointmentListDetail.DateTime;

            MailMessage mail = new MailMessage("hjebasin@purdue.edu", Detail.MailId);
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            //System.Net.NetworkCredential credentials =
              
            //client.EnableSsl = true;
            //client.Credentials = credentials;
            client.Host = "smtp-mail.outlook.com";
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            client.Send(mail);


            return RedirectToAction("/Counselor/");
        }

        public ActionResult Accept(int studentId)
        {
            CounsellingEntities objentity = new CounsellingEntities();

            var counselorID = Convert.ToInt32((string)Session["UserName"]);

            if ((string)(Session["UserName"])==null) return Counselor();

            objentity.appointments.Add(new appointment()
            {
                Confirmed = true,
                StudentID = studentId,
                CounselorID = Convert.ToInt32((string)Session["UserName"])
            });

            objentity.SaveChanges();

            AppointmentListModel model = new AppointmentListModel();

            model.AppointmentListCollction = GetAppointmentList();

            var student = model.AppointmentListCollction.FirstOrDefault(i => i.StudentID.ToString() == studentId.ToString());

            scheduledappointment objstudentdetail = new scheduledappointment();
            objstudentdetail = (from data in objentity.scheduledappointments
                                where data.StudentID.ToString() == studentId.ToString()
                                select data).FirstOrDefault();

            scheduledappointment _objstudentlist = new scheduledappointment();
            _objstudentlist.StudentID = Convert.ToInt32(studentId);
            _objstudentlist.StudentName = objstudentdetail.StudentName;
            _objstudentlist.DateTime = objstudentdetail.DateTime;
            AppointmentListModel objstudentlistmodel = new AppointmentListModel();
            objstudentlistmodel.AppointmentListDetail = _objstudentlist;

            ViewBag.Message = "Appointment Accepted!.";

            return View(objstudentlistmodel);
        }

    }

   
}