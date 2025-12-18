namespace ODBlazorApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "/images/default-recipe.jpg";
        public int PreparationTime { get; set; } // en minutes
        public int CookingTime { get; set; } // en minutes
        public DifficultyLevel Difficulty { get; set; }
        public string Category { get; set; } = "Autre";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<string> Steps { get; set; } = new();
        public int Servings { get; set; } = 4;
        public List<string> Tags { get; set; } = new();
    }
    public class Ingredient
    {
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }

    public enum DifficultyLevel
    {
        Facile,
        Moyen,
        Difficile
    }
}
