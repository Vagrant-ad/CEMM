using System;
namespace CEMM.Model
{
	/// <summary>
	/// sectionwork:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sectionwork
	{
		public sectionwork()
		{}
		#region Model
		private string _sectionid;
		private string _itermid;
		private string _subworkid;
		/// <summary>
		/// 
		/// </summary>
		public string sectionid
		{
			set{ _sectionid=value;}
			get{return _sectionid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string itermid
		{
			set{ _itermid=value;}
			get{return _itermid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string subworkid
		{
			set{ _subworkid=value;}
			get{return _subworkid;}
		}
		#endregion Model

	}
}

