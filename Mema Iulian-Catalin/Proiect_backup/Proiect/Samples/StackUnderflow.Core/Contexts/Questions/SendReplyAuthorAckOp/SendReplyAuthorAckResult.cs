using CSharp.Choices;
using StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendReplyAuthorAckOp
{
    [AsChoice]
    public static partial class SendReplyAuthorAckResult
    {
        public interface ISendReplyAuthorAckResult { }
        public class ReplyAuthorAckSent : ISendReplyAuthorAckResult
        {
            public Guid UserId { get; set; }
            public int QuestionId { get; set; }
            public string Body { get; set; }
            public ValidLetter Letter {get;set;}
            public ReplyAuthorAckSent(Guid userId,string body, ValidLetter letter)
            {
                UserId = userId;
                Body = body;
                Letter = letter;
            }
        }
        public class ReplyAuthorAckNotSent : ISendReplyAuthorAckResult
        {
            public ReplyAuthorAckNotSent(string message)
            {
                Message = message;
            }
        public string Message { get; }
    }
    }
}
