using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PROG6221_POE
{
    public class Recipe
    {

        //global declarations for delegate
        //class object for method
        Checking check = new Checking();
        public List<RecipeValue> recipes = new List<RecipeValue>();

        public delegate bool containsIntDelegate(string toCheck);
        public delegate bool containsStringDelegate(string toCheck);
        public delegate bool containsStringDoubleDelegate(string toCheck);
        public delegate bool correctUnitDelegate(string toCheck);
        public delegate bool correctFoodGroupDelegate(string toCheck);
        public delegate int calorieCounterDelegate(double calorie);

        //const

        public bool recipeCreator()
        {

            containsIntDelegate cid = new containsIntDelegate(check.containsInt);
            containsStringDelegate csd = new containsStringDelegate(check.containsString);
            calorieCounterDelegate ccd = new calorieCounterDelegate(check.calorieCalculator);

            bool pass = false;
            string recipeName = null;
            RecipeValue rv = new RecipeValue();
            Console.Clear();
            while (!pass)
            {
                Console.Write("Enter the ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("name ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("of the recipe");
                Console.WriteLine();
                Console.Write(">> ");
                recipeName = Console.ReadLine();
                pass = cid(recipeName);
            }
           rv.RecipeName = recipeName;
            Console.Clear();
            pass = false;
            int numberOfIngredients = 0;
            while(!pass)
            {
                Console.Write("Enter the number of ingredients that ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{recipeName} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("has");
                Console.WriteLine();   
                Console.Write(">> ");
                string numberOfIngredientsStr = Console.ReadLine();
                pass = csd(numberOfIngredientsStr);
                if(pass) { numberOfIngredients = int.Parse(numberOfIngredientsStr); }
            }

            addIngredients(rv ,numberOfIngredients);

            pass = false;
            int numberOfSteps = 0;
            Console.Clear();
            while(!pass)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter the number of ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("steps ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("required to make ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{recipeName}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n>> ");
                string numberOfStepsStr = Console.ReadLine();
                pass = csd(numberOfStepsStr);
                if(pass) { numberOfSteps = int.Parse(numberOfStepsStr); }
            }

            string recipeStep;
            for (int i = 0; i < numberOfSteps; i++)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter step ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{i+1} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("for creating ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{recipeName} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n>> ");
                recipeStep = Console.ReadLine();
                rv.Steps.Add(new IngredientAndStep
                {
                    Step = recipeStep
                });
            }
            recipes.Add(rv);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Recipe added successfully!");
            Console.WriteLine("Press [enter] to return to the menu");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(500);
            Console.Write(">> ");
            Console.ReadLine();

            return true;
        }
        public void addIngredients(RecipeValue rv, int iterate, int? indexToEdit = -1)
        {
            containsIntDelegate cid = new containsIntDelegate(check.containsInt);
            containsStringDoubleDelegate csdd = new containsStringDoubleDelegate(check.containsString);
            containsStringDelegate csd = new containsStringDelegate(check.containsString);
            correctUnitDelegate cud = new correctUnitDelegate(check.correctMeasurement);
            correctFoodGroupDelegate cfgd = new correctFoodGroupDelegate(check.correctFoodGroup);

            bool pass = false;
            Console.Clear();
            for (int i = 0; i < iterate; i++)
            {
                string ingredientName = null;
                while (!pass)
                {
                    Console.Write("Enter the ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("name ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("of ingredient ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{i+1}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n>> ");
                    ingredientName = Console.ReadLine();
                    pass = cid(ingredientName);
                }
                Console.Clear();
                pass = false;
                double ingredientQuantity = 0;
                while (!pass)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Enter the ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("quantity ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("of ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{ingredientName} ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n>> ");
                    string ingredientQuantityStr = Console.ReadLine();
                    pass = csdd(ingredientQuantityStr);
                    if (pass) { ingredientQuantity = double.Parse(ingredientQuantityStr); }
                }
                Console.Clear();
                pass = false;
                string ingredientMeasurement = null;
                do
                {
                    Console.Write("Enter the ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("measurement ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("of ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{ingredientName} \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("(n.b. Enter the value within the parenthesis)");
                    Console.Write("Teaspoon");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("(tsp)\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Tablespoon");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("(tbsp)\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Grams");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("(g)\n");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("------------------------");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Cups");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("(c)\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Millilitre");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("(ml)\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Litres");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("(l)\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n>> ");
                    ingredientMeasurement = Console.ReadLine().ToLower();
                    pass = cud(ingredientMeasurement);
                } while (!pass);

                pass = false;
                double ingredientCalorie = 0;
                Console.Clear();
                while (!pass)
                {
                    Console.Write("Enter the number of ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("calories ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("that ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{ingredientName} ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"has");
                    Console.Write("\n>> ");
                    string ingredientCalorieStr = Console.ReadLine();
                    pass = csdd(ingredientCalorieStr);
                    if (pass) { ingredientCalorie = double.Parse(ingredientCalorieStr); }
                }

                pass = false;
                string ingredientFoodGroup = null;
                Console.Clear();
                while (!pass)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Enter the ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("food group ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("that ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{ingredientName} ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("is part of");
                    Console.WriteLine("\n(n.b. Enter the word within the parenthesis)");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("(Liquid)");
                    Console.WriteLine("(Starch)");
                    Console.WriteLine("(Veg)");
                    Console.WriteLine("(Protein)");
                    Console.WriteLine("(Dairy)");
                    Console.WriteLine("(Fat)");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(">> ");
                    ingredientFoodGroup = Console.ReadLine().ToLower();
                    pass = cfgd(ingredientFoodGroup);
                }
                rv.Ingredients.Add(new IngredientAndStep
                {
                    IngredientName = ingredientName,
                    IngredientQuantity = ingredientQuantity,
                    IngredientMeasurement = ingredientMeasurement,
                    IngredientCalorie = ingredientCalorie,
                    IngredientFoodGroup = ingredientFoodGroup
                });
                pass = false;
            }
        }
        public RecipeValue SelectRecipe()
        {
            containsIntDelegate cid = new containsIntDelegate(check.containsInt);
            List<string> listOfRecipes = new List<string>();
            bool pass = false;

            Console.Clear();
            RecipeValue rv = new RecipeValue();
            Console.WriteLine("====================" +
            "\nRECIPES" +
            "\n====================");
            listOfRecipes = recipes.Select(rn => rn.RecipeName).OrderBy(recipeName => recipeName).ToList();
            foreach(var name in listOfRecipes)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(name);
                Console.ForegroundColor = ConsoleColor.White;
            }
            RecipeValue selectedRecipe = null;
            Console.WriteLine("====================");
            Console.Write("Enter the ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("name ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("of the recipe you would like to work with");
            Console.Write("\n>> ");

            string toDisplay = Console.ReadLine();
            pass = cid(toDisplay);
            if(!pass) { SelectRecipe(); }
            selectedRecipe = recipes.FirstOrDefault(rep => rep.RecipeName == toDisplay);
            if (selectedRecipe != null) { pass = true; }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Error! No recipe was found");
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
            }
            return selectedRecipe;
        }
        public void recipeDisplayer()
        {
            RecipeValue recipeValue = SelectRecipe();
            if (recipeValue != null)
            {
                display(recipeValue);
            }
            else { return; }
        }

        void display(RecipeValue recipeValue)
        {
            calorieCounterDelegate ccd = new calorieCounterDelegate(check.calorieCalculator);
            //placeholders to get recipe name
            Console.Clear();
            Console.WriteLine("====================");
            Console.Write("How to make ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{recipeValue.RecipeName}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n====================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("INGREDIENTS REQUIRED");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(500);
            foreach (var ingredients in recipeValue.Ingredients)
            {
                Console.WriteLine("====================\n");
                Console.Write($"{ingredients.IngredientQuantity} {ingredients.IngredientMeasurement} of {ingredients.IngredientName}" +
                    $"\nFood group: {ingredients.IngredientFoodGroup}" +
                    $"\nCalories: {ingredients.IngredientCalorie}\n\n");
            }
            Console.WriteLine("====================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("STEPS");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("====================\n"); 
            Thread.Sleep(500);
            for (int i = 0; i < recipeValue.Steps.Count(); i++)
            {
                Console.WriteLine($"Step {i+1}: {recipeValue.Steps[i].Step}");
            }
            Console.WriteLine("\n====================");
            double totalCalories = recipeValue.Ingredients.Sum(ing => ing.IngredientCalorie);
            Console.Write($"Total calories of {recipeValue.RecipeName}: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{totalCalories}");
            Console.ForegroundColor = ConsoleColor.White;
            ccd(totalCalories);

            //need to add foodgroup and calorie
            //do calorie count as well 
            //printed successfully
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Recipe printed successfully!\n");
            Console.WriteLine("Press [enter] to return to the menu");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(500);
            Console.Write(">> ");
            Console.ReadLine();
        }
        public void editRecipe()
        {
            RecipeValue rv = new RecipeValue();
            rv = SelectRecipe();
            if(rv!=null) 
            {
                Edit edit = new Edit();
                edit.editing(recipes, rv);
            }
        }
        public void recipeScaler()
        {
            calorieCounterDelegate ccd = new calorieCounterDelegate(check.calorieCalculator);

            bool pass = false;
            double scale = 0;
            Recipe recipe = new Recipe();
            RecipeValue recipeValue = new RecipeValue();

            recipeValue = SelectRecipe();
            if (recipeValue == null) { return; }
            while (!pass)
            {
                Console.Clear();
                Console.WriteLine($"Enter the value that you want {recipeValue.RecipeName} to be scaled to" +
                    "\nn.b Enter the value within the parenthesis" +
                    "\nHalf (0.5)" +
                    "\nDouble (2)" +
                    "\nTriple (3)", recipeValue.RecipeName);
                Console.Write($"Enter the value that you want {recipeValue.RecipeName} to be ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("scaled ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("to");
                Console.WriteLine("\n(n.b. Enter the value within the parenthesis)");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Half ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("(0.5)\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Double ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("(2)\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Triple ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("(3)\n");
                Console.Write("\n>> ");
                string scaleChoice = Console.ReadLine();
                switch (scaleChoice) 
                {
                    case "0.5": scale = 0.5; pass = true; break;
                    case "2": scale = 2; pass = true; break;
                    case "3": scale = 3; pass = true; break;
                    default: incorrectChoice(); break;
                }
            }
            var ingredients = recipeValue.Ingredients;
            double totalScaledCalories = 0;
            //placeholders to get recipe name
            Console.Clear();
            Console.WriteLine("====================");
            Console.Write("How to make ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"{recipeValue.RecipeName}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n====================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("INGREDIENTS REQUIRED");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(500);
            foreach (var items in ingredients)
            {
                double scaledQuantity = items.IngredientQuantity * scale;
                double scaledCalories = items.IngredientCalorie * scale;
                totalScaledCalories += scaledCalories;

                Console.Write($"{scaledQuantity} {items.IngredientMeasurement} of {items.IngredientName}" +
                    $"\nFood group: {items.IngredientFoodGroup}" +
                    $"\nCalories: {scaledCalories}" +
                    $"\n\n");
            }
            Console.WriteLine("====================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("STEPS");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("====================\n");
            Thread.Sleep(500);
            for (int i = 0; i < recipeValue.Steps.Count(); i++)
            {
                Console.WriteLine($"Step {i + 1}: {recipeValue.Steps[i].Step}");
            }
            Console.WriteLine("====================");

            Console.Write($"Total calories of {recipeValue.RecipeName}: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{totalScaledCalories}");
            Console.ForegroundColor = ConsoleColor.White;
            ccd(totalScaledCalories);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Recipe printed successfully!\n");
            Console.WriteLine("Press [enter] to return to the menu");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(500);
            Console.Write(">> ");
            Console.ReadLine();
        }

        public void deleteRecipe()
        {
            bool pass = false;
            RecipeValue recipeValue = new RecipeValue();
            recipeValue = SelectRecipe();
            Console.Clear();
            if (recipeValue == null) { return; }
            Console.Write("Are you sure you want to ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("delete ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("this recipe?\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Enter (yes) or (no)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(">> ");
            string answer = Console.ReadLine().ToLower();
            while(!pass)
            {
                if(answer.Equals("yes"))
                {
                    pass = true;
                }
                else if(answer.Equals("no"))
                {
                    return;
                }
                else { incorrectChoice(); }
            }
            recipes.Remove(recipeValue);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Recipe deleted successfully!\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
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
