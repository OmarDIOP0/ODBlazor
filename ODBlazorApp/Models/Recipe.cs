using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODBlazorApp.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; } = "Recipe";

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = "/images/default-recipe.jpg";

        [Range(0, 600, ErrorMessage = "Le temps doit être entre 0 et 600 minutes")]
        public int PreparationTime { get; set; } = 0;

        [Range(0, 600, ErrorMessage = "Le temps doit être entre 0 et 600 minutes")]
        public int CookingTime { get; set; } = 0;

        public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.Facile;

        [MaxLength(50)]
        public string Category { get; set; } = "Autre";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Range(1, 100, ErrorMessage = "Le nombre de personnes doit être entre 1 et 100")]
        public int Servings { get; set; } = 4;

        // Navigation properties - RETIRÉ [Required] des collections
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<RecipeStep> Steps { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
    }

    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        // RETIRÉ [Required] pour permettre des ingrédients vides
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 10000)]
        public decimal Quantity { get; set; }

        [MaxLength(20)]
        public string Unit { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Notes { get; set; } = string.Empty;

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }

    public class RecipeStep
    {
        [Key]
        public int Id { get; set; }

        // RETIRÉ [Required] de StepNumber
        public int StepNumber { get; set; }

        // RETIRÉ [Required] pour permettre des étapes vides
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }

    public class Tag
    {
        [Key]
        public int Id { get; set; }

        // RETIRÉ [Required] pour permettre des tags vides
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }

    public enum DifficultyLevel
    {
        Facile,
        Moyen,
        Difficile
    }
}