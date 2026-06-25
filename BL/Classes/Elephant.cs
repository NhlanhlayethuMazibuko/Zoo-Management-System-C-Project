public class Elephant : Animal
{
    public double TrunkLength { get; set; }

    public Elephant() { }

    public Elephant(string id, string name, int age) : base(id, name, age) { }

    public override string Eat()
    {
        return "The elephant eats leaves and fruits.";
    }

    public override string MakeSound()
    {
        return "Trumpet!";
    }

    public string SprayWater()
    {
        return "The elephant sprays water using its trunk.";
    }
}
