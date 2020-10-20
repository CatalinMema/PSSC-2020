using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Profile.Domain.CreateQuestionWorkflow
{
    [AsChoice]
    public static partial class CreateQuestionResult
    {
        public interface ICreateQuestionResult
        { 
        
        }
        public class QuestionCreated : ICreateQuestionResult
        {
            public Guid QuestionId { get; private set; }
            public string Content { get; private set; }

            public QuestionCreated(Guid questionid, string content)
            {
                QuestionId = questionid;
                Content = content;
            }
        }
        public class QuestionNotCreated:ICreateQuestionResult
        {
            public string Reason { get; set; }
            public QuestionNotCreated(string reason)
            {
                Reason = reason;
            }
        }
        public class QuestionValidationFailed:ICreateQuestionResult
        {
            public IEnumerable<string> ValidationErrors { get; private set; }
            public QuestionValidationFailed(IEnumerable<string>errors)
            {

                ValidationErrors = errors.AsEnumerable();
            }
        }
    }

}

