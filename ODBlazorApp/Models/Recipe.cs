using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ODBlazorApp.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est requis")]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = "/images/default-recipe.jpg";

        [Required(ErrorMessage = "Le temps de préparation est requis")]
        [Range(1, 600, ErrorMessage = "Le temps doit être entre 1 et 600 minutes")]
        public int PreparationTime { get; set; }

        [Range(0, 600, ErrorMessage = "Le temps doit être entre 0 et 600 minutes")]
        public int CookingTime { get; set; }

        public DifficultyLevel Difficulty { get; set; }

        [Required(ErrorMessage = "La catégorie est requise")]
        [MaxLength(50)]
        public string Category { get; set; } = "Autre";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int Servings { get; set; } = 4;

        // Navigation properties
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<RecipeStep> Steps { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
    }

    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
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

        [Required]
        public int StepNumber { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }

    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
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