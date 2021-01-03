using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Streams;
using System;
using System.Threading.Tasks;

namespace Grains
{
    public class EmailSenderGrain : Orleans.Grain, IEmailSender
    {
        private readonly ILogger logger;

        public EmailSenderGrain(ILogger<EmailSenderGrain> logger)
        {
            this.logger = logger;
        }
        async Task<string> IEmailSender.SendEmailAsync(string message)
        {
            IAsyncStream<string> stream = this.GetStreamProvider("SMSProvider").GetStream<string>(Guid.Empty, "chat");
            await stream.OnNextAsync($"{this.GetPrimaryKeyString()} - {message}");


            logger.LogInformation($"\n SayHello message received: greeting = '{message}'");
            return ($"\n Client said: '{message}', so HelloGrain says: Hello!");
        }
    }
}
