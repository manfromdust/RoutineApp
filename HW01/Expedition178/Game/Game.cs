using Expedition178.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expedition178.Game
{
    internal class Game : IGame
    {
        private int round = 1;
        private Adventurer[] adventurers;

        public Game()
        {
            adventurers = new Adventurer[3];
            Console.WriteLine("Welcome to Expedition 178!");
        }

        // can return only with 2. Fight or 6. Quit choices
        private Choice ChooseInput()
        {
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1. Check");
            Console.WriteLine("2. Fight");
            Console.WriteLine("3. Info");
            Console.WriteLine("4. Help");
            Console.WriteLine("5. Sort");
            Console.WriteLine("6. Quit");
            while (true)
            {
                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        return Choice.Check;
                    case "2":
                        return Choice.Fight;
                    case "3":
                        return Choice.Info;
                    case "4":
                        return Choice.Help;
                    case "5":
                        return Choice.Sort;
                    case "6":
                        return Choice.Quit;
                    default:
                        Console.WriteLine("Invalid input, please try again.");
                        break;
                }
            }
        }

        public void Start()
        {
            Choice chosen = ChooseInput();
        }
    }
}
