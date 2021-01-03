using GrainInterfaces;
using Microsoft.EntityFrameworkCore;
using Orleans.Streams;
using StackUnderflow.Domain.Schema.Models;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class QuestionProjectionGrain : Orleans.Grain,IQuestionProjectionGrain,IAsyncObserver<Post>
    {
        private readonly StackUnderflowContext _dbContext;
        private IList<Post> _questions;
        private readonly int tenantId=1;

        public QuestionProjectionGrain(StackUnderflowContext dbContext=null)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Post>> GetQuestionsAsync()
        {
            return _questions;
        }

        public async Task<IEnumerable<Post>> GetQuestionsAsync(int questionsId)
        {
            return _questions.Where(p => p.PostId == questionsId);
        }

        public override async Task OnActivateAsync()
        {
            IAsyncStream<Post> stream = this.GetStreamProvider("SMSProvider").GetStream<Post>(Guid.Empty, "questions");
           await stream.SubscribeAsync(this);
           _questions = await _dbContext.Post.Include(i => i.Vote).Where(p => p.PostId == 1).ToListAsync();
           
            /*_questions = new List<Post>() {
                new Post
                {
                PostId=2,
                PostText="My Question2"
                }
           };*/
        }

        public async Task OnNextAsync(Post item, StreamSequenceToken token = null)
        {
            _questions = await _dbContext.Post.Include(i => i.Vote).Where(p => p.PostId == 1).ToListAsync();
            //_questions.Add(item);
        } 
 

        public Task OnCompletedAsync()
        {
            throw new NotImplementedException();
        }

        public Task OnErrorAsync(Exception ex)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync()
        {
            throw new NotImplementedException();
        }
    }
}