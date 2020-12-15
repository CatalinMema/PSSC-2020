using System;
using System.Collections.Generic;
using System.Text;
using Access.Primitives.IO;
using Access.Primitives.IO.Mocking;
using StackUnderflow.EF.Models;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp.ValidationQuestionResult;
using Access.Primitives.Extensions.ObjectExtensions;

namespace StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp 
{ 
    public partial class ValidationQuestionAdapter : Adapter<ValidationQuestionCmd, IValidationQuestionResult, QuestionsWriteContext, QuestionsDependencies>
    {
        private readonly IExecutionContext _ex;
        public ValidationQuestionAdapter(IExecutionContext ex)
        {
            _ex = ex;
        }

        public override async Task<IValidationQuestionResult> Work(ValidationQuestionCmd cmd,QuestionsWriteContext state,QuestionsDependencies dependencies)
        {
            var wf = from isValid in cmd.TryValidate()
                     from user in cmd.QUser.ToTryAsync()
                     let letter = GenerateValidationLetter(user)
                     from validationAck in dependencies.SendValidationEmail(letter)
                     select (user, validationAck);

            return await wf.Match(
                Succ: r=> new QuestionValidated(r.user,r.validationAck.Receipt),
                Fail: ex=>(IValidationQuestionResult)new InvalidRequest(ex.ToString()));
        }
        private ValidationLetter GenerateValidationLetter(User user)
        {
            var link = $"https://stackunderflow/QuestionCreated";
            var letter = $@"Dear {user.DisplayName} question is created.Please click on {link}";
            return new ValidationLetter(user.Email, letter, new Uri(link));
        }
        public override Task PostConditions(ValidationQuestionCmd cmd, IValidationQuestionResult result,QuestionsWriteContext state)
        {
            return Task.CompletedTask;
        }
        
    }
}
