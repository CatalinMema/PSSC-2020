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

        public ICreateQuestionResult AddQuestionIfMissing(QuestionsWriteContext state,QuestionTable question)
        {
            if (state.Questions.Any(obj => obj.Title.Equals(question.Title)))
                return new QuestionNotCreated("Titlu deja inserat");
            if (state.Questions.All(obj => obj.QuestionId != question.QuestionId))
                state.Questions.Add(question);
            return new QuestionCreated(question);
        }
        private QuestionTable CreateQuestionFromCommand(CreateQuestionCmd cmd)
        {
            var question = new QuestionTable()
            {
                QuestionId=cmd.QuestionId,
                Body=cmd.Body,
                Title = cmd.Title,
                Tag=cmd.Tag
            };
            return question;
        }
        public override Task PostConditions(CreateQuestionCmd op, CreateQuestionResult.ICreateQuestionResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }
    }
}
