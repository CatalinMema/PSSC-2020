using LanguageExt;
using StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public class QuestionsDependencies
    {
        public Func<ValidationLetter, TryAsync<ValidationAck>> SendValidationEmail { get; set; }
    }
}
