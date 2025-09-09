using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager.Core.Entities
{
    // A supporting entity for a recipe.
    public class Ingredient
    {
        public string Name { get; private set; }
        public double Quantity { get; private set; }
        public string Unit { get; private set; }

        public Ingredient(string name, double quantity, string unit)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
        }
    }
}
