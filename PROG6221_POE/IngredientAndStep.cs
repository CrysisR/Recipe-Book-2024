using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221_POE
{
    public class IngredientAndStep
    {
        //components of ingredients
        public string IngredientName {  get; set; }
        public double IngredientQuantity { get; set; }
        public string IngredientMeasurement { get; set; }
        public double IngredientCalorie { get; set; }
        public string IngredientFoodGroup { get; set; }

        //components of steps
        public string Step {  get; set; }
    }
}
