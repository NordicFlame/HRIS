using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace HRIS
{
    class StaffController
    {
        private const string STAFF_LIST_KEY = "UnitListDataSource";
        private UnitController UnitListController = (UnitController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;

        //Mainly only contains information about staffmembers in order to run other DBmethods
        private List<Staff> StaffListMain;
        public List<Staff> Workers { get { return StaffListMain; } set { } }

        //Contains basic information and uses that to add more, or modify current information
        private ObservableCollection<Staff> viewableStaff;
        public ObservableCollection<Staff> VisibleWorkers { get { return viewableStaff; } set { } }

        public StaffController()
        {
            //set up the main list, that wont be changed
            StaffListMain = DbAdaptor.LoadStaffList();

            //active working variable that is always changed as needed to display different information to the user
            viewableStaff = new ObservableCollection<Staff>(StaffListMain);
        }

        //returns the List that is always being modified by user actions
        public ObservableCollection<Staff> GetViewableList()
        {
            return VisibleWorkers;
        }

        public Staff LoadStaffDetails(Staff e)
        {
            e = DbAdaptor.LoadStaffDetails(e.Id);
            e.Consult = DbAdaptor.LoadConsultationDetails(e.Id);
            e.Teaching = DbAdaptor.LoadTeachingDetails(e.Id);
            foreach (Unit u in e.Teaching)
            {
                u.UnitDetails = DbAdaptor.LoadUnitDetails(u.Code);
            }

            return e;
        }

        /*Range of filters used to modify the Stafflist*/
        public void FilterByName(string name)
        {
            var selected = from Staff s in StaffListMain
                               where s.FullName.ToLower().Contains(name.ToLower())
                               // Uses .ToLower() to get rid of case sensitivity, then uses the LINQ .Contains for partial matches
                               select s;
            viewableStaff.Clear();
            //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
            selected.ToList().ForEach(viewableStaff.Add);

        }

        public void FilterByCategory(Category cat)
        {
            if (cat == (Category)Enum.Parse(typeof(Category), "All"))
            {
                viewableStaff.Clear();
                StaffListMain.ToList().ForEach(viewableStaff.Add);
            }
            else
            {
                var selected = from Staff s in StaffListMain
                               where s.Category == cat
                               select s;
                viewableStaff.Clear();

                //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
                selected.ToList().ForEach(viewableStaff.Add);
            }
        }

        public void showSelectedUnit(Unit temp)
        {
            MainWindow.Stuff.ShowUnit();

            UnitDetailsView.UnitDetailsWindow.DataContext = UnitListController.LoadClassDetails(temp);
        }
    }
}
