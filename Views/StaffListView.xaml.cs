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
    /// Interaction logic for StaffListView.xaml
    /// </summary>
    
    public partial class StaffListView : UserControl
    {
        private const string STAFF_LIST_KEY = "StaffListDataSource";
        private StaffController StaffListController = (StaffController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;


        public StaffListView()
        {
            InitializeComponent();

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Staff temp = StaffListBox.SelectedValue as Staff;

            if (e.AddedItems.Count > 0)
            {
                StaffDetailsView.StaffDetailsWindow.DataContext = StaffListController.LoadStaffDetails(temp);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category temp = (Category)Enum.Parse(typeof(Category), StaffListCategory.SelectedValue.ToString());

            if (e.RemovedItems.Count > 0)
            {
                StaffListController.FilterByCategory(temp);
            }
            
        }

        public void FilterStaff_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Enter)
            {
                
                StaffListController.FilterByName(FilterStaffList.Text);
            }
        }
    }
}
