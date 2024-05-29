using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221_POE
{
    public class RecipeValue
    {
        //recipe names
        public string RecipeName { get; set; }

        public List<IngredientAndStep> Ingredients { get; set; }
        public List<IngredientAndStep> Steps { get; set; }
        public RecipeValue()
        {
            Ingredients = new List<IngredientAndStep>();
            Steps = new List<IngredientAndStep>();
        }
    }
}
