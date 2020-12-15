using Orleans;
using System;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IGrainSend : IGrainWithIntegerKey
    {
        Task<string> SendMessage(string message);
    }
}
