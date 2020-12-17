using System.Linq;
using System.Threading.Tasks;
using Access.Primitives.IO;
using Microsoft.AspNetCore.Mvc;
using Access.Primitives.EFCore;
using LanguageExt;
using StackUnderflow.Domain.Core.Contexts.Questions;
using StackUnderflow.EF;
using Microsoft.EntityFrameworkCore;
using StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp;
using StackUnderflow.DatabaseModel.Models;
using static StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp.CreateQuestionResult;
namespace StackUnderflow.API.Rest.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly DatabaseContext _dbContext;

        public QuestionsController(IInterpreterAsync interpreter, DatabaseContext dbContext)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
        }

        [HttpPost("createquestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCmd cmd)
        {
            var dep = new QuestionsDependencies();
            var questions = await _dbContext.Question.ToListAsync();
            var ctx = new QuestionsWriteContext(new EFList<Question>(_dbContext.Question));

            var expr = from createQuestionResult in QuestionsContext.CreateQuestion(cmd)
                       select createQuestionResult;

            var r = await _interpreter.Interpret(expr, ctx, dep);
            await _dbContext.Question.Add(new DatabaseModel.Models.Question { Title = cmd.Title, Body = cmd.Body, Tags = cmd.Tags.ToString() }).GetDatabaseValuesAsync();

            await _dbContext.SaveChangesAsync();

            return r.Match(
                    created => (IActionResult)Ok(created.Body),
                    notcreated => BadRequest("NotPosted"),
                    invalidRequest => ValidationProblem()
                    );
        }
        /*[HttpPost("createReply")]
        public async Task<IActionResult> CreateReply([FromBody] CreateReplyCmd cmd)
        {
            var dep = new QuestionsDependencies();
            var replies = await _dbContext.Replies.ToListAsync();
            var ctx = new QuestionsWriteContext(replies);

            var expr = from createTenantResult in QuestionsContext.CreateReply(cmd)
                       select createTenantResult;

            var r = await _interpreter.Interpret(expr, ctx, dep);

            _dbContext.Replies.Add(new DatabaseModel.Models.Reply { Body = cmd.Body, AuthorUserId = new Guid("f505c32f-3573-4459-8112-af8276d3e919"), QuestionId = cmd.QuestionId, ReplyId = 4 });
            //var reply = await _dbContext.Replies.Where(r => r.ReplyId == 4).SingleOrDefaultAsync();
            //reply.Body = "Text updated";
            //_dbContext.Replies.Update(reply);
            await _dbContext.SaveChangesAsync();


            return r.Match(
                succ => (IActionResult)Ok(succ.Body),
                fail => BadRequest("Reply could not be added")
                );
        }*/
    }
}
