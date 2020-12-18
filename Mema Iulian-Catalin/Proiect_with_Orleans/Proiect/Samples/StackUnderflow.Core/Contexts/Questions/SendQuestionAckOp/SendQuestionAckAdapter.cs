using Access.Primitives.IO;
using GrainInterfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAckOp.SendQuestionAckResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAckOp
{
    class SendQuestionAckAdaptor : Adapter<SendQuestionAckCmd, ISendQuestionAckResult, QuestionsWriteContext, QuestionsDependencies>
    {
        private readonly IClusterClient clusterClient;

        public SendQuestionAckAdaptor(IClusterClient client)
        {
            clusterClient = client;
        }
        public override Task PostConditions(SendQuestionAckCmd cmd, ISendQuestionAckResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ISendQuestionAckResult> Work(SendQuestionAckCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var asyncHelloGrain = this.clusterClient.GetGrain<IAsyncHello>("user1");
            await asyncHelloGrain.StartAsync();

            var stream = clusterClient.GetStreamProvider("SMSProvider").GetStream<string>(Guid.Empty, "chat");
            await stream.OnNextAsync("email@address.com");
            return new AcknowledgementSent(1, Guid.NewGuid(), "Body of the Question");
        }
    }
}

