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
namespace CEMM.Web.sectionwork
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					string sectionid= Request.Params["id"];
					ShowInfo(sectionid);
				}
			}
		}
			
	private void ShowInfo(string sectionid)
	{
		CEMM.BLL.sectionwork bll=new CEMM.BLL.sectionwork();
		CEMM.Model.sectionwork model=bll.GetModel(sectionid);
		this.lblsectionid.Text=model.sectionid;
		this.txtitermid.Text=model.itermid;
		this.txtsubworkid.Text=model.subworkid;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtitermid.Text.Trim().Length==0)
			{
				strErr+="itermid不能为空！\\n";	
			}
			if(this.txtsubworkid.Text.Trim().Length==0)
			{
				strErr+="subworkid不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string sectionid=this.lblsectionid.Text;
			string itermid=this.txtitermid.Text;
			string subworkid=this.txtsubworkid.Text;


			CEMM.Model.sectionwork model=new CEMM.Model.sectionwork();
			model.sectionid=sectionid;
			model.itermid=itermid;
			model.subworkid=subworkid;

			CEMM.BLL.sectionwork bll=new CEMM.BLL.sectionwork();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
