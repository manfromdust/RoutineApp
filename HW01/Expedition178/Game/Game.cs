using Expedition178.Characters;
using Expedition178.GameMechanics;
using Expedition178.Game.Parameters;
using Expedition178.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expedition178.Game
{
    public class Game : IGame
    {
        private int wave = 1;
        private Adventurer[] adventurers;
        private RandomGenerator generator = new();

        public Game()
        {
            adventurers = new Adventurer[Parameters.Parameters.MaxAdventurers];
            Console.WriteLine("New game has started.\n");
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
            var chosen = CharacterGenerator.GenerateAdventurers(generator);
            int[] indexes = new int[Parameters.Parameters.MaxAdventurers];
            bool success = true;

            while (true)
            {
                success = true;

                Console.WriteLine("Adventurers to choose from:\n");

                for (int i = 0; i < chosen.Length; i++)
                {
                    Console.WriteLine($"{i + 1}: {chosen[i].ToString()}");
                }

                Console.WriteLine($"\nChoose your {Parameters.Parameters.MaxAdventurers} adventurers (in format eg. '4 1 2')\n");
                Console.Write("Your choice: ");

                string? input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine($"You have not chosen any character. You need to choose {Parameters.Parameters.MaxAdventurers}.\n");
                    continue;
                }

                string[] indexesStr = input.Split(' ');

                if (indexesStr.Length != Parameters.Parameters.MaxAdventurers)
                {
                    Console.WriteLine($"You need to choose exactly {Parameters.Parameters.MaxAdventurers} characters. Please try again.\n");
                    continue;
                }

                if (indexesStr.Distinct().Count() != Parameters.Parameters.MaxAdventurers)
                {
                    Console.WriteLine("You cannot choose the same character more than once. Please try again.\n");
                    continue;
                }

                for (int i = 0; i < indexesStr.Length; i++)
                {
                    if (!int.TryParse(indexesStr[i], out indexes[i]))
                    {
                        Console.WriteLine("Invalid input. Please enter numbers corresponding to the adventurers.\n");
                        success = false;
                        break;
                    }
                    indexes[i] -= 1; // Adjust for 0-based indexing
                }

                if (success)
                {
                    Console.WriteLine("You have chosen:\n");

                    int pos = 0;
                    foreach (int i in indexes)
                    {
                        Console.WriteLine(chosen[i].ToString());
                        adventurers[pos++] = chosen[i];
                    }
                    Console.WriteLine("\nIs this your final decision? You can change only their order during the game.");
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
            Console.WriteLine("Your adventurers and their order:\n");

            for (int i = 0; i < adventurers.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {adventurers[i].ToString()}");
            }
        }

        private void SortAdventurers()
        {
            PrintAdventurers();
            Console.WriteLine("Enter the new order of your adventurers (e.g., '2 1 3'):\n");
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
            Console.WriteLine("Monsters you will fight in next wave:\n");
            for (int i = 0; i < monsters.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {monsters[i]}");
            }
        }

        private Choice ChooseInput(Battle battle)
        {
            Console.WriteLine("Choose an action (if you're not sure, type 'help'),");
            Console.WriteLine("don't forget to check monsters in next round and choose order of your adventurers.");

            while (true)
            {
                Console.WriteLine("\nYour action:");
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
                Battle battle = new Battle(wave, generator);
                Choice choice = ChooseInput(battle);

                if (choice == Choice.Quit) return;

                Character[] winners = battle.PerformBattle(adventurers, battle.Monsters);

                if (winners is Adventurer[])
                {
                    Console.WriteLine($"\nCongratulations! You have won wave {wave}!\n");
                    wave++;
                }
                else
                {
                    Console.WriteLine("\nYou have lost. Better luck next time!\n");
                }
            }
        }
    }
}
