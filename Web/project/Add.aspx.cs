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
namespace CEMM.Web.project
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtprojectid.Text.Trim().Length==0)
			{
				strErr+="projectid不能为空！\\n";	
			}
			if(this.txtprojectname.Text.Trim().Length==0)
			{
				strErr+="projectname不能为空！\\n";	
			}
			if(this.txtprojectsource.Text.Trim().Length==0)
			{
				strErr+="projectsource不能为空！\\n";	
			}
			if(this.txtprojectinfo.Text.Trim().Length==0)
			{
				strErr+="projectinfo不能为空！\\n";	
			}
			if(!PageValidate.IsDateTime(txtprojstartdate.Text))
			{
				strErr+="projstartdate格式错误！\\n";	
			}
			if(!PageValidate.IsDateTime(txtprojenddate.Text))
			{
				strErr+="projenddate格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string projectid=this.txtprojectid.Text;
			string projectname=this.txtprojectname.Text;
			string projectsource=this.txtprojectsource.Text;
			string projectinfo=this.txtprojectinfo.Text;
			DateTime projstartdate=DateTime.Parse(this.txtprojstartdate.Text);
			DateTime projenddate=DateTime.Parse(this.txtprojenddate.Text);

			CEMM.Model.project model=new CEMM.Model.project();
			model.projectid=projectid;
			model.projectname=projectname;
			model.projectsource=projectsource;
			model.projectinfo=projectinfo;
			model.projstartdate=projstartdate;
			model.projenddate=projenddate;

			CEMM.BLL.project bll=new CEMM.BLL.project();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
