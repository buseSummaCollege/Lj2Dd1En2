using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Lj2Dd1En2.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lj2Dd1En2.Views
{
    /// <summary>
    /// Interaction logic for MealUpdateWindow.xaml
    /// </summary>
    public partial class MealUpdateWindow : Window, INotifyPropertyChanged
    {
        
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region fields
        private readonly LosPollosHermanosDb db = new LosPollosHermanosDb();
        private readonly string serviceDeskBericht = "\n\nNeem contact op met de service desk";
        #endregion

        #region Properties
        private Meal existingMeal;
        public Meal ExistingMeal
        {
            get { return existingMeal; }
            set { existingMeal = value; OnPropertyChanged(); }
        }
        #endregion

        public MealUpdateWindow(int mealId)
        {
            InitializeComponent();
            string result = db.GetMeal(mealId, out existingMeal);
            if (result != LosPollosHermanosDb.OK)
            {
                MessageBox.Show(result);
            }
            DataContext = this;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ExistingMeal == null)
            {
                DialogResult = false;
                Close();
                return;
            }
            if (string.IsNullOrWhiteSpace(ExistingMeal.Name))
            {
                MessageBox.Show("Vul naam van de maaltijd in");
                return;
            }

            string resultaat = db.UpdateMeal(ExistingMeal.MealId, ExistingMeal);
            if (resultaat == LosPollosHermanosDb.OK)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show(resultaat + serviceDeskBericht);
            }
        }
    }
}
