using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using Access.Primitives.IO.Mocking;
using StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp;
using StackUnderflow.EF.Models;
using static StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp.CreateQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp
{
    public partial class CreateQuestionAdapter : Adapter<CreateQuestionCmd, ICreateQuestionResult, QuestionsWriteContext, QuestionsDependencies>
    {
        private readonly IExecutionContext _ex;
        
        public CreateQuestionAdapter(IExecutionContext ex)
        {
            _ex = ex;
        }

        public override async Task<ICreateQuestionResult> Work(CreateQuestionCmd cmd,QuestionsWriteContext state,QuestionsDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = AddQuestionIfMissing(state, CreateQuestionFromCommand(cmd))
                           select t;
            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new InvalidQuestion(ex.ToString()));
            return result;
        }

        public ICreateQuestionResult AddQuestionIfMissing(QuestionsWriteContext state,Post post)
        {
            if (state.Questions.Any(p => p.Title.Equals(post.Title)))
                return new QuestionNotCreated();
            if (state.Questions.All(p => p.PostId != post.PostId))
                state.Questions.Add(post);
            return new QuestionCreated(post, post.TenantUser.User);
        }
        private Post CreateQuestionFromCommand(CreateQuestionCmd cmd)
        {
            var question = new Post()
            {
                Title = cmd.Title,
                PostText = cmd.Body,
                PostTag = cmd.Tags,
            };
            return question;
        }
        public override Task PostConditions(CreateQuestionCmd op, CreateQuestionResult.ICreateQuestionResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }
    }
}
