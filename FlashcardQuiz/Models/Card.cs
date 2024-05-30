using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlashcardQuiz.Models
{
    [Table("cards")]
    public class Card
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("question")]
        [Required]
        public string Question { get; set; }

        [Column("answer")]
        [Required]
        public string Answer { get; set; }

        [Column("quiz_id")]
        [Required]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}