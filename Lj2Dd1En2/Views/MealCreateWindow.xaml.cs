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
    /// Interaction logic for MealCreateWindow.xaml
    /// </summary>
    public partial class MealCreateWindow : Window, INotifyPropertyChanged
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
        private Meal? newMeal = new();
        public Meal? NewMeal
        {
            get { return newMeal; }
            set { newMeal = value; OnPropertyChanged(); }
        }
        #endregion

        public MealCreateWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewMeal.Name))
            {
                MessageBox.Show("Vul naam van de maaltijd in");
                return;
            }

            string resultaat = db.CreateMeal(NewMeal);
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
