using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp_D_NrimanProject.Model;

namespace WpfApp_D_NrimanProject
{
    /// <summary>
    /// Interaction logic for ViewBills.xaml
    /// </summary>
    public partial class ViewBills : Window
    {
        public ViewBills()
        {
            InitializeComponent();

            List<BillsViewModel> res = new List<BillsViewModel>();
            using (AppDB appdb = new AppDB())
            {
                List<Item> tmp = new List<Item>();
                foreach (Bill bill in appdb.Bills.ToList())
                {
                    tmp = appdb.Items.Where(i => i.BillID == bill.BillID).ToList();
                    int cnt = tmp.Count;
                    double tPrc = 0;

                    foreach (Item item1 in tmp)
                        tPrc += item1.Price;

                    res.Add(new BillsViewModel
                    {
                        BillID = bill.BillID,
                        TotalPrice = tPrc,
                        ItemsCount = cnt,
                        CustomerName = bill.CustomerName,
                        OutTime = bill.OutTime
                    });
                }
            }
            Bills_ListView.ItemsSource = res;

            CollectionView view1 = (CollectionView)CollectionViewSource.GetDefaultView(Bills_ListView.ItemsSource);
            view1.GroupDescriptions.Add(new PropertyGroupDescription("CustomerName"));
        }

        private void Bills_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Item> res = new List<Item>();

            int BillID =  (e.AddedItems[0] as BillsViewModel).BillID;
            using (AppDB appdb = new AppDB())
            {
                res = appdb.Items.Where(i => i.BillID == BillID).ToList();
            }

            Items_ListView.ItemsSource = res;

            CollectionView view1 = (CollectionView)CollectionViewSource.GetDefaultView(Items_ListView.ItemsSource);
            view1.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
        }
    }
}
