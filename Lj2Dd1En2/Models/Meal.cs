using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lj2Dd1En2.Models
{
    public class Meal : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private int mealId;

        public int MealId
        {
            get { return mealId; }
            set { mealId = value; OnPropertyChanged(); }
        }

        private string? name;

        public string? Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        public string? Description {
            get =>
                MealIngredients == null || MealIngredients.Count == 0
                ? null
                : MealIngredients
                    .Select(x => $"{x.Quantity} {x.Ingredient?.Unit?.Name} {x?.Ingredient?.Name}")
                    .Aggregate((x, y) => $"{x}, {y}");
        }

        public ICollection<MealIngredient>? MealIngredients { get; set; }

        public decimal Price { get => MealIngredients == null ? 0 : MealIngredients.Sum(x => x.Amount); }
    }
}
