using Microsoft.AspNetCore.Mvc;
using FlashcardQuiz.Models;
using FlashcardQuiz.Repositories;
using FlashcardQuiz.DTOs;

namespace FlashcardQuiz.Endpoints
{
    public static class CardApi
    {

        public static void ConfigureCardApiEndpoint(this WebApplication app)
        {
            var libraryGroup = app.MapGroup("cards");

            libraryGroup.MapGet("/{cardId}", GetCard);
            libraryGroup.MapPost("/", CreateCard);
            libraryGroup.MapPut("/{cardId}", UpdateCard);
            libraryGroup.MapDelete("/{cardId}", DeleteCard);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetCard(ICardRepository repository, int cardId)
        {
            Card? card = await repository.GetCard(cardId);
            if (card == null)
            {
                return Results.NotFound("Card not found");
            }
            return TypedResults.Ok(new CardResponse(card));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateCard(ICardRepository repository, IQuizRepository quizRepository, CreateCardPayload payload)
        {
            Quiz? quiz = await quizRepository.GetQuiz(payload.QuizId);
            if (quiz == null)
            {
                return Results.NotFound("Quiz not found");
            }

            Card? card = await repository.CreateCard(payload.Question, payload.Answer, quiz);
            if (card == null)
            {
                return Results.BadRequest("Failed to create card");
            }

            return TypedResults.Ok(new CardResponse(card));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateCard(ICardRepository repository, int cardId, UpdateCardPayload payload)
        {
            Card? card = await repository.GetCard(cardId);
            if (card == null)
            {
                return Results.NotFound("Card not found");
            }
            if (payload.Question == null || payload.Question == "")
            {
                return Results.BadRequest("A non-empty Question is required");
            }
            if (payload.Answer == null || payload.Answer == "")
            {
                return Results.BadRequest("A non-empty Question is required");
            }

            card.Question = payload.Question;
            card.Answer = payload.Answer;

            await repository.UpdateCard(card);
            return TypedResults.Ok(new CardResponse(card));

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteCard(ICardRepository repository, int cardId)
        {
            Card? card = await repository.DeleteCard(cardId);
            if (card == null)
            {
                return Results.NotFound("Card not found");
            }
            return TypedResults.Ok(card);
        }

    }
}