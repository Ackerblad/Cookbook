using System.Windows;
using System.Windows.Media;

namespace Cookbook
{
    /// <summary>
    /// Interaction logic for AddIngredientWindow.xaml
    /// </summary>
    public partial class AddIngredientWindow : Window
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();

        public AddIngredientWindow()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ingredientNameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ingredientNameBox.Text == "Enter the name of the ingredient")
            {  
                ingredientNameBox.Text = "";
                ingredientNameBox.HorizontalContentAlignment = HorizontalAlignment.Left;
                ingredientNameBox.FontStyle = FontStyles.Normal;
                ingredientNameBox.Foreground = Brushes.Black;
            }   
        }

        private void ingredientNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ingredientNameBox.Text))
            {
                ingredientNameBox.Text = "Enter the name of the ingredient";
                ingredientNameBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                ingredientNameBox.FontStyle = FontStyles.Italic;
                ingredientNameBox.Foreground = Brushes.LightSlateGray;
            }
        }

        //Save the ingredient to database
        private void SaveIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ingredientNameBox.Text) && ingredientNameBox.Text != "Enter the name of the ingredient")
            {
                string ingredientName = ingredientNameBox.Text;

                if (!databaseConnection.IngredientExists(ingredientName))
                {
                    databaseConnection.AddIngredient(ingredientName);
                    MessageBox.Show($"{ingredientName} was successfully added to your cookbook!");
                    ingredientNameBox.Text = "";
                }
                else
                {
                    MessageBox.Show($"{ingredientName} already exists in your cookbook.");
                }
            }
            else
            {
                MessageBox.Show("You need to enter the name of the ingredient.");
            }
        }
    }
}
