using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WinForms;
using rvAldar.Reports;

namespace rvAldar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        MyDataSet mydata = new MyDataSet();
        public ActionResult Contact()
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            //reportViewer.SizeToReportContent = true;
            reportViewer.Width = 300;
            reportViewer.Height = 300;

            var connectionString = "Data Source=LAPTOP-J25I2S9H\\SQLEXPRESS;Initial Catalog=OGAPS;Integrated Security=True";


            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM Employees", conx);

            adp.Fill(mydata, mydata.Employees.TableName);
            
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Report1.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("MyDataSet", mydata.Tables[0]));


            ViewBag.ReportViewer = reportViewer;

            return View();
        }
    }
}