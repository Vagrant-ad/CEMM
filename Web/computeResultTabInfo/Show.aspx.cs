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
namespace CEMM.Web.computeResultTabInfo
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
					int tableID=(Convert.ToInt32(strid));
					ShowInfo(tableID);
				}
			}
		}
		
	private void ShowInfo(int tableID)
	{
		CEMM.BLL.computeResultTabInfo bll=new CEMM.BLL.computeResultTabInfo();
		CEMM.Model.computeResultTabInfo model=bll.GetModel(tableID);
		this.lbltableID.Text=model.tableID.ToString();
		this.lbltableName.Text=model.tableName;
		this.lblinputTime.Text=model.inputTime.ToString();

	}


    }
}
