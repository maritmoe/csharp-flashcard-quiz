using Microsoft.EntityFrameworkCore;
using FlashcardQuiz.Database;
using FlashcardQuiz.Models;

namespace FlashcardQuiz.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly DatabaseContext _context;

        public CardRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Card>> GetCardsFromQuiz(int quizId)
        {
            return await _context.Cards.Where(c => c.QuizId == quizId).ToListAsync();
        }

        public async Task<Card?> GetCard(int cardId)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.Id == cardId);
        }

        public async Task<Card?> CreateCard(string question, string answer, Quiz quiz)
        {
            if (quiz == null) return null;
            var card = new Card
            {
                Question = question,
                Answer = answer,
                QuizId = quiz.Id,
                Quiz = quiz
            };

            try
            {
                await _context.Cards.AddAsync(card);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // failed to create card, maybe the quiz id is wrong
                return null;
            }
            return card;
        }

        public async Task<Card?> UpdateCard(Card card)
        {
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<Card?> DeleteCard(int cardId)
        {
            Card? card = await _context.Cards.FindAsync(cardId);
            if (card == null)
                return null;

            Card? cardCopy = _context.Entry(card).CurrentValues.Clone().ToObject() as Card;

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return cardCopy;
        }
    }
}