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
    /// Interaction logic for UnitListView.xaml
    /// </summary>
    public partial class UnitListView : UserControl
    {
        private const string STAFF_LIST_KEY = "UnitListDataSource";
        private UnitController UnitListController = (UnitController)(Application.Current.FindResource(STAFF_LIST_KEY) as ObjectDataProvider).ObjectInstance;

        public UnitListView()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Unit temp = UnitListBox.SelectedValue as Unit;

            if (e.AddedItems.Count > 0)
            {
                UnitDetailsView.UnitDetailsWindow.DataContext = UnitListController.LoadClassDetails(temp);
            }

        }

        private void FilterUnitList_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void FilterUnitList_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                UnitListController.FilterByName(FilterUnitList.Text);
            }
        }
    }
}
