using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lj2Dd1En2.Models
{
    public class MealIngredient : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private int mealIngredientId;
        public int MealIngredientId
        {
            get { return mealIngredientId; }
            set { mealIngredientId = value; OnPropertyChanged(); }
        }

        private int mealId;
        public int MealId
        {
            get { return mealId; }
            set { mealId = value; OnPropertyChanged(); }
        }

        private Meal? meal;
        public Meal? Meal
        {
            get { return meal; }
            set { meal = value; OnPropertyChanged(); }
        }

        private int ingredientId;
        public int IngredientId
        {
            get { return ingredientId; }
            set { ingredientId = value; OnPropertyChanged(); }
        }

        private Ingredient? ingredient;
        public Ingredient? Ingredient
        {
            get { return ingredient; }
            set { ingredient = value; OnPropertyChanged(); }
        }


        private uint quantity;
        public uint Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged(); }
        }

        public decimal Amount { get => Ingredient == null ? 0.0m : Quantity * Ingredient.Price; }
    }
}
