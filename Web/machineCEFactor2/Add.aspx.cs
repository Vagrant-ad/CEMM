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
namespace CEMM.Web.machineCEFactor2
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(!PageValidate.IsNumber(txtmfid.Text))
			{
				strErr+="mfid格式错误！\\n";	
			}
			if(this.txtname.Text.Trim().Length==0)
			{
				strErr+="name不能为空！\\n";	
			}
			if(this.txtcode.Text.Trim().Length==0)
			{
				strErr+="code不能为空！\\n";	
			}
			if(this.txtspecific.Text.Trim().Length==0)
			{
				strErr+="specific不能为空！\\n";	
			}
			if(this.txtunit.Text.Trim().Length==0)
			{
				strErr+="unit不能为空！\\n";	
			}
			if(!PageValidate.IsDecimal(txtenergyfactor.Text))
			{
				strErr+="energyfactor格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtmachinefactor.Text))
			{
				strErr+="machinefactor格式错误！\\n";	
			}
			if(this.txtstandardid.Text.Trim().Length==0)
			{
				strErr+="standardid不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int mfid=int.Parse(this.txtmfid.Text);
			string name=this.txtname.Text;
			string code=this.txtcode.Text;
			string specific=this.txtspecific.Text;
			string unit=this.txtunit.Text;
			decimal energyfactor=decimal.Parse(this.txtenergyfactor.Text);
			decimal machinefactor=decimal.Parse(this.txtmachinefactor.Text);
			string standardid=this.txtstandardid.Text;

			CEMM.Model.machineCEFactor2 model=new CEMM.Model.machineCEFactor2();
			model.mfid=mfid;
			model.name=name;
			model.code=code;
			model.specific=specific;
			model.unit=unit;
			model.energyfactor=energyfactor;
			model.machinefactor=machinefactor;
			model.standardid=standardid;

			CEMM.BLL.machineCEFactor2 bll=new CEMM.BLL.machineCEFactor2();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
