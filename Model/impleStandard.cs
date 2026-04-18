using System;
namespace CEMM.Model
{
	/// <summary>
	/// impleStandard:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class impleStandard
	{
		public impleStandard()
		{}
		#region Model
		private string _standardid;
		private string _standardcode;
		private DateTime? _implementdate;
		/// <summary>
		/// 
		/// </summary>
		public string standardid
		{
			set{ _standardid=value;}
			get{return _standardid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string standardcode
		{
			set{ _standardcode=value;}
			get{return _standardcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? implementdate
		{
			set{ _implementdate=value;}
			get{return _implementdate;}
		}
		#endregion Model

	}
}

