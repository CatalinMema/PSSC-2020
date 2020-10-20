using Profile.Domain.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using static Profile.Domain.CreateQuestionWorkflow.CreateQuestionResult;

namespace Test.App
{
    class ProgramQuestion
    {
        static void Main(string[] args)
        {
            var cmd = new CreateQuestionCmd("Titlu", "Continut", "Taguri");
            var result = CreateQuestion(cmd);

            result.Match(
                ProcessQuestionCreated, 
                ProcessQuestionNotCreated, 
                ProcessInvalidQuestion
                );
            Console.ReadLine();
        }


        private static ICreateQuestionResult ProcessInvalidQuestion(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Question validation failed:");
            foreach(var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);

            }
            return validationErrors;
        }
        private static ICreateQuestionResult ProcessQuestionNotCreated(QuestionNotCreated questionNotCreated)
        {
            Console.WriteLine($"Question not created:{questionNotCreated.Reason}");
            return questionNotCreated;
        }
        private static ICreateQuestionResult ProcessQuestionCreated(QuestionCreated question)
        {
            Console.WriteLine($"Question {question.QuestionId}");
            return question;
        }


        public static ICreateQuestionResult CreateQuestion(CreateQuestionCmd createQuestion)
        {
            if(string.IsNullOrWhiteSpace(createQuestion.Body))
            {
                var errors = new List<string>() { "Invalid body" };
                return new QuestionValidationFailed(errors);
            }
            if(string.IsNullOrEmpty(createQuestion.Title))
            {
                return new QuestionNotCreated("Enter a valid title");
            }
            if(new Random().Next(10)>1)
            {
                return new QuestionNotCreated("Question could not be verified");
            }
            var questionId = Guid.NewGuid();
            var result = new QuestionCreated(questionId, createQuestion.Body);

            return result;

        }
    }
}
