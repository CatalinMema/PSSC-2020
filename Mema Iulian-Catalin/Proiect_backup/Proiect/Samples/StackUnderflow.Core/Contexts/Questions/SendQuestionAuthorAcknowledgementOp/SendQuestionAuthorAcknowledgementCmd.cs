using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAuthorAcknowledgementOp
{
    public class SendQuestionAuthorAcknowledgementCmd
    {
        public SendQuestionAuthorAcknowledgementCmd(Guid userId, int questionId,string email)
        {
            UserId = userId;
            QuestionId = questionId;
            AuthorEmail = email;

          
        }
        public string AuthorEmail { get; }
        public Guid UserId { get; }
        public int QuestionId { get; }
       
    }
}
