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
namespace CEMM.Web.quotaEngiInfo
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtitermid.Text.Trim().Length==0)
			{
				strErr+="itermid不能为空！\\n";	
			}
			if(this.txtitermname.Text.Trim().Length==0)
			{
				strErr+="itermname不能为空！\\n";	
			}
			if(this.txtitermlevel.Text.Trim().Length==0)
			{
				strErr+="itermlevel不能为空！\\n";	
			}
			if(this.txtstandard.Text.Trim().Length==0)
			{
				strErr+="standard不能为空！\\n";	
			}
			if(this.txtbaseinfo.Text.Trim().Length==0)
			{
				strErr+="baseinfo不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string itermid=this.txtitermid.Text;
			string itermname=this.txtitermname.Text;
			string itermlevel=this.txtitermlevel.Text;
			string standard=this.txtstandard.Text;
			string baseinfo=this.txtbaseinfo.Text;

			CEMM.Model.quotaEngiInfo model=new CEMM.Model.quotaEngiInfo();
			model.itermid=itermid;
			model.itermname=itermname;
			model.itermlevel=itermlevel;
			model.standard=standard;
			model.baseinfo=baseinfo;

			CEMM.BLL.quotaEngiInfo bll=new CEMM.BLL.quotaEngiInfo();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
