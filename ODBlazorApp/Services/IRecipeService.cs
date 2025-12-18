using ODBlazorApp.Models;
using System.Collections.Concurrent;

namespace ODBlazorApp.Services
{
    public interface IRecipeService
    {
        List<Recipe> GetAllRecipes();
        Recipe? GetRecipeById(int id);
        void AddRecipe(Recipe recipe);
        void UpdateRecipe(Recipe recipe);
        void DeleteRecipe(int id);
        List<string> GetCategories();
        List<Recipe> SearchRecipes(string query, string? category = null);
    }
    public class RecipeService : IRecipeService
    {
        private readonly ConcurrentDictionary<int, Recipe> _recipes = new();
        private int _nextId = 1;

        public RecipeService()
        {
            // Données d'exemple
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            var sampleRecipe = new Recipe
            {
                Id = _nextId++,
                Title = "Pâtes Carbonara",
                Description = "Une recette classique italienne",
                PreparationTime = 15,
                CookingTime = 10,
                Difficulty = DifficultyLevel.Facile,
                Category = "Italien",
                Servings = 4,
                Ingredients = new List<Ingredient>
            {
                new() { Name = "Spaghetti", Quantity = 400, Unit = "g" },
                new() { Name = "Lardons", Quantity = 200, Unit = "g" },
                new() { Name = "Œufs", Quantity = 4, Unit = "unités" },
                new() { Name = "Parmesan", Quantity = 100, Unit = "g" }
            },
                Steps = new List<string>
            {
                "Faire cuire les pâtes al dente",
                "Faire revenir les lardons",
                "Battre les œufs avec le parmesan",
                "Mélanger le tout hors du feu"
            },
                Tags = new List<string> { "rapide", "italien", "dîner" }
            };

            _recipes[sampleRecipe.Id] = sampleRecipe;
        }

        public List<Recipe> GetAllRecipes() => _recipes.Values.OrderBy(r => r.Title).ToList();

        public Recipe? GetRecipeById(int id) => _recipes.GetValueOrDefault(id);

        public void AddRecipe(Recipe recipe)
        {
            recipe.Id = _nextId++;
            recipe.CreatedDate = DateTime.Now;
            _recipes[recipe.Id] = recipe;
        }

        public void UpdateRecipe(Recipe recipe)
        {
            if (_recipes.ContainsKey(recipe.Id))
                _recipes[recipe.Id] = recipe;
        }

        public void DeleteRecipe(int id) => _recipes.TryRemove(id, out _);

        public List<string> GetCategories() =>
            _recipes.Values.Select(r => r.Category).Distinct().OrderBy(c => c).ToList();

        public List<Recipe> SearchRecipes(string query, string? category = null)
        {
            var results = _recipes.Values.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                results = results.Where(r =>
                    r.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    r.Description.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    r.Tags.Any(t => t.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
                    r.Ingredients.Any(i => i.Name.Contains(query, StringComparison.OrdinalIgnoreCase)));
            }

            if (!string.IsNullOrWhiteSpace(category))
                results = results.Where(r => r.Category == category);

            return results.ToList();
        }
    }
    }
