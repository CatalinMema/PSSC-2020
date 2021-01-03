using Access.Primitives.IO;
using StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp;
using StackUnderflow.Domain.Core.Contexts.Questions.CreateReplyOp;
using StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAuthorAcknowledgementOp;
using StackUnderflow.Domain.Core.Contexts.Questions.SendReplyAuthorAckOp;
using StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp;
using StackUnderflow.Domain.Schema.Questions.CheckLanguageOp;
using System;
using System.Collections.Generic;
using System.Text;
using static PortExt;
using static StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp.CreateQuestionResult;
using static StackUnderflow.Domain.Core.Contexts.Questions.CreateReplyOp.CreateReplyResult;
using static StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAuthorAcknowledgementOp.SendQuestionAuthorAcknowledgementResult;
using static StackUnderflow.Domain.Core.Contexts.Questions.SendReplyAuthorAckOp.SendReplyAuthorAckResult;
using static StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp.ValidationQuestionResult;
using static StackUnderflow.Domain.Schema.Questions.CheckLanguageOp.CheckLanguageResult;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public static class QuestionsContext
    {
        public static Port<ICreateQuestionResult> CreateQuestion(CreateQuestionCmd command) => NewPort<CreateQuestionCmd, ICreateQuestionResult>(command);
        public static Port<ICreateReplyResult> CreateReply(CreateReplyCmd command) => NewPort<CreateReplyCmd, ICreateReplyResult>(command);
        public static Port<IValidationQuestionResult> ValidateQuestion(ValidationQuestionCmd command) => NewPort<ValidationQuestionCmd, IValidationQuestionResult>(command);
        public static Port<ICheckLanguageResult> CheckLanguage(CheckLanguageCmd checkLanguageCmd) =>
           NewPort<CheckLanguageCmd, ICheckLanguageResult>(checkLanguageCmd);
        public static Port<ISendQuestionAuthorAcknowledgementResult> SendQuestionAuthorAcknowledgement(SendQuestionAuthorAcknowledgementCmd cmd) =>
          NewPort<SendQuestionAuthorAcknowledgementCmd, ISendQuestionAuthorAcknowledgementResult>(cmd);

        public static Port<ISendReplyAuthorAckResult> SendReplyAuthorAcknowledgement(SendReplyAuthorAckCmd cmd) =>
          NewPort<SendReplyAuthorAckCmd, ISendReplyAuthorAckResult>(cmd);
    }
}
