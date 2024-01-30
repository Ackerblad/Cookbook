using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cookbook
{
    /// <summary>
    /// Interaction logic for RecipesWindow.xaml
    /// </summary>
    public partial class RecipesWindow : Window
    {
        public MainMenu MainMenu { get; set; }

        DatabaseConnection databaseConnection = new DatabaseConnection();
        
        public RecipesWindow()
        {
            InitializeComponent();
            this.Closed += Window_Closed;

            GetCategories();
        }

        private void Window_Closed(Object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            resultListBox.ItemsSource = null;
            searchBox.Text = "";
            categoryBox.SelectedIndex = -1;
            MainMenu.Show();
            this.Hide();
        }

        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchBox.Text == "Search for recipe")
            {
                searchBox.Text = "";
                searchBox.HorizontalContentAlignment = HorizontalAlignment.Left;
                searchBox.FontStyle = FontStyles.Normal;
                searchBox.Foreground = Brushes.Black;
            }
        }

        private void searchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBox.Text))
            {
                searchBox.Text = "Search for recipe";
                searchBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                searchBox.FontStyle = FontStyles.Italic;
                searchBox.Foreground = Brushes.LightSlateGray;
            }
        }

        //Display all recipe categories from database
        private void GetCategories()
        {
            List<string> categories = databaseConnection.GetCategories();

            categoryBox.ItemsSource = categories;
        }

        //Search for matching recipes in database
        private void searchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (categoryBox.SelectedItem != null)
            {
                string selectedCategory = categoryBox.SelectedItem.ToString();
                string searchQuery = searchBox.Text;

                List<string> searchResults = databaseConnection.SearchRecipe(searchQuery, selectedCategory);

                resultListBox.ItemsSource = searchResults;

                if (!string.IsNullOrWhiteSpace(searchBox.Text))
                {
                    resultListBox.Visibility = Visibility.Visible;
                }
                else
                {
                    resultListBox.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("Please choose a category.");
                searchBox.Text = "";
            }
        }

        private void resultListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selectedRecipe = resultListBox.SelectedItem.ToString();
            searchBox.Text = selectedRecipe;
        }

        //Send the selected recipe title to the template window
        private void OpenRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (resultListBox.SelectedItem != null)
            {
                string recipeTitle = resultListBox.SelectedItem.ToString();

                RecipeTemplateWindow recipeTemplateWindow = new RecipeTemplateWindow();
                recipeTemplateWindow.SetRecipe(recipeTitle);

                recipeTemplateWindow.Show();
            }
            else
            {
                MessageBox.Show("Please choose a recipe from the list.");
            }
        }

        private void CreateRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            CreateRecipeWindow createRecipeWindow = new CreateRecipeWindow();
            createRecipeWindow.Show();
        }

        //Send the selected recipe to be edited
        private void EditRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (resultListBox.SelectedItem != null)
            {
                string selectedRecipe = resultListBox.SelectedItem.ToString();

                CreateRecipeWindow createRecipeWindow = new CreateRecipeWindow(selectedRecipe);
                createRecipeWindow.Show();
            }
            else
            {
                MessageBox.Show("Please choose a recipe from the list.");
            }
        }

        //Delete the selected recipe
        private void DeleteRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (resultListBox.SelectedIndex != -1)
            {
                string recipeTitle = resultListBox.SelectedItem.ToString();

                MessageBoxResult result = MessageBox.Show($"Do you want to delete the recipe '{recipeTitle}'?", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    databaseConnection.DeleteRecipe(recipeTitle);

                    MessageBox.Show($"Recipe '{recipeTitle}' was successfully deleted.");
                    resultListBox.ItemsSource = null;
                    searchBox.Text = "";
                    categoryBox.SelectedIndex = -1;
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe to delete.", "Information");
            }
        }
    }
}
