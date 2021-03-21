using System.Windows;
using System.Windows.Controls;


namespace HRIS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Grid DetailsArea { get; set; }
        public static MainWindow Stuff { get; set; } 

        public MainWindow()
        {
            InitializeComponent();
            Stuff = this;
        }

        private void BtnClickStaff(object sender, RoutedEventArgs e)
        {
            ShowStaff();
            
        }

        private void BtnClickUnit(object sender, RoutedEventArgs e)
        {
            ShowUnit();
        }

        public void ShowStaff()
        {
            UnitListObject.Visibility = Visibility.Hidden;
            UnitDetailsObject.Visibility = Visibility.Hidden;
            StaffListObject.Visibility = Visibility.Visible;
            StaffDetailsObject.Visibility = Visibility.Visible;
        }

        public void ShowUnit()
        {
            //shows and hides needed display views from Mainwindow
            StaffListObject.Visibility = Visibility.Hidden;
            StaffDetailsObject.Visibility = Visibility.Hidden;
            UnitListObject.Visibility = Visibility.Visible;
            UnitDetailsObject.Visibility = Visibility.Visible;
        }
    }
}
