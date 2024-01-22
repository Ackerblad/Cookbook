using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook
{
    public class DatabaseConnection
    {
        string server = "localhost";
        string database = "cookbook_database"; 
        string username = "root";
        string password = ""; 

        string connectionString = "";

        public DatabaseConnection(string server, string database, string username, string password)
        {
            connectionString =
                "SERVER=" + server + ";" +
                "DATABASE=" + database + ";" +
                "UID=" + username + ";" +
                "PASSWORD=" + password + ";";
            Console.WriteLine($"ConnectionString {connectionString}");
        }

        public DatabaseConnection()
        {
            connectionString =
                "SERVER=" + server + ";" +
                "DATABASE=" + database + ";" +
                "UID=" + username + ";" +
                "PASSWORD=" + password + ";";
            Console.WriteLine($"ConnectionString {connectionString}");
        }

        //Use stored procedure to search for an ingredient in database
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

        //Use stored procedure to add an ingredient to database
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

        //Use stored procedure to prevent duplicates by checking if the ingredient already exists in database
        public bool IngredientExists(string ingredientName)
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

        //Use stored procedure to delete an ingredient from database
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
    }
}
