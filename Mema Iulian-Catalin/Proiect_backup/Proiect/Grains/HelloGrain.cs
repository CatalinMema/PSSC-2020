using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class HelloGrain : Orleans.Grain, IHello
    {
        private readonly ILogger logger;

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            this.logger = logger;
        }

        public async Task<string> SayHello(string greeting)
        {
            IAsyncStream<string> stream = this.GetStreamProvider("SMSProvider").GetStream<string>(Guid.Empty, "chat");
            await stream.OnNextAsync($"{this.GetPrimaryKeyString()} - {greeting}");


            logger.LogInformation($"\n SayHello message received: greeting = '{greeting}'");
            return ($"\n Client said: '{greeting}', so HelloGrain says: Hello!");
        }
    }
}
