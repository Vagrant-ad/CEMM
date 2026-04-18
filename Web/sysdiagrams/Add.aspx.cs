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
namespace CEMM.Web.sysdiagrams
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtname.Text.Trim().Length==0)
			{
				strErr+="name不能为空！\\n";	
			}
			if(!PageValidate.IsNumber(txtprincipal_id.Text))
			{
				strErr+="principal_id格式错误！\\n";	
			}
			if(!PageValidate.IsNumber(txtversion.Text))
			{
				strErr+="version格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string name=this.txtname.Text;
			int principal_id=int.Parse(this.txtprincipal_id.Text);
			int version=int.Parse(this.txtversion.Text);
			byte[] definition= new UnicodeEncoding().GetBytes(this.txtdefinition.Text);

			CEMM.Model.sysdiagrams model=new CEMM.Model.sysdiagrams();
			model.name=name;
			model.principal_id=principal_id;
			model.version=version;
			model.definition=definition;

			CEMM.BLL.sysdiagrams bll=new CEMM.BLL.sysdiagrams();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
