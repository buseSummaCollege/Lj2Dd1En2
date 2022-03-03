using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lj2Dd1En2.Models
{
    public class Unit
    {
        private int unitId;

        public int UnitId
        {
            get { return unitId; }
            set { unitId = value; }
        }

        private string? name;

        public string? Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
