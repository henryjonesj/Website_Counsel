using MVCEntityLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            AppointmentListModel objAppointmentListModel = new AppointmentListModel();
            objAppointmentListModel.AppointmentListCollction = new List<ScheduledAppointment>();
  ;
            objAppointmentListModel.AllAppointmentCollection = GetAllAppointmentList(); 
            return View(objAppointmentListModel);
        }

        public List<AppointmentDetail> GetAllAppointmentList()
        {
            List<AppointmentDetail> objAppointment = new List<AppointmentDetail>();


            AppointmentEntity objentity = new AppointmentEntity();

            AllAppointment appentity = new AllAppointment();



            var appointments = (from data in appentity.Appointments select data).ToList();
            var scheduledAppointments = (from data in objentity.ScheduledAppointments select data).ToList();

            var confirmedAppointments = (from data in appointments
                          join data2 in scheduledAppointments
                          on data.StudentID equals data2.StudentID
                          select new { data.StudentID, data.Confirmed, data2.StudentName, data2.DateTime });


            if(confirmedAppointments.Count()==0)
            {
                foreach (var item in scheduledAppointments)
                {
                    objAppointment.Add(new AppointmentDetail
                    {
                        StudentID = item.StudentID,
                        StudentName = item.StudentName,
                        DateTime = item.DateTime,
                        ConfirmationStatus = "No"
                    });

                }

                return objAppointment;
            }

            var confirmed = false;

            foreach (var item in scheduledAppointments)
            {
                foreach (var appoint in confirmedAppointments)
                    if (item.StudentID == appoint.StudentID)
                        confirmed = true;

                objAppointment.Add(new AppointmentDetail
                {
                    StudentID = item.StudentID,
                    StudentName = item.StudentName,
                    DateTime = item.DateTime,
                    ConfirmationStatus = confirmed ? "Yes" : "No"
                });

                confirmed = false;
            }

            return objAppointment;
        }

    }
}