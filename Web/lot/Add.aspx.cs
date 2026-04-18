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
namespace CEMM.Web.lot
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtlotid.Text.Trim().Length==0)
			{
				strErr+="lotid不能为空！\\n";	
			}
			if(this.txtlotname.Text.Trim().Length==0)
			{
				strErr+="lotname不能为空！\\n";	
			}
			if(this.txtlotstartpos.Text.Trim().Length==0)
			{
				strErr+="lotstartpos不能为空！\\n";	
			}
			if(this.txtlotendpos.Text.Trim().Length==0)
			{
				strErr+="lotendpos不能为空！\\n";	
			}
			if(this.txtprojectid.Text.Trim().Length==0)
			{
				strErr+="projectid不能为空！\\n";	
			}
			if(this.txtConstruparty.Text.Trim().Length==0)
			{
				strErr+="Construparty不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtlotstartdate.Text))
			{
				strErr+="lotstartdate格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtlotenddate.Text))
			{
				strErr+="lotenddate格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string lotid=this.txtlotid.Text;
			string lotname=this.txtlotname.Text;
			string lotstartpos=this.txtlotstartpos.Text;
			string lotendpos=this.txtlotendpos.Text;
			string projectid=this.txtprojectid.Text;
			string Construparty=this.txtConstruparty.Text;
			DateTime lotstartdate=DateTime.Parse(this.txtlotstartdate.Text);
			DateTime lotenddate=DateTime.Parse(this.txtlotenddate.Text);

			CEMM.Model.lot model=new CEMM.Model.lot();
			model.lotid=lotid;
			model.lotname=lotname;
			model.lotstartpos=lotstartpos;
			model.lotendpos=lotendpos;
			model.projectid=projectid;
			model.Construparty=Construparty;
			model.lotstartdate=lotstartdate;
			model.lotenddate=lotenddate;

			CEMM.BLL.lot bll=new CEMM.BLL.lot();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
