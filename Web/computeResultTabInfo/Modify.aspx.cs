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
using Maticsoft.Common;
using LTP.Accounts.Bus;
namespace CEMM.Web.computeResultTabInfo
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int tableID=(Convert.ToInt32(Request.Params["id"]));
					ShowInfo(tableID);
				}
			}
		}
			
	private void ShowInfo(int tableID)
	{
		CEMM.BLL.computeResultTabInfo bll=new CEMM.BLL.computeResultTabInfo();
		CEMM.Model.computeResultTabInfo model=bll.GetModel(tableID);
		this.lbltableID.Text=model.tableID.ToString();
		this.txttableName.Text=model.tableName;
		this.txtinputTime.Text=model.inputTime.ToString();

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txttableName.Text.Trim().Length==0)
			{
				strErr+="tableName不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtinputTime.Text))
			{
				strErr+="inputTime格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int tableID=int.Parse(this.lbltableID.Text);
			string tableName=this.txttableName.Text;
			DateTime inputTime=DateTime.Parse(this.txtinputTime.Text);


			CEMM.Model.computeResultTabInfo model=new CEMM.Model.computeResultTabInfo();
			model.tableID=tableID;
			model.tableName=tableName;
			model.inputTime=inputTime;

			CEMM.BLL.computeResultTabInfo bll=new CEMM.BLL.computeResultTabInfo();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
