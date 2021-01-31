using System;
using System.Threading.Tasks;
using AppNetCore.DispatchQuartz.job;
using AppNetCore.DispatchQuartz.joblistener;
using Quartz;
using Quartz.Impl;

namespace AppNetCore.DispatchQuartz
{
    public class Quartz
    {
        public static async void Init()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            scheduler.ListenerManager.AddJobListener(new TestJobListener());
            // and start it off
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<TestJob>()
                   .WithIdentity("testjob", "group1")
                   .WithDescription("this is TestJOb")
                   .Build();
            jobDetail.JobDataMap.Add("student1", "Milor");
            jobDetail.JobDataMap.Add("student2", "心如迷醉");
            jobDetail.JobDataMap.Add("student3", "宇洋");
            jobDetail.JobDataMap.Add("Year", DateTime.Now.Year);
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
            // some sleep to show what's happening
            await Task.Delay(TimeSpan.FromSeconds(10));

            //// and last shut down the scheduler when you are ready to close your program
          // await scheduler.Shutdown();

        }



    }
}
