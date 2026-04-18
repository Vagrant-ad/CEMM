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
namespace CEMM.Web.subwork
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
					string subworkid= strid;
					ShowInfo(subworkid);
				}
			}
		}
		
	private void ShowInfo(string subworkid)
	{
		CEMM.BLL.subwork bll=new CEMM.BLL.subwork();
		CEMM.Model.subwork model=bll.GetModel(subworkid);
		this.lblsubworkid.Text=model.subworkid;
		this.lblsubworkquant.Text=model.subworkquant.ToString();
		this.lblworkid.Text=model.workid;
		this.lblsubworkstartdate.Text=model.subworkstartdate.ToString();
		this.lblsubworkenddate.Text=model.subworkenddate.ToString();

	}


    }
}
