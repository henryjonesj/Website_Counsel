using MVCEntityLogin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
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

        public ActionResult Admin(string searchString)
        {
            AppointmentListModel objAppointmentListModel = new AppointmentListModel();
            objAppointmentListModel.AppointmentListCollction = new List<scheduledappointment>();      
           objAppointmentListModel.AllAppointmentCollection = GetAllAppointmentList(searchString);

            return View(objAppointmentListModel);
        }

        public ActionResult SelectedAppointments(string prefix)
        {
            AppointmentListModel objAppointmentListModel = new AppointmentListModel();
            objAppointmentListModel.AppointmentListCollction = new List<scheduledappointment>();
            
            objAppointmentListModel.AllAppointmentCollection = GetAllAppointmentList(prefix);
            return View(objAppointmentListModel);
        }

        public List<AppointmentDetail> GetAllAppointmentList()
        {
            List<AppointmentDetail> objAppointment = new List<AppointmentDetail>();

            CounsellingEntities objentity = new CounsellingEntities();

            var appointments = (from data in objentity.appointments select data).ToList();
            var scheduledAppointments = (from data in objentity.scheduledappointments select data).ToList();

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

        public List<AppointmentDetail> GetAllAppointmentList(string Prefix)
        {
            var appointmentList = GetAllAppointmentList();

            return string.IsNullOrEmpty(Prefix)? appointmentList:
                    appointmentList.Where(i => i.StudentName.StartsWith(Prefix)).ToList();
        }

        [HttpPost]
        public JsonResult SearchStudents(string Prefix)
        {
            List<AppointmentDetail> ObjList = new List<AppointmentDetail>();

            ObjList = GetAllAppointmentList();

            var studentName = (from data in ObjList
                           where data.StudentName.StartsWith(Prefix)
                           select data.StudentName
                       ).ToList();

            SelectedAppointments(Prefix);

            return Json(studentName);

        }

        public ActionResult ExportAppointments()
        {
            AppointmentListModel objAppointmentListModel = new AppointmentListModel();
            objAppointmentListModel.AppointmentListCollction = new List<scheduledappointment>();
            objAppointmentListModel.AllAppointmentCollection = GetAllAppointmentList();

            var appointments = new System.Data.DataTable("appointments");
            appointments.Columns.Add("StudentID", typeof(int));
            appointments.Columns.Add("StudentName", typeof(string));
            appointments.Columns.Add("DateTime", typeof(DateTime));

            foreach (var item in objAppointmentListModel.AllAppointmentCollection)
                appointments.Rows.Add(item.StudentID, item.StudentName, item.DateTime);

            var grid = new GridView();
            grid.DataSource = appointments;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyAppointments.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View();
            
        }




    }
}