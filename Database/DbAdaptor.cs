using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; //for generating a MessageBox upon encountering an error

using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace HRIS
{
    /*Sourced from Tutorial 10 solution credit goes to the original authors
     * Editor: Mikael Ekvall (419133), Douglas Jones (446502), Nicholas King (172002)
     */
    abstract class DbAdaptor
    {
        //If including error reporting within this class (as this sample does) then you'll need a way
        //to control whether the errors are actually shown or silently ignored, since once you have
        //connected the GUI to the Boss object then the GUI designer will execute its code, which
        //will try to connect to the database to load live data into the GUI at design time.
        private static bool reportingErrors = true;

        //These would not be hard coded in the source file normally, but read from the application's settings (and, ideally, with some amount of basic encryption applied)
        private const string db = "kit206";
        private const string user = "kit206";
        private const string pass = "kit206";
        private const string server = "alacritas.cis.utas.edu.au";

        private static MySqlConnection conn = null;

        //Part of step 2.3.3 in Week 9 tutorial. This method is a gift to you because .NET's approach to converting strings to enums is so horribly broken
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Creates and returns (but does not open) the connection to the database.
        /// </summary>
        private static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                //Note: This approach is not thread-safe
                string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3};SslMode=none", db, server, user, pass);

                //string connectionString = String.Format("Database={0};Data Source={1};User Id={2};Password={3}", db, server, user, pass);
                conn = new MySqlConnection(connectionString);
            }
            return conn;
        }

        // Retrieves ID, Given Name, Family Name, Title & Category for all Staff
        public static List<Staff> LoadStaffList()
        {
            List<Staff> staff = new List<Staff>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("" + "select id, " + "given_name, " + "family_name, " + "title, " + "category " + "from staff", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    staff.Add(new Staff {
                        Id = rdr.GetInt32(0),
                        Given_name = rdr.GetString(1),
                        Family_name = rdr.GetString(2),
                        Title = rdr.GetString(3),
                        Category = ParseEnum<Category>(rdr.GetString(4))
                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading staff", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return staff;
        }

        // Retrieves full details of a particular staff
        public static Staff LoadStaffDetails(int id)
        {
            Staff staff_extended = new Staff();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select given_name, family_name, title, category, photo, room, campus, email, phone from staff where id=?id", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    staff_extended.Given_name = rdr.GetString(0);
                    staff_extended.Family_name = rdr.GetString(1);
                    staff_extended.Title = rdr.GetString(2);
                    staff_extended.Category = ParseEnum<Category>(rdr.GetString(3));
                    staff_extended.Photo = rdr.GetString(4);
                    staff_extended.RoomLocation = rdr.GetString(5);
                    staff_extended.Campus = ParseEnum<Campus>(rdr.GetString(6));
                    staff_extended.Email = rdr.GetString(7);
                    staff_extended.Phone = rdr.GetString(8);
                    staff_extended.Id = id;
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading staff", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return staff_extended;
        }

        // Loads teaching details for a particular staff member
        public static List<Unit> LoadTeachingDetails(int id)
        {
            List<Unit> units = new List<Unit>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select code, title from unit where coordinator=?id", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    units.Add(new Unit
                    {
                        
                        Code = rdr.GetString(0),
                        Title = rdr.GetString(1)
                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading units", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return units;
        }

        // Retrieves Code, Title & Coordinator for all Units
        public static List<Unit> LoadUnitList()
        {
            List<Unit> units = new List<Unit>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select code, title, coordinator from unit", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    units.Add(new Unit
                    {
                        Code = rdr.GetString(0),
                        Title = rdr.GetString(1),
                        Coordinator = rdr.GetInt32(2)
                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading units", e);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return units;
        }

        // Retrieves full details for a given Unit
        public static List<Class> LoadUnitDetails(string code)
        {
            List<Class> classes = new List<Class>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("select campus, day, start, end, type, room, staff from class where unit_code=?code", conn);
                cmd.Parameters.AddWithValue("code", code);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    classes.Add(new Class
                    {
                        Campus = ParseEnum<Campus>(rdr.GetString(0)),
                        Day = ParseEnum<DayOfWeek>(rdr.GetString(1)),
                        Start = rdr.GetTimeSpan(2),
                        End = rdr.GetTimeSpan(3),
                        Type = ParseEnum<Type>(rdr.GetString(4)),
                        Room = rdr.GetString(5),
                        Staff = rdr.GetInt32(6)
                        
                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading units", e);
                MessageBox.Show("error");
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return classes;
        }

        // Retrieves consult details for a given Staff
        public static List<Consultation> LoadConsultationDetails(int id)
        {
            List<Consultation> consult = new List<Consultation>();

            MySqlConnection conn = GetConnection();
            MySqlDataReader rdr = null;

            try
            {
                conn.Open();


                MySqlCommand cmd = new MySqlCommand("select day, start, end from consultation where staff_id=?id", conn);
                cmd.Parameters.AddWithValue("id", id);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    consult.Add(new Consultation
                    {
                        Day = ParseEnum<DayOfWeek>(rdr.GetString(0)),
                        Start = rdr.GetTimeSpan(1),
                        End = rdr.GetTimeSpan(2),
                        Staff_id = id
                    });
                }
            }
            catch (MySqlException e)
            {
                ReportError("loading units", e);
                MessageBox.Show("error");
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return consult;
        }

        /// <summary>
        /// In a more complete application this error would be logged to a file
        /// and the error reported back to the original caller, who is closer
        /// to the GUI and hence better able to produce the error message box
        /// (which would not show the actual error details like this does).
        /// </summary>
        private static void ReportError(string msg, Exception e)
        {
            if (reportingErrors)
            {
                MessageBox.Show("An error occurred while " + msg + ". Try again later.\n\nError Details:\n" + e,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
