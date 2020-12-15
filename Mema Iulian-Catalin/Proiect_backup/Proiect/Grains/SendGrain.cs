using Orleans;
using System.Threading.Tasks;

namespace Grains
{
    public class SendGrain : Orleans.Grain, IGrain
    {
        public Task<string> SendMessage(string message)
        {
            return Task.FromResult(message);
        }
    }
}
