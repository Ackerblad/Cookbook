namespace Cookbook
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Unit(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

