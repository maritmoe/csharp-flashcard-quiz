using FlashcardQuiz.Models;

namespace FlashcardQuiz.Repositories
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetCardsFromQuiz(int quizId);
        Task<Card?> GetCard(int cardId);
        Task<Card?> CreateCard(string question, string answer, Quiz quiz);
        Task<Card?> UpdateCard(Card card);
        Task<Card?> DeleteCard(int cardId);
    }
}