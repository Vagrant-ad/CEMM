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
namespace CEMM.Web.lot
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
					string lotid= strid;
					ShowInfo(lotid);
				}
			}
		}
		
	private void ShowInfo(string lotid)
	{
		CEMM.BLL.lot bll=new CEMM.BLL.lot();
		CEMM.Model.lot model=bll.GetModel(lotid);
		this.lbllotid.Text=model.lotid;
		this.lbllotname.Text=model.lotname;
		this.lbllotstartpos.Text=model.lotstartpos;
		this.lbllotendpos.Text=model.lotendpos;
		this.lblprojectid.Text=model.projectid;
		this.lblConstruparty.Text=model.Construparty;
		this.lbllotstartdate.Text=model.lotstartdate.ToString();
		this.lbllotenddate.Text=model.lotenddate.ToString();

	}


    }
}
