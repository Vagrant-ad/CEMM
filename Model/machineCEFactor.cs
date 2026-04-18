using System;
namespace CEMM.Model
{
	/// <summary>
	/// machineCEFactor:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class machineCEFactor
	{
		public machineCEFactor()
		{}
		#region Model
		private int _mfid;
		private string _name;
		private string _code;
		private string _specific;
		private string _unit;
		private decimal? _emissfactor;
		private string _standardid;
		/// <summary>
		/// 
		/// </summary>
		public int mfid
		{
			set{ _mfid=value;}
			get{return _mfid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string specific
		{
			set{ _specific=value;}
			get{return _specific;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string unit
		{
			set{ _unit=value;}
			get{return _unit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? emissfactor
		{
			set{ _emissfactor=value;}
			get{return _emissfactor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string standardid
		{
			set{ _standardid=value;}
			get{return _standardid;}
		}
		#endregion Model

	}
}

