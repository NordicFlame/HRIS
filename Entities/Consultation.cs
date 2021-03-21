using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Author: Mikael Ekvall (419133), Douglas Jones (446502), Nicholas King (172002)
 * 
 * Description: This class "Consultation" creates and instatiates
 * all the required variables with getters and setters.
 * 
 * Date: 15/10/2018
 */

namespace HRIS
{
    class Consultation
    {
        //This defines an integer for Staff id with a getter and setter. 
        public int Staff_id { get; set; }

        /*This uses type DayOfWeek to store which day of the week a 
         * consultation session is held on/
         */ 
        public DayOfWeek Day { get; set; }

        /*The two methods below use type TimeSpan to store the start
         * and end times of a consultation session.
         */
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        // This public override wraps everything up to nicely output a string.
        public override string ToString()
        {
            return Staff_id + ": " + Day + "(" + Start + " - " + End + ")";
        }

    }
}
