using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace PROG6221_POE
{
    //we are checking
    public class Checking
    {
        bool pass = false;
        public bool stringContainsInt(string toCheck)
        {
            if (toCheck.Any(char.IsDigit) || string.IsNullOrEmpty(toCheck))
            {
                return false;
            }
            return true;
        }
        public bool stringIsIntegerAndPositive(string toCheck)
        {
            return int.TryParse(toCheck, out int result) && result > 0 && !string.IsNullOrEmpty(toCheck);
        }

        // Method for checking if a string can be parsed as a double and is positive
        public bool stringIsDoubleAndPositive(string toCheck)
        {
            return double.TryParse(toCheck, out double result) && result > 0 && !string.IsNullOrEmpty(toCheck);
        }

        // Method for checking if a string is a correct measurement unit
        public bool isCorrectMeasurement(string toCheck)
        {
            return toCheck.Equals("tsp") || toCheck.Equals("tbsp") || toCheck.Equals("g") || toCheck.Equals("kg") ||
                   toCheck.Equals("c") || toCheck.Equals("ml") || toCheck.Equals("l");
        }

        // Method for checking if a string is a correct food group
        public bool isCorrectFoodGroup(string toCheck)
        {
            return toCheck.Equals("liquid") || toCheck.Equals("starch") || toCheck.Equals("veg") ||
                   toCheck.Equals("protein") || toCheck.Equals("dairy") || toCheck.Equals("fat");
        }

        //seperating the logic so that i can do a unit test on it
        public bool containsInt(string toCheck)
        {
            bool pass = stringContainsInt(toCheck);
            if (!pass)
            {
                incorrectChoice();
            }
            return pass;
        }
        public bool containsString(string toCheck)
        {
            bool pass = stringIsIntegerAndPositive(toCheck);
            if (!pass)
            {
                incorrectChoice();
            }
            return pass;
        }

        public bool containsStringD(string toCheck)
        {
            bool pass = stringIsDoubleAndPositive(toCheck);
            if (!pass)
            {
                incorrectChoice();
            }
            return pass;
        }

        public bool correctMeasurement(string toCheck)
        {
            bool pass = isCorrectMeasurement(toCheck);
            if (pass)
            {
                Console.Clear();
            }
            else
            {
                incorrectChoice();
                Console.Clear();
            }
            return pass;
        }

        public bool correctFoodGroup(string toCheck)
        {
            bool pass = isCorrectFoodGroup(toCheck);
            if (pass)
            {
                Console.Clear();
            }
            else
            {
                incorrectChoice();
                Console.Clear();
            }
            return pass;
        }
        public int calorieCalculator(double calorie)
        {
            if(calorie > 300)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("WARNING! Calorie exceeds 300!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            return 0;
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
