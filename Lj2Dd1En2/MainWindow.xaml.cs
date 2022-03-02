using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;
using Microsoft.Win32;
using System.IO;

namespace Lj2Dd1En2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection MonteurVanAuto = new("server=localhost;database=carsdb;uid=root;pwd=;");
        byte[]? nieuw;
        byte[]? wijzig;

        public MainWindow()
        {
            InitializeComponent();
            lezen();
        }


        private void lezen()
        {
            DataTable t = new();
            MonteurVanAuto.Open();
            MySqlCommand hulp = MonteurVanAuto.CreateCommand();
            hulp.CommandText = "select * from cars order by make;";
            t.Load(hulp.ExecuteReader());
            LbAutos.ItemsSource = t.DefaultView;
            MonteurVanAuto.Close();
        }

        // Eventhandler zet de gegevens van de auto die in de ListBox is geselecteerd, in de controls 
        // die bedoeld zijn om te wijzigen. Is er geen auto geselecteerd, worden de controles leeggemaakt
        private void LbAutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LbAutos.SelectedItem == null)
            {
                return;
            }

            DataRowView auto = (DataRowView)LbAutos.SelectedItem;

            tbauto.Text = auto["make"].ToString();
            TbJaar.Text = auto["yearOfIntroduction"].ToString();

            if (auto["picture"] == DBNull.Value)
            {
                wijzig = null;
                imgAfbeelding1.Source = null;
            }
            else
            {
                wijzig = (byte[])auto["picture"];
                imgAfbeelding1.Source = new ImageSourceConverter().ConvertFrom(wijzig) as ImageSource;
            }
        }


        // Eventhandeler voegt een nieuwe auto toe aan de database
        private void bewaar_Click(object sender, RoutedEventArgs e)
        {
            MonteurVanAuto.Open();
            MySqlCommand sql = MonteurVanAuto.CreateCommand();
            sql.CommandText =
                $"INSERT INTO cars(carId,  make,  picture, yearOfIntroduction) " +
                $"         VALUES ( null, @make, @picture, {TbJaar1.Text});";
            sql.Parameters.AddRange(
                new[]   {
                    new MySqlParameter(){ ParameterName = "@make", Value = TbMerk2.Text},
                    new MySqlParameter(){ ParameterName = "@picture", Value = nieuw, }
                    }
                );
            sql.ExecuteReader();
            MonteurVanAuto.Close();

            lezen();
        }

        private void ok1_Click(object sender, RoutedEventArgs e)
        {
            if (LbAutos.SelectedItem == null)
            {
                MessageBox.Show("U heeft een auto geselecteerd");
                return;
            }
            if (string.IsNullOrEmpty(tbauto.Text))
            {
                MessageBox.Show("Vul het merk van de auto in.");
                return;
            }
            if (string.IsNullOrEmpty(TbJaar.Text))
            {
                MessageBox.Show("Vul het introductiejaar van de auto in.");
                return;
            }

            if (!int.TryParse(TbJaar.Text, out int jaar) || jaar < 1769)
            {
                MessageBox.Show("Het introductiejaar mag alleen uit cijfers bestaan en mag niet voor 1769 liggen.");
                return;
            }

            try
            {
                DataTable t = new();
                MonteurVanAuto.Open();
                MySqlCommand hulp = MonteurVanAuto.CreateCommand();
                hulp.CommandText =
                    $"UPDATE cars SET make= '{tbauto.Text}',picture = @picture, yearOfIntroduction = {TbJaar.Text} " +
                    $" WHERE carId = {LbAutos.SelectedValue};";
                hulp.Parameters.AddWithValue("@picture", wijzig);
                t.Load(hulp.ExecuteReader());
                LbAutos.ItemsSource = t.DefaultView;
                MonteurVanAuto.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Er is iets fout gegaan");
            }
            finally
            {
                new MainWindow().Show();
                Close();
            }
        }

        private void nok_Click(object sender, RoutedEventArgs e)
        {
            if (LbAutos.SelectedItem == null)
            {
                MessageBox.Show("Selecteer eerst de auto die u wilt verwijderen.");
                return;
            }

            try
            {
                MonteurVanAuto.Open();
                MySqlCommand hulp = MonteurVanAuto.CreateCommand();
                hulp.CommandText = $"DELETE FROM cars WHERE carId = " + LbAutos.SelectedValue;
                hulp.ExecuteNonQuery();
                MonteurVanAuto.Close();
            }
            catch (Exception)
            {
                throw;
            }

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
        }

        private void BtnAfbeelding1_Click(object sender, RoutedEventArgs e)
        {
            wijzig = GetLocalPicture();
            if (wijzig != null)
            {
                imgAfbeelding1.Source =
                    new ImageSourceConverter().ConvertFrom(wijzig) as ImageSource;
            }
            else
            {
                imgAfbeelding1.Source = null;
            }
        }

        private void BtnAfbeelding2_Click(object sender, RoutedEventArgs e)
        {
            nieuw = GetLocalPicture();
            if (nieuw != null)
            {
                imgAfbeelding2.Source =
                    new ImageSourceConverter().ConvertFrom(nieuw) as ImageSource;
            }
            else
            {
                imgAfbeelding2.Source = null;
            }
        }

        #region GetLocalPicture
        // GetLocalPicture leest een afbeelding op je computer in een array van byte in.
        // GetLocalPicture heef de volgende waarden:
        // - null: geen afbeelding ingelezen
        // - ongelijk null: de ingelezen afbeelding
        private byte[]? GetLocalPicture()
        {
            // Create OpenFileDialog 
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            dlg.FilterIndex = 3;    // 3de blok, is jpg

            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                return File.ReadAllBytes(dlg.FileName);
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
