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
    /// Interaction logic for MyFridgeWindow.xaml
    /// </summary>
    public partial class MyFridgeWindow : Window
    {
        public MainMenu MainMenu { get; set; }

        public MyFridgeWindow()
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
            MainMenu.Show();
            this.Hide();
        }
    }
}
