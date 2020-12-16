using Access.Primitives.IO;
using StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp;
using StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp;
using System;
using System.Collections.Generic;
using System.Text;
using static PortExt;
using static StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp.CreateQuestionResult;
using static StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp.ValidationQuestionResult;
namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public static class QuestionsContext
    {
        public static Port<ICreateQuestionResult> CreateQuestion(CreateQuestionCmd cmd)
        {
            return NewPort<CreateQuestionCmd, ICreateQuestionResult>(cmd); }

        public static Port<IValidationQuestionResult> ValidateQuestion(ValidationQuestionCmd cmd) => NewPort<ValidationQuestionCmd, IValidationQuestionResult>(cmd);
        
    }
}
