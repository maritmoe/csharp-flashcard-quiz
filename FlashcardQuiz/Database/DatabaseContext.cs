using Microsoft.EntityFrameworkCore;
using FlashcardQuiz.Models;

namespace FlashcardQuiz.Database
{
    public class DatabaseContext : DbContext
    {
        private string _connectionString;
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            // loading the DefaultConnectionString value from the appsettings.json
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // use PostgreSQL db
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}