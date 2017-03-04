using System.Net;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.AspNet.Identity;
using SendGrid;
using System.Web.Script.Serialization;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.Azure;
namespace Project.WebUI.Services
{
    public class AzureEmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configAzureasync(message);
        }
        private async Task configAzureasync(IdentityMessage message)
        {
            var serializer = new JavaScriptSerializer();
            var modelAsString = serializer.Serialize(message);

            // сохраняем сообщение в очередь emailconfirmation
            var setting = CloudConfigurationManager.GetSetting("StorageConnection");
            var account = CloudStorageAccount.Parse(setting);

            var queueClient = account.CreateCloudQueueClient();
            var emailqueue = CloudConfigurationManager.GetSetting("EmailQueue");
            var queue = queueClient.GetQueueReference(emailqueue);
            queue.CreateIfNotExists();

            await queue.AddMessageAsync(new CloudQueueMessage(modelAsString));
        }
    }
}