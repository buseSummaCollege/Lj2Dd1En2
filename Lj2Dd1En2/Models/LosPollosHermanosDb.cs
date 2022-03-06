using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using System.Configuration;

namespace Lj2Dd1En2.Models
{

    public class LosPollosHermanosDb
    {
        public static readonly string UNKNOWN = "Unknown";
        public static readonly string OK = "Ok";

        private readonly string connString = ConfigurationManager.ConnectionStrings["Lj2Dd1En2Conn"].ConnectionString;

        #region Meals
        // GetMeals leest alle rijen in uit de databasetabel Meals en voegt deze toe aan een ICollection. 
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetMeals:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding, als er wel fouten waten (mogelijk zijn niet alle maaltijden ingelezen)
        public string GetMeals(ICollection<Meal> meals)
        {
            if (meals == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetMeals");
            }

            string methodResult = UNKNOWN;


            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT m.mealId, m.name, m.description, m.price
                        FROM meals m
                        ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Meal meal = new Meal()
                        {
                            MealId = (int)reader["mealId"],
                            Name = (string)reader["name"],
                            Description = reader["description"] == DBNull.Value
                                            ? null
                                            : (string)reader["description"],
                            Price = (decimal)reader["price"],
                        };

                        meals.Add(meal);
                    }
                    methodResult = OK;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetMeals));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion

        #region Ingredients
        // GetIngredients leest alle rijen in uit de databasetabel Ingredients en voegt deze toe aan een ICollection. 
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetIngredients:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding, als er wel fouten waten (mogelijk zijn niet alle maaltijden ingelezen)
        public string GetIngredients(ICollection<Ingredient> ingredients)
        {
            if (ingredients == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetIngredients");
            }

            string methodResult = UNKNOWN;


            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT i.ingredientId, i.name, i.price, i.unit
                        FROM ingredients i
                        ORDER BY i.name
                    ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Ingredient ingredient = new()
                        {
                            IngredientId = (int)reader["ingredientId"],
                            Name = (string)reader["name"],
                            Price = (decimal)reader["price"],
                            Unit = (string)reader["unit"]
                        };

                        ingredients.Add(ingredient);
                    }
                    methodResult = OK;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetIngredients));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
    }
}
