using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Mikael Ekvall (419133), Douglas Jones (446502), Nicholas King (172002)
 * 
 * Description: The purpose of this class is to define all 
 * necessary variables and enums that will be used in the
 * "class" class. Everything will be stored in a list and 
 * used elsewhere.
 * 
 * Date: 15/10/2018
 */

namespace HRIS
{
    //Below is a public enum to define which type of session is being run.
    public enum Type { Tutorial, Lecture, Workshop }


    class Class
    {
        //Below are the Enum definitions with getter and setters.
        public Campus Campus { get; set; }
        public Type Type { get; set; }
        public DayOfWeek Day { get; set; }

        //Below are the String definitions with getter and setters.
        public string Unit_Code { get; set; }
        public string Room { get; set; }

        //Below is an int definition containing staff id number.
        public int Staff { get; set; }

        ////Below are the time definitions with getter and setters.
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        // This public override wraps everything up to nicely output a string.
        public override string ToString()
        {
            return Start + " - " + End + "; " + Type + Unit_Code + "; " + Room + "; " + Campus + "; " + Staff;
        }
    }
    
}
