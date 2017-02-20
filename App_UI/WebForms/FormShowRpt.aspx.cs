using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.Mvc;

namespace App_UI.WebForms
{
    public partial class FormShowRpt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bool isValid = true;

                // Get the template Rpt (The path of it,with its name)
                string rptPath = System.Web.HttpContext.Current.Session["ReportPath"].ToString();
                //string rptPath = "D:\\雷迪克ERP\\LDK_ERP\\Code\\Model\\Rpts//CrTest.rpt";
                // Get the date which will insert into Rpt-template    
                var rptSource = System.Web.HttpContext.Current.Session["ReportSource"];

                // Checking is Report Path provided or not
                if (string.IsNullOrEmpty(rptPath)||!File.Exists (rptPath)) 
                {
                    isValid = false;
                }
                // If Report Name provided then do other operation
                if (isValid) 
                {
                    ReportDocument rd = new ReportDocument();
                    //Loading Report
                    rd.Load(rptPath);

                    // Setting report data source
                    if (rptSource != null && rptSource.GetType().ToString() != "System.String")
                        rd.SetDataSource(rptSource);
                    CrystalReportViewer1.ReportSource = rd;


                    Session["ReportPath"] = "";
                    Session["ReportSource"] = "";
                }

                else
                {
                    Response.Write("<H2>目标报表："+ rptPath+"不存在</H2>");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

        }
    }
}