using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
namespace AppNetCore.DispatchQuartz.joblistener
{
    public class TestJobListener : IJobListener
    {
        public string Name => "TestJobListener";

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => {
                Console.WriteLine($"CustomJobListener JobExecutionVetoed {context.JobDetail.Description}");
            });
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => {
                Console.WriteLine($"CustomJobListener JobToBeExecuted {context.JobDetail.Description}");
            });
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => {
                Console.WriteLine($"CustomJobListener JobWasExecuted {context.JobDetail.Description}");
            });
        }
    }
}
