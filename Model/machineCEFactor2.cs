using System;
namespace CEMM.Model
{
	/// <summary>
	/// machineCEFactor2:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class machineCEFactor2
	{
		public machineCEFactor2()
		{}
		#region Model
		private int _mfid;
		private string _name;
		private string _code;
		private string _specific;
		private string _unit;
		private decimal? _energyfactor;
		private decimal? _machinefactor;
		private string _standardid;
        private int? _energytype;
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
		public decimal? energyfactor
		{
			set{ _energyfactor=value;}
			get{return _energyfactor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? machinefactor
		{
			set{ _machinefactor=value;}
			get{return _machinefactor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string standardid
		{
			set{ _standardid=value;}
			get{return _standardid;}
		}

        /// <summary>
        /// 能源类型（1,2,3,4为直接碳排放，5,6,7为间接碳排放）
        /// </summary>
        public int? energytype
        {
            set { _energytype = value; }
            get { return _energytype; }
        }

		#endregion Model

	}
}

