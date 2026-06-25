public class Trainer
{
    public int ExperienceYears { get; set; }
    public string Name { get; set; }

    public Trainer() { }

    public Trainer(string name, int experienceYears)
    {
        Name = name;
        ExperienceYears = experienceYears;
    }

    public string OnDolphinJump(string name, double jumpHeight)
    {
        return $"{Name}, the trainer, says: Wow! {name} jumped {jumpHeight} meters high!";
    }

    public override string ToString()
    {
        return $"Trainer {Name} has {ExperienceYears} years of experience";
    }

    public string TrainAnimal()
    {
        return $"{Name} (with {ExperienceYears} years of experience) is training the animal.";
    }
}
