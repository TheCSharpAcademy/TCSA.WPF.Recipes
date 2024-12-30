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

        Recipes = new ObservableCollection<Recipe>
        {
            new Recipe { Name = "Spaghetti Bolognese", Details = "Ingredients: Spaghetti, Ground Beef, Tomato Sauce.\nSteps: Cook pasta, make sauce, mix together." },
            new Recipe { Name = "Pancakes", Details = "Ingredients: Flour, Eggs, Milk.\nSteps: Mix ingredients, cook on a skillet." }
        };

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
        Recipes.Add(new Recipe { Name = "New Recipe", Details = "Details about the recipe." });
    }

    private void EditRecipe_Click(object sender, RoutedEventArgs e)
    {
        if (RecipeList.SelectedItem is Recipe selectedRecipe)
        {
            // Prompt user for new details
            var newName = Microsoft.VisualBasic.Interaction.InputBox(
                "Edit Recipe Name:", "Edit Recipe", selectedRecipe.Name);
            var newDetails = Microsoft.VisualBasic.Interaction.InputBox(
                "Edit Recipe Details:", "Edit Recipe", selectedRecipe.Details);

            if (!string.IsNullOrWhiteSpace(newName))
            {
                selectedRecipe.Name = newName;
            }
            if (!string.IsNullOrWhiteSpace(newDetails))
            {
                selectedRecipe.Details = newDetails;
            }

            // Refresh ListView to reflect changes
            RecipeList.Items.Refresh();
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
                Recipes.Remove(selectedRecipe);
            }
        }
        else
        {
            MessageBox.Show("Please select a recipe to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}

public class Recipe
{
    public string Name { get; set; }
    public string Details { get; set; }
}