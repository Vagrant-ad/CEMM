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
namespace CEMM.Web.quotaEngiInfo
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
					string itermid= strid;
					ShowInfo(itermid);
				}
			}
		}
		
	private void ShowInfo(string itermid)
	{
		CEMM.BLL.quotaEngiInfo bll=new CEMM.BLL.quotaEngiInfo();
		CEMM.Model.quotaEngiInfo model=bll.GetModel(itermid);
		this.lblitermid.Text=model.itermid;
		this.lblitermname.Text=model.itermname;
		this.lblitermlevel.Text=model.itermlevel;
		this.lblstandard.Text=model.standard;
		this.lblbaseinfo.Text=model.baseinfo;

	}


    }
}
