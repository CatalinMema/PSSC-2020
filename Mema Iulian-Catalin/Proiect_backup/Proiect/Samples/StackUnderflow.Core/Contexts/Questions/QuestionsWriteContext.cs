using StackUnderflow.DatabaseModel.Models;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public class QuestionsWriteContext
    {
        public ICollection<Post> Questions { get; }
        public ICollection<User> Users { get; }
        public QuestionsWriteContext(ICollection<Post> questions,ICollection<User> users)
        {
            Questions = questions ?? new List<Post>();
            Users = users ?? new List<User>();
        }

       
    }
}
