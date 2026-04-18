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
namespace CEMM.Web.computeResultInfo
{
    public partial class Add : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtcode.Text.Trim().Length==0)
			{
				strErr+="code不能为空！\\n";	
			}
			if(this.txtformName.Text.Trim().Length==0)
			{
				strErr+="formName不能为空！\\n";	
			}
			if(this.txtunit.Text.Trim().Length==0)
			{
				strErr+="unit不能为空！\\n";	
			}
			if(!PageValidate.IsDecimal(txtprice.Text))
			{
				strErr+="price格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtemissionfactor.Text))
			{
				strErr+="emissionfactor格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txttotal_quantity.Text))
			{
				strErr+="total_quantity格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txttotal_emission.Text))
			{
				strErr+="total_emission格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txttemp_project.Text))
			{
				strErr+="temp_project格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txttemp_emission.Text))
			{
				strErr+="temp_emission格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtsubgrade_project.Text))
			{
				strErr+="subgrade_project格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtsubgrade_emission.Text))
			{
				strErr+="subgrade_emission格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtpavement_project.Text))
			{
				strErr+="pavement_project格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtpavement_emission.Text))
			{
				strErr+="pavement_emission格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtbridge_project.Text))
			{
				strErr+="bridge_project格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtbridge_emission.Text))
			{
				strErr+="bridge_emission格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txttunnel_project.Text))
			{
				strErr+="tunnel_project格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txttunnel_emission.Text))
			{
				strErr+="tunnel_emission格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtcrossing_project.Text))
			{
				strErr+="crossing_project格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtcrossing_emission.Text))
			{
				strErr+="crossing_emission格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txttraffic_project.Text))
			{
				strErr+="traffic_project格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txttraffic_emission.Text))
			{
				strErr+="traffic_emission格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtgreening_project.Text))
			{
				strErr+="greening_project格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtgreening_emission.Text))
			{
				strErr+="greening_emission格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtother_project.Text))
			{
				strErr+="other_project格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtother_emission.Text))
			{
				strErr+="other_emission格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtassistant_project.Text))
			{
				strErr+="assistant_project格式错误！\\n";	
			}
			if(!PageValidate.IsDecimal(txtassistant_emission.Text))
			{
				strErr+="assistant_emission格式错误！\\n";	
			}
			if(!PageValidate.IsNumber(txttableID.Text))
			{
				strErr+="tableID格式错误！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string code=this.txtcode.Text;
			string formName=this.txtformName.Text;
			string unit=this.txtunit.Text;
			decimal price=decimal.Parse(this.txtprice.Text);
			decimal emissionfactor=decimal.Parse(this.txtemissionfactor.Text);
			decimal total_quantity=decimal.Parse(this.txttotal_quantity.Text);
			decimal total_emission=decimal.Parse(this.txttotal_emission.Text);
			decimal temp_project=decimal.Parse(this.txttemp_project.Text);
			decimal temp_emission=decimal.Parse(this.txttemp_emission.Text);
			decimal subgrade_project=decimal.Parse(this.txtsubgrade_project.Text);
			decimal subgrade_emission=decimal.Parse(this.txtsubgrade_emission.Text);
			decimal pavement_project=decimal.Parse(this.txtpavement_project.Text);
			decimal pavement_emission=decimal.Parse(this.txtpavement_emission.Text);
			decimal bridge_project=decimal.Parse(this.txtbridge_project.Text);
			decimal bridge_emission=decimal.Parse(this.txtbridge_emission.Text);
			decimal tunnel_project=decimal.Parse(this.txttunnel_project.Text);
			decimal tunnel_emission=decimal.Parse(this.txttunnel_emission.Text);
			decimal crossing_project=decimal.Parse(this.txtcrossing_project.Text);
			decimal crossing_emission=decimal.Parse(this.txtcrossing_emission.Text);
			decimal traffic_project=decimal.Parse(this.txttraffic_project.Text);
			decimal traffic_emission=decimal.Parse(this.txttraffic_emission.Text);
			decimal greening_project=decimal.Parse(this.txtgreening_project.Text);
			decimal greening_emission=decimal.Parse(this.txtgreening_emission.Text);
			decimal other_project=decimal.Parse(this.txtother_project.Text);
			decimal other_emission=decimal.Parse(this.txtother_emission.Text);
			decimal assistant_project=decimal.Parse(this.txtassistant_project.Text);
			decimal assistant_emission=decimal.Parse(this.txtassistant_emission.Text);
			int tableID=int.Parse(this.txttableID.Text);

			CEMM.Model.computeResultInfo model=new CEMM.Model.computeResultInfo();
			model.code=code;
			model.formName=formName;
			model.unit=unit;
			model.price=price;
			model.emissionfactor=emissionfactor;
			model.total_quantity=total_quantity;
			model.total_emission=total_emission;
			model.temp_project=temp_project;
			model.temp_emission=temp_emission;
			model.subgrade_project=subgrade_project;
			model.subgrade_emission=subgrade_emission;
			model.pavement_project=pavement_project;
			model.pavement_emission=pavement_emission;
			model.bridge_project=bridge_project;
			model.bridge_emission=bridge_emission;
			model.tunnel_project=tunnel_project;
			model.tunnel_emission=tunnel_emission;
			model.crossing_project=crossing_project;
			model.crossing_emission=crossing_emission;
			model.traffic_project=traffic_project;
			model.traffic_emission=traffic_emission;
			model.greening_project=greening_project;
			model.greening_emission=greening_emission;
			model.other_project=other_project;
			model.other_emission=other_emission;
			model.assistant_project=assistant_project;
			model.assistant_emission=assistant_emission;
			model.tableID=tableID;

			CEMM.BLL.computeResultInfo bll=new CEMM.BLL.computeResultInfo();
			bll.Add(model);
			Maticsoft.Common.MessageBox.ShowAndRedirect(this,"保存成功！","add.aspx");

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
