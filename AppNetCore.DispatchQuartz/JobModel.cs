using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AppNetCore.DispatchQuartz
{
    public abstract class AbstractJob
    {
		public DateTime FileInfoLastTime { get; set; }

		public abstract JobModel InitJobModel();
	}
    public class JobModel
    {
		public string Name { get; set; }
		/// <summary>
		/// 定时作业的组名称
		/// </summary>
		public string GroupName { get; set; }
		/// <summary>
		/// 描述,自己加,如果后面我们自己做管理页面,我展示
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		///  定时的时间表达式
		/// </summary>
		public string CronSchedule { get; set; }
		/// <summary>
		/// 定时任务需要传的值
		/// </summary>
		public Hashtable Data { get; set; }

		/// <summary>
		///  有参构造
		/// </summary>
		/// <param name="name"></param>
		/// <param name="groupName"></param>
		/// <param name="description"></param>
		/// <param name="cronSchedule"></param>
		/// <param name="hashtable"></param>
		public JobModel(string name, string groupName, string description, string cronSchedule, Hashtable hashtable)
		{
			Name = name;
			GroupName = groupName;
			Description = description;
			CronSchedule = cronSchedule;
			Data = hashtable;
		}
	}
}
