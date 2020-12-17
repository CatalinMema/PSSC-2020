using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Schema.Questions.CheckLanguageOp
{
    [AsChoice]
    public static partial class CheckLanguageResult
    {
        public interface ICheckLanguageResult { }

        public class TextChecked : ICheckLanguageResult
        {
            public string Message { get; }
            public TextChecked(string message)
            {
                Message = message;
            }
        }
        public class TextNotChecked : ICheckLanguageResult
        {
            public string ErrorMessage { get; }
            public TextNotChecked(string errorMessage)
            {
                ErrorMessage = errorMessage;
            }
        }
        public class ManualReviewRequired : ICheckLanguageResult
        {
            public string Text { get; }
            public ManualReviewRequired(string text)
            {
                Text = text;
            }
        }
    }
}
