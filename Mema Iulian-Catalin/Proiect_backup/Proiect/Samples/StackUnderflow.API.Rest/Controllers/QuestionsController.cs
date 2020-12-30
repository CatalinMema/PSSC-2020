using System;
using System.Threading.Tasks;
using Access.Primitives.IO;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Domain.Schema.Backoffice.CreateTenantOp;
using StackUnderflow.EF.Models;
using Access.Primitives.EFCore;
using StackUnderflow.Domain.Schema.Backoffice.InviteTenantAdminOp;
using LanguageExt;
using StackUnderflow.Domain.Core.Contexts.Questions;
using Orleans;
using StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp;
using StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp;
using Microsoft.AspNetCore.Http;
using GrainInterfaces;

namespace StackUnderflow.API.Rest.Controllers
{
    [ApiController]
    [Route("Question")]
    public class QuestionsController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly StackUnderflowContext _dbContext;
        private readonly IClusterClient _client;

        public QuestionsController(IInterpreterAsync interpreter, StackUnderflowContext dbContext, IClusterClient client)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
            _client = client;
        }

        [HttpPost("CreateQuestion")]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCmd cmd)
        {
            QuestionsWriteContext ctx = new QuestionsWriteContext(
                new EFList<Post>(_dbContext.Post));

            var dependencies = new QuestionsDependencies();
            dependencies.SendValidationEmail = SendEmail;

            var expr = from createQuestionResult in QuestionsContext.CreateQuestion(cmd)
                       select  createQuestionResult;

            var r = await _interpreter.Interpret(expr, ctx, dependencies);
            _dbContext.SaveChanges();
            return r.Match(
                created => (IActionResult)Ok(created),
                notCreated => StatusCode(StatusCodes.Status500InternalServerError, "Question could not be created"),
                invalidRequest => BadRequest("Invalid request")); 
        }
        private TryAsync<ValidationAck> SendEmail(ValidationLetter letter) 
            => async () =>
            {
            var emialSender = _client.GetGrain<IEmailSender>(0);
            await emialSender.SendEmailAsync(letter.Letter);
            return new ValidationAck(Guid.NewGuid().ToString());
            };
    }
}
