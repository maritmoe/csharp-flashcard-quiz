using FlashcardQuiz.Models;

namespace FlashcardQuiz.Repositories
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetQuizes();
        Task<Quiz?> GetQuiz(int quizId);
        Task<Quiz?> CreateQuiz(string title);
        Task<Quiz?> UpdateQuiz(Quiz quiz);
        Task<Quiz?> DeleteQuiz(int quizId);
    }
}