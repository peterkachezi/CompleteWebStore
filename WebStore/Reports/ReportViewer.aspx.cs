using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WebStore.Data.EDMX;

namespace WebStore.Reports
{
    public partial class ReportViewer : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string searchText = string.Empty;

                if (Request.QueryString["searchText"] != null)
                {
                    searchText = Request.QueryString["searchText"].ToString();
                }

                List<Customer> customers = null;
                using (var _context = new WinkadStoreEntities())
                {
                    customers = _context.Customers.ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/CustomesReport.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", customers);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                    ReportViewer1.DataBind();
                }

            }


        }



    }

}