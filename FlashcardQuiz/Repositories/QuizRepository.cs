using Microsoft.EntityFrameworkCore;
using FlashcardQuiz.Database;
using FlashcardQuiz.Models;

namespace FlashcardQuiz.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly DatabaseContext _context;

        public QuizRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quiz>> GetQuizes()
        {
            return await _context.Quizes.ToListAsync();
        }

        public async Task<Quiz?> GetQuiz(int quizId)
        {
            return await _context.Quizes.FirstOrDefaultAsync(q => q.Id == quizId);
        }

        public async Task<Quiz?> CreateQuiz(string title)
        {
            if (title == "") return null;
            var quiz = new Quiz { Title = title };
            await _context.Quizes.AddAsync(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task<Quiz?> UpdateQuiz(Quiz quiz)
        {
            _context.Quizes.Update(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task<Quiz?> DeleteQuiz(int quizId)
        {
            Quiz? quiz = await _context.Quizes.FindAsync(quizId);
            if (quiz == null)
                return null;

            Quiz? quizCopy = _context.Entry(quiz).CurrentValues.Clone().ToObject() as Quiz;

            _context.Quizes.Remove(quiz);
            await _context.SaveChangesAsync();

            return quizCopy;
        }
    }
}