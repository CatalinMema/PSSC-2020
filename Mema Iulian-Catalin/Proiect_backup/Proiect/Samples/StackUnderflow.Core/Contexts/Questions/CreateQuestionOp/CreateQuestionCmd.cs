using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp
{
    public struct CreateQuestionCmd
    {
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string Tag { get; set; }
        public CreateQuestionCmd(int questionId, string title, string body, string tag)
        {
            QuestionId = questionId;
            Title = title;
            Body = body;
            Tag = tag;
        }
    }
}
