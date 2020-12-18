using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAckOp
{
    [AsChoice]
    public static partial class SendQuestionAckResult
    {
        public interface ISendQuestionAckResult { }
        public class AcknowledgementSent : ISendQuestionAckResult
        {
            public int QuestionId { get; }
            public Guid AuthorUserId { get; }
            public string Body { get; set; }
            public AcknowledgementSent(int questionId, Guid authorUserId, string body)
            {
                QuestionId = questionId;
                AuthorUserId = authorUserId;
                Body = body;
            }

        }
        public class AcknowledgementNotSent : ISendQuestionAckResult
        {
            public string Message { get; }
            public AcknowledgementNotSent(string message)
            {
                Message = message;
            }
        }
    }
}

