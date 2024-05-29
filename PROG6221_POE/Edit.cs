using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PROG6221_POE
{
    public class Edit
    {
        public delegate bool containsIntDelegate(string toCheck);
        public delegate bool containsStringDelegate(string toCheck);
        Checking check = new Checking();
        int indexOfRecipe = 0;

        public void editing(List<RecipeValue> recipes, RecipeValue rv)
        {
            containsIntDelegate cid = new containsIntDelegate(check.containsInt);
            containsStringDelegate csd = new containsStringDelegate(check.containsString);

            Console.Clear();
            bool pass = false;
            string action = "";

            Console.Clear();
            pass = false;
            while (!pass)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("(n.b. Enter the value within the parenthesis)");
                Console.Write("Edit the recipe ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("(name)\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Edit the recipe ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("(ingredient)\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Edit the recipe ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("(step)\n");
                Console.WriteLine("(Cancel)");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">> ");
                switch (Console.ReadLine().ToLower())
                {
                    case "name": action = "n"; pass = true; break;
                    case "ingredient": action = "i"; pass = true; break;
                    case "step": action = "s"; pass = true; break;
                    case "cancel": return; 
                    default: incorrectChoice(); Console.Clear(); break;
                }
            }
            if(action.Equals("n")) { changeRecipeName(rv); }
            if(action.Equals("i")) { changeIngredient(rv); }
            if(action.Equals("s")) { changeStep(rv); }
        }
        private void changeRecipeName(RecipeValue rv)
        {
            containsIntDelegate cid = new containsIntDelegate(check.containsInt);
            bool pass = false;
            string newRecipeName="";
            while (!pass)
            {
                Console.Clear();
                Console.Write("Current recipe name: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{rv.RecipeName}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nEnter the new ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("name ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("for the recipe");
                Console.Write("\n>> ");
                newRecipeName = Console.ReadLine();
                pass = cid(newRecipeName);
            }
            rv.RecipeName = newRecipeName;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Recipe name updated successfully!\n");
            Console.WriteLine("Press [enter] to return to the menu");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(500);
            Console.Write(">> ");
            Console.ReadLine();
        }
        private void changeIngredient(RecipeValue rv)
        {
            containsIntDelegate cid = new containsIntDelegate(check.containsInt);
            containsStringDelegate csd = new containsStringDelegate(check.containsString);

            bool pass = false;
            pass = false;
            string ingredientName="";
            while (!pass)
            {
                Console.Clear();
                Console.Write("Below are the ingredients for making ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{rv.RecipeName}\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < rv.Ingredients.Count; i++)
                {
                    Console.Write($"Ingredient {i + 1}: {rv.Ingredients[i].IngredientName}\n");
                }
                Console.Write("\nEnter the ingredient ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("name ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("you want to change");
                Console.Write("\n>> ");
                ingredientName = Console.ReadLine();
                pass = cid(ingredientName);
            }
            var ingredientIndex = rv.Ingredients.FindIndex(i => i.IngredientName.Equals(ingredientName));
            if (ingredientIndex == -1)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error! Ingredient not found");
                Console.ForegroundColor = ConsoleColor.White;
                pass = false;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Current ingredient: {rv.Ingredients[ingredientIndex].IngredientName}");
                Console.Write("Current ingredient: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{rv.Ingredients[ingredientIndex].IngredientName}");
                Console.ForegroundColor = ConsoleColor.White;
                rv.Ingredients.RemoveAt(ingredientIndex);
                Recipe rep = new Recipe();
                rep.addIngredients(rv, 1, ingredientIndex);

                Console.Clear();
                pass = false;
                while (!pass)
                {
                    Console.WriteLine("Would you like to edit another ingredient");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Enter (yes) or (no)");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(">> ");
                    switch (Console.ReadLine().ToLower())
                    {
                        case "yes": changeIngredient(rv); pass = true; break;
                        case "no": pass = true; break;
                        default: incorrectChoice(); break;
                    }
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Ingredient updated successfully!\n");
                Console.WriteLine("Press [enter] to return to the menu");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(500);
                Console.Write(">> ");
                Console.ReadLine();
            }
        }
        private void changeStep(RecipeValue rv)
        {
            containsIntDelegate cid = new containsIntDelegate(check.containsInt);
            containsStringDelegate csd = new containsStringDelegate(check.containsString);
            bool pass = false;
            Console.Clear();
            Console.Write("Below are the steps for making ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{rv.RecipeName}\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < rv.Steps.Count; i++)
            {
                Console.WriteLine($"Step {i+1}: {rv.Steps[i].Step}");
            }
            int indexOfStep = 0;
            while(!pass)
            {
                Console.Write("\nEnter the step ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("NUMBER ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("you would like to change");
                Console.Write("\n>> ");
                string indexOfStepToChange = Console.ReadLine();
                pass = csd(indexOfStepToChange);
                if(!pass)
                {
                    for (int i = 0; i < rv.Steps.Count; i++)
                    {
                        Console.WriteLine($"Step {i + 1}: {rv.Steps[i]}");
                    }
                }
                indexOfStep = int.Parse(indexOfStepToChange) - 1;
            }
            Console.Clear();
            Console.WriteLine($"Current Step: {rv.Steps[indexOfStep].Step}");
            Console.WriteLine("Enter the new step");
            Console.Write(">> ");
            string stepToAdd = Console.ReadLine();
            rv.Steps[indexOfStep].Step.Remove(indexOfStep);
            rv.Steps[indexOfStep].Step = stepToAdd;

            Console.Clear();
            pass = false;
            while (!pass)
            {
                Console.WriteLine("Would you like to edit another step?");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Enter (yes) or (no)");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">> ");
                switch (Console.ReadLine().ToLower())
                {
                    case "yes": changeStep(rv); pass = true; break;
                    case "no": pass = true; break;
                    default: incorrectChoice(); break;
                }
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Step updated successfully!\n");
            Console.WriteLine("Press [enter] to return to the menu");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(500);
            Console.Write(">> ");
            Console.ReadLine();
        }

        void incorrectChoice()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Error! Invalid Choice");
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }
    }
}
