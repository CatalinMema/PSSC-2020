using Orleans;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IQuestionProjectionGrain : IGrainWithStringKey
    {
        Task<IEnumerable<Post>> GetQuestionsAsync();
    }
}
