namespace Cookbook
{
    public class Recipe
    {
        public int Id { get; set; }
        public string RecipeTitle { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Instruction> Instructions { get; set; }

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Instructions = new List<Instruction>();
        }
    }
}
