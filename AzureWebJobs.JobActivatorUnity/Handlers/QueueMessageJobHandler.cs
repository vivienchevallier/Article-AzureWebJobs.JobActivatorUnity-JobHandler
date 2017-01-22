using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AzureWebJobs.JobActivatorUnity.Contracts;

namespace AzureWebJobs.JobActivatorUnity.Handlers
{
    public sealed class QueueMessageJobHandler : IQueueMessageJob
    {
        private readonly INumberService numberService;
        private readonly IUnitOfWork unitOfWork;

        public QueueMessageJobHandler(INumberService numberService, IUnitOfWork unitOfWork)
        {
            if (numberService == null) throw new ArgumentNullException(nameof(numberService));
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            this.numberService = numberService;
            this.unitOfWork = unitOfWork;
        }

        public async Task Process(CancellationToken ct, string message, TextWriter log)
        {
            Console.WriteLine("Beginning QueueMessageJobHandler work...");

            log.WriteLine("New random number {0} from number service for message: {1}", this.numberService.GetRandomNumber(), message);

            await this.unitOfWork.DoWork(ct, message);

            Console.WriteLine("Finishing QueueMessageJobHandler work...");
        }
    }
}
