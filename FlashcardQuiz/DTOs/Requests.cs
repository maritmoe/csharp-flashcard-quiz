namespace FlashcardQuiz.DTOs
{
    public record CreateQuizPayload(string Title);

    public record CreateCardPayload(int QuizId, string Question, string Answer);

    public record UpdateCardPayload(string Question, string Answer);

}