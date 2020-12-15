using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp
{
    public struct CreateQuestionCmd
    {
        public CreateQuestionCmd(Guid questionId, string title,string body, HashSet<PostTag> tags, int votes)
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
        public HashSet<PostTag> Tags;
        [Required]
        public int Votes { get; set; }
    }
}
