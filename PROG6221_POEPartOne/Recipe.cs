using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PROG6221_POEPartOne
{
    internal class Recipe
    {
        //recipe creating class
        //global declaration of lists so that all methods can see it
        private List<string> recipeIngredients = new List<string>();
        private List<string> recipeSteps = new List<string>();
        private List <double> scaledIngredients = new List<double> ();
        private List<string> scaledIngredientName = new List<string> ();

        private string recipeName;

        //creating recipe method, bool return type to show filled List<T>
        public bool recipeCreator()
        {
            //intialization, which clears all List<T> and variables
            Console.Clear();
            recipeName = null;
            recipeIngredients.Clear();
            recipeSteps.Clear();
            bool containsInt = false;

            //entering recipe name that saved to public variable so that all methods can see it
            //checks whether string contains an int
            while(!containsInt)
            {
                Console.WriteLine("Please enter the name of the recipe");
                Console.Write(">> ");
                recipeName = Console.ReadLine();

                if (recipeName.Any(char.IsDigit) || string.IsNullOrEmpty(recipeName))
                {
                    incorrectChoice(); Console.Clear();
                } else { break; }
            }
            //intializing variables that will be used in loops
            int numberOfIngredients=0;
            bool pass = false;
            Console.Clear();

            //while loop to check for correctly formatted strings
            while (!pass)
            {
                Console.WriteLine("Enter the number of ingredients that {0} has", recipeName);
                Console.Write(">> ");
                string numberOfIngredientsStr = Console.ReadLine();

                //if statement try.Parses the above string, as well as checks whether the int is greater for zero, thus doing 2 checks in 1 statements
                if (int.TryParse(numberOfIngredientsStr, out int tempNumberOfIngredients) && tempNumberOfIngredients > 0)
                {
                    //makes var = to temp var, and makes pass true to leave the while statement
                    numberOfIngredients = tempNumberOfIngredients;
                    pass = true;
                }
                else { incorrectChoice(); Console.Clear(); }
            }

            Console.Clear();

            //add ingredients have their own method so that it can be called for the editing method
            //for loop int i is take as an argument to iterate through each step of the List<T>
            for (int i = 0; i < numberOfIngredients; i++) 
            {
                addIngredient(i); 
            }
            Console.Clear();

            //check for non-int and non-negative values
            int numberOfSteps=0;
            pass = false;
            while (!pass)
            {
                try 
                {
                    Console.WriteLine("Enter the number of steps required to make {0}", recipeName);
                    Console.Write(">> ");
                    numberOfSteps = int.Parse(Console.ReadLine());

                    if (numberOfSteps > 0)
                    {
                        pass = true;
                    }
                    else { incorrectChoice(); Console.Clear(); }
                }
                catch (Exception) 
                { incorrectChoice(); Console.Clear();}
            }

            //for loop used to iterate through List<T> as well as get step number for QOL
            string recipeStep;
            for (int i = 0; i < numberOfSteps; i++)
            {
                Console.Clear();
                Console.WriteLine("Enter step {0} for creating {1}",(i+1), recipeName);
                Console.Write(">> ");
                recipeStep = Console.ReadLine();
                recipeSteps.Add(recipeStep);
            }

            //completion of adding recipe
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Recipe added successfully!");
            Console.WriteLine("Press [enter] to return to menu");
            Thread.Sleep(500);
            Console.Write(">> ");
            Console.ReadLine();
            //returns true so that other methods can be accessed from menu
            return true;
        }//end of recipeCreatorMethod

        //method takes int argument to get the index from the for loop for the ingredient and step number
        void addIngredient(int i, int index = -1)
        {
            bool pass;
            bool containsInt = false;
            string ingredientName = null;

            while (!containsInt)
            {
                Console.WriteLine($"Enter the name of ingredient {(i + 1)}");
                Console.Write(">> ");
                ingredientName = Console.ReadLine();

                if (ingredientName.Any(char.IsDigit) || string.IsNullOrEmpty(ingredientName))
                {
                    incorrectChoice(); Console.Clear();
                }
                else { break; }
            }

            Console.Clear();
            //logic for catching non int vars
            //uses try catch instead of try.Parse
            double ingredientQuantity = 0;
            pass = false;
            while (!pass)
            {
                try
                {
                    Console.WriteLine("Enter the quantity of {0}", ingredientName);
                    Console.Write(">> ");
                    ingredientQuantity = double.Parse(Console.ReadLine());

                    if (ingredientQuantity > 0)
                    {
                        pass = true;
                    }
                    else { incorrectChoice(); Console.Clear(); }
                }
                catch (Exception)
                { incorrectChoice(); Console.Clear(); }
            }

            Console.Clear();
            //checks for accepted values using do while and switch statement
            bool acceptedString = false;
            string ingredientMeasurement;

            do
            {
                Console.WriteLine($"Enter the measurement of {ingredientName} \n(nb. Enter the value within the parenthesis)" +
                "\nTeaspoon(tsp)" +
                "\nTablespoon(tbsp)" +
                "\nGrams(g)" +
                "\nKilograms(kg)" +
                "\n--------------------" +
                "\nCups(c)" +
                "\nMillilitres(ml)" +
                "\nLitres(l)");

                Console.Write(">> ");
                ingredientMeasurement = Console.ReadLine().ToLower();

                //logic of switch : makes the temp var the one that added to the list
                //makes the boolean variable true
                //breaks the switch so multiple values cant be inputted
                //edit
                //changed it to an if statement because taking the temp var and making it permanent would have had too much clutter
                //therefore, if statement checking for all viable measurements with "or" operator, and if fails, dowhile loop catches it
                //and prompts again
                if (ingredientMeasurement.Equals("tsp") || ingredientMeasurement.Equals("tbsp") || ingredientMeasurement.Equals("g") || ingredientMeasurement.Equals("kg") ||
                ingredientMeasurement.Equals("c") || ingredientMeasurement.Equals("ml") || ingredientMeasurement.Equals("l"))
                {
                    acceptedString = true;
                    Console.Clear();
                }
                else
                { incorrectChoice(); Console.Clear(); }
            } while (!acceptedString);

            //makes the entire ingredient into 1 string and saves it to the List<T>
            string ingredientsToAdd = $"{ingredientQuantity} {ingredientMeasurement} of {ingredientName}";

            if(index == -1)
            {
                recipeIngredients.Add(ingredientsToAdd);

                //adding for scaled arrayLists for future use in scaling
                //another string for just adding the measurement and ingredient name so that the quantity can be scaled properly
                string toAddForScaled = $"{ingredientMeasurement} of {ingredientName}";
                scaledIngredientName.Add(toAddForScaled);
                scaledIngredients.Add(Convert.ToDouble(ingredientQuantity));
            }
            else if (index >=0 && index < recipeIngredients.Count)
            {
                recipeIngredients[index] = ingredientsToAdd;
            }
        }//end of add Ingredient method

        //displays recipe
        public void recipeDisplayer()
        {
            //placeholders to get recipe name
            Console.Clear();
            Console.WriteLine($"====================" +
                $"\nHow to make {recipeName}" +
                $"\n====================" +
                $"\n\nINGREDIENTS REQUIRED\n");
            Thread.Sleep(500);
            //foreach used to print ingredients in a line
            foreach(var ingredients in recipeIngredients)
            {
                Console.WriteLine(ingredients);
            }

            Console.WriteLine("\nSTEPS");
            Thread.Sleep(500);
            //for loop gets step number and the step itself by iterating through List
            for (int i = 0; i < recipeSteps.Count; i++)
            {
                Console.WriteLine($"Step {i + 1}: {recipeSteps[i]}\n");
            }
            Console.WriteLine("====================");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Recipe printed successfully!\n");
            Console.WriteLine("Press [enter] to return to the menu");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(500);
            Console.Write(">> ");
            Console.ReadLine();
        }//end of display recipe method

        //method that edits variables and the List<T>
        public void editValues()
        {
            Console.Clear();
            bool pass = false;
            string action = "";
            while (pass == false)
            {
                Console.WriteLine("Enter which value you would like to edit" +
                "\nn.b. Enter the value within the parenthesis" +
                "\n1. The recipe (name)" +
                "\n2. The recipe (ingredients)" +
                "\n3. The recipe (steps)");
                Console.Write(">> ");
                //switch that makes expressions true in certain if statements
                switch (Console.ReadLine().ToLower())
                {
                    case "name": action = "name"; pass = true; break;
                    case "ingredients":
                        action = "ing"; pass = true;
                        break;
                    case "steps":
                        action = "step"; pass = true;
                        break;
                    default: incorrectChoice(); Console.Clear(); break;
                }
            }

            Console.Clear();
            //if statement that changes the name of the recipe
            if (action.Equals("name"))
            {
                
                    bool containsInt = false;
                    while (!containsInt)
                    {
                        Console.WriteLine($"Current Recipe Name: {recipeName}");
                        Console.WriteLine("Please enter the new name of the recipe");
                        Console.Write(">> ");
                        recipeName = Console.ReadLine();

                        if (recipeName.Any(char.IsDigit) || string.IsNullOrEmpty(recipeName))
                        {
                            incorrectChoice(); Console.Clear();
                        }
                        else { break; }
                    }
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Clear();
                    Console.WriteLine("The recipe name has been updated successfully!");
                
            }

            //if statement that changes ingredients in the recipe
            if (action.Equals("ing"))
            {
                Console.WriteLine("Below are the following ingredients\n");
                Thread.Sleep(500);
                changeIngredient();
            }


            if (action.Equals("step"))
            {
                Console.Clear();
                Console.WriteLine("Below are the inputed steps\n");
                Thread.Sleep(500);
                changeStep();
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press [enter] to return to menu");
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(">> ");
            Console.ReadLine();
        }//end of editListMethod


        //for scaling the ingredients make in recipieCreator
        public void recipeScaler()
        {
            //list that takes the values from the ScaledIngredients List that are scaled and adds them
            List<double> tempScale = new List<double>();
            tempScale.Clear();
            Console.Clear();
            //checking for correct value
            double toScale = 0;
            bool acceptedScale=false;
            while(!acceptedScale)
            {
                Console.WriteLine("Enter the value that you want the ingredients scaled to" +
                "\nn.b. Enter the values within the parenthesis" +
                "\nHalf (0.5)" +
                "\nDouble (2)" +
                "\nTriple (3)");
                Console.Write(">> ");
                string scaleChoice = Console.ReadLine();
                //if suitable string is found, make acceptedScale true to exist while loop
                switch (scaleChoice)
                {
                    case "0.5": toScale = 0.5; acceptedScale = true; break;
                    case "2": toScale = 2; acceptedScale = true; break;
                    case "3": toScale = 3; acceptedScale = true; break;
                    default:
                        incorrectChoice(); Thread.Sleep(1000); 
                    break;
                }
            }
            //iterates through scaledIngredients, multiplies the elements with toScale, and adds them to temp scale, so original values are not lost
            foreach(var temp in scaledIngredients)
            {
                double toAdd = temp * toScale;
                tempScale.Add(toAdd);
            }
            //same logic as recipeDisplayer
            Console.WriteLine($"====================" +
                $"\nSCALED RECIPE FOR {recipeName}" +
                $"\n====================" +
                $"\nINGREDIENTS REQUIRED\n");
            Thread.Sleep(500);

            //since the recipe measurement and name had to be stored seperately, Console.Write is used instead of Console.Writeline
            //so that the string is printed in the correct format 
            for (int i = 0; i < scaledIngredients.Count; i++)
            {
                Console.Write($"{tempScale[i]} {scaledIngredientName[i]}\n");
            }

            Console.WriteLine("\nSTEPS");
            Thread.Sleep(500);
            for (int i = 0; i < recipeSteps.Count; i++)
            {
                Console.WriteLine($"Step {i + 1}: {recipeSteps[i]}\n");
            }

            Console.WriteLine("====================\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Recipe printed successfully!\n");
            Console.WriteLine("Press [enter] to return to the menu");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(500);
            Console.Write(">> ");
            Console.ReadLine();
        }//end of recipe Scaler method

        //name
        public void clearVars()
        {
            Console.Clear();
            string response;
            bool acceptedVal=false;
            //same do while logic as above
            do
            {
                Console.WriteLine("Are you sure you want to remove all values?" +
                    "\nEnter (yes) or (no)");
                response = Console.ReadLine().ToLower();
                switch (response)
                {
                    
                    case "yes":
                        recipeName = null;
                        recipeIngredients.Clear(); 
                        recipeSteps.Clear(); 
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Values cleared successfully");
                        Console.WriteLine("Enter [enter] to return to the menu");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(500);
                        Console.Write(">> ");
                        Console.ReadLine();
                        acceptedVal = true;
                        break;
                    case "no": return;
                    default: incorrectChoice(); break;
                }
            } while (acceptedVal == false);
        }//end of clear var method

        //change the recipe steps method
        void changeStep()
        {
            for (int i = 0; i < recipeSteps.Count; i++)
            {
                Console.Write($"Step {i + 1}: {recipeSteps[i]}\n");
            }
            bool pass = false;
            int index = 0;
            while (!pass)
            {
                Console.WriteLine("Enter the step number that you would like to change");
                Console.Write(">> ");
                string choice = Console.ReadLine();
                //checking for non int values
                if (int.TryParse(choice, out index) && index > 0 && recipeSteps.Count > index - 1)
                {
                    pass = true;
                }
                else
                {
                    incorrectChoice();
                    Console.Clear();
                    //if the value is incorrect, display the steps again so that the user chan chose again
                    for (int i = 0; i < recipeSteps.Count; i++)
                    {
                        Console.Write($"Step {i + 1}: {recipeSteps[i]}\n");
                    }
                }
            }
            //errors usually occur here
            Console.Clear();
            Console.WriteLine($"Step {index}: {recipeSteps[index - 1]}");
            Console.WriteLine($"Enter the new step");
            Console.Write(">> ");
            string newStep = Console.ReadLine();

            //overwrites the step
            recipeSteps[index - 1] = newStep;

            pass = false;
            while (!pass)
            {
                Console.WriteLine("Would you like to edit another step?" +
                    "\nEnter (yes) or (no)");
                Console.Write(">> ");
                switch (Console.ReadLine().ToLower())
                {
                    case "yes":
                        Console.Clear();
                        for(int i = 0;i < recipeSteps.Count;i++)
                        {
                            Console.WriteLine($"Step {i + 1}: {recipeSteps[i]}\n");
                        }
                        changeStep();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Step updated successfully!");
                        pass = true; break;
                    case "no": pass = true; break;
                    default:
                        incorrectChoice(); Console.Clear();
                        break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press [enter] to return to menu");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(500);
            Console.Write(">> ");
            Console.ReadLine();
        }//end of change step method

        //change ingredients method
        void changeIngredient()
        {
            for (int i = 0; i < recipeIngredients.Count; i++)
            {
                Console.Write($"Ingredient {i + 1}: {recipeIngredients[i]}\n");
            }
            //logic for checking for an int and negative value
            int index = 0;
            bool pass = false;
            while (!pass)
            {
                Console.WriteLine("\nEnter the ingredient number you want to change");
                Console.Write(">> ");
                string choice = Console.ReadLine();
                if (int.TryParse(choice, out index) && index > 0 && recipeIngredients.Count > index - 1)
                {
                    pass = true;
                }
                else
                {
                    incorrectChoice();
                    Console.Clear();
                    for (int i = 0; i < recipeIngredients.Count; i++)
                    {
                        Console.Write($"Ingredient {i + 1}: {recipeIngredients[i]}\n");
                    }
                }
            }

            Console.Clear();
            Console.WriteLine($"Ingredient {index}: {recipeIngredients[index - 1]}");

            //calls the addingIngredient method again to change the specific index in the array
            addIngredient(index - 1, index - 1);

            Console.Clear();
            pass = false;
            while (!pass)
            {
                Console.WriteLine("Would you like to edit another step?\nEnter (yes) or (no)");
                Console.Write(">> ");
                switch (Console.ReadLine().ToLower())
                {
                    case "yes": changeIngredient(); pass = true; break;
                    case "no": pass = true; break;
                    default: incorrectChoice(); Console.Clear(); break;
                }
            }
        }//end of change ingredient step

        //method that outputs the error messages for all the loops
        void incorrectChoice()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Error! Invalid Choice");
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
