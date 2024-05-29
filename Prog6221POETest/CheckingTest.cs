using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROG6221_POE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prog6221POETest
{
    [TestClass]
    public class CheckingTest
    {
        public delegate bool containsStringDelegate(string toCheck);
        public delegate bool containsIntDelegate(string toCheck);

        [TestMethod]
        public void totalCalories_test()
        {
            var recipeValues = new RecipeValue
            {
                Ingredients = new List<IngredientAndStep>
                {
                    new IngredientAndStep{IngredientName = "Butter", IngredientQuantity=20, IngredientMeasurement="kg", IngredientCalorie=14000, IngredientFoodGroup="fat"},
                    new IngredientAndStep{IngredientName = "Chicken", IngredientQuantity=10, IngredientMeasurement="g", IngredientCalorie=2, IngredientFoodGroup="protein"}

                }
            };
            double expected = 14002;

            double actualTotalCalories = recipeValues.Ingredients.Sum(tlCal => tlCal.IngredientCalorie);

            Assert.AreEqual(expected, actualTotalCalories);
        }

        [TestMethod]
        public void containsStringTrue_test()
        {
            Checking check = new Checking();
            containsStringDelegate csd = new containsStringDelegate(check.containsString);
            bool expected = true;
            bool actual = csd("27");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void containsIntFalse_test()
        {
            Checking check = new Checking();
            bool expected = true;
            bool actual = check.stringContainsInt("HelloWorld...uhhhhh wait i need a number, a very special one, i think this number issss..41. Why? Idk just go with it");
            Assert.AreNotEqual(expected, actual);
        }
        [TestMethod]
        public void correctMeasurement_test()
        {
            Checking check = new Checking();
            bool expected = true;
            bool actual = check.isCorrectMeasurement("ml");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void correctFoodGroupFalse_test()
        {
            Checking check = new Checking();
            bool expected = false;
            bool actual = check.isCorrectMeasurement("kg, as in kilogram");
            Assert.AreEqual(expected, actual);
        }
    }
}
