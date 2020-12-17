using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp.CreateQuestionResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp
{
    public class CreateQuestionAdaptor : Adapter<CreateQuestionCmd, ICreateQuestionResult, QuestionsWriteContext, QuestionsDependencies>
    {
        public override Task PostConditions(CreateQuestionCmd cmd, ICreateQuestionResult result, QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }

        public async override Task<ICreateQuestionResult> Work(CreateQuestionCmd cmd, QuestionsWriteContext state, QuestionsDependencies dependencies)
        {
            var workflow = from valid in cmd.TryValidate()
                           let t = PostQuestion(state, PostQuestionFromCmd(cmd))
                           select t;

            state.Questions.Add(new DatabaseModel.Models.Question { QuestionId = new Guid("1fcbdc4c-e4d7-4e4e-9ce2-8e186d64e277"), Title = "Title1 question1", Body = "Body question", Tags = "Tags" });



            var result = await workflow.Match(
                Succ: r => r,
                Fail: ex => new QuestionNotCreated(ex.Message)
                );

            return result;
        }

        private ICreateQuestionResult PostQuestion(QuestionsWriteContext state, object v)
        {
            List<string> tags = new List<string> { "React", "JavaScript" };
            return new QuestionCreated(Guid.NewGuid(),"My question title","My question body", tags);
        }

        private object PostQuestionFromCmd(CreateQuestionCmd cmd)
        {
            return new { };
        }
    }
}
