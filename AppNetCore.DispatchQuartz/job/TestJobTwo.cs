using Quartz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppNetCore.DispatchQuartz.job
{
    [PersistJobDataAfterExecution] //下一次执行可以本次的结果
    [DisallowConcurrentExecution] //不让任务在同一段时间内执行,5s,但是5s没有执行完,然后等执行完,在来执行

    public class TestJobTwo : AbstractJob, IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("定时作业触发");               
             
            }
             );
        }

        public override JobModel InitJobModel()
        {
            return new JobModel("deflaut", "deflautgroup", "deflaut", "0/5 * * * * ?", new Hashtable());
        }
    }
}
