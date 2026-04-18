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
namespace CEMM.Web.unitwork
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
					string workid= strid;
					ShowInfo(workid);
				}
			}
		}
		
	private void ShowInfo(string workid)
	{
		CEMM.BLL.unitwork bll=new CEMM.BLL.unitwork();
		CEMM.Model.unitwork model=bll.GetModel(workid);
		this.lblworkid.Text=model.workid;
		this.lblworkname.Text=model.workname;
		this.lbllotid.Text=model.lotid;
		this.lblworkstartdate.Text=model.workstartdate.ToString();
		this.lblworkenddate.Text=model.workenddate.ToString();

	}


    }
}
