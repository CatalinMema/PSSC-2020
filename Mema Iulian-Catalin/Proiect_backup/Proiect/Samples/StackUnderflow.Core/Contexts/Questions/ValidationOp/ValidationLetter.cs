using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp
{
    public class ValidationLetter
    {
        public string VEmail { get; private set; }
        public string VLetter { get; private set; }
        public Uri VLink { get; private set; }

        public ValidationLetter(string email,string letter,Uri Vlink)
        {
            VEmail = email;
            VLetter = letter;
            VLink = Vlink;
        }
    }
}
