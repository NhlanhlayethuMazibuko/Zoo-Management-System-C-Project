
using System;
using System.Linq;
using PA1.BL.Interfaces;

namespace PA1.BL.Animals
{
    public class Parrot : Animal, ICanPerformTricks, ICloneable
    {
        private string featherColor;
        private readonly string[] validColors = new[]
        {
            "Bright Yellow", "Black & White", "Green",
            "Blue", "Yellow", "Red", "Orange"
        };

        public string FeatherColor
        {
            get => featherColor;
            set
            {
                if (!validColors.Contains(value))
                {
                    throw new InvalidFeatherColorException(
                        $"'{value}' is not a valid feather color. Valid colors: {string.Join(", ", validColors)}");
                }
                featherColor = value;
            }
        }

        public Trainer Trainer { get; }

        public Parrot() : base()
        {
            Trainer = new Trainer();
        }

        public Parrot(string id, string name, int age) : base(id, name, age)
        {
            Trainer = new Trainer();
        }

        public object Clone()
        {
            Parrot cloneParrot = new Parrot
            {
                Age = this.Age,
                FeatherColor = this.FeatherColor,
                Name = this.Name,
                ID = this.ID
            };
            cloneParrot.Trainer.Name = this.Trainer.Name;
            cloneParrot.Trainer.ExperienceYears = this.Trainer.ExperienceYears;
            return cloneParrot;
        }

        public override string Eat()
        {
            return "The parrot eats seeds and fruits.";
        }

        public override string MakeSound()
        {
            return "Squawk! Hello!";
        }

        public string MimicSound(string sound)
        {
            return $"The parrot mimics: {sound}";
        }

        public string PerformTrick()
        {
            return $"{Name} does a somersault in the air!";
        }
    } 
}