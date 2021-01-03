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
        [Required]
        public byte Votes { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string EmailAdress { get; set; }
        public CreateQuestionCmd(int questionId, string title, string body, string tag, byte votes, Guid userId,string email)
        {
            QuestionId = questionId;
            Title = title;
            Body = body;
            Tag = tag;
            Votes = votes;
            UserId = userId;
            EmailAdress = email;

        }
    }
}
