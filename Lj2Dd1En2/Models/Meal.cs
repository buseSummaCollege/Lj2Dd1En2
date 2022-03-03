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

        private string? description;

        public string? Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }
    }
}
