using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppNetCore.DispatchQuartz.job
{
    public class TestJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(()=> {
                Console.WriteLine("*************");
                Console.WriteLine($"This is {Thread.CurrentThread.ManagedThreadId}|{DateTime.Now}" +
                    $",{context.JobDetail.JobDataMap.GetString("student1")}|{context.JobDetail.JobDataMap.GetString("student2")}");
            });
        }
    }
}
