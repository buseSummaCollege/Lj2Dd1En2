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
        private readonly string connString = ConfigurationManager.ConnectionStrings["Lj2Dd1En2Conn"].ConnectionString;
    }
}
