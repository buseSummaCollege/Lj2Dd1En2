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
        #region Messages
        public static readonly string UNKNOWN = "Unknown";
        public static readonly string OK = "Ok";
        public static readonly string NOTFOUND = "not found";
        #endregion

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
                        SELECT m.mealId, m.name
                        FROM meals m
                        ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Meal meal = new Meal()
                        {
                            MealId = (int)reader["mealId"],
                            Name = (string)reader["name"],
                        };
                        meal.MealIngredients = new List<MealIngredient>();
                        GetMealIngredientsByMeal(meal.MealId, meal.MealIngredients);

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
                        SELECT i.ingredientId, i.name, i.price, i.unitId, u.name as unitName
                        FROM ingredients i
                        INNER JOIN units u ON u.unitId = i.unitId
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
                            UnitId = (int)reader["unitId"],
                            Unit = new Unit()
                            {
                                UnitId = (int)reader["unitId"],
                                Name = (string)reader["unitName"],

                            }
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

        // GetIngredient leest 1 rij in uit de databasetabel Ingredients. Wordt er een rij gevonden, dan
        // worden de gegevens hiervan in de output parameter ingredient gezet. 
        // Parameters:
        // - ingredientId   : Id van het in te lezen ingredient
        // - ingredient(o)  : null = niet gevonden
        //                    anders nieuw ingredient object met de database gegevens
        // De waarde van GetIngredient:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding, als er wel fouten waren 
        public string GetIngredient(int ingredientId, out Ingredient? ingredient)
        {
            ingredient = null;
            string methodResult = UNKNOWN;

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                SELECT i.ingredientId, i.name, i.price, i.unitId, u.name as unitName
                FROM ingredients i
                INNER JOIN units u ON u.unitId = i.unitId
                WHERE i.ingredientId = @ingredientId;
                ";
                    sql.Parameters.AddWithValue("@ingredientId", ingredientId);
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        ingredient = new()
                        {
                            IngredientId = (int)reader["ingredientId"],
                            Name = (string)reader["name"],
                            Price = (decimal)reader["price"],
                            UnitId = (int)reader["unitId"],
                            Unit = new Unit()
                            {
                                UnitId = (int)reader["unitId"],
                                Name = (string)reader["unitName"],

                            }
                        };
                    }

                    methodResult = ingredient == null ? NOTFOUND : OK;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        // CreateIngredient voegt het ingredient object uit de parameter toe aan de database. 
        // Het ingredient object moet aan alle database eisen voldoen. De waarde van CreateIngredient:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string CreateIngredient(Ingredient ingredient)
        {
            if (ingredient == null || string.IsNullOrEmpty(ingredient.Name)
                || ingredient.Price < 0 || ingredient.UnitId == 0)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van CreateIngredient");
            }

            string methodResult = UNKNOWN;

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                    INSERT INTO ingredients 
                            (ingredientId,  name,  price,  unitId) 
                    VALUES  (NULL,         @name, @price, @unitId);
                    ";
                    sql.Parameters.AddWithValue("@name", ingredient.Name);
                    sql.Parameters.AddWithValue("@price", ingredient.Price);
                    sql.Parameters.AddWithValue("@unitId", ingredient.UnitId);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingrediënt {ingredient.Name} kon niet toegevoegd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(CreateIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        // UpdateIngredient wijzigt het ingredient met id ingredientId (parameter) met de gegevens uit
        // de parameter ingredient. De gegevens van ingredient moeten aan alle database eisen voldoen.
        // De waarde van UpdateIngredient:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string UpdateIngredient(int ingredientId, Ingredient ingredient)
        {
            if (ingredient == null || string.IsNullOrEmpty(ingredient.Name)
                || ingredient.Price < 0 || ingredient.UnitId == 0)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van UpdateIngredient");
            }

            string methodResult = UNKNOWN;

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        UPDATE ingredients
                        SET name = @name, 
                            price = @price,
                            unitId = @unitId
                        WHERE ingredientId = @ingredientId;
                        ";
                    sql.Parameters.AddWithValue("@ingredientId", ingredientId);
                    sql.Parameters.AddWithValue("@name", ingredient.Name);
                    sql.Parameters.AddWithValue("@price", ingredient.Price);
                    sql.Parameters.AddWithValue("@unitId", ingredient.UnitId);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingredient {ingredient.Name} kon niet gewijzigd worden.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(UpdateIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        // DeleteIngredient verwijdert het ingredient met de id ingredientId uit de database. De waarde
        // van DeleteIngredient :
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string DeleteIngredient(int ingredientId)
        {
            string methodResult = UNKNOWN;

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        DELETE 
                        FROM ingredients 
                        WHERE ingredientId = @ingredientId 
                    ";
                    sql.Parameters.AddWithValue("@ingredientId", ingredientId);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingredient met id {ingredientId} kon niet verwijderd worden.";
                    }

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(DeleteIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion

        #region Unit
        // GetUnits leest alle rijen in uit de databasetabel Units en voegt deze toe aan een ICollection. 
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetUnits:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding, als er wel fouten waten (mogelijk zijn niet alle maaltijden ingelezen)
        public string GetUnits(ICollection<Unit> units)
        {
            if (units == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetUnits");
            }

            string methodResult = UNKNOWN;


            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        SELECT u.unitId, u.name
                        FROM units u
                        ORDER BY u.name
                    ";
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        Unit unit = new()
                        {
                            UnitId = (int)reader["unitId"],
                            Name = (string)reader["name"],
                        };
                        units.Add(unit);
                    }
                    methodResult = OK;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetUnits));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion

        #region MealIngredient
        // GetMealIngredientsByMeal leest alle rijen in uit de databasetabel MealIngredients en voegt deze toe aan
        // een ICollection. Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetMealIngredientsByMeal:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding, als er wel fouten waten (mogelijk zijn niet alle maaltijden ingelezen)
        public string GetMealIngredientsByMeal(int mealId, ICollection<MealIngredient> mealIngredients)
        {
            if (mealIngredients == null)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van GetMealIngredientsByMeal");
            }

            string methodResult = UNKNOWN;


            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                            SELECT mi.mealIngredientId, mi.mealId, mi.ingredientId, mi.quantity, 
                                   i.name, i.price, i.unitId, 
                                   u.name as 'UnitName'
                            FROM mealingredients mi
                            INNER JOIN ingredients i ON i.ingredientId = mi.ingredientId
                            INNER JOIN units u ON u.unitId = i.unitId
                            WHERE mi.mealId = @mealId
                        ";
                    sql.Parameters.AddWithValue("@mealId", mealId);
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        MealIngredient mealIngredient = new ()
                        {
                            MealIngredientId = (int)reader["mealIngredientId"],
                            MealId = (int)reader["mealId"],
                            IngredientId = (int)reader["ingredientId"],
                            Quantity = (uint)reader["quantity"],
                            Ingredient = new()
                            {
                                IngredientId = (int)reader["ingredientId"],
                                Name = (string)reader["name"],  
                                Price = (decimal)reader["price"],
                                UnitId = (int)reader["unitId"],
                                Unit = new()
                                {
                                    UnitId = (int)reader["unitId"],
                                    Name = (string)reader["UnitName"]
                                }
                            }
                        };
                        mealIngredients.Add(mealIngredient);
                    }
                    methodResult = OK;

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(GetMealIngredientsByMeal));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        // CreateMealIngredient voegt het mealingredient object uit de parameter toe aan de database. 
        // Het mealingredient object moet aan alle database eisen voldoen. De waarde van CreateMealIngredient:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string CreateMealIngredient(MealIngredient mealIngredient)
        {
            if (mealIngredient == null 
                || mealIngredient.Quantity == 0 
                || mealIngredient.MealId == 0 
                || mealIngredient.IngredientId == 0)
            {
                throw new ArgumentException("Ongeldig argument bij gebruik van CreateMealIngredient");
            }

            string methodResult = UNKNOWN;

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        INSERT INTO mealingredients
		                        (mealIngredientId,  mealId,  ingredientId,  quantity) 
                        VALUES  (NULL,             @mealId, @ingredientId, @quantity) 
                    ";
                    sql.Parameters.AddWithValue("@mealId", mealIngredient.MealId);
                    sql.Parameters.AddWithValue("@ingredientId", mealIngredient.IngredientId);
                    sql.Parameters.AddWithValue("@quantity", mealIngredient.Quantity);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Ingrediënt {mealIngredient.IngredientId} kon niet toegevoegd " +
                            $"worden aan maaltijd {mealIngredient.MealId}.";
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(CreateMealIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }

        // DeleteMealIngredient verwijdert het mealingredient met de id mealingredientId uit de database. De waarde
        // van DeleteMealIngredient :
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string DeleteMealIngredient(int mealIngredientId)
        {
            string methodResult = UNKNOWN;

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = @"
                        DELETE FROM mealingredients
                        WHERE mealIngredientId = @mealIngredientId
                    ";
                    sql.Parameters.AddWithValue("@mealIngredientId", mealIngredientId);
                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = $"Maaltijdingredient met id {mealIngredientId} kon niet verwijderd worden.";
                    }

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(DeleteMealIngredient));
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult;
        }
        #endregion
    }
}
