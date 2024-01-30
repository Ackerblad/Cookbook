using System;
using System.Collections.Generic;
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
    /// Interaction logic for IngredientsWindow.xaml
    /// </summary>
    public partial class IngredientsWindow : Window
    {
        public MainMenu MainMenu { get; set; }

        DatabaseConnection databaseConnection = new DatabaseConnection();

        public IngredientsWindow()
        {
            InitializeComponent();
            this.Closed += Window_Closed;
        }

        private void Window_Closed(Object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            resultListBox.ItemsSource = null;
            searchBox.Text = "";
            MainMenu.Show();
            this.Hide();
        }

        private void searchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchBox.Text == "Search for ingredient")
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
                searchBox.Text = "Search for ingredient";
                searchBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                searchBox.FontStyle = FontStyles.Italic;
                searchBox.Foreground = Brushes.LightSlateGray;
            }
        }

        //Search for matching ingredients in database
        private void searchBox_KeyUp(object sender, KeyEventArgs e)
        {
        
            string searchQuery = searchBox.Text;

            List<string> searchResults = databaseConnection.SearchIngredient(searchQuery);

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

        private void resultListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string selectedingredient = resultListBox.SelectedItem.ToString();
            searchBox.Text = selectedingredient;
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            AddIngredientWindow addIngredientWindow = new AddIngredientWindow();
            addIngredientWindow.Show();
        }

        //Delete ingredient from database
        private void DeleteIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (resultListBox.SelectedIndex != -1)
            {
                string ingredientName = resultListBox.SelectedItem.ToString();

                if (databaseConnection.IsIngredientInRecipe(ingredientName))
                {
                    MessageBox.Show($"This ingredient belongs to a recipe and cannot be deleted.");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show($"Do you want to delete {resultListBox.SelectedItem}?", "Confirmation", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        databaseConnection.DeleteIngredient(ingredientName);
                        MessageBox.Show($"{ingredientName} was successfully deleted from your cookbook!");

                        resultListBox.ItemsSource = null;
                        searchBox.Text = "";
                    }
                }       
            }
            else
            {
                MessageBox.Show("Please select an ingredient to delete.", "Information");
            }
        }
    }
}
