using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp
{
    public class ValidationAck
    {
        public string Receipt { get; private set; }
        public ValidationAck(string receipt)
        {
            Receipt = receipt;
        }
    }
}
