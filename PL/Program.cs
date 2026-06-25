using System;
using System.Collections.Generic;
using PA1.BL;
using PA1.BL.Animals;
using PA1.DAL;

namespace PA1.PL
{
    class Program
    {
        private static ParrotBL _parrotBl;
        private static bool _exitRequested = false;

        static void Main(string[] args)
        {
            InitializeApplication();
            RunMainMenuLoop();
            CleanupApplication();
        }

        #region Application Lifecycle
        private static void InitializeApplication()
        {
            try
            {
                Console.WriteLine("Initializing Animal Management System...");

                using (var context = new AnimalContext())
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }

                _parrotBl = new ParrotBL("sqlite");
                Console.WriteLine("System ready. Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal initialization error: {ex.Message}");
                _exitRequested = true;
            }
        }

        private static void CleanupApplication()
        {
            Console.WriteLine("\nShutting down Animal Management System...");
            Console.WriteLine("Goodbye!");
        }
        #endregion

        #region Main Menu System
        private static void RunMainMenuLoop()
        {
            while (!_exitRequested)
            {
                Console.Clear();
                DisplayMainMenu();
                ProcessMenuChoice(GetMenuChoice());
            }
        }

        private static void DisplayMainMenu()
        {
            Console.WriteLine("=== ANIMAL MANAGEMENT SYSTEM ===");
            Console.WriteLine("1. Parrot Management");
            Console.WriteLine("2. Dolphin Demonstration");
            Console.WriteLine("3. Elephant Demonstration");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option (1-4): ");
        }

        private static int GetMenuChoice()
        {
            int choice = 0;
            while (choice < 1 || choice > 4)
            {
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.Write("Invalid input. Please enter 1-4: ");
                }
            }
            return choice;
        }

        private static void ProcessMenuChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    ShowParrotMenu();
                    break;
                case 2:
                    RunDolphinDemo();
                    break;
                case 3:
                    RunElephantDemo();
                    break;
                case 4:
                    _exitRequested = true;
                    break;
            }
        }
        #endregion

        #region Parrot Management
        private static void ShowParrotMenu()
        {
            bool backToMain = false;
            while (!backToMain)
            {
                Console.Clear();
                Console.WriteLine("=== PARROT MANAGEMENT ===");
                Console.WriteLine("1. Add New Parrot");
                Console.WriteLine("2. View All Parrots");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Select an option (1-3): ");

                switch (GetMenuChoice(3))
                {
                    case 1:
                        AddNewParrot();
                        break;
                    case 2:
                        DisplayAllParrots();
                        break;
                    case 3:
                        backToMain = true;
                        break;
                }
            }
        }

        private static void AddNewParrot()
        {
            try
            {
                Console.WriteLine("\n--- ADD NEW PARROT ---");
                var parrot = new Parrot(
                    GetInput("Enter Parrot ID: "),
                    GetInput("Enter Parrot Name: "),
                    int.Parse(GetInput("Enter Parrot Age: "))
                )
                {
                    FeatherColor = GetInput("Enter Feather Color: ")
                };

                int result = _parrotBl.Insert(parrot);
                Console.WriteLine(result > 0 ? "Parrot added successfully!" : "Failed to add parrot.");
            }
            catch (InvalidFeatherColorException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private static void DisplayAllParrots()
        {
            try
            {
                Console.WriteLine("\n--- ALL PARROTS ---");
                List<Parrot> parrots = _parrotBl.SelectAll();

                if (parrots.Count == 0)
                {
                    Console.WriteLine("No parrots found in database.");
                }
                else
                {
                    foreach (var parrot in parrots)
                    {
                        Console.WriteLine($"ID: {parrot.ID}");
                        Console.WriteLine($"Name: {parrot.Name}");
                        Console.WriteLine($"Age: {parrot.Age}");
                        Console.WriteLine($"Feather Color: {parrot.FeatherColor}");
                        Console.WriteLine($"Sound: {parrot.MakeSound()}");
                        Console.WriteLine($"Trick: {parrot.PerformTrick()}\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving parrots: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
        #endregion

        #region Animal Demonstrations
        private static void RunDolphinDemo()
        {
            Console.Clear();
            Console.WriteLine("=== DOLPHIN DEMONSTRATION ===");

            var trainer = new Trainer("Marine Mary", 8);
            var dolphin = new Dolphin("D001", "Flipper", 5)
            {
                Trainer = trainer,
                SwimSpeed = 42.5
            };

            dolphin.Jumped += trainer.OnDolphinJump;

            Console.WriteLine(dolphin.MakeSound());
            Console.WriteLine(dolphin.Jump(3.5));
            Console.WriteLine(dolphin.PerformTrick());
            Console.WriteLine(dolphin.ShowTrainer());

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void RunElephantDemo()
        {
            Console.Clear();
            Console.WriteLine("=== ELEPHANT DEMONSTRATION ===");

            var elephant = new Elephant("E001", "Dumbo", 10)
            {
                TrunkLength = 2.1
            };

            Console.WriteLine(elephant.MakeSound());
            Console.WriteLine(elephant.SprayWater());
            Console.WriteLine(elephant.Eat());

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        #endregion

        #region Utility Methods
        private static int GetMenuChoice(int maxOption)
        {
            int choice = 0;
            while (choice < 1 || choice > maxOption)
            {
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.Write($"Invalid input. Please enter 1-{maxOption}: ");
                }
            }
            return choice;
        }

        private static string GetInput(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();
            } while (string.IsNullOrEmpty(input));

            return input;
        }
        #endregion
    }
}