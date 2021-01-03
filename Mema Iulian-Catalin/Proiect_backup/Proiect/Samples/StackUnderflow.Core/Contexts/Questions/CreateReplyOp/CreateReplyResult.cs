using Access.Primitives.Extensions.Cloning;
using CSharp.Choices;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateReplyOp
{
    [AsChoice]
    public static partial class CreateReplyResult
    {
        public interface ICreateReplyResult : IDynClonable { }
        public class ReplyCreated : ICreateReplyResult
        {
            public Post Reply { get; }
            public Guid ReplyUser { get; }
            public  ReplyCreated (Post reply, Guid replyUser)
            {
                Reply = reply;
                ReplyUser = replyUser;

            }
            public object Clone() => this.ShallowClone();
        }
        public class ReplyNotCreated : ICreateReplyResult
        {
            public string Reason { get; private set; }

            public ReplyNotCreated(string reason)
            {
                Reason = reason;
            }
            public object Clone() => this.ShallowClone();
        }

        public class InvalidReply : ICreateReplyResult
        {
            public string Message { get; }
            public InvalidReply(string message)
            {
                Message = message;
            }
            public object Clone() => this.ShallowClone();
        }
    }
}
