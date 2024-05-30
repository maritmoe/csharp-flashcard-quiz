using Microsoft.AspNetCore.Mvc;
using FlashcardQuiz.Models;
using FlashcardQuiz.Repositories;
using FlashcardQuiz.DTOs;

namespace FlashcardQuiz.Endpoints
{
    public static class QuizApi
    {

        public static void ConfigureQuizApiEndpoint(this WebApplication app)
        {
            var libraryGroup = app.MapGroup("quiz");

            libraryGroup.MapGet("/", GetQuizes);
            libraryGroup.MapGet("/{quizId}", GetQuiz);
            libraryGroup.MapPost("/", CreateQuiz);
            libraryGroup.MapPut("/{quizId}", UpdateQuiz);
            libraryGroup.MapDelete("/{quizId}", DeleteQuiz);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetQuizes(IQuizRepository repository)
        {
            var quizes = await repository.GetQuizes();
            var quizesTitleResponse = new List<QuizDTO>();
            foreach (Quiz quiz in quizes)
            {
                quizesTitleResponse.Add(new QuizDTO(quiz));
            }
            return TypedResults.Ok(quizesTitleResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetQuiz(IQuizRepository repository, int quizId)
        {
            Quiz? quiz = await repository.GetQuiz(quizId);
            if (quiz == null)
            {
                return Results.NotFound("Quiz not found");
            }
            return TypedResults.Ok(new QuizResponse(quiz));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateQuiz(IQuizRepository repository, CreateQuizPayload payload)
        {
            if (payload.Title == null || payload.Title == "")
            {
                return Results.BadRequest("A non-empty Title is required");
            }

            Quiz? quiz = await repository.CreateQuiz(payload.Title);
            if (quiz == null)
            {
                return Results.BadRequest("Failed to create quiz.");
            }
            return TypedResults.Ok(new QuizResponse(quiz));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateQuiz(IQuizRepository repository, int quizId, CreateQuizPayload payload)
        {
            Quiz? quiz = await repository.GetQuiz(quizId);
            if (quiz == null)
            {
                return Results.NotFound("Quiz not found");
            }
            if (payload.Title == null || payload.Title == "")
            {
                return Results.BadRequest("A non-empty Title is required");
            }

            quiz.Title = payload.Title;

            await repository.UpdateQuiz(quiz);
            return TypedResults.Ok(new QuizResponse(quiz));

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteQuiz(IQuizRepository repository, int quizId)
        {
            Quiz? quiz = await repository.DeleteQuiz(quizId);
            if (quiz == null)
            {
                return Results.NotFound("Quiz not found");
            }
            return TypedResults.Ok(quiz);
        }

    }
}