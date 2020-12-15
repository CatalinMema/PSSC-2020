using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp
{
    public class ValidationLetter
    {
        public string Email { get; private set; }
        public string Letter { get; private set; }
        public Uri Link { get; private set; }

        public ValidationLetter(string email,string letter,Uri link)
        {
            Email = email;
            Letter = letter;
            Link = link;
        }
    }
}
