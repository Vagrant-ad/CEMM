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
namespace CEMM.Web.subwork
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtsubworkid.Text.Trim().Length==0)
			{
				strErr+="subworkid不能为空！\\n";	
			}
			if(!PageValidate.IsDecimal(txtsubworkquant.Text))
			{
				strErr+="subworkquant格式错误！\\n";	
			}
			if(this.txtworkid.Text.Trim().Length==0)
			{
				strErr+="workid不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtsubworkstartdate.Text))
			{
				strErr+="subworkstartdate格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtsubworkenddate.Text))
			{
				strErr+="subworkenddate格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string subworkid=this.txtsubworkid.Text;
			decimal subworkquant=decimal.Parse(this.txtsubworkquant.Text);
			string workid=this.txtworkid.Text;
			DateTime subworkstartdate=DateTime.Parse(this.txtsubworkstartdate.Text);
			DateTime subworkenddate=DateTime.Parse(this.txtsubworkenddate.Text);

			CEMM.Model.subwork model=new CEMM.Model.subwork();
			model.subworkid=subworkid;
			model.subworkquant=subworkquant;
			model.workid=workid;
			model.subworkstartdate=subworkstartdate;
			model.subworkenddate=subworkenddate;

			CEMM.BLL.subwork bll=new CEMM.BLL.subwork();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
