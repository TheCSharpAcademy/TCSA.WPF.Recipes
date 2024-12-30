using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TCSA.WPF.Recipes;

public class RecipeDbContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=recipes.db");
    }
}

public class Recipe
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
}