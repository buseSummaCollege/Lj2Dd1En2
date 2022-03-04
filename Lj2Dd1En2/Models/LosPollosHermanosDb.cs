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
        private static readonly string ILLEGALARGUMENT = "Ongeldig argument";
        private static readonly string UPDATERROR = "fout bij bijwerken van database rijen";
        #endregion

        private readonly string connString = ConfigurationManager.ConnectionStrings["Lj2Dd1En2Conn"].ConnectionString;

        #region General Purpose SQL Methods
        // Call back method om een Reader-row in een nieuw object van de class T te zetten:
        private delegate T EntityToClass<T>(MySqlDataReader reader);

        // Method ReadObjects verwerkt een SQL SELECT statement en zorgt ervoor dat alle gelezen
        // rijen in een ICollection worden gezet.
        // De method kent de volgende parameters 
        // - objectList   : objectList: Een Typed ICollection. Het type is een Generic
        // - sqlCommand   : het SQL statement dat gebruikt moet worden
        // - sqlParameters: (optioneel)
        // - entityToClass: call back functie om de database rij naar een class object om te zetten
        //
        // Er zijn 2 overloads: 1 met SQL-parameters en 1 zonder SQL-parameters
        private string ReadObjects<T>(ICollection<T> objectList, string sqlCommand, EntityToClass<T> entityToClass)
        {
            return ReadObjects(objectList, sqlCommand, null, entityToClass);
        }
        private string ReadObjects<T>(ICollection<T> objectList, string sqlCommand,
            MySqlParameter[]? sqlParameters, EntityToClass<T> entityToClass)
        {
            if (objectList == null)
            {
                throw new ArgumentException(ILLEGALARGUMENT);
            }
            string methodResult = "";

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = sqlCommand;
                    if (sqlParameters != null)
                    {
                        sql.Parameters.AddRange(sqlParameters);
                    }
                    MySqlDataReader reader = sql.ExecuteReader();

                    while (reader.Read())
                    {
                        objectList.Add(entityToClass(reader));
                    }
                    methodResult = OK;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(ReadObjects));
                    Console.Error.WriteLine(sqlCommand);
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult != "" ? methodResult : OK;
        }

        // Method ReadObject verwerkt een SQL SELECT statement en zorgt ervoor dat de eerste gelezen
        // rij in een nieuw T object wordt gezet.
        // De method kent de volgende parameters 
        // - t            : object van het type T. Het type is een Generic
        // - sqlCommand   : het SQL statement dat gebruikt moet worden
        // - sqlParameters: (optioneel)
        // - entityToClass: call back functie om de database rij naar een class object om te zetten
        //
        // Er zijn geen overloads: om ervoor te zorgen dat de method ook zonder SQL-parameters
        // gebruikt kan worden, is hier voor de SQL-parameters een default waarde null gebruikt
        private string ReadObject<T>(string sqlCommand, out T t, EntityToClass<T> entityToClass,
            MySqlParameter[]? sqlParameters = null)
        {
            string methodResult = "";
            t = default(T);             // Instantieer t op null; null mag niet

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = sqlCommand;
                    if (sqlParameters != null)
                    {
                        sql.Parameters.AddRange(sqlParameters);
                    }
                    MySqlDataReader reader = sql.ExecuteReader();

                    if (reader.Read())
                    {
                        t = entityToClass(reader);
                    }
                    methodResult = OK;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(ReadObjects));
                    Console.Error.WriteLine(sqlCommand);
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult != "" ? methodResult : OK;
        }

        // Method CreateUpdateOrDeleteObject verwerkt een UPDATE of DELETE statement. Veiligheidhalve is het 
        // verplicht om SQL parameters mee te geven die in de WHERE conditie gebruikt worden.
        // De method kent de volgende parameters 
        // - sqlCommand   : het SQL statement dat gebruikt moet worden
        // - sqlParameters: (optioneel)
        private string CreateUpdateOrDeleteObject(string sqlCommand, MySqlParameter[]? sqlParameters = null)
        {
            string methodResult = "";

            using (MySqlConnection conn = new(connString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand sql = conn.CreateCommand();
                    sql.CommandText = sqlCommand;
                    sql.Parameters.AddRange(sqlParameters);

                    if (sql.ExecuteNonQuery() == 1)
                    {
                        methodResult = OK;
                    }
                    else
                    {
                        methodResult = UPDATERROR;
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(nameof(ReadObjects));
                    Console.Error.WriteLine(sqlCommand);
                    Console.Error.WriteLine(e.Message);
                    methodResult = e.Message;
                }
            }
            return methodResult != "" ? methodResult : OK;
        }
        #endregion

        #region Meals
        // GetMeals leest alle rijen in uit de databasetabel Meals en voegt deze toe aan een ICollection. 
        // Als de ICollection bij aanroep null is, volgt er een ArgumentException
        // De waarde van GetMeals:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding, als er wel fouten waten (mogelijk zijn niet alle maaltijden ingelezen)
        public string GetMeals(ICollection<Meal> meals)
        {
            string sqlCommand = @"
                SELECT m.mealId, m.name
                FROM meals m
                ";

            return ReadObjects<Meal>(
                meals,                          // De lijst met Meals
                sqlCommand,                     // Het SQL Statement
                reader =>                       // Conversie van database Entiteit naar een Object
                {
                    Meal meal = new()           
                    {
                        MealId = (int)reader["mealId"],
                        Name = (string)reader["name"],
                    };
                    meal.MealIngredients = new List<MealIngredient>();
                    GetMealIngredientsByMeal(meal.MealId, meal.MealIngredients);
                  
                    return meal;
                }
                );
        }

        // GetMeal leest 1 rij in uit de databasetabel Meals. De waarde van GetMeal:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding, als er wel fouten waren 
        public string GetMeal(int mealId, out Meal meal)
        {
            string sqlCommand = @"
                    SELECT m.mealId, m.name
                    FROM meals m
                    WHERE m.mealId = @mealId;
                ";
            MySqlParameter[] sqlParameters = {
                new MySqlParameter("@mealId", mealId)
                };

            return ReadObject<Meal>(
                sqlCommand, 
                out meal,
                reader =>
                {
                    Meal meal = new()
                    {
                        MealId = (int)reader["mealId"],
                        Name = (string)reader["name"],
                    };
                    meal.MealIngredients = new List<MealIngredient>();
                    GetMealIngredientsByMeal(meal.MealId, meal.MealIngredients);
                    
                    return meal;
                },
                sqlParameters);
        }

        // CreateMeal voegt een maaltijd object toe aan de database. Het maaltijd object is
        // een parameter van de method. Deze moet aan alle database eisen voldoen.
        // De waarde van CreateMeal:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string CreateMeal(Meal meal)
        {
            string sqlCommand = @"
                    INSERT INTO meals  (mealId,  name) 
                    VALUES             (NULL,   @name) 
                ";
            MySqlParameter[] sqlParameters = {
                new MySqlParameter("@name", meal.Name),
                };
            return CreateUpdateOrDeleteObject(sqlCommand, sqlParameters);
        }

        // UpdateMeal wijzigt in de database tabel Meals de maaltijd met de id mealId. De gegevens
        // worden overgenomen uit het object meal. De parameter meal moet aan alle database eisen
        // voldoen.
        // De waarde van UpdateMeal:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string UpdateMeal(int mealId, Meal meal)
        {
            string sqlCommand = @"
                        UPDATE meals 
                        SET name = @name
                        WHERE mealId = @mealId;
                ";
            MySqlParameter[] sqlParameters = {
                new MySqlParameter("@mealId", mealId),
                new MySqlParameter("@name", meal.Name),
                };

            return CreateUpdateOrDeleteObject(sqlCommand, sqlParameters);
        }

        // DeleteMeal verwijdert de maaltijd met id mealId uit de database. De waarde van DeleteMeal:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string DeleteMeal(int mealId)
        {
            string sqlCommand = @"DELETE FROM meals WHERE mealId = @mealId";
            MySqlParameter[] sqlParameters = { new MySqlParameter("@mealId", mealId) };
            return CreateUpdateOrDeleteObject(sqlCommand, sqlParameters);
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
            string sqlCommand = @"
                    SELECT i.ingredientId, i.name, i.price, i.unitId, u.name as unitName
                    FROM ingredients i
                    INNER JOIN units u ON u.unitId = i.unitId
                    ORDER BY i.name
                    ";

            return ReadObjects<Ingredient>(ingredients, sqlCommand,
                reader => new ()
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
                }
                );
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
            string sqlCommand = @"
                SELECT i.ingredientId, i.name, i.price, i.unitId, u.name as unitName
                FROM ingredients i
                INNER JOIN units u ON u.unitId = i.unitId
                WHERE i.ingredientId = @ingredientId;
                ";

            MySqlParameter[] sqlParameters = {
                new MySqlParameter("@ingredientId", ingredientId)
                };

            return ReadObject<Ingredient>(sqlCommand, out ingredient,
                reader => new()
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
                },
                sqlParameters
                );
        }

        // CreateIngredient voegt het ingredient object uit de parameter toe aan de database. 
        // Het ingredient object moet aan alle database eisen voldoen. De waarde van CreateIngredient:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string CreateIngredient(Ingredient ingredient)
        {
            string sqlCommand = @"
                INSERT INTO ingredients 
                        (ingredientId,  name,  price,  unitId) 
                VALUES  (NULL,         @name, @price, @unitId);
                ";
            MySqlParameter[] sqlParameters = {
                new MySqlParameter("@name", ingredient.Name),
                new MySqlParameter("@price", ingredient.Price),
                new MySqlParameter("@unitId", ingredient.UnitId),
                };

            return CreateUpdateOrDeleteObject(sqlCommand, sqlParameters);
        }

        // UpdateIngredient wijzigt het ingredient met id ingredientId (parameter) met de gegevens uit
        // de parameter ingredient. De gegevens van ingredient moeten aan alle database eisen voldoen.
        // De waarde van UpdateIngredient:
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string UpdateIngredient(int ingredientId, Ingredient ingredient)
        {
            string sqlCommand = @"
                UPDATE ingredients
                SET name = @name, 
                    price = @price,
                    unitId = @unitId
                WHERE ingredientId = @ingredientId;
                ";
            MySqlParameter[] sqlParameters = {
                new MySqlParameter("@ingredientId", ingredientId),
                new MySqlParameter("@name", ingredient.Name),
                new MySqlParameter("@price", ingredient.Price),
                new MySqlParameter("@unitId", ingredient.UnitId),
                };

            return CreateUpdateOrDeleteObject(sqlCommand, sqlParameters);
        }

        // DeleteIngredient verwijdert het ingredient met de id ingredientId uit de database. De waarde
        // van DeleteIngredient :
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string DeleteIngredient(int ingredientId)
        {
            string sqlCommand = "DELETE FROM ingredients WHERE ingredientId = @ingredientId";
            MySqlParameter[] sqlParameters = {
                new MySqlParameter("@ingredientId", ingredientId),
                };

            return CreateUpdateOrDeleteObject(sqlCommand, sqlParameters);
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
            string sqlCommand = @"
                SELECT u.unitId, u.name
                FROM units u
                ORDER BY u.name
                ";

            return ReadObjects(units, sqlCommand,
                reader => new Unit()
                {
                    UnitId = (int)reader["unitId"],
                    Name = (string)reader["name"],
                });
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
            string sqlCommand = @"
                SELECT mi.mealIngredientId, mi.mealId, mi.ingredientId, mi.quantity, 
                        i.name, i.price, i.unitId, 
                        u.name as 'UnitName'
                FROM mealingredients mi
                INNER JOIN ingredients i ON i.ingredientId = mi.ingredientId
                INNER JOIN units u ON u.unitId = i.unitId
                WHERE mi.mealId = @mealId
                ";

            MySqlParameter[] sqlParameters = {
                new MySqlParameter("@mealId", mealId),
                };

            return ReadObjects<MealIngredient>(mealIngredients, sqlCommand, sqlParameters,
                reader => new MealIngredient()
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
                });
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

            string sqlCommand = @"
                INSERT INTO mealingredients
		                    (mealIngredientId,  mealId,  ingredientId,  quantity) 
                    VALUES  (NULL,             @mealId, @ingredientId, @quantity) 
                ";
            MySqlParameter[] sqlParameters = {
                new MySqlParameter("@mealId", mealIngredient.MealId),
                new MySqlParameter("@ingredientId", mealIngredient.IngredientId),
                new MySqlParameter("@quantity", mealIngredient.Quantity),
                };

            return CreateUpdateOrDeleteObject(sqlCommand, sqlParameters);
        }

        // DeleteMealIngredient verwijdert het mealingredient met de id mealingredientId uit de database. De waarde
        // van DeleteMealIngredient :
        // - "ok" als er geen fouten waren. 
        // - een foutmelding (de melding geeft aan wat er fout was)
        public string DeleteMealIngredient(int mealIngredientId)
        {
            string sqlCommand = "DELETE FROM mealingredients WHERE mealIngredientId = @mealIngredientId";
            MySqlParameter[] sqlParameters = {
                new MySqlParameter("@mealIngredientId", mealIngredientId),
                };

            return CreateUpdateOrDeleteObject(sqlCommand, sqlParameters);
        }
        #endregion
    }
}
