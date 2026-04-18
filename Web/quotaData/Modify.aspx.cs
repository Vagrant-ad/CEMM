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
namespace CEMM.Web.quotaData
{
    public partial class Modify : Page
    {       

        		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					int srid=(Convert.ToInt32(Request.Params["id"]));
					ShowInfo(srid);
				}
			}
		}
			
	private void ShowInfo(int srid)
	{
		CEMM.BLL.quotaData bll=new CEMM.BLL.quotaData();
		CEMM.Model.quotaData model=bll.GetModel(srid);
		this.lblsrid.Text=model.srid.ToString();
		this.txtsubitermid.Text=model.subitermid;
		this.txtsubitermsrid.Text=model.subitermsrid;
		this.txtsubitermname.Text=model.subitermname;
		this.txttoolid.Text=model.toolid;
		this.txttoolquant.Text=model.toolquant.ToString();
		this.txtjcjs.Text=model.jcjs.ToString();
		this.txtzljs.Text=model.zljs.ToString();
		this.txtdygx.Text=model.dygx;
		this.txtisuse.Text=model.isuse;

	}

		public void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtsubitermid.Text.Trim().Length==0)
			{
				strErr+="subitermid不能为空！\\n";	
			}
			if(this.txtsubitermsrid.Text.Trim().Length==0)
			{
				strErr+="subitermsrid不能为空！\\n";	
			}
			if(this.txtsubitermname.Text.Trim().Length==0)
			{
				strErr+="subitermname不能为空！\\n";	
			}
			if(this.txttoolid.Text.Trim().Length==0)
			{
				strErr+="toolid不能为空！\\n";	
			}
			if(!PageValidate.IsDecimal(txttoolquant.Text))
			{
				strErr+="toolquant格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtjcjs.Text))
			{
				strErr+="jcjs格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtzljs.Text))
			{
				strErr+="zljs格式错误！\\n";	
			}
			if(this.txtdygx.Text.Trim().Length==0)
			{
				strErr+="dygx不能为空！\\n";	
			}
			if(this.txtisuse.Text.Trim().Length==0)
			{
				strErr+="isuse不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			int srid=int.Parse(this.lblsrid.Text);
			string subitermid=this.txtsubitermid.Text;
			string subitermsrid=this.txtsubitermsrid.Text;
			string subitermname=this.txtsubitermname.Text;
			string toolid=this.txttoolid.Text;
			decimal toolquant=decimal.Parse(this.txttoolquant.Text);
			decimal jcjs=decimal.Parse(this.txtjcjs.Text);
			decimal zljs=decimal.Parse(this.txtzljs.Text);
			string dygx=this.txtdygx.Text;
			string isuse=this.txtisuse.Text;


			CEMM.Model.quotaData model=new CEMM.Model.quotaData();
			model.srid=srid;
			model.subitermid=subitermid;
			model.subitermsrid=subitermsrid;
			model.subitermname=subitermname;
			model.toolid=toolid;
			model.toolquant=toolquant;
			model.jcjs=jcjs;
			model.zljs=zljs;
			model.dygx=dygx;
			model.isuse=isuse;

			CEMM.BLL.quotaData bll=new CEMM.BLL.quotaData();
			bll.Update(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","list.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
