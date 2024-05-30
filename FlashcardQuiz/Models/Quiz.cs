using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlashcardQuiz.Models
{
    [Table("quizes")]
    public class Quiz
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("title")]
        [Required]
        public required string Title { get; set; }

        public ICollection<Card>? Cards { get; set; }
    }
}