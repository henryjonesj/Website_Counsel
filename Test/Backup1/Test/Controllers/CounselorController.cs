using MVCEntityLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            objAppointmentListModel.AppointmentListCollction = new List<ScheduledAppointment>();
            objAppointmentListModel.AppointmentListCollction = GetAppointmentList();
            return View(objAppointmentListModel);
        }
        ///


        /// Get student record
        ///
        public List<ScheduledAppointment> GetAppointmentList()
        {
            List<ScheduledAppointment> objAppointment = new List<ScheduledAppointment>();
            /*Create instance of entity model*/

            AppointmentEntity objentity = new AppointmentEntity();
            /*Getting data from database for user validation*/

            var _objappointmentdetail = (from data in objentity.ScheduledAppointments
                                   select data);

            foreach (var item in _objappointmentdetail)
            {
                objAppointment.Add(new ScheduledAppointment
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
                ScheduledAppointment _objstudentlist = new ScheduledAppointment();
                _objstudentlist.StudentID = Convert.ToInt32(Guid.NewGuid());
                _objstudentlist.StudentName = objstudentlistmodel.AppointmentListDetail.StudentName;
                _objstudentlist.DateTime = objstudentlistmodel.AppointmentListDetail.DateTime;
   
                Save(_objstudentlist);
                /*Retrived Data*/
                List<ScheduledAppointment> objfilelist = new List<ScheduledAppointment>();
                objfilelist = GetAppointmentList();
                objstudentlistmodel.AppointmentListCollction = objfilelist;
                objstudentlistmodel.AppointmentListDetail = new ScheduledAppointment();
                ViewBag.Message = "Data saved successfully.";
            }
            catch
            {
                ViewBag.Message = "Error while saving data.";
            }
            return View(objstudentlistmodel);
        }

        public int Save(ScheduledAppointment _objstudentlist)
        {
            AppointmentEntity objentity = new AppointmentEntity();
            ScheduledAppointment objAppointmentDetail = new ScheduledAppointment();


            objAppointmentDetail.StudentID = _objstudentlist.StudentID;
            objAppointmentDetail.StudentName = _objstudentlist.StudentName;
            objAppointmentDetail.DateTime = _objstudentlist.DateTime;
            /*Save data to database*/
            objentity.SaveChanges();
            return 1;
        }

        public ActionResult Edit(string studentId)
        {
            AppointmentEntity objentity = new AppointmentEntity();

            AppointmentListModel model = new AppointmentListModel();

            model.AppointmentListCollction = GetAppointmentList();

            var student= model.AppointmentListCollction.FirstOrDefault(i => i.StudentID.ToString() == studentId);
   
            ScheduledAppointment objstudentdetail = new ScheduledAppointment();
            objstudentdetail = (from data in objentity.ScheduledAppointments
                               where data.StudentID.ToString() == studentId
                               select data).FirstOrDefault();

            ScheduledAppointment _objstudentlist = new ScheduledAppointment();
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
            AppointmentEntity objentity = new AppointmentEntity();
            var student = objentity.ScheduledAppointments.FirstOrDefault(i => i.StudentID.ToString() == Id);

           
            student.StudentName = objstudentlistmodel.AppointmentListDetail.StudentName;
            student.DateTime = objstudentlistmodel.AppointmentListDetail.DateTime;
            
            ///*Save data to database*/
            objentity.SaveChanges();
            return RedirectToAction("/Counselor/");
        }

        public ActionResult Accept(int studentId)
        {

            AllAppointment objentity = new AllAppointment();

            objentity.Appointments.Add(new Appointment()
            {
                Confirmed = true,
                StudentID = studentId,
                CounselorID = Convert.ToInt32((string)Session["UserName"])
            });

            objentity.SaveChanges();


            AppointmentEntity appEntity = new AppointmentEntity();
            

            AppointmentListModel model = new AppointmentListModel();

            model.AppointmentListCollction = GetAppointmentList();

            var student = model.AppointmentListCollction.FirstOrDefault(i => i.StudentID.ToString() == studentId.ToString());

            ScheduledAppointment objstudentdetail = new ScheduledAppointment();
            objstudentdetail = (from data in appEntity.ScheduledAppointments
                                where data.StudentID.ToString() == studentId.ToString()
                                select data).FirstOrDefault();

            ScheduledAppointment _objstudentlist = new ScheduledAppointment();
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