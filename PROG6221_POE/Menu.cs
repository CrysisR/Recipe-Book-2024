using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PROG6221_POE
{
    public class Menu
    {
        static Recipe recipe = new Recipe();
        public static void welcomeToRecipeCreator()
        {
            Console.WriteLine("Starting Application: Please Wait...");
            Thread.Sleep(3000);
            Console.Write("Loading"); Thread.Sleep(1000);
            Console.Write("."); Thread.Sleep(1000);
            Console.Write("."); Thread.Sleep(1000);
            Console.Write("."); Thread.Sleep(1000);
            Console.Clear();
            recipeCreatorMenu();
        }

        //display menu method
        static void recipeCreatorMenu()
        {
            string menuChoice="";
            //do while statements that checks for exit value
            do
            {
                logo();
                Console.WriteLine("What would you like to do");
                Console.WriteLine("(n.b. Enter the word within the parenthesis)");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("(Add) ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Recipe");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("(Display) ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Recipe");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("(Edit) ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Recipe");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("(Scale) ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Recipe");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("(Delete) ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Recipe");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("(Exit) ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Recipe");
                Console.WriteLine();
                Console.Write(">> ");
                menuChoice = Console.ReadLine().ToLower();
                switch (menuChoice)
                {
                    case "add":
                        recipe.recipeCreator();
                        break;
                    case "display":
                        //checks whether the recipieCreator method has been run through, otherwise it calls a method for displaying no values
                        //first list recipe names, then when user chooses recipe, display that one
                        if (recipe.recipes.Count > 0) { recipe.recipeDisplayer(); } else { noValues(); }
                        break;
                    case "edit":
                        //ask to chose recipe, then go through the steps like in part 1
                        if (recipe.recipes.Count > 0) { recipe.editRecipe(); } else { noValues(); }
                        break;
                    case "scale":
                        //again choose, then ask for scale factor
                        //add this
                        if (recipe.recipes.Count > 0) { recipe.recipeScaler(); } else { noValues(); }
                        break;
                    case "delete":
                        //choose, clear
                        //add this
                        if (recipe.recipes.Count > 0) { recipe.deleteRecipe(); } else { noValues(); }
                        break;
                    case "exit": goodbye(); break;
                    case null: break;
                    default:
                        //for invalid menu entries
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Error! Invalid menu choice");
                        Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        break;
                }
            } while (menuChoice != "exit");
        }

        //pseudo shut down screen
        static void goodbye()
        {
            Console.Clear();
            Console.WriteLine("Thank you for using the recipie Creator!");
            Console.Write("Closing in "); Thread.Sleep(1000);
            Console.Write("3 "); Thread.Sleep(1000);
            Console.Write("2 "); Thread.Sleep(1000);
            Console.Write("1"); Thread.Sleep(1000);
            //find out how to close console
            Environment.Exit(0);
        }

        static void logo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  _____           _             _____                _             ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" |  __ \\         (_)           / ____|              | |            ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" | |__) |___  ___ _ _ __   ___| |     _ __ ___  __ _| |_ ___  _ __ ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" |  _  // _ \\/ __| | '_ \\ / _ \\ |    | '__/ _ \\/ _` | __/ _ \\| '__|");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" | | \\ \\  __/ (__| | |_) |  __/ |____| | |  __/ (_| | || (_) | |   ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" |_|  \\_\\___|\\___|_| .__/ \\___|\\_____|_|  \\___|\\__,_|\\__\\___/|_|   ");
            Console.WriteLine("                   | |                                             ");
            Console.WriteLine("                   |_|                                             \n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
        }

        //called in switch statement 
        static void noValues()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Error! No values to display");
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }
    }
}
