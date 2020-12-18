﻿using GrainInterfaces;
using Orleans;
using System.Threading.Tasks;

namespace Grains
{
    public class EmailSenderGrain : Orleans.Grain, IEmailSender
    {
        public Task<string> SendEmailAsync(string message)
        {
            return Task.FromResult(message);
        }
    }
}