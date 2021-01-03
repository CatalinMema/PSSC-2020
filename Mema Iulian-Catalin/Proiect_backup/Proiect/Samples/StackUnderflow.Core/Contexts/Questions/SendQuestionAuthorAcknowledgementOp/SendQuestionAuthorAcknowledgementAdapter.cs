using System;
using System.Collections.Generic;
using System.Text;
using static StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAuthorAcknowledgementOp.SendQuestionAuthorAcknowledgementResult;
using Access.Primitives.IO;
using Orleans;
using System.Threading.Tasks;
using GrainInterfaces;
using Access.Primitives.Extensions.ObjectExtensions;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAuthorAcknowledgementOp
{
    public class SendQuestionAuthorAcknowledgementAdapter : Adapter<SendQuestionAuthorAcknowledgementCmd, ISendQuestionAuthorAcknowledgementResult,QuestionsWriteContext,QuestionsDependencies>
    {

        public override Task PostConditions(SendQuestionAuthorAcknowledgementCmd cmd, ISendQuestionAuthorAcknowledgementResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }

        public override async Task<ISendQuestionAuthorAcknowledgementResult> Work(SendQuestionAuthorAcknowledgementCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var workflow = from valid in state.TryValidate()
                           let letter = GenerateValidationLetter(cmd.UserId, cmd.AuthorEmail)
                           //let t = AddQuestToUser(state, (SendReplyAuthorAckCmd)CreateQuestFromCmd(cmd))
                           from validationAck in dependencies.SendQuestionEmail(letter)
                           select validationAck;




            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => (ISendQuestionAuthorAcknowledgementResult)new AckNotSent(ex.ToString())
                );
            return result;
        }

        private ValidLetter GenerateValidationLetter(Guid userId, string email)
        {

            var link = $"https://stackunderflow/QuestionCreated";
            var letter = $@"Dear {userId} your question is created.Please click on {link}";
            return new ValidLetter(email, letter, new Uri(link));
        }

        /*private readonly IClusterClient clusterClient;

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
        */
    }
}
