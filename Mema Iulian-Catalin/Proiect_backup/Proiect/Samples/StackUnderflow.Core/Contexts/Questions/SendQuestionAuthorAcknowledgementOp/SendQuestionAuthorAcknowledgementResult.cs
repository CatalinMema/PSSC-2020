using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAuthorAcknowledgementOp
{
    [AsChoice]
    public static partial class SendQuestionAuthorAcknowledgementResult
    {
        public interface ISendQuestionAuthorAcknowledgementResult { }
        public class AckSent:ISendQuestionAuthorAcknowledgementResult
        {
            public AckSent(int questionId)
            {
                QuestionId = questionId;
            }
            public int QuestionId { get; }
        
        
        }

        public class AckNotSent:ISendQuestionAuthorAcknowledgementResult
        {
            public AckNotSent(string message)
            {
                Message = message;
            }
            public string Message { get; }
        }
    }
}
