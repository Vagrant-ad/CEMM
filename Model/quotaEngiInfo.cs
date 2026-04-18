using System;
namespace CEMM.Model
{
	/// <summary>
	/// quotaEngiInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class quotaEngiInfo
	{
		public quotaEngiInfo()
		{}
		#region Model
		private string _itermid;
		private string _itermname;
		private string _itermlevel;
		private string _standard;
		private string _baseinfo;
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
		public string itermname
		{
			set{ _itermname=value;}
			get{return _itermname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string itermlevel
		{
			set{ _itermlevel=value;}
			get{return _itermlevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string standard
		{
			set{ _standard=value;}
			get{return _standard;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string baseinfo
		{
			set{ _baseinfo=value;}
			get{return _baseinfo;}
		}
		#endregion Model

	}
}

