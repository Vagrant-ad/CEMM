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
namespace CEMM.Web.impleStandard
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
					string standardid= strid;
					ShowInfo(standardid);
				}
			}
		}
		
	private void ShowInfo(string standardid)
	{
		CEMM.BLL.impleStandard bll=new CEMM.BLL.impleStandard();
		CEMM.Model.impleStandard model=bll.GetModel(standardid);
		this.lblstandardid.Text=model.standardid;
		this.lblstandardcode.Text=model.standardcode;
		this.lblimplementdate.Text=model.implementdate.ToString();

	}


    }
}
