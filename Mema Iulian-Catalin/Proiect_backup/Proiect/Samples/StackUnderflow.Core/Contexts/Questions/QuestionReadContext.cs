using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public class QuestionReadContext
    {
        public IEnumerable<QuestionTable> Questions { get; }
        public QuestionReadContext(IEnumerable<QuestionTable> questions)
        {
            Questions = questions;
        }

    }
   
}
