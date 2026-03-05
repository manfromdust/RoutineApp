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
            Console.WriteLine("Choose an action (if you're not sure, type 'help'):");

            while (true)
            {
                string? input = Console.ReadLine();
                switch (input)
                {
                    case "check":
                        return Choice.Check;
                    case "fight":
                        return Choice.Fight;
                    case "info":
                        return Choice.Info;
                    case "help":
                        return Choice.Help;
                    case "sort":
                        return Choice.Sort;
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
            Choice chosen = ChooseInput();
        }
    }
}
