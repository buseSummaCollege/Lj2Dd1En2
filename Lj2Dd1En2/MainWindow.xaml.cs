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
using Lj2Dd1En2.Models;

namespace Lj2Dd1En2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region private fields
        private byte[]? afbeeldingNieuweAuto;
        private byte[]? afbeeldingBestaandeAuto;
        private readonly CarsDb db = new();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            PopulateLbCars();
        }

        // Method zet alle auto's uit de database in de ListBox lbCars.
        private void PopulateLbCars()
        {
            DataTable? carsTable = db.GetCars();
            if (carsTable == null)
            {
                MessageBox.Show("De auto's konden niet ingelezen worden. Waarschuw de service desk");
                return;
            }

            lbAutos.ItemsSource = carsTable.DefaultView;
        }

        // Eventhandler zet de gegevens van de auto die in de ListBox is geselecteerd, in de controls 
        // die bedoeld zijn om te wijzigen. Is er geen auto geselecteerd, worden de controles leeggemaakt
        private void LbAutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbAutos.SelectedItem == null)
            {
                tbJaarTeWijzigenAuto.Clear();
                tbMerkTeWijzigenAuto.Clear();
                imgAfbeeldingTeWijzigenAuto.Source = null;
                return;
            }

            DataRowView auto = (DataRowView)lbAutos.SelectedItem;

            tbMerkTeWijzigenAuto.Text = auto["make"].ToString();
            tbJaarTeWijzigenAuto.Text = auto["yearOfIntroduction"].ToString();

            if (auto["picture"] == DBNull.Value)
            {
                afbeeldingBestaandeAuto = null;
                imgAfbeeldingTeWijzigenAuto.Source = null;
            }
            else
            {
                afbeeldingBestaandeAuto = (byte[])auto["picture"];
                imgAfbeeldingTeWijzigenAuto.Source
                    = new ImageSourceConverter().ConvertFrom(afbeeldingBestaandeAuto) as ImageSource;
            }
        }

        // Method controleert de door de gebruiker ingevulde gegevens
        private bool ValidateCarValues(string merk, string jaar, out int introductiejaar)
        {
            introductiejaar = 0;
            bool waardenZijnGoed = false;

            if (string.IsNullOrEmpty(merk))
            {
                MessageBox.Show("Vul het merk van de auto in.");
            }
            else if (string.IsNullOrEmpty(jaar))
            {
                MessageBox.Show("Vul het introductiejaar van de auto in.");
            }
            else if (!int.TryParse(jaar, out introductiejaar) || introductiejaar < 1769)
            {
                MessageBox.Show("Het introductiejaar mag alleen uit cijfers bestaan en mag niet voor 1769 liggen.");
            }
            else
            {
                waardenZijnGoed = true;
            }
            return waardenZijnGoed;
        }

        // Eventhandeler voegt een nieuwe auto toe aan de database
        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateCarValues(tbMerkNieuweAuto.Text, tbJaarNieuweAuto.Text, out int introductiejaar))
            {
                if (db.CreateCar(tbMerkNieuweAuto.Text, afbeeldingNieuweAuto, introductiejaar))
                {
                    PopulateLbCars();
                    tbMerkNieuweAuto.Clear();
                    tbJaarNieuweAuto.Clear();
                    imgAfbeeldingNieuweAuto.Source = null;
                }
                else
                {
                    MessageBox.Show("De nieuwe auto kon niet worden toegevoegd. Neem contact op met de service desk");
                }
            }
        }

        // Eventhandeler wijzigt de gegevens van de geselecteerde auto in de database
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lbAutos.SelectedItem == null)
            {
                MessageBox.Show("Selecteer eerst de auto waarvan U de gegevens wilt wijzigen.");
                return;
            }
            if (ValidateCarValues(tbMerkTeWijzigenAuto.Text, tbJaarTeWijzigenAuto.Text, out int introductiejaar))
            {
                if (db.UpdateCar((int)lbAutos.SelectedValue, tbMerkTeWijzigenAuto.Text, afbeeldingBestaandeAuto, introductiejaar))
                {
                    PopulateLbCars();
                }
                else
                {
                    MessageBox.Show("De gegevens van de auto konden niet worden gewijzigd. Neem contact op met de service desk.");
                }
            }
        }

        // Eventhandeler verwijdert de gegevens van de geselecteerde auto uit de database
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbAutos.SelectedItem == null)
            {
                MessageBox.Show("Selecteer eerst de auto die u wilt verwijderen.");
                return;
            }

            if (db.DeleteCar((int)lbAutos.SelectedValue))
            {
                PopulateLbCars();
            }
            else
            {
                MessageBox.Show("De gegevens van de auto konden niet worden verwijderd. Neem contact op met de service desk.");
            }
        }

        // Method laat de gebruiker een afbeelding kiezen voor de te wijzigen auto. 
        private void BtnAfbeeldingTeWijzigenAuto_Click(object sender, RoutedEventArgs e)
        {
            afbeeldingBestaandeAuto = GetLocalPicture();
            if (afbeeldingBestaandeAuto != null)
            {
                imgAfbeeldingTeWijzigenAuto.Source =
                    new ImageSourceConverter().ConvertFrom(afbeeldingBestaandeAuto) as ImageSource;
            }
            else
            {
                imgAfbeeldingTeWijzigenAuto.Source = null;
            }
        }

        // Method laat de gebruiker een afbeelding kiezen voor de nieuw toe te voegen auto. 
        private void BtnAfbeeldingNieuweAuto_Click(object sender, RoutedEventArgs e)
        {
            afbeeldingNieuweAuto = GetLocalPicture();
            if (afbeeldingNieuweAuto != null)
            {
                imgAfbeeldingNieuweAuto.Source =
                    new ImageSourceConverter().ConvertFrom(afbeeldingNieuweAuto) as ImageSource;
            }
            else
            {
                imgAfbeeldingNieuweAuto.Source = null;
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
