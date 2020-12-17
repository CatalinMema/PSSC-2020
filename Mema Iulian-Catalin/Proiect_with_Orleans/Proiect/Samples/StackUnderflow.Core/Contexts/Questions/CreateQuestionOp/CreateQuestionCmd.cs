using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp
{
    public class CreateQuestionCmd
    {
        public CreateQuestionCmd()
        {

        }

        public CreateQuestionCmd(Guid questionId, string title, string body, List<string> tags, int votes)
        {
            QuestionId = questionId;
            Title = title;
            Body = body;
            Tags = tags;
            Votes = votes;
        }

        [Required]
        public Guid QuestionId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Body { get; set; }
        [Required]
        public List<string> Tags { get; set; }
        //public string Tags { get; set; }
        [Required]
        public int Votes { get; set; }
    }
}
