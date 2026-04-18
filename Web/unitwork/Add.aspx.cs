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
namespace CEMM.Web.unitwork
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtworkid.Text.Trim().Length==0)
			{
				strErr+="workid不能为空！\\n";	
			}
			if(this.txtworkname.Text.Trim().Length==0)
			{
				strErr+="workname不能为空！\\n";	
			}
			if(this.txtlotid.Text.Trim().Length==0)
			{
				strErr+="lotid不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtworkstartdate.Text))
			{
				strErr+="workstartdate格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtworkenddate.Text))
			{
				strErr+="workenddate格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string workid=this.txtworkid.Text;
			string workname=this.txtworkname.Text;
			string lotid=this.txtlotid.Text;
			DateTime workstartdate=DateTime.Parse(this.txtworkstartdate.Text);
			DateTime workenddate=DateTime.Parse(this.txtworkenddate.Text);

			CEMM.Model.unitwork model=new CEMM.Model.unitwork();
			model.workid=workid;
			model.workname=workname;
			model.lotid=lotid;
			model.workstartdate=workstartdate;
			model.workenddate=workenddate;

			CEMM.BLL.unitwork bll=new CEMM.BLL.unitwork();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
