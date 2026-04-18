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
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int mfid=(Convert.ToInt32(Request.Params["id"]));
					ShowInfo(mfid);
				}
			}
		}
			
	private void ShowInfo(int mfid)
	{
		CEMM.BLL.machineCEFactor2 bll=new CEMM.BLL.machineCEFactor2();
		CEMM.Model.machineCEFactor2 model=bll.GetModel(mfid);
		this.lblmfid.Text=model.mfid.ToString();
		this.txtname.Text=model.name;
		this.txtcode.Text=model.code;
		this.txtspecific.Text=model.specific;
		this.txtunit.Text=model.unit;
		this.txtenergyfactor.Text=model.energyfactor.ToString();
		this.txtmachinefactor.Text=model.machinefactor.ToString();
		this.txtstandardid.Text=model.standardid;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
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
			int mfid=int.Parse(this.lblmfid.Text);
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
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
