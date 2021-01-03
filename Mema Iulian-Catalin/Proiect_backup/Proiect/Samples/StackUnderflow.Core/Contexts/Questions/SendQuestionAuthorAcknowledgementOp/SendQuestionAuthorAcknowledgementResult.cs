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
            public AckSent(Guid userId, string body, ValidLetter letter)
            {
                UserId = userId;
                Body = body;
                Letter = letter;
            }
            public Guid UserId { get; set; }
            //public int QuestionId { get; set; }
            public string Body { get; set; }
            public ValidLetter Letter { get; set; }


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
