using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendReplyAuthorAckOp
{
    public struct SendReplyAuthorAckCmd
    {
        public Guid UserId { get; set; }
        public int QuestionId { get; set; }
        public string Body { get; set; }
        public string AuthorEmail { get; }


        public SendReplyAuthorAckCmd(Guid userId,int questionId,string body, string email)
        {
            UserId = userId;
            QuestionId = questionId;
            Body = body;
            AuthorEmail = email;
        }
    }
}
