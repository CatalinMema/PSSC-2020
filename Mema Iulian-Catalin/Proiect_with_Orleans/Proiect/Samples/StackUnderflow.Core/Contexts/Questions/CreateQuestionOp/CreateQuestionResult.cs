using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult { }

        public class QuestionCreated : ICreateQuestionResult
        {
            public Guid QuestionId { get; }
            public string Title { get; } 
            public string Body { get; } 
            public List<string> Tags { get; }  
           
            public QuestionCreated(Guid questionId, string title, string body, List<string> tags)
            {
                QuestionId = questionId;
                Title = title;
                Body = body;
                Tags = tags;       
            }
        }

        public class QuestionNotCreated : ICreateQuestionResult
        {
            public string Reason { get; }

            public QuestionNotCreated(string reason)
            {
                Reason = reason;
            }
        }

        public class QuestionValidationFailed : ICreateQuestionResult
        {
            public IEnumerable<string> ValidationErrors { get; }

            public QuestionValidationFailed(IEnumerable<string> errors)
            {
                ValidationErrors = errors.AsEnumerable();
            }
        }
    }
}

