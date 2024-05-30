using FlashcardQuiz.Models;

namespace FlashcardQuiz.DTOs
{
    class QuizDTO
    {
        public int QuizId { get; set; }
        public string Title { get; set; }

        public QuizDTO(Quiz quiz)
        {
            QuizId = quiz.Id;
            Title = quiz.Title;
        }
    }

    class CardInQuizDTO
    {
        public int CardId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public CardInQuizDTO(Card card)
        {
            CardId = card.Id;
            Question = card.Question;
            Answer = card.Answer;
        }
    }

    class CardResponse
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public QuizDTO Quiz { get; set; }

        public CardResponse(Card card)
        {
            Id = card.Id;
            Question = card.Question;
            Answer = card.Answer;
            Quiz = new QuizDTO(card.Quiz);
        }
    }

    class QuizResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<CardInQuizDTO> Cards { get; set; } = new List<CardInQuizDTO>();

        public QuizResponse(Quiz quiz)
        {
            Id = quiz.Id;
            Title = quiz.Title;
            if (quiz.Cards != null)
            {
                foreach (Card card in quiz.Cards)
                {
                    Cards.Add(new CardInQuizDTO(card));
                }
            }
        }
    }

}