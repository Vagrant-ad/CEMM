using System;
namespace CEMM.Model
{
	/// <summary>
	/// lot:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class lot
	{
		public lot()
		{}
		#region Model
		private string _lotid;
		private string _lotname;
		private string _lotstartpos;
		private string _lotendpos;
		private string _projectid;
		private string _construparty;
		private DateTime? _lotstartdate;
		private DateTime? _lotenddate;
		/// <summary>
		/// 
		/// </summary>
		public string lotid
		{
			set{ _lotid=value;}
			get{return _lotid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lotname
		{
			set{ _lotname=value;}
			get{return _lotname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lotstartpos
		{
			set{ _lotstartpos=value;}
			get{return _lotstartpos;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lotendpos
		{
			set{ _lotendpos=value;}
			get{return _lotendpos;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string projectid
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Construparty
		{
			set{ _construparty=value;}
			get{return _construparty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? lotstartdate
		{
			set{ _lotstartdate=value;}
			get{return _lotstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? lotenddate
		{
			set{ _lotenddate=value;}
			get{return _lotenddate;}
		}
		#endregion Model

	}
}

