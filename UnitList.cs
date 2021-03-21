using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS
{
    class UnitList
    {
        public List<Unit> GetUnits()
        {
            List<Unit> e = new List<Unit>();

            Unit f1 = new Unit
            {
                Code = "106",
                Title = "Assignment Writing and boredom generation",
                Coordinator = 1
            };

            e.Add(f1);


            return e;
        }
    }
}
