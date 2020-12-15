﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Domain.Core;
using StackUnderflow.Domain.Core.Contexts;
using StackUnderflow.Domain.Schema.Backoffice.CreateTenantOp;
using StackUnderflow.EF.Models;
using Access.Primitives.EFCore;
using StackUnderflow.Domain.Schema.Backoffice.InviteTenantAdminOp;
using StackUnderflow.Domain.Schema.Backoffice;
using LanguageExt;
using StackUnderflow.Domain.Core.Contexts.Questions;
using StackUnderflow.EF;
using Microsoft.EntityFrameworkCore;
using Orleans;
using StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp;
using StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp;
using Microsoft.AspNetCore.Http;
using GrainInterfaces;

namespace StackUnderflow.API.Rest.Controllers
{
    [ApiController]
    [Route("questions")]
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

        [HttpPost("question")]
        public async Task<IActionResult> CreateAndValidateQuestion([FromBody] CreateQuestionCmd cmd)
        {
            QuestionsWriteContext ctx = new QuestionsWriteContext(
                new EFList<Post>(_dbContext.Post),
                new EFList<User>(_dbContext.User));

            var dep = new QuestionsDependencies();
            dep.GenerateConfirmationToken = () => Guid.NewGuid().ToString();
            dep.SendEmail = SendValidationEmail;

            var expr = from createQuestionResult in QuestionsContext.CreateQuestion(cmd)
                       let quser = createQuestionResult.SafeCast<CreateQuestionResult.QuestionCreated>().Select(p => p.User)
                       let validationQuestionCmd = new ValidationQuestionCmd(quser)
                       from ValidationQuestionResult in QuestionsContext.ValidateQuestion(validationQuestionCmd)
                       select new { createQuestionResult, ValidationQuestionResult };

            var r = await _interpreter.Interpret(expr, ctx, dep);
            _dbContext.SaveChanges();
            return r.createQuestionResult.Match(
                created => (IActionResult)Ok(created.Question.PostId),
                notCreated => StatusCode(StatusCodes.Status500InternalServerError, "QQuestion could not be created"),
                invalidRequest => BadRequest("Invalid request")); 
        }
        private TryAsync<ValidationAck> SendValidationEmail(ValidationLetter letter) => async () =>
        {
            var eSender = _client.GetGrain<IGrainSend>(0);
            await eSender.SendMessage(letter.VLetter);
            return new ValidationAck(Guid.NewGuid().ToString());
        };
    }
}