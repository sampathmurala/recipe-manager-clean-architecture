using System.Collections.Generic;

namespace RecipeManager.Api.Models
{
    public class RecipeRequest
    {
        public string Name { get; set; }
        public string Instructions { get; set; }
        public List<IngredientRequest> Ingredients { get; set; }
    }

    public class IngredientRequest
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }
}