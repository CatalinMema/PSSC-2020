using System;
using System.Collections.Generic;
using System.Text;
using static StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAuthorAcknowledgementOp.SendQuestionAuthorAcknowledgementResult;
using Access.Primitives.IO;
using Orleans;
using System.Threading.Tasks;
using GrainInterfaces;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAuthorAcknowledgementOp
{
    class SendQuestionAuthorAcknowledgementAdapter : Adapter<SendQuestionAuthorAcknowledgementCmd, ISendQuestionAuthorAcknowledgementResult,QuestionsWriteContext,QuestionsDependencies>
    {
        private readonly IClusterClient clusterClient;

        public SendQuestionAuthorAcknowledgementAdapter(IClusterClient client)
        {
            this.clusterClient = client;
        }

        public override Task PostConditions(SendQuestionAuthorAcknowledgementCmd cmd, ISendQuestionAuthorAcknowledgementResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ISendQuestionAuthorAcknowledgementResult> Work(SendQuestionAuthorAcknowledgementCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            
            var asyncHelloGrain = this.clusterClient.GetGrain<IAsyncHello>($"UserID:{cmd.UserId}");
            await asyncHelloGrain.StartAsync();

            var stream = clusterClient.GetStreamProvider("SMSProvider").GetStream<string>(Guid.Empty, "chat");
            await stream.OnNextAsync($"\nAdresa de email : {cmd.AuthorEmail} ");

            return new AckSent(cmd.QuestionId);
        }

    }
}
