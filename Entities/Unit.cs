using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Mikael Ekvall (419133), Douglas Jones (446502), Nicholas King (172002)
 * 
 * Description:The purpose of this class is to define all 
 * necessary variables and enums that will be used in the
 * "Unit" class. Everything will be stored in a list and 
 * used elsewhere.
 * 
 * Date: 15/10/2018
 */

namespace HRIS
{ 

    class Unit
    {
        //below are string variable definitions with getter and setters.
        public string Code { get; set; }
        public string Title { get; set; }
        //Below will contain the coordinators staff id.
        public int Coordinator { get; set; }

        public List<Class> UnitDetails { get; set; }

        //Template for showing unit list 
        public string UnitListShow
        {
            get
            {
                return string.Format("{0}: {1}", Code, Title);
            }
        }

        // This public override wraps everything up to nicely output a string.
        public override string ToString()
        {
            return Code + ": " + Title + ", " + Coordinator;
        }

    }
}
