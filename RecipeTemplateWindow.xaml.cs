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
    /// Interaction logic for RecipeTemplateWindow.xaml
    /// </summary>
    public partial class RecipeTemplateWindow : Window
    {
        public Recipe CurrentRecipe { get; set; }

        DatabaseConnection databaseConnection = new DatabaseConnection();
    
        public RecipeTemplateWindow()
        {
            InitializeComponent();
            CurrentRecipe = new Recipe();
            DataContext = this;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Fill the template window with information from database about the chosen recipe
        public void SetRecipe(string recipeTitle)
        {
            DataTable ingredientsDetails = databaseConnection.GetRecipeIngredients(recipeTitle);

            DataTable instructionsDetails = databaseConnection.GetRecipeInstructions(recipeTitle);

            if (ingredientsDetails.Rows.Count > 0 && instructionsDetails.Rows.Count > 0)
            {
                DataRow recipeRow = ingredientsDetails.Rows[0];

                CurrentRecipe.RecipeTitle = recipeRow["recipe_title"].ToString();

                foreach (DataRow row in ingredientsDetails.Rows)
                {
                    string ingredientName = row["ingredient_name"].ToString();
                    double? quantity = row["quantity"] == DBNull.Value ? null : (double?)Convert.ToDouble(row["quantity"]);
                    string unitName = row["unit_name"].ToString();

                    if (unitName == "no unit")
                    {
                        unitName = null;
                    }

                    var ingredient = new Ingredient { Name = ingredientName, Quantity = quantity, Unit = unitName };

                    CurrentRecipe.Ingredients.Add(ingredient);
                }

                foreach (DataRow row in instructionsDetails.Rows)
                {
                    string instructionDescription = row["instruction_description"].ToString();
                    var instruction = new Instruction { InstructionDescription = instructionDescription };
                    CurrentRecipe.Instructions.Add(instruction);
                }
            }
            else
            {
                MessageBox.Show("Recipe not found.");
            }
        }
    }
}
