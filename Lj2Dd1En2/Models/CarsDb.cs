using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Data;


namespace Lj2Dd1En2.Models
{
    public class CarsDb
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;database=carsdb;uid=root;pwd=;");

        // Method GetCars leest alle autos uit de databasetabel cars in en zet deze in een DataTable.
        // De waarde van GetCars is:
        // - null: Er is trad een onverwachte fout (esception) op.
        // - !null: De DataTable, gevuld met gegevens over de autos.
        public DataTable? GetCars()
        {
            DataTable? carTable = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand sql = conn.CreateCommand();
                sql.CommandText =
                    @"
                        SELECT c.carId, c.make, c.picture, c.yearOfIntroduction
                        FROM cars c                    
                    ";
                MySqlDataReader carReader = sql.ExecuteReader();
                carTable.Load(carReader);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                carTable = null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return carTable;
        }

        // Method GetCar leest uit de database tabel cars, de gegevens in van ed auto met de opgegeven id
        // De waarde van GetCar is:
        // - null: Er is trad een onverwachte fout (esception) op.
        // - !null: De DataTable, gevuld met gegevens over de auto
        // NB: werd de auto niet gevonden, dan is de DataTable leeg
        public DataTable? GetCar(int id)
        {
            DataTable? carTable = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand sql = conn.CreateCommand();
                sql.CommandText =
                    @"
                        SELECT c.carId, c.make, c.picture, c.yearOfIntroduction
                        FROM cars c
                        WHERE carId = @carId;
                    ";
                sql.Parameters.AddWithValue("@carId", id);
                MySqlDataReader carReader = sql.ExecuteReader();
                carTable.Load(carReader);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                carTable = null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return carTable;
        }

        // Method CreateCar voegt de gegevens van een auto toe aan de database.
        // De waarde van CreateCar is:
        // - true: de gegevens zijn toegevoegd
        // - false: de gegevens zijn niet toegevoegd, mogelijk door een exception
        public bool CreateCar(string make, byte[]? picture, int yearOfIntroduction)
        {
            bool toevoegenGelukt = false;
            try
            {
                conn.Open();
                MySqlCommand sql = conn.CreateCommand();
                sql.CommandText =
                    @"
                        INSERT INTO cars (carId, make, picture, yearOfIntroduction) 
                        VALUES (NULL, @make, @picture, @yearOfIntroduction)
                    ";
                sql.Parameters.AddWithValue("@make", make);
                sql.Parameters.AddWithValue("@picture", picture);
                sql.Parameters.AddWithValue("@yearOfIntroduction", yearOfIntroduction);

                int rowsAffected = sql.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    toevoegenGelukt = true;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return toevoegenGelukt;
        }

        // Method UpdateCar wijzigt de databasegegevens van de auto met id carId.
        // De waarde van UpdateCar:
        // - true: wijzigen is goed gegaan
        // - false: wijzigen is niet goed gegaan of een exception. 
        public bool UpdateCar(int carId, string make, byte[]? picture, int yearOfIntroduction)
        {
            bool wijzigenGelukt;

            try
            {
                conn.Open();
                MySqlCommand sql = conn.CreateCommand();
                sql.CommandText =
                    @"
                        UPDATE cars 
                        SET make = @make, 
                            picture = @picture,
                            yearOfIntroduction = @yearOfIntroduction
                        WHERE carId = @carId;
                    ";
                sql.Parameters.AddWithValue("@carId", carId);
                sql.Parameters.AddWithValue("@make", make);
                sql.Parameters.AddWithValue("@picture", picture);
                sql.Parameters.AddWithValue("@yearOfIntroduction", yearOfIntroduction);

                int rowsAffected = sql.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    wijzigenGelukt = true;
                }
                else
                {
                    wijzigenGelukt = false;
                    Console.Error.WriteLine($"Auto met id {carId} niet in database aanwezig");
                }

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                wijzigenGelukt = false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return wijzigenGelukt;
        }

        // Method DeleteCar verwijdert de databasegegevens van de auto met id carId.
        // De waarde van DeleteCar :
        // - true: verwijderen is goed gegaan
        // - false: verwijderen is niet goed gegaan of een exception. 
        public bool DeleteCar(int carId)
        {
            bool verwijderenGelukt;

            try
            {
                conn.Open();
                MySqlCommand sql = conn.CreateCommand();
                sql.CommandText =
                    @"
                        DELETE FROM cars 
                        WHERE carId = @carId;
                    ";
                sql.Parameters.AddWithValue("@carId", carId);

                if (sql.ExecuteNonQuery() == 1)
                {
                    verwijderenGelukt = true;
                }
                else
                {
                    verwijderenGelukt = false;
                    Console.Error.WriteLine($"Auto met id {carId} niet in database aanwezig");
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                verwijderenGelukt = false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return verwijderenGelukt;
        }
    }
}
