using MySql.Data.MySqlClient;
using System.Data;

namespace Cookbook
{
    public class DatabaseConnection
    {
        string server = "localhost";
        string database = "cookbook_database"; 
        string username = "cookbook_user";
        string password = "cookbook_password"; 

        string connectionString = "";

        public DatabaseConnection()
        {
            connectionString =
                "SERVER=" + server + ";" +
                "DATABASE=" + database + ";" +
                "UID=" + username + ";" +
                "PASSWORD=" + password + ";";
        }

        public DatabaseConnection(string server, string database, string username, string password)
        {
            connectionString =
                "SERVER=" + server + ";" +
                "DATABASE=" + database + ";" +
                "UID=" + username + ";" +
                "PASSWORD=" + password + ";";
        }                                                                                     

        //Search for an ingredient in database                                                       
        public List<string> SearchIngredient(string searchQuery)                                    
        {
            List<string> searchResults = new List<string>();

            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("SearchIngredient", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@search_term_ingredient", searchQuery);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string ingredientName = reader["ingredient_name"].ToString();
                                searchResults.Add(ingredientName);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return searchResults;
        }

        //Add an ingredient to database
        public void AddIngredient(string ingredientName)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("AddIngredient", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ingredient_name_param", ingredientName);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            } 
        }

        //Prevent duplicates by checking if the ingredient already exists in database
        public bool IngredientExists(string ingredientName)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("IngredientExists", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ingredient_name_param", ingredientName);

                        object result = command.ExecuteScalar();                                                 
                        int rowCount = Convert.ToInt32(result);

                        return rowCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        //Delete an ingredient from database
        public void DeleteIngredient(string ingredientName)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("DeleteIngredient", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ingredient_name_param", ingredientName);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //Check if an ingredient belongs to a recipe
        public bool IsIngredientInRecipe(string ingredientName)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("CheckIngredientUsage", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ingredient_name_param", ingredientName);

                        object result = command.ExecuteScalar();                                                     
                        int rowCount = Convert.ToInt32(result);

                        return rowCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        //Get recipe categories from database
        public Dictionary<int, string> GetCategories()
        {
            Dictionary<int, string> categories = new Dictionary<int, string>();

            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM all_categories", mySqlConnection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                int categoryIdIndex = reader.GetOrdinal("category_id");
                                int categoryNameIndex = reader.GetOrdinal("category_name");

                                while (reader.Read())
                                {
                                    int categoryId = reader.GetInt32(categoryIdIndex);
                                    string categoryName = reader.GetString(categoryNameIndex);

                                    categories.Add(categoryId, categoryName);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return categories;
        }

        //Get units from database
        public List<Unit> GetUnits()
        {
            List<Unit> units = new List<Unit>();

            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM all_units", mySqlConnection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                int unitIdIndex = reader.GetOrdinal("unit_id");
                                int unitNameIndex = reader.GetOrdinal("unit_name");

                                while (reader.Read())
                                {
                                    int unitId = reader.GetInt32(unitIdIndex);
                                    string unitName = reader.GetString(unitNameIndex);

                                    Unit unit = new Unit(unitId, unitName);
                                    units.Add(unit);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return units;
        }

        //Search for a recipe filtered by category in database
        public List<string> SearchRecipe(string searchQuery, string categoryName)                               
        {
            List<string> searchResults = new List<string>();

            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("SearchRecipe", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@search_term_recipe", searchQuery);
                        command.Parameters.AddWithValue("@category_name_param", categoryName);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string recipeTitle = reader["recipe_title"].ToString();
                                searchResults.Add(recipeTitle);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return searchResults;
        }

        //Get ingredients from database for chosen recipe
        public DataTable GetRecipeIngredients(string recipeTitle)
        {
            DataTable dataTable = new DataTable();                                                

            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    string query = "SELECT * FROM recipe_ingredients_overview WHERE recipe_title = @RecipeTitle";

                    using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                    {
                        command.Parameters.AddWithValue("@RecipeTitle", recipeTitle);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(dataTable);    
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return dataTable;
        }

        //Get instructions from database for chosen recipe
        public DataTable GetRecipeInstructions(string recipeTitle)                                          
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    string query = "SELECT * FROM recipe_instructions_overview WHERE recipe_title = @RecipeTitle";

                    using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                    {
                        command.Parameters.AddWithValue("@RecipeTitle", recipeTitle);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return dataTable;
        }

        //Match chosen category with category id
        public int GetCategoryId(string categoryName)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("GetCategoryId", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@category_name_param", categoryName);

                        var categoryId = command.ExecuteScalar();
                        return Convert.ToInt32(categoryId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }
        }

        //Match chosen ingredient with ingredient id
        public int GetIngredientId(string ingredientName)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("GetIngredientId", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ingredient_name_param", ingredientName);

                        int ingredientId = (int)command.ExecuteScalar();
                        return ingredientId;
                    }
                }
            }  
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }
        }

        //Check if chosen recipe title already exists in database
        public bool RecipeTitleExists(string recipeTitle)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("RecipeTitleExists", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@recipe_title_param", recipeTitle);

                        object result = command.ExecuteScalar();                                                            
                        int rowCount = Convert.ToInt32(result);

                        return rowCount > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        //Insert recipe into database and return the id
        public int InsertRecipe (string recipeTitle, int categoryId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("InsertRecipe", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@recipe_title_param", recipeTitle);
                        command.Parameters.AddWithValue("@category_id_param", categoryId);

                        int result = Convert.ToInt32(command.ExecuteScalar());

                        return result;                                              
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }          
        }

        //Insert the instructions from user recipe
        public void InsertInstruction(int recipeId, string instructionDescription)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("InsertInstruction", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@recipe_id_param", recipeId);
                        command.Parameters.AddWithValue("@instruction_description_param", instructionDescription);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //Insert a new recipe-ingredient association
        public void InsertRecipeIngredient(int recipeId, int ingredientId, double? quantity, string unit)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("InsertRecipeIngredient", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@recipe_id_param", recipeId);
                        command.Parameters.AddWithValue("@ingredient_id_param", ingredientId);
                        command.Parameters.AddWithValue("@quantity_param", quantity);
                        command.Parameters.AddWithValue("@unit_param", unit);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //Delete a recipes ingredients
        public void DeleteRecipeIngredient(int recipeId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand ("DeleteRecipeIngredient", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@recipe_id_param", recipeId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //Delete a recipes instructions
        public void DeleteRecipeInstruction(int recipeId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("DeleteRecipeInstruction", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@recipe_id_param", recipeId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //Match chosen recipe and return recipe id
        public int GetRecipeId(string recipeTitle)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("GetRecipeId", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@recipe_title_param", recipeTitle);

                        int recipeId = Convert.ToInt32(command.ExecuteScalar());
                        return recipeId;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return -1;
            }
        }

        //Update an existing recipe
        public void UpdateRecipe(int recipeId, string recipeTitle, int categoryId)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("UpdateRecipe", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@recipe_id_param", recipeId);
                        command.Parameters.AddWithValue("@recipe_title_param", recipeTitle);
                        command.Parameters.AddWithValue("@category_id_param", categoryId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //Delete recipe from database
        public void DeleteRecipe(string recipeTitle)
        {
            try
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(connectionString))
                {
                    mySqlConnection.Open();

                    using (MySqlCommand command = new MySqlCommand("DeleteRecipe", mySqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@recipe_title_param", recipeTitle);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
