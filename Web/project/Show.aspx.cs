using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
namespace CEMM.Web.project
{
    public partial class Show : Page
    {        
        		public string strid=""; 
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					strid = Request.Params["id"];
					string projectid= strid;
					ShowInfo(projectid);
				}
			}
		}
		
	private void ShowInfo(string projectid)
	{
		CEMM.BLL.project bll=new CEMM.BLL.project();
		CEMM.Model.project model=bll.GetModel(projectid);
		this.lblprojectid.Text=model.projectid;
		this.lblprojectname.Text=model.projectname;
		this.lblprojectsource.Text=model.projectsource;
		this.lblprojectinfo.Text=model.projectinfo;
		this.lblprojstartdate.Text=model.projstartdate.ToString();
		this.lblprojenddate.Text=model.projenddate.ToString();

	}


    }
}
