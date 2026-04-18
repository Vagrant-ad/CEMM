using System;
namespace CEMM.Model
{
	/// <summary>
	/// 1
	/// </summary>
	[Serializable]
	public partial class unitwork
	{
		public unitwork()
		{}
		#region Model
		private string _workid;
		private string _workname;
		private string _lotid;
		private DateTime? _workstartdate;
		private DateTime? _workenddate;
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
		public string workname
		{
			set{ _workname=value;}
			get{return _workname;}
		}
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
		public DateTime? workstartdate
		{
			set{ _workstartdate=value;}
			get{return _workstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? workenddate
		{
			set{ _workenddate=value;}
			get{return _workenddate;}
		}
		#endregion Model

	}
}

