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
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtsectionid.Text.Trim().Length==0)
			{
				strErr+="sectionid不能为空！\\n";	
			}
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
			string sectionid=this.txtsectionid.Text;
			string itermid=this.txtitermid.Text;
			string subworkid=this.txtsubworkid.Text;

			CEMM.Model.sectionwork model=new CEMM.Model.sectionwork();
			model.sectionid=sectionid;
			model.itermid=itermid;
			model.subworkid=subworkid;

			CEMM.BLL.sectionwork bll=new CEMM.BLL.sectionwork();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
