using System;
namespace CEMM.Model
{
	/// <summary>
	/// project:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class project
	{
		public project()
		{}
		#region Model
		private string _projectid;
		private string _projectname;
		private string _projectsource;
		private string _projectinfo;
		private DateTime? _projstartdate;
		private DateTime? _projenddate;
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
		public string projectname
		{
			set{ _projectname=value;}
			get{return _projectname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string projectsource
		{
			set{ _projectsource=value;}
			get{return _projectsource;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string projectinfo
		{
			set{ _projectinfo=value;}
			get{return _projectinfo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? projstartdate
		{
			set{ _projstartdate=value;}
			get{return _projstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? projenddate
		{
			set{ _projenddate=value;}
			get{return _projenddate;}
		}
		#endregion Model

	}
}

