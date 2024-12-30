using System.Collections.ObjectModel;
using System.Windows;

namespace TCSA.WPF.Recipes;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<Recipe> Recipes { get; set; }

    public MainWindow()
    {
        InitializeComponent();

        using (var db = new RecipeDbContext())
        {
            db.Database.EnsureCreated();

            Recipes = new ObservableCollection<Recipe>(db.Recipes.ToList());
        }

        RecipeList.ItemsSource = Recipes;
    }

    private void RecipeList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (RecipeList.SelectedItem is Recipe selectedRecipe)
        {
            RecipeDetails.Text = selectedRecipe.Details;
        }
        else
        {
            RecipeDetails.Text = "Select a recipe to view details.";
        }
    }

    private void AddRecipe_Click(object sender, RoutedEventArgs e)
    {
        var newRecipe = new Recipe { Name = "New Recipe", Details = "Details about the recipe." };

        using (var db = new RecipeDbContext())
        {
            db.Recipes.Add(newRecipe);
            db.SaveChanges();
        }

        Recipes.Add(newRecipe);
    }

    private void EditRecipe_Click(object sender, RoutedEventArgs e)
    {
        if (RecipeList.SelectedItem is Recipe selectedRecipe)
        {
            var newName = Microsoft.VisualBasic.Interaction.InputBox(
                "Edit Recipe Name:", "Edit Recipe", selectedRecipe.Name);
            var newDetails = Microsoft.VisualBasic.Interaction.InputBox(
                "Edit Recipe Details:", "Edit Recipe", selectedRecipe.Details);

            if (!string.IsNullOrWhiteSpace(newName) && !string.IsNullOrWhiteSpace(newDetails))
            {
                selectedRecipe.Name = newName;
                selectedRecipe.Details = newDetails;

                using (var db = new RecipeDbContext())
                {
                    db.Recipes.Update(selectedRecipe);
                    db.SaveChanges();
                }

                RecipeList.Items.Refresh();
            }
        }
        else
        {
            MessageBox.Show("Please select a recipe to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
    {
        if (RecipeList.SelectedItem is Recipe selectedRecipe)
        {
            var result = MessageBox.Show(
                $"Are you sure you want to delete '{selectedRecipe.Name}'?",
                "Delete Recipe", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                using (var db = new RecipeDbContext())
                {
                    db.Recipes.Remove(selectedRecipe);
                    db.SaveChanges();
                }
                Recipes.Remove(selectedRecipe);
            }
        }
        else
        {
            MessageBox.Show("Please select a recipe to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
