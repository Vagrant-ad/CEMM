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
namespace CEMM.Web.machineCEFactor
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
					int mfid=(Convert.ToInt32(strid));
					ShowInfo(mfid);
				}
			}
		}
		
	private void ShowInfo(int mfid)
	{
		CEMM.BLL.machineCEFactor bll=new CEMM.BLL.machineCEFactor();
		CEMM.Model.machineCEFactor model=bll.GetModel(mfid);
		this.lblmfid.Text=model.mfid.ToString();
		this.lblname.Text=model.name;
		this.lblcode.Text=model.code;
		this.lblspecific.Text=model.specific;
		this.lblunit.Text=model.unit;
		this.lblemissfactor.Text=model.emissfactor.ToString();
		this.lblstandardid.Text=model.standardid;

	}


    }
}
