using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;
using StackUnderflow.EF.Models;

namespace StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp
{
    [AsChoice]
    public static partial class ValidationQuestionResult
    {
        public interface IValidationQuestionResult
        {

        }
        public class QuestionValidated : IValidationQuestionResult
        {
            public User QuestionUser { get; }
            public string ValidationAck { get; set; }
            public QuestionValidated( User quser,string vack)
            {
                QuestionUser = quser;
                ValidationAck = vack;
            }

        }
        public class QuestionNotValidated : IValidationQuestionResult 
        {
            public string Reason { get; }
            public QuestionNotValidated(string reason)
            {
                Reason = reason;
            }
        }

        public class InvalidRequest: IValidationQuestionResult
        {
            public string Message { get; }
            public InvalidRequest(string message)
            {
                Message = message;
            }
        }
    }
}
