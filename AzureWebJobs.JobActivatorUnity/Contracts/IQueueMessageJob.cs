using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AzureWebJobs.JobActivatorUnity.Contracts
{
    public interface IQueueMessageJob
    {
        Task Process(CancellationToken ct, string message, TextWriter log);
    }
}
