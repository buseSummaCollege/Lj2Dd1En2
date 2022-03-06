using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lj2Dd1En2.Models
{
    public class Ingredient
    {
        private int ingredientId;

        public int IngredientId
        {
            get { return ingredientId; }
            set { ingredientId = value; }
        }

        private string name = null!;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private string unit = null!;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
    }
}
