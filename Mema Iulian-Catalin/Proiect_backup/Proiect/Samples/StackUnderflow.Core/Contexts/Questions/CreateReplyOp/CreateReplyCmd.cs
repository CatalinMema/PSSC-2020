using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateReplyOp
{
    public struct CreateReplyCmd
    {
        [Required]
        public string EmailAdress { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public string Body { get; set; }
       
        [Required]
        public byte Votes { get; set; }
        [Required]
        public int ReplyId { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public CreateReplyCmd(int questionId,int replyId,string body,byte votes,Guid userId,string email)
        {
            QuestionId = questionId;
            ReplyId = replyId;
            Body = body;
            Votes = votes;
            UserId = userId;
            EmailAdress = email;
        }
    }
}
