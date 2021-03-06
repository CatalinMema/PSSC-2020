﻿using StackUnderflow.DatabaseModel.Models;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public class QuestionsWriteContext
    {
        public ICollection<Post> Questions { get; }

        
        public QuestionsWriteContext(ICollection<Post> questions)
        {
            Questions = questions ?? new List<Post>(0);
            
        }

       
    }
}
