using System;
namespace CEMM.Model
{
	/// <summary>
	/// computeResultTabInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class computeResultTabInfo
	{
		public computeResultTabInfo()
		{}
		#region Model
		private int _tableid;
		private string _tablename;
		private DateTime? _inputtime;
		/// <summary>
		/// 
		/// </summary>
		public int tableID
		{
			set{ _tableid=value;}
			get{return _tableid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string tableName
		{
			set{ _tablename=value;}
			get{return _tablename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? inputTime
		{
			set{ _inputtime=value;}
			get{return _inputtime;}
		}
		#endregion Model

	}
}

