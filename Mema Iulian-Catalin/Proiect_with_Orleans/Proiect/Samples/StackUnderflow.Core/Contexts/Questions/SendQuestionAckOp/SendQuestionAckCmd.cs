using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAckOp
{
    public class SendQuestionAckCmd
    {
        public int QuestionId { get; }
        public Guid AuthorUserId { get; }
        public string Body { get; }
        public SendQuestionAckCmd() { }
        public SendQuestionAckCmd(int questionId, Guid authorUserId, string body)
        {
            QuestionId = questionId;
            AuthorUserId = authorUserId;
            Body = body;
        }
    }
}
