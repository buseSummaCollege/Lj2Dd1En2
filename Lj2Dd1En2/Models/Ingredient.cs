using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lj2Dd1En2.Models
{
    public class Ingredient : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private int ingredientId;

        public int IngredientId
        {
            get { return ingredientId; }
            set { ingredientId = value; OnPropertyChanged(); }
        }

        private string? name;

        public string? Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }

        private int unitId;

        public int UnitId
        {
            get { return unitId; }
            set { unitId = value; OnPropertyChanged(); }
        }

        private Unit? unit;

        public Unit? Unit {
            get { return unit; }
            set { unit = value; OnPropertyChanged(); }
        }
    }
}
