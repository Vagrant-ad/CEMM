using System;
namespace CEMM.Model
{
	/// <summary>
	/// subwork:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class subwork
	{
		public subwork()
		{}
		#region Model
		private string _subworkid;
		private decimal? _subworkquant;
		private string _workid;
		private DateTime? _subworkstartdate;
		private DateTime? _subworkenddate;
		/// <summary>
		/// 
		/// </summary>
		public string subworkid
		{
			set{ _subworkid=value;}
			get{return _subworkid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? subworkquant
		{
			set{ _subworkquant=value;}
			get{return _subworkquant;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string workid
		{
			set{ _workid=value;}
			get{return _workid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? subworkstartdate
		{
			set{ _subworkstartdate=value;}
			get{return _subworkstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? subworkenddate
		{
			set{ _subworkenddate=value;}
			get{return _subworkenddate;}
		}
		#endregion Model

	}
}

