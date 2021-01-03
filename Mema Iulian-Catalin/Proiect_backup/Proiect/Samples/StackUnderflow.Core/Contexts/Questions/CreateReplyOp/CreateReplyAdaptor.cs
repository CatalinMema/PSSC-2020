using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using Access.Primitives.IO.Mocking;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Questions.CreateReplyOp.CreateReplyResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateReplyOp
{
    public partial class CreateReplyAdaptor : Adapter<CreateReplyCmd,ICreateReplyResult,QuestionsWriteContext,QuestionsDependencies>
    {
        private readonly IExecutionContext _ex;

        public CreateReplyAdaptor(IExecutionContext ex)
        {
            _ex = ex;
        }

        public override async Task<ICreateReplyResult> Work(CreateReplyCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = AddReplyIfMissing(state, CreateReplyFromCommand(cmd))
                           select t;
            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new InvalidReply(ex.ToString()));
            return result;
        }

        public ICreateReplyResult AddReplyIfMissing(QuestionsWriteContext state, Post reply)
        {
            if (state.Questions.Any(obj => obj.PostText.Equals(reply.PostText)))
                return new ReplyNotCreated("-");
            if (state.Questions.All(obj => obj.PostId != reply.PostId))
                state.Questions.Add(reply);
            return new ReplyCreated(reply, reply.PostedBy);
        }
        private Post CreateReplyFromCommand(CreateReplyCmd cmd)
        {
            var reply = new Post()
            {
                PostId = cmd.ReplyId,
                TenantId = cmd.QuestionId,
                PostText = cmd.Body,
                Title = cmd.QuestionId.ToString(),
                PostedBy = cmd.UserId,
                PostTypeId = cmd.Votes,
                //ParentPostId=cmd.QuestionId
            };
            return reply;
        }

        public override Task PostConditions(CreateReplyCmd op, CreateReplyResult.ICreateReplyResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }
    }
}
