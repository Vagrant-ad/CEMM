using System;
namespace CEMM.Model
{
	/// <summary>
	/// computeResultInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class computeResultInfo
	{
		public computeResultInfo()
		{}
		#region Model
		private int _resultid;
		private string _code;
		private string _formname;
		private string _unit;
		private decimal? _price;
		private decimal? _emissionfactor;
		private decimal? _total_quantity;
		private decimal? _total_emission;
		private decimal? _temp_project=0M;
		private decimal? _temp_emission=0M;
		private decimal? _subgrade_project=0M;
		private decimal? _subgrade_emission=0M;
		private decimal? _pavement_project=0M;
		private decimal? _pavement_emission=0M;
		private decimal? _bridge_project=0M;
		private decimal? _bridge_emission=0M;
		private decimal? _tunnel_project=0M;
		private decimal? _tunnel_emission=0M;
		private decimal? _crossing_project=0M;
		private decimal? _crossing_emission=0M;
		private decimal? _traffic_project=0M;
		private decimal? _traffic_emission=0M;
		private decimal? _greening_project=0M;
		private decimal? _greening_emission=0M;
		private decimal? _other_project=0M;
		private decimal? _other_emission=0M;
		private decimal? _assistant_project=0M;
		private decimal? _assistant_emission=0M;
		private int? _tableid;
		/// <summary>
		/// 
		/// </summary>
		public int resultid
		{
			set{ _resultid=value;}
			get{return _resultid;}
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
		public string formName
		{
			set{ _formname=value;}
			get{return _formname;}
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
		public decimal? price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? emissionfactor
		{
			set{ _emissionfactor=value;}
			get{return _emissionfactor;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? total_quantity
		{
			set{ _total_quantity=value;}
			get{return _total_quantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? total_emission
		{
			set{ _total_emission=value;}
			get{return _total_emission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? temp_project
		{
			set{ _temp_project=value;}
			get{return _temp_project;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? temp_emission
		{
			set{ _temp_emission=value;}
			get{return _temp_emission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? subgrade_project
		{
			set{ _subgrade_project=value;}
			get{return _subgrade_project;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? subgrade_emission
		{
			set{ _subgrade_emission=value;}
			get{return _subgrade_emission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? pavement_project
		{
			set{ _pavement_project=value;}
			get{return _pavement_project;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? pavement_emission
		{
			set{ _pavement_emission=value;}
			get{return _pavement_emission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? bridge_project
		{
			set{ _bridge_project=value;}
			get{return _bridge_project;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? bridge_emission
		{
			set{ _bridge_emission=value;}
			get{return _bridge_emission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? tunnel_project
		{
			set{ _tunnel_project=value;}
			get{return _tunnel_project;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? tunnel_emission
		{
			set{ _tunnel_emission=value;}
			get{return _tunnel_emission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? crossing_project
		{
			set{ _crossing_project=value;}
			get{return _crossing_project;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? crossing_emission
		{
			set{ _crossing_emission=value;}
			get{return _crossing_emission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? traffic_project
		{
			set{ _traffic_project=value;}
			get{return _traffic_project;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? traffic_emission
		{
			set{ _traffic_emission=value;}
			get{return _traffic_emission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? greening_project
		{
			set{ _greening_project=value;}
			get{return _greening_project;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? greening_emission
		{
			set{ _greening_emission=value;}
			get{return _greening_emission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? other_project
		{
			set{ _other_project=value;}
			get{return _other_project;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? other_emission
		{
			set{ _other_emission=value;}
			get{return _other_emission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? assistant_project
		{
			set{ _assistant_project=value;}
			get{return _assistant_project;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? assistant_emission
		{
			set{ _assistant_emission=value;}
			get{return _assistant_emission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? tableID
		{
			set{ _tableid=value;}
			get{return _tableid;}
		}
		#endregion Model

	}
}

