using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

/* 
 * Author: Mikael Ekvall (419133), Douglas Jones (446502), Nicholas King (172002)
 * 
 * Description: The purpose of this class is to define all 
 * necessary variables and enums that will be used in the
 * "Staff" class. Everything will be stored in a list and 
 * used elsewhere.
 * 
 * Date: 3/10/18
 */

namespace HRIS
{

    //Below are enum variable definitions used within the Staff.cs, StaffController.cs and DbAdaptor.cs.
    public enum Category { All, Academic, Technical, Admin, Casual }
    public enum Campus { Both, Hobart, Launceston }


    class Staff
    {
        //below is an int variable definition with getter and setter.
        public int Id { get; set; }

        //below are string variable definitions with getter and setters.
        public string Given_name { get; set; }
        public string Family_name { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string RoomLocation { get; set; }
        public List<Consultation> Consult { get; set; }
        public List<Unit> Teaching { get; set; }

        //templates for showing staff list
        public string StaffListShow
        {
            get
            {
                return string.Format("{0}, {1} ({2})", Family_name, Given_name, Title);
            }
        }

        //getter and setter for enums.
        public Category Category { get; set; }
        public Campus Campus { get; set; }

        // Method for returning full name for filtering purposes
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", Given_name, Family_name);
            }
        }

        // This public override wraps everything up to nicely output a string.
        public override string ToString()
        {
            return Family_name + ", " + Given_name + "( " + Title + ")";
        }

        public string Availability
        {
            get
            {
                DateTime day = DateTime.Now;
                TimeSpan now = DateTime.Now.TimeOfDay;
                foreach (Consultation c in Consult)
                {
                    if (c.Day == day.DayOfWeek)
                    {
                        if ((c.Start < now) && (c.End > now))
                        {
                            return "Consulting";
                        }
                    }
                }
                foreach (Unit u in Teaching)
                {
                    foreach (Class c in u.UnitDetails)
                    {
                        if (c.Day == day.DayOfWeek)
                        {
                            if ((c.Start < now) && (c.End > now))
                            {
                                return "Teaching " + u.Code + " in " + c.Room;
                            }
                        }
                    }
                }
                return "Free";
            }
        }

    }
}
