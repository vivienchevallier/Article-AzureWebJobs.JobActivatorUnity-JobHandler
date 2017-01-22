using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AzureWebJobs.JobActivatorUnity.Contracts;
using AzureWebJobs.JobActivatorUnity.Dependencies;
using Microsoft.Azure.WebJobs;

namespace AzureWebJobs.JobActivatorUnity
{
    public class Functions
    {
        private readonly IJobActivatorDependencyResolver jobActivatorDependencyResolver;

        public Functions(IJobActivatorDependencyResolver jobActivatorDependencyResolver)
        {
            if (jobActivatorDependencyResolver == null) throw new ArgumentNullException(nameof(jobActivatorDependencyResolver));

            this.jobActivatorDependencyResolver = jobActivatorDependencyResolver;
        }

        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public async Task ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log, CancellationToken ct)
        {
            using (var scope = this.jobActivatorDependencyResolver.BeginScope())
            {
                await scope.CreateInstance<IQueueMessageJob>().Process(ct, message, log);
            }
        }
    }
}