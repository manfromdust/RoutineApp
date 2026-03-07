using Expedition178.Game;

namespace Expedition178
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Expedition 178!");
            while (true)
            {
                Console.WriteLine("Type 'start' to begin a new game or 'quit' to exit.");
                Console.Write("Your choice: ");

                string? input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                switch (input)
                {
                    case "start":
                        Game.Game game = new();
                        game.Start();
                        break;
                    case "quit":
                        Console.WriteLine("Thanks for playing Expedition 178!");
                        return;
                    default:
                        Console.WriteLine("Unknown command. Please type 'start' or 'quit'.");
                        break;
                }
            }
            
        }
    }
}