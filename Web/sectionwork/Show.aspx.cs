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
namespace CEMM.Web.sectionwork
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
					string sectionid= strid;
					ShowInfo(sectionid);
				}
			}
		}
		
	private void ShowInfo(string sectionid)
	{
		CEMM.BLL.sectionwork bll=new CEMM.BLL.sectionwork();
		CEMM.Model.sectionwork model=bll.GetModel(sectionid);
		this.lblsectionid.Text=model.sectionid;
		this.lblitermid.Text=model.itermid;
		this.lblsubworkid.Text=model.subworkid;

	}


    }
}
