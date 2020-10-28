using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Question.Domain.PostQuestionWorkflow
{
    // product type = Title*Body*Tags
    public struct PostQuestionCmd
    {
        
        [Required]
        [MinLength(2),MaxLength(1000)]
        public string Title { get; private set; } // question title

        [Required]
        public string Body { get; set; } // question body ( question description )

        [Required]
        public List<string> Tags { get; set; } // question tags . Am folosit lista pentru ca putem adauga mai multe tags.
        
        [Required]
        public int Votes { get; private set; }

        public PostQuestionCmd(string title, string body, List<string> tags,int votes)
        {
            Title = title;
            Body = body;
            Tags = tags;
            Votes=votes;
        }
    }
}
