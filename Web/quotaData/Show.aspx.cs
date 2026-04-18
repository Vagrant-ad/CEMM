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
namespace CEMM.Web.quotaData
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
					int srid=(Convert.ToInt32(strid));
					ShowInfo(srid);
				}
			}
		}
		
	private void ShowInfo(int srid)
	{
		CEMM.BLL.quotaData bll=new CEMM.BLL.quotaData();
		CEMM.Model.quotaData model=bll.GetModel(srid);
		this.lblsrid.Text=model.srid.ToString();
		this.lblsubitermid.Text=model.subitermid;
		this.lblsubitermsrid.Text=model.subitermsrid;
		this.lblsubitermname.Text=model.subitermname;
		this.lbltoolid.Text=model.toolid;
		this.lbltoolquant.Text=model.toolquant.ToString();
		this.lbljcjs.Text=model.jcjs.ToString();
		this.lblzljs.Text=model.zljs.ToString();
		this.lbldygx.Text=model.dygx;
		this.lblisuse.Text=model.isuse;

	}


    }
}
