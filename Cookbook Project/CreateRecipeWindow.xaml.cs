using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Cookbook
{
    /// <summary>
    /// Interaction logic for CreateRecipeWindow.xaml
    /// </summary>
    public partial class CreateRecipeWindow : Window
    {
        Recipe currentRecipe = new Recipe();
        public bool EditMode { get; private set; }

        DatabaseConnection databaseConnection = new DatabaseConnection();

        public CreateRecipeWindow()
        {
            InitializeComponent();
            GetCategories();
            GetUnits();
        }

        //Constructor that runs when the user chooses to edit an existing recipe
        public CreateRecipeWindow(string selectedRecipe = null) : this()
        {
            if (!string.IsNullOrEmpty(selectedRecipe))
            {
                EditMode = true;
                ImportRecipe(selectedRecipe);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Import chosen recipe for editing
        private void ImportRecipe(string selectedRecipe)
        {
            DataTable ingredientsDetails = databaseConnection.GetRecipeIngredients(selectedRecipe);
            DataTable instructionsDetails = databaseConnection.GetRecipeInstructions(selectedRecipe);

            if (ingredientsDetails.Rows.Count > 0 && instructionsDetails.Rows.Count > 0)
            {
                DataRow recipeRow = ingredientsDetails.Rows[0];

                addRecipeTitle.Text = recipeRow["recipe_title"].ToString();
                addRecipeTitle.IsReadOnly = true;

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
                    currentRecipe.Ingredients.Add(ingredient);
                }

                foreach (DataRow row in instructionsDetails.Rows)
                {
                    string instructionDescription = row["instruction_description"].ToString();
                    var instruction = new Instruction { InstructionDescription = instructionDescription };
                    currentRecipe.Instructions.Add(instruction);
                }

                addIngredientsListBox.ItemsSource = currentRecipe.Ingredients;
                addInstructionsListBox.ItemsSource = currentRecipe.Instructions;
            }
            else
            {
                MessageBox.Show("Recipe not found.");
            }
        }

        //Display all recipe categories from database
        private void GetCategories()
        {
            Dictionary<int, string> categories = databaseConnection.GetCategories();

            categoryBox.ItemsSource = categories.Values.ToList();
        }

        //Display all units from database
        private void GetUnits()
        {
            List<Unit> units = databaseConnection.GetUnits();

            unitBox.ItemsSource = units;
        }

        private void addRecipeTitle_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text == "Add a title")
            {
                textBox.Text = "";
                textBox.FontStyle = FontStyles.Normal;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void addRecipeTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(addRecipeTitle.Text))
            {
                addRecipeTitle.Text = "Add a title";
                addRecipeTitle.FontStyle = FontStyles.Italic;
                addRecipeTitle.Foreground = Brushes.LightSlateGray;
            }
        }

        private void quantityBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (quantityBox.Text == "Enter quantity")
            {
                quantityBox.Text = "";
                quantityBox.FontStyle = FontStyles.Normal;
                quantityBox.Foreground = Brushes.Black;
            }
        }

        private void quantityBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(quantityBox.Text))
            {
                quantityBox.Text = "Enter quantity";
                quantityBox.FontStyle = FontStyles.Italic;
                quantityBox.Foreground = Brushes.LightSlateGray;
            }
        }

        private void searchIngredientBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchIngredientBox.Text == "Search for ingredient")
            {
                searchIngredientBox.Text = "";
                searchIngredientBox.HorizontalContentAlignment = HorizontalAlignment.Left;
                searchIngredientBox.FontStyle = FontStyles.Normal;
                searchIngredientBox.Foreground = Brushes.Black;
            }
        }

        private void searchIngredientBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchIngredientBox.Text))
            {
                searchIngredientBox.Text = "Search for ingredient";
                searchIngredientBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                searchIngredientBox.FontStyle = FontStyles.Italic;
                searchIngredientBox.Foreground = Brushes.LightSlateGray;
            }
        }

        //Display existing ingredients from database when user search for them 
        private void searchIngredientBox_KeyUp(object sender, KeyEventArgs e)
        {
            string searchQuery = searchIngredientBox.Text;

            List<string> searchResults = databaseConnection.SearchIngredient(searchQuery);

            resultListBox.ItemsSource = searchResults;
        }

        private void resultListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (resultListBox.SelectedItem != null)
            {
                string selectedIngredient = resultListBox.SelectedItem.ToString();

                if (databaseConnection.IngredientExists(selectedIngredient))
                {
                    searchIngredientBox.Text = selectedIngredient;
                }
                else
                {
                    MessageBox.Show("Please select a valid ingredient from the list.");
                }
            }
        }

        //Add the chosen ingredient complete with quantity and unit to the ingredient list
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(quantityBox.Text, out double quantity) && !string.IsNullOrEmpty(unitBox.Text) && 
                !string.IsNullOrWhiteSpace(searchIngredientBox.Text) && searchIngredientBox.Text != "Search for ingredient")
            {
                if (databaseConnection.IngredientExists(searchIngredientBox.Text))
                {
                    Ingredient ingredient = new Ingredient
                    {
                        Name = searchIngredientBox.Text,
                        Quantity = quantity,
                        Unit = unitBox.Text
                    };

                    currentRecipe.Ingredients.Add(ingredient);

                    addIngredientsListBox.ItemsSource = null;
                    addIngredientsListBox.ItemsSource = currentRecipe.Ingredients;

                    RestoreDesign();
                }
                else
                {
                    MessageBox.Show("Please enter a valid ingredient.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity, select a unit, and specify an ingredient.");
            }
        }

        //Remove the chosen ingredient from ingredient list
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (addIngredientsListBox.SelectedItem != null)
            {
                Ingredient selectedIngredient = (Ingredient)addIngredientsListBox.SelectedItem;
                currentRecipe.Ingredients.Remove(selectedIngredient);

                addIngredientsListBox.ItemsSource = null;
                addIngredientsListBox.ItemsSource = currentRecipe.Ingredients;
            }
            else
            {
                MessageBox.Show("Select the ingredient you want to remove, then press the remove button.");
            }
        }

        //Restore the default design after an ingredient has been added
        private void RestoreDesign()
        {
            quantityBox.Text = "Enter quantity";
            quantityBox.FontStyle = FontStyles.Italic;
            quantityBox.Foreground = Brushes.LightSlateGray;

            searchIngredientBox.Text = "Search for ingredient";
            searchIngredientBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            searchIngredientBox.FontStyle = FontStyles.Italic;
            searchIngredientBox.Foreground = Brushes.LightSlateGray;

            unitBox.SelectedIndex = -1;
            resultListBox.ItemsSource = null;
        }

        private void instructionInputBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (instructionInputBox.Text == "Add instructions to your recipe, step by step")
            {  
                instructionInputBox.Text = "";
                instructionInputBox.HorizontalContentAlignment = HorizontalAlignment.Left;
                instructionInputBox.FontStyle = FontStyles.Normal;
                instructionInputBox.Foreground = Brushes.Black;
            }
        }

        private void instructionInputBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(instructionInputBox.Text))
            {
                instructionInputBox.Text = "Add instructions to your recipe, step by step";
                instructionInputBox.HorizontalContentAlignment = HorizontalAlignment.Center;
                instructionInputBox.FontStyle = FontStyles.Italic;
                instructionInputBox.Foreground = Brushes.LightSlateGray;
            }
        }

        //Add the entered instruction step to instruction list 
        private void AddInstructionButton_Click(object sender, RoutedEventArgs e)
        {
            string instructionInput = instructionInputBox.Text.Trim();

            if (!string.IsNullOrEmpty(instructionInput) && instructionInput != "Add instructions to your recipe, step by step")
            {
                Instruction instruction = new Instruction { InstructionDescription = instructionInput };
                currentRecipe.Instructions.Add(instruction);

                addInstructionsListBox.ItemsSource = null;
                addInstructionsListBox.ItemsSource = currentRecipe.Instructions;

                instructionInputBox.Clear();
            }
        }

        //Remove the chosen step from instruction list
        private void RemoveInstructionButton_Click(object sender, RoutedEventArgs e)
        {
            if (addInstructionsListBox.SelectedItem != null)
            {
                Instruction selectedInstruction = (Instruction)addInstructionsListBox.SelectedItem;
                currentRecipe.Instructions.Remove(selectedInstruction);

                addInstructionsListBox.ItemsSource = null;
                addInstructionsListBox.ItemsSource = currentRecipe.Instructions;
            }
            else
            {
                MessageBox.Show("Select the instruction step you want to remove, then press the remove button.");
            }
        }

        //Update the existing recipe if in Edit Mode
        //Save the new recipe if in Create Mode
        private void SaveRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(addRecipeTitle.Text) && addRecipeTitle.Text != "Add a title" && addIngredientsListBox.HasItems && addInstructionsListBox.HasItems && categoryBox.SelectedItem != null)
            {
                string recipeTitle = addRecipeTitle.Text;
                string categoryName = categoryBox.SelectedItem.ToString();
                int categoryId = databaseConnection.GetCategoryId(categoryName);

                if (EditMode)
                {
                    int recipeId = databaseConnection.GetRecipeId(recipeTitle);

                    if (recipeId > 0)
                    {
                        databaseConnection.UpdateRecipe(recipeId, recipeTitle, categoryId);
                        databaseConnection.DeleteRecipeInstruction(recipeId);
                        databaseConnection.DeleteRecipeIngredient(recipeId);

                        foreach (Instruction instruction in currentRecipe.Instructions)
                        {
                            databaseConnection.InsertInstruction(recipeId, instruction.InstructionDescription);
                        }

                        foreach (Ingredient ingredient in currentRecipe.Ingredients)
                        {
                            int ingredientId = databaseConnection.GetIngredientId(ingredient.Name);
                            databaseConnection.InsertRecipeIngredient(recipeId, ingredientId, ingredient.Quantity, ingredient.Unit);
                        }

                        MessageBox.Show("Recipe updated successfully.");

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update recipe.");
                    }
                }
                else
                {
                    if (databaseConnection.RecipeTitleExists(recipeTitle))
                    {
                        MessageBox.Show($"Recipe with title '{recipeTitle}' already exists. Please choose a different title.");
                        return;
                    }

                    int recipeId = databaseConnection.InsertRecipe(recipeTitle, categoryId);

                    if (recipeId > 0)
                    {
                        foreach (Instruction instruction in currentRecipe.Instructions)
                        {
                            databaseConnection.InsertInstruction(recipeId, instruction.InstructionDescription);
                        }

                        foreach (Ingredient ingredient in currentRecipe.Ingredients)
                        {
                            int ingredientId = databaseConnection.GetIngredientId(ingredient.Name);

                            databaseConnection.InsertRecipeIngredient(recipeId, ingredientId, ingredient.Quantity, ingredient.Unit);
                        }

                        MessageBox.Show("Recipe saved successfully.");

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to save recipe.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill in all the details about your recipe: title, ingredients, instructions and category");
            }
        }
    }
}
