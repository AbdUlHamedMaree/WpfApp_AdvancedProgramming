using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp_D_NrimanProject.Model;

namespace WpfApp_D_NrimanProject
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Employee Employee { get; set; }
        public AdminWindow(Employee emp)
        {
            InitializeComponent();
            Employee = emp;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RenderItems();
        }

        private void AddUser_Button_Click(object sender, RoutedEventArgs e)
        {
            new AddEmployee().ShowDialog();
        }

        private void AddItems_Button_Click(object sender, RoutedEventArgs e)
        {
            new AddItems().ShowDialog();
            RenderItems();
        }

        private void DisplayBills_Button_Click(object sender, RoutedEventArgs e)
        {
            new ViewBills().ShowDialog();
        }

        private void RenderItems()
        {
            using (AppDB appdb = new AppDB())
            {
                Foods_ListView.ItemsSource = appdb.Foods.Where(i => !i.Sold).ToList();
                Glasses_ListView.ItemsSource = appdb.Glasses.Where(i => !i.Sold).ToList();
                Cleaners_ListView.ItemsSource = appdb.Cleaners.Where(i => !i.Sold).ToList();
            }
            CollectionView view1 = (CollectionView)CollectionViewSource.GetDefaultView(Foods_ListView.ItemsSource);
            CollectionView view2 = (CollectionView)CollectionViewSource.GetDefaultView(Glasses_ListView.ItemsSource);
            CollectionView view3 = (CollectionView)CollectionViewSource.GetDefaultView(Cleaners_ListView.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Name");
            view1.GroupDescriptions.Add(groupDescription);
            view2.GroupDescriptions.Add(groupDescription);
            view3.GroupDescriptions.Add(groupDescription);
        }
    }
}
