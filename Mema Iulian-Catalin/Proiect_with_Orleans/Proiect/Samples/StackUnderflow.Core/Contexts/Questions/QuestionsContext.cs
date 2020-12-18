using Access.Primitives.IO;
using System;
using System.Collections.Generic;
using System.Text;
using static PortExt;
using StackUnderflow.Domain.Schema.Questions.CheckLanguageOp;
using static StackUnderflow.Domain.Schema.Questions.CheckLanguageOp.CheckLanguageResult;
using static StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp.CreateQuestionResult;
using StackUnderflow.Domain.Core.Contexts.Questions.CreateQuestionOp;
using static StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAckOp.SendQuestionAckResult;
using StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionAckOp;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    public static class QuestionsContext
    {
        public static Port<ICreateQuestionResult> CreateQuestion(CreateQuestionCmd createQuestionCmd) =>
           NewPort<CreateQuestionCmd, ICreateQuestionResult>(createQuestionCmd);

        public static Port<ICheckLanguageResult> CheckLanguage(CheckLanguageCmd checkLanguageCmd) =>
            NewPort<CheckLanguageCmd, ICheckLanguageResult>(checkLanguageCmd);

        public static Port<ISendQuestionAckResult> SendQuestionAckt(SendQuestionAckCmd cmd) =>
            NewPort<SendQuestionAckCmd, ISendQuestionAckResult>(cmd);
    }
}
