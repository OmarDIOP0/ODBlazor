using Microsoft.EntityFrameworkCore;
using ODBlazorApp.Models;
namespace ODBlazorApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeStep> Steps { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des relations
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithOne(i => i.Recipe)
                .HasForeignKey(i => i.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Steps)
                .WithOne(s => s.Recipe)
                .HasForeignKey(s => s.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Tags)
                .WithOne(t => t.Recipe)
                .HasForeignKey(t => t.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ajout de données d'exemple
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    Title = "Pâtes Carbonara",
                    Description = "Une recette classique italienne",
                    PreparationTime = 15,
                    CookingTime = 10,
                    Difficulty = DifficultyLevel.Facile,
                    Category = "Italien",
                    Servings = 4,
                    CreatedDate = DateTime.Now
                }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1, RecipeId = 1, Name = "Spaghetti", Quantity = 400, Unit = "g" },
                new Ingredient { Id = 2, RecipeId = 1, Name = "Lardons", Quantity = 200, Unit = "g" },
                new Ingredient { Id = 3, RecipeId = 1, Name = "Œufs", Quantity = 4, Unit = "unités" },
                new Ingredient { Id = 4, RecipeId = 1, Name = "Parmesan", Quantity = 100, Unit = "g" }
            );

            modelBuilder.Entity<RecipeStep>().HasData(
                new RecipeStep { Id = 1, RecipeId = 1, StepNumber = 1, Description = "Faire cuire les pâtes al dente" },
                new RecipeStep { Id = 2, RecipeId = 1, StepNumber = 2, Description = "Faire revenir les lardons" },
                new RecipeStep { Id = 3, RecipeId = 1, StepNumber = 3, Description = "Battre les œufs avec le parmesan" },
                new RecipeStep { Id = 4, RecipeId = 1, StepNumber = 4, Description = "Mélanger le tout hors du feu" }
            );

            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, RecipeId = 1, Name = "rapide" },
                new Tag { Id = 2, RecipeId = 1, Name = "italien" },
                new Tag { Id = 3, RecipeId = 1, Name = "dîner" }
            );
        }
    }
}