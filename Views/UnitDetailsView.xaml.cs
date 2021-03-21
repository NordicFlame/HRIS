using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for UnitDetailsView.xaml
    /// </summary>
    /// 
    public partial class UnitDetailsView : UserControl
    {
        private const string STAFF_LIST_KEY = "UnitListDataSource";
        private UnitController UnitListController = (UnitController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;

        //allows an element to be visable to other classes
        public static Grid UnitDetailsWindow { get; set; }

        public UnitDetailsView()
        {
            InitializeComponent();

            //assign the staffDetailsPanel to a variable in order for it to be used in another class
            UnitDetailsWindow = this.UnitDetailsPanel;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Campus temp = (Campus)Enum.Parse(typeof(Campus), UnitListCampus.SelectedValue.ToString());

            if (e.RemovedItems.Count > 0)
            {
                UnitListController.FilterByCampus(temp);
            }
            
        }

        // Gets the unit clicked on and creates a staff object with that Id
        // Then opens the StaffDetailsView for that staff.
        private void ClassGrid_Selected(object sender, RoutedEventArgs e)
        {
            /*Unit f = ClassGrid.SelectedItem as Unit;
            Staff s = new Staff();
            s.Id = f.Coordinator;

            if (f != null)
                UnitListController.showSelectedStaff(s);*/
            // Commented out as issues with UnitListController.showSelectedStaff
        }
    }
}
