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
namespace CEMM.Web.computeResultInfo
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
					int resultid=(Convert.ToInt32(strid));
					ShowInfo(resultid);
				}
			}
		}
		
	private void ShowInfo(int resultid)
	{
		CEMM.BLL.computeResultInfo bll=new CEMM.BLL.computeResultInfo();
		CEMM.Model.computeResultInfo model=bll.GetModel(resultid);
		this.lblresultid.Text=model.resultid.ToString();
		this.lblcode.Text=model.code;
		this.lblformName.Text=model.formName;
		this.lblunit.Text=model.unit;
		this.lblprice.Text=model.price.ToString();
		this.lblemissionfactor.Text=model.emissionfactor.ToString();
		this.lbltotal_quantity.Text=model.total_quantity.ToString();
		this.lbltotal_emission.Text=model.total_emission.ToString();
		this.lbltemp_project.Text=model.temp_project.ToString();
		this.lbltemp_emission.Text=model.temp_emission.ToString();
		this.lblsubgrade_project.Text=model.subgrade_project.ToString();
		this.lblsubgrade_emission.Text=model.subgrade_emission.ToString();
		this.lblpavement_project.Text=model.pavement_project.ToString();
		this.lblpavement_emission.Text=model.pavement_emission.ToString();
		this.lblbridge_project.Text=model.bridge_project.ToString();
		this.lblbridge_emission.Text=model.bridge_emission.ToString();
		this.lbltunnel_project.Text=model.tunnel_project.ToString();
		this.lbltunnel_emission.Text=model.tunnel_emission.ToString();
		this.lblcrossing_project.Text=model.crossing_project.ToString();
		this.lblcrossing_emission.Text=model.crossing_emission.ToString();
		this.lbltraffic_project.Text=model.traffic_project.ToString();
		this.lbltraffic_emission.Text=model.traffic_emission.ToString();
		this.lblgreening_project.Text=model.greening_project.ToString();
		this.lblgreening_emission.Text=model.greening_emission.ToString();
		this.lblother_project.Text=model.other_project.ToString();
		this.lblother_emission.Text=model.other_emission.ToString();
		this.lblassistant_project.Text=model.assistant_project.ToString();
		this.lblassistant_emission.Text=model.assistant_emission.ToString();
		this.lbltableID.Text=model.tableID.ToString();

	}


    }
}
