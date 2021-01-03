using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using GrainInterfaces;
using Orleans;
using StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Questions.SendReplyAuthorAckOp.SendReplyAuthorAckResult;
using static StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp.ValidationQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendReplyAuthorAckOp
{
    public class SendReplyAuthorAckAdaptor : Adapter<SendReplyAuthorAckCmd,ISendReplyAuthorAckResult,QuestionsWriteContext,QuestionsDependencies>
    {
       /* private readonly IClusterClient clusterClient;

        public SendReplyAuthorAckAdaptor(IClusterClient client)
        {
            this.clusterClient = client;
        }*/

        public override Task PostConditions(SendReplyAuthorAckCmd cmd, ISendReplyAuthorAckResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ISendReplyAuthorAckResult> Work(SendReplyAuthorAckCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var workflow = from valid in state.TryValidate()
                           let t = AddQuestToUser(state, CreateQuestFromCmd(cmd))
                           select t;

            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new ReplyAuthorAckNotSent(ex.Message)
                );
            return result;
        }
        private ISendReplyAuthorAckResult AddQuestToUser(QuestionsWriteContext state, SendReplyAuthorAckCmd cmd)
        {
            return new ReplyAuthorAckSent(cmd.UserId,cmd.Body, new ValidLetter(cmd.AuthorEmail, cmd.Body, new Uri("https://stackoverflow.com/questions")));
        }

        private SendReplyAuthorAckCmd CreateQuestFromCmd(SendReplyAuthorAckCmd cmd)
        {
            return cmd;
        }
        /*
        public async override Task<ISendReplyAuthorAckResult> Work(SendReplyAuthorAckCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {

            var asyncHelloGrain = this.clusterClient.GetGrain<IAsyncHello>($"UserID:{cmd.UserId}");
            await asyncHelloGrain.StartAsync();

            var stream = clusterClient.GetStreamProvider("SMSProvider").GetStream<string>(Guid.Empty, "chat");
            await stream.OnNextAsync($"\nAdresa de email : {cmd.AuthorEmail} ");

            return new ReplyAuthorAckSent(cmd.UserId,cmd.QuestionId,cmd.Body);*/
    }
}

