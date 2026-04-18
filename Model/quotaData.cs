using System;
namespace CEMM.Model
{
	/// <summary>
	/// quotaData:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class quotaData
	{
		public quotaData()
		{}
		#region Model
		private int _srid;
		private string _subitermid;
		private string _subitermsrid;
		private string _subitermname;
		private string _toolid;
		private decimal? _toolquant;
		private decimal? _jcjs;
		private decimal? _zljs;
		private string _dygx;
		private string _isuse="1";
		/// <summary>
		/// 
		/// </summary>
		public int srid
		{
			set{ _srid=value;}
			get{return _srid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string subitermid
		{
			set{ _subitermid=value;}
			get{return _subitermid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string subitermsrid
		{
			set{ _subitermsrid=value;}
			get{return _subitermsrid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string subitermname
		{
			set{ _subitermname=value;}
			get{return _subitermname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string toolid
		{
			set{ _toolid=value;}
			get{return _toolid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? toolquant
		{
			set{ _toolquant=value;}
			get{return _toolquant;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? jcjs
		{
			set{ _jcjs=value;}
			get{return _jcjs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? zljs
		{
			set{ _zljs=value;}
			get{return _zljs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dygx
		{
			set{ _dygx=value;}
			get{return _dygx;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string isuse
		{
			set{ _isuse=value;}
			get{return _isuse;}
		}
		#endregion Model

	}
}

