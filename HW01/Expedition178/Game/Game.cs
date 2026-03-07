using Expedition178.Characters;
using Expedition178.GameMechanics;
using Expedition178.Game.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expedition178.Game
{
    public class Game : IGame
    {
        private int wave = 1;
        private Adventurer[] adventurers;

        public Game()
        {
            adventurers = new Adventurer[Parameters.Parameters.MaxAdventurers];
            Console.WriteLine("Welcome to Expedition 178!");
        }

        private void DisplayHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  'check': to see what monsters you will fight in next round");
            Console.WriteLine("  'fight': to begin next round");
            Console.WriteLine("  'info': to see your adventurers and their order");
            Console.WriteLine("  'sort': for a menu to decide in what order send your adventurers to next round");
            Console.WriteLine("  'quit': to exit the game");
        }

        private void ChooseAdventurers()
        {
            var chosen = CharacterGenerator.GenerateAdventurers();
            int[] indexes = new int[Parameters.Parameters.MaxAdventurers];
            bool success = true;

            while (true)
            {
                success = true;

                Console.WriteLine("Adventurers to choose from:");

                for (int i = 0; i < chosen.Length; i++)
                {
                    Console.WriteLine($"{i + 1}: {chosen[i].ToString()}");
                }

                Console.WriteLine($"Choose your {Parameters.Parameters.MaxAdventurers} adventurers (in format eg. '4 1 2'");
                Console.Write("Your choice: ");

                string? input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine($"You have not chosen any character. You need to choose {Parameters.Parameters.MaxAdventurers}...");
                    continue;
                }

                string[] indexesStr = input.Split(' ');

                if (indexesStr.Length != Parameters.Parameters.MaxAdventurers)
                {
                    Console.WriteLine($"You need to choose exactly {Parameters.Parameters.MaxAdventurers} characters. Please try again.");
                    continue;
                }

                for (int i = 0; i < indexesStr.Length; i++)
                {
                    if (!int.TryParse(indexesStr[i], out indexes[i]))
                    {
                        Console.WriteLine("Invalid input. Please enter numbers corresponding to the adventurers.");
                        success = false;
                        break;
                    }
                    indexes[i] -= 1; // Adjust for 0-based indexing
                }

                if (success)
                {
                    Console.WriteLine("You have chosen:");
                    foreach (int i in indexes)
                    {
                        Console.WriteLine(chosen[i].ToString());
                        adventurers[i] = chosen[i];
                    }
                    Console.WriteLine("Is this your final decision? You can change only their order during the game.");
                    Console.Write("Type 'y' to confirm or 'n' to choose again: ");

                    string? confirmation = Console.ReadLine();

                    if (confirmation != null && confirmation.ToLower() == "y")
                    {
                        return;
                    }
                }
            }
        }

        private void PrintAdventurers()
        {
            Console.WriteLine("Your adventurers and their order:");

            for (int i = 0; i < adventurers.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {adventurers[i].ToString()}");
            }
        }

        private void SortAdventurers()
        {
            PrintAdventurers();
            Console.WriteLine("Enter the new order of your adventurers (e.g., '2 1 3'):");
            Console.Write("Your new order: ");

            int[] indexes = new int[3];

            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.WriteLine("Invalid input. Order not changed.");
                return;
            }

            string[] indexesStr = input.Split(' ');

            if (indexesStr.Length != Parameters.Parameters.MaxAdventurers)
            {
                Console.WriteLine($"You need to choose exactly {adventurers.Length} characters. Please try again.");
                return;
            }

            for (int i = 0; i < indexesStr.Length; i++)
            {
                if (!int.TryParse(indexesStr[i], out indexes[i]))
                {
                    Console.WriteLine("Invalid input. Please enter numbers corresponding to the adventurers.");
                    return;
                }
                if (indexes[i] < 1 || indexes[i] > Parameters.Parameters.MaxAdventurers)
                {
                    Console.WriteLine($"Invalid input. Please enter numbers between 1 and {Parameters.Parameters.MaxAdventurers}.");
                    return;
                }
            }

            if (indexes.Length != indexes.Distinct().Count())
            {
                Console.WriteLine("Invalid input. Not all positional values were stated.");
                return;
            }

            Array.Sort(indexes, this.adventurers);
        }

        private void PrintMonsters(Monster[] monsters)
        {
            Console.WriteLine("Monsters you will fight in next wave:");
            for (int i = 0; i < monsters.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {monsters[i]}");
            }
        }

        // can return only with fight or quit choices
        private Choice ChooseInput(Battle battle)
        {
            Console.WriteLine("Choose an action (if you're not sure, type 'help'),");
            Console.WriteLine("don't forget to check monsters in next round and choose order of your adventurers.");

            while (true)
            {
                Console.WriteLine("Your action:");
                Console.Write("[Player]: ");

                string? input = Console.ReadLine();
                switch (input)
                {
                    case "check":
                        PrintMonsters(battle.Monsters);
                        break;
                    case "fight":
                        return Choice.Fight;
                    case "info":
                        PrintAdventurers();
                        break;
                    case "help":
                        DisplayHelp();
                        break;
                    case "sort":
                        SortAdventurers();
                        break;
                    case "quit":
                        return Choice.Quit;
                    default:
                        Console.WriteLine("Invalid input, please try again.");
                        break;
                }
            }
        }

        public void Start()
        {
            ChooseAdventurers();
            Play();
        }

        private void Play()
        {
            while (wave < Parameters.Parameters.MaxWaves)
            {
                Battle battle = new Battle(wave);
                Choice choice = ChooseInput(battle);

                if (choice == Choice.Quit) return;

                Character[] winners = battle.PerformBattle(adventurers, battle.Monsters);

                if (winners is Adventurer[])
                {
                    Console.WriteLine($"Congratulations! You have won wave {wave}!");
                    wave++;
                }
                else
                {
                    Console.WriteLine("You have lost. Better luck next time!");
                }
            }
        }
    }
}
