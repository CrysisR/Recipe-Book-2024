using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PROG6221_POEPartOne
{
    internal class Program
    {
        //program that creates recipe for Sanele
        public static Recipe repClass = new Recipe();

        static void Main(string[] args)
        { 
            welcomeToRecipeCreator();
        }
        //pseudo start screen for application
        static void welcomeToRecipeCreator()
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
            string menuChoice;
            bool recipeCreated=false;
            //do while statements that checks for exit value
            do
            {
                logo();
                Console.WriteLine("What would you like to do?" +
                    "\n(n.b. Enter the words within the parenthesis)" +
                    "\n1. Add Recipe(add)" +
                    "\n2. Display Recipe(display)" +
                    "\n3. Edit values (edit)" +
                    "\n4. Scale Recipe(scale)" +
                    "\n5. Clear Recipe(clear)" +
                    "\n7. Exit program(exit)");
                Console.Write(">> ");
                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "add":
                        recipeCreated = repClass.recipeCreator();
                        break;
                    case "display":
                        //checks whether the recipieCreator method has been run through, otherwise it calls a method for displaying no values
                        if (recipeCreated == true)
                        { repClass.recipeDisplayer(); }
                        else { noValues(); recipeCreatorMenu(); }
                        break;
                    case "edit":
                        if (recipeCreated == true)
                        { repClass.editValues(); }
                        else { noValues(); recipeCreatorMenu(); }
                    break;
                    case "scale":
                        if (recipeCreated == true) { repClass.recipeScaler(); }
                        else { noValues(); recipeCreatorMenu(); }
                    break;
                    case "clear":
                        if (recipeCreated == true) { repClass.clearVars(); recipeCreated = false; }
                        else { noValues(); recipeCreatorMenu(); }
                    break;
                    case "exit": break;
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
            } while (menuChoice!="exit");
            //if statement to call closing method
            if(menuChoice.Equals("exit")) { goodbye(); }
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

        //logo that is called by the recipeCreatorMenu method
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
            Console.ForegroundColor= ConsoleColor.DarkRed;
            Console.WriteLine("Error! No values to display");
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }
    }

}
