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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lj2Dd1En2.Views
{
    /// <summary>
    /// Interaction logic for IngredientWindow.xaml
    /// </summary>
    public partial class IngredientWindow : Window, INotifyPropertyChanged
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
        private ObservableCollection<Unit> units = new();
        public ObservableCollection<Unit> Units
        {
            get { return units; }
            set { units = value; OnPropertyChanged(); }
        }
        private Unit? newIngredientUnit;
        public Unit? NewIngredientUnit
        {
            get { return newIngredientUnit; }
            set
            {
                newIngredientUnit = value;
                OnPropertyChanged();
                NewIngredient.UnitId = value == null ? 0 : value.UnitId;
            }
        }

        private Unit? existingIngredientUnit;
        public Unit? ExistingIngredientUnit
        {
            get { return existingIngredientUnit; }
            set
            {
                existingIngredientUnit = value;
                OnPropertyChanged();
                if (SelectedIngredient != null)
                {
                    SelectedIngredient.UnitId = value == null ? 0 : value.UnitId;
                }
            }
        }


        private ObservableCollection<Ingredient> ingredients = new();
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; OnPropertyChanged(); }
        }

        private Ingredient? selectedIngredient;
        public Ingredient? SelectedIngredient
        {
            get { return selectedIngredient; }
            set
            {
                selectedIngredient = value;
                // geen ingrediënt geselecteerd of Units property nog niet gevuld?
                if (value == null || Units == null)
                {
                    // er is nog geen geselecteerde unit
                    ExistingIngredientUnit = null;
                }
                else
                {
                    // zoek de unit op waarvan de Unit Id gelijk is aan de Unit id van het geselecteerde ingrediënt
                    ExistingIngredientUnit = Units.FirstOrDefault(x => x.UnitId == value.UnitId);
                }
                OnPropertyChanged();
            }
        }

        private Ingredient newIngredient = new();
        public Ingredient NewIngredient
        {
            get { return newIngredient; }
            set
            {
                newIngredient = value;
                OnPropertyChanged();
                NewIngredientUnit = null;   // Voor een nieuw ingredient is nog geen unit bekend
            }
        }
        #endregion

        public IngredientWindow()
        {
            InitializeComponent();

            PopulateUnits();
            PopulateIngredients();

            DataContext = this;
        }

        // Method zet alle units uit de database in de property Units zodat deze in de comboboxen
        // getoond worden.
        // Trad er een fout op bij het inlezen, wordt hiervan een melding getoond.
        private void PopulateUnits()
        {
            Units.Clear();
            string dbResult = db.GetUnits(Units);
            if (dbResult != LosPollosHermanosDb.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        // Method zet alle maaltijden uit de database op het scherm in de control lvMeals
        // Trad er een fout op bij het inlezen, wordt hiervan een melding getoond.
        private void PopulateIngredients()
        {
            Ingredients.Clear();
            string dbResult = db.GetIngredients(Ingredients);
            if (dbResult != LosPollosHermanosDb.OK)
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NewIngredient.Name))
            {
                MessageBox.Show("Vul naam van het ingrediënt in");
                return;
            }
            if (NewIngredient.UnitId == 0)
            {
                MessageBox.Show("Selecteer een eenheid.");
                return;
            }
            if (NewIngredient.Price < 0)
            {
                MessageBox.Show("Wijzig de prijs. Deze mag niet negatief zijn.");
                return;
            }

            string dbResult = db.CreateIngredient(NewIngredient);
            if (dbResult == LosPollosHermanosDb.OK)
            {
                NewIngredient = new();
                PopulateIngredients();
            }
            else
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedIngredient == null)
            {
                MessageBox.Show("Selecteer eerst het ingredient dat u wil wijzigen.");
                return;
            }
            if (string.IsNullOrEmpty(SelectedIngredient.Name))
            {
                MessageBox.Show("Vul naam van het ingrediënt in.");
                return;
            }
            if (SelectedIngredient.UnitId == 0)
            {
                MessageBox.Show("Selecteer een eenheid.");
                return;
            }
            if (SelectedIngredient.Price < 0)
            {
                MessageBox.Show("Wijzig de prijs. Deze mag niet negatief zijn.");
                return;
            }

            string dbResult = db.UpdateIngredient(SelectedIngredient.IngredientId, SelectedIngredient);
            if (dbResult == LosPollosHermanosDb.OK)
            {
                PopulateIngredients();
            }
            else
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btnDel = (Button)sender;
            Ingredient ingredient = (Ingredient)btnDel.DataContext;     

            string dbResult = db.DeleteIngredient(ingredient.IngredientId);
            if (dbResult == LosPollosHermanosDb.OK)
            {
                PopulateIngredients();
            }
            else
            {
                MessageBox.Show(dbResult + serviceDeskBericht);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            new KeuzeWindow().Show();
        }
    }
}

