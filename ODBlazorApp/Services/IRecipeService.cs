using Microsoft.EntityFrameworkCore;
using ODBlazorApp.Data;
using ODBlazorApp.Models;

namespace ODBlazorApp.Services
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetAllRecipesAsync();
        Task<Recipe?> GetRecipeByIdAsync(int id);
        Task AddRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(int id);
        Task<List<string>> GetCategoriesAsync();
        Task<List<Recipe>> SearchRecipesAsync(string? query = null, string? category = null, DifficultyLevel? difficulty = null);
    }

    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Include(r => r.Tags)
                .OrderBy(r => r.Title)
                .ToListAsync();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Include(r => r.Tags)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            recipe.CreatedDate = DateTime.Now;

            // Réorganiser les étapes
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                recipe.Steps[i].StepNumber = i + 1;
            }

            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            // Supprimer les anciennes données liées
            var existingRecipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Include(r => r.Tags)
                .FirstOrDefaultAsync(r => r.Id == recipe.Id);

            if (existingRecipe != null)
            {
                // Mettre à jour les propriétés
                _context.Entry(existingRecipe).CurrentValues.SetValues(recipe);

                // Mettre à jour les ingrédients
                existingRecipe.Ingredients.Clear();
                foreach (var ingredient in recipe.Ingredients)
                {
                    existingRecipe.Ingredients.Add(ingredient);
                }

                // Mettre à jour les étapes
                existingRecipe.Steps.Clear();
                for (int i = 0; i < recipe.Steps.Count; i++)
                {
                    recipe.Steps[i].StepNumber = i + 1;
                    existingRecipe.Steps.Add(recipe.Steps[i]);
                }

                // Mettre à jour les tags
                existingRecipe.Tags.Clear();
                foreach (var tag in recipe.Tags)
                {
                    existingRecipe.Tags.Add(tag);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            return await _context.Recipes
                .Select(r => r.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
        }

        public async Task<List<Recipe>> SearchRecipesAsync(string? query = null, string? category = null, DifficultyLevel? difficulty = null)
        {
            var recipes = _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Tags)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                recipes = recipes.Where(r =>
                    r.Title.Contains(query) ||
                    r.Description.Contains(query) ||
                    r.Ingredients.Any(i => i.Name.Contains(query)) ||
                    r.Tags.Any(t => t.Name.Contains(query)));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                recipes = recipes.Where(r => r.Category == category);
            }

            if (difficulty.HasValue)
            {
                recipes = recipes.Where(r => r.Difficulty == difficulty.Value);
            }

            return await recipes.ToListAsync();
        }
    }
}