using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackUnderflow.API.AspNetCore.Controllers
{
    [ApiController]
    [Route("questions-proj")]
    public class QuestionProjectionController : ControllerBase
    {
        private readonly IClusterClient clusterClient;
        public QuestionProjectionController(IClusterClient clusterC)
        {
            clusterClient = clusterC;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllQuestions()
        {
            //var questions= GetQuestionsFromDb();
            var questionsProjectionGrain = this.clusterClient.GetGrain<IQuestionProjectionGrain>("PostId");
            var questions = await questionsProjectionGrain.GetQuestionsAsync();

            
            return Ok(questions);
        }

        private List<Post> GetQuestionsFromDb()
        {
            return new List<Post>
            {
                new Post
                {
                    PostId=1,
                    PostText="Question body",
                }
            };
        }

        
    }
}
