using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public class QuestionReadContext
    {
        public IEnumerable<Post> Questions { get; }
        public IEnumerable<User> Users { get; }
        public QuestionReadContext(IEnumerable<Post> questions, IEnumerable<User> users)
        {
            Questions = questions;
            Users = users;
        }

    }
   
}
