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

namespace Lj2Dd1En2.Views
{
    /// <summary>
    /// Interaction logic for KeuzeWindow.xaml
    /// </summary>
    public partial class KeuzeWindow : Window
    {
        public KeuzeWindow()
        {
            InitializeComponent();
        }

        private void BtnDisplayWindow_Click(object sender, RoutedEventArgs e)
        {
            new DisplayWindow().Show();
            this.Close();
        }

        private void BtnMaaltijdenWindow_Click(object sender, RoutedEventArgs e)
        {
            new MealWindow().Show();
            this.Close();
        }

        private void BtnIngredientenWindow_Click(object sender, RoutedEventArgs e)
        {
            new IngredientWindow().Show();
            this.Close();
        }
    }
}
