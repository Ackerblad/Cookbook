using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cookbook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        IngredientsWindow ingredientsWindow = new IngredientsWindow();
        RecipesWindow recipesWindow = new RecipesWindow();
        
        public MainMenu()
        {
            InitializeComponent();
            this.Closed += Window_Closed;
        }

        private void Window_Closed(Object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void IngredientsButton_Click(object sender, RoutedEventArgs e)
        {
            ingredientsWindow.MainMenu = this;
            ingredientsWindow.Show();
            this.Hide();
        }

        private void RecipesButton_Click(object sender, RoutedEventArgs e)
        {
            recipesWindow.MainMenu = this;
            recipesWindow.Show();
            this.Hide();
        }
    }
}