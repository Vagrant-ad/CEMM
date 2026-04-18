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
namespace CEMM.Web.impleStandard
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					string standardid= Request.Params["id"];
					ShowInfo(standardid);
				}
			}
		}
			
	private void ShowInfo(string standardid)
	{
		CEMM.BLL.impleStandard bll=new CEMM.BLL.impleStandard();
		CEMM.Model.impleStandard model=bll.GetModel(standardid);
		this.lblstandardid.Text=model.standardid;
		this.txtstandardcode.Text=model.standardcode;
		this.txtimplementdate.Text=model.implementdate.ToString();

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtstandardcode.Text.Trim().Length==0)
			{
				strErr+="standardcode不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtimplementdate.Text))
			{
				strErr+="implementdate格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string standardid=this.lblstandardid.Text;
			string standardcode=this.txtstandardcode.Text;
			DateTime implementdate=DateTime.Parse(this.txtimplementdate.Text);


			CEMM.Model.impleStandard model=new CEMM.Model.impleStandard();
			model.standardid=standardid;
			model.standardcode=standardcode;
			model.implementdate=implementdate;

			CEMM.BLL.impleStandard bll=new CEMM.BLL.impleStandard();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
