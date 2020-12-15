using EarlyPay.Primitives.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using StackUnderflow.EF.Models;
using LanguageExt;
using Access.Primitives.IO;

namespace StackUnderflow.Domain.Core.Contexts.Questions.ValidationOp
{
    public struct ValidationQuestionCmd
    {
        [OptionValidator(typeof(RequiredAttribute))]
        public Option<User> QUser { get; }
        public ValidationQuestionCmd(Option<User> quser)
        {
            QUser = quser;
        }
    }
    public enum ConfQuestionCmdInput
    {
        Valid,
        UserIsNone
    }
    public class ConfQuestionCmdInputGen : InputGenerator<ValidationQuestionCmd,ConfQuestionCmdInput>
    {
        public ConfQuestionCmdInputGen()
        {
            mappings.Add(ConfQuestionCmdInput.Valid, () =>
             new ValidationQuestionCmd(
                 Option<User>.Some(new User()
                 {
                     DisplayName = Guid.NewGuid().ToString(),
                     Email = $"{Guid.NewGuid()}@mailinator.com"
                 })));
            mappings.Add(ConfQuestionCmdInput.UserIsNone, () =>
             new ValidationQuestionCmd(
                 Option<User>.None));
        }
    }
}
