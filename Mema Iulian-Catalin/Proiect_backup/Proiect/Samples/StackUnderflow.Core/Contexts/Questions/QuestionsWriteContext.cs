using StackUnderflow.DatabaseModel.Models;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public class QuestionsWriteContext
    {
        public ICollection<QuestionTable> Questions { get; }

        
        public QuestionsWriteContext(ICollection<QuestionTable> questions)
        {
            Questions = questions ?? new List<QuestionTable>();
            
        }

       
    }
}
