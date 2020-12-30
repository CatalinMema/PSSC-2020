using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;
using StackUnderflow.EF.Models;
using Access.Primitives.Extensions.Cloning;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp
{   
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult : IDynClonable { }
        public class QuestionCreated : ICreateQuestionResult
        {
            public Post Question { get; }
            public Guid QuestionUser { get; }
            public QuestionCreated(Post question, Guid Questionuser)
            {
                Question = question;
                QuestionUser = Questionuser;
            }

            public object Clone() => this.ShallowClone();
        }

        public class QuestionNotCreated : ICreateQuestionResult
        {
            public string Reason { get; private set; }

            public QuestionNotCreated(string reason)
            {
                Reason = reason;
            }
            public object Clone() => this.ShallowClone();
        }

        public class InvalidQuestion : ICreateQuestionResult
        {
            public string Message { get; }
            public InvalidQuestion(string message)
            {
                Message = message;
            }
            public object Clone() => this.ShallowClone();
        }
    }
}
