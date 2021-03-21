using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HRIS
{
    class UnitController
    {
        //private const string STAFF_LIST_KEY = "StaffListDataSource";
        //private StaffController StaffListController = (StaffController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;

        private List<Unit> UnitListMain;
        public List<Unit> Workers { get { return UnitListMain; } set { } }

        private Unit SelectedUnitMain;

        private ObservableCollection<Unit> viewableUnits;
        public ObservableCollection<Unit> VisibleWorkers { get { return viewableUnits; } set { } }

        private ObservableCollection<Class> viewableClasses;
        public ObservableCollection<Class> VisibleWorkers2 { get { return viewableClasses; } set { } }

        public UnitController()
        {
            UnitListMain = DbAdaptor.LoadUnitList();
            viewableUnits = new ObservableCollection<Unit>(UnitListMain);
        }

        public Unit LoadClassDetails(Unit u)
        {
            SelectedUnitMain = u;

            SelectedUnitMain.UnitDetails = DbAdaptor.LoadUnitDetails(u.Code);
            viewableClasses = new ObservableCollection<Class>(SelectedUnitMain.UnitDetails);

            return SelectedUnitMain;
        }

        public ObservableCollection<Unit> GetViewableList()
        {
            return VisibleWorkers;
        }

        public ObservableCollection<Class> GetViewableClassList()
        {
            return VisibleWorkers2;
        }

        /*Range of filters used to modify the UnitList*/
        public void FilterByName(string filter)
        {
            var selected = from Unit u in UnitListMain
                           where u.Code.ToLower().Contains(filter.ToLower()) || u.Title.ToLower().Contains(filter.ToLower())
                           // Uses .ToLower() to get rid of case sensitivity, then uses the LINQ .Contains for partial matches
                           select u;
            viewableUnits.Clear();
            //Converts the result of the LINQ expression to a List and then calls viewableUnits.Add with each element of that list in turn
            selected.ToList().ForEach(viewableUnits.Add);
        }

        public void FilterByCampus(Campus filter)
        {
            if (filter == (Campus)Enum.Parse(typeof(Campus), "Both"))
            {
                viewableClasses.Clear();
                SelectedUnitMain.UnitDetails.ToList().ForEach(viewableClasses.Add);
            }
            else
            {
                var selected = from Class c in SelectedUnitMain.UnitDetails
                               where c.Campus == filter
                               select c;
                viewableClasses.Clear();
                //Converts the result of the LINQ expression to a List and then calls viewableStaff.Add with each element of that list in turn
                selected.ToList().ForEach(viewableClasses.Add);
            }
        }

        // Opens StaffDetails from the Unit Timetable.
        public void showSelectedStaff(Staff temp)
        {
            /*MainWindow.Stuff.ShowUnit();

            StaffDetailsView.StaffDetailsWindow.DataContext = StaffListController.LoadStaffDetails(temp);*/
            // Unable to access the StaffListController reference (see top of this page).
            // Otherwise the code for Class -> Staff should've worked.
        }
    }
}
