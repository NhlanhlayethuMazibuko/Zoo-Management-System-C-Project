using System;
using PA1.BL.Interfaces;

namespace PA1.BL.Animals
{
    public class Dolphin : Animal, ICanPerformTricks
    {
        public delegate string JumpedEventDelegate(string name, double jumpHeight);
        public event JumpedEventDelegate Jumped;

        public Trainer Trainer { get; set; }
        public double SwimSpeed { get; set; }

        public Dolphin() : base() { }

        public Dolphin(string id, string name, int age) : base(id, name, age) { }

        public override string Eat()
        {
            return $"The dolphin, {Name}, eats fish and squid.";
        }

        public string Jump(double height)
        {
            string result = $"{Name} jumps {height} meters out of the water!";
            Jumped?.Invoke(Name, height);
            return result;
        }

        public override string MakeSound()
        {
            return "Click-click-whistle!";
        }

        public string PerformTrick()
        {
            return $"The dolphin, {Name}, jumps through a hoop.";
        }

        public string ShowTrainer()
        {
            return $"{Name} is trained by {Trainer?.Name ?? "no trainer"}, " +
                   $"who has {Trainer?.ExperienceYears ?? 0} years of experience.";
        }
    } 
}