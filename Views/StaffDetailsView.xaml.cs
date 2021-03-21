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
    /// Interaction logic for StaffDetailsView.xaml
    /// </summary>

    public partial class StaffDetailsView : UserControl
    {
        private const string STAFF_LIST_KEY = "StaffListDataSource";
        private StaffController StaffListController = (StaffController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;

        //allows an element to be visable to other classes
        public static Grid StaffDetailsWindow { get; set; }

        public StaffDetailsView()
        {
            InitializeComponent();

            //assign the staffDetailsPanel to a variable in order for it to be used in another class
            StaffDetailsWindow = this.StaffDetailsPanel;
        }

        private void TeachingGrid_Selected(object sender, RoutedEventArgs e)
        {
            Unit f = TeachingGrid.SelectedItem as Unit;

            if (f != null)
                StaffListController.showSelectedUnit(f);
        }
    }
}
