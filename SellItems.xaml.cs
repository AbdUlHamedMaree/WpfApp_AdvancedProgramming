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
    /// Interaction logic for SellItems.xaml
    /// </summary>
    public partial class SellItems : Window
    {
        List<Food> Foods;
        List<Glass> Glasses;
        List<Cleaner> Cleaners;
        List<Food> SoledFoods;
        List<Glass> SoledGlasses;
        List<Cleaner> SoledCleaners;
        string selItm;
        public SellItems()
        {
            InitializeComponent();
            ItemType_ComboBox.ItemsSource = Enum.GetNames(typeof(ItemType));
            using (AppDB appdb = new AppDB())
            {
                Foods = appdb.Foods.Where(i => !i.Sold).ToList();
                Glasses = appdb.Glasses.Where(i => !i.Sold).ToList();
                Cleaners = appdb.Cleaners.Where(i => !i.Sold).ToList();
            }
            SoledFoods = new List<Food>();
            SoledGlasses = new List<Glass>();
            SoledCleaners = new List<Cleaner>();
        }

        private void ItemType_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selItm = (sender as ComboBox).SelectedItem.ToString();

                List<string> ItemNames = new List<string>();

                if (ItemType.Food.ToString() == selItm)
                {
                    foreach (Food item in Foods)
                    {
                        if (!ItemNames.Exists(s => s == item.Name))
                            ItemNames.Add(item.Name);
                    }
                }

                else if (ItemType.Glass.ToString() == selItm)
                {
                    foreach (Glass item in Glasses)
                    {
                        if (!ItemNames.Exists(s => s == item.Name))
                            ItemNames.Add(item.Name);
                    }
                }

                else if (ItemType.Cleaner.ToString() == selItm)
                {
                    foreach (Cleaner item in Cleaners)
                    {
                        if (!ItemNames.Exists(s => s == item.Name))
                            ItemNames.Add(item.Name);
                    }

                }

                ItemName_ComboBox.ItemsSource = ItemNames;

                ItemName_ComboBox.IsEnabled = true;
            }
            catch
            {

            }
        }

        private void ItemName_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int count = 0;
            try
            {
                string ItemName = (sender as ComboBox).SelectedItem.ToString();

                if (ItemType.Food.ToString() == selItm)
                {
                    count = Foods.Where(f => f.Name == ItemName).ToList().Count;
                }

                else if (ItemType.Glass.ToString() == selItm)
                {
                    count = Glasses.Where(f => f.Name == ItemName).ToList().Count;
                }

                else if (ItemType.Cleaner.ToString() == selItm)
                {
                    count = Cleaners.Where(f => f.Name == ItemName).ToList().Count;
                }

                ItemCount_TextBlock.Text = Convert.ToString(count);

                ItemCount_TextBox.IsEnabled = true;
            }
            catch (Exception)
            {
                ClearTheForm();
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void ItemCount_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32((sender as TextBox).Text) > Convert.ToInt32(ItemCount_TextBlock.Text))
                    (sender as TextBox).Text = ItemCount_TextBlock.Text;
            }
            catch (Exception)
            {
                (sender as TextBox).Text = "";
            }

        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ItemType_ComboBox.SelectedIndex == -1 || ItemName_ComboBox.SelectedIndex == -1 || string.IsNullOrWhiteSpace(ItemCount_TextBox.Text))
                MessageBox.Show("يجب عدم ترك الحقول فارغة", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);

            else
            {
                object itm = null;

                if (ItemType.Food.ToString() == selItm)
                {
                    string prc = Convert.ToString(Foods.Where(f => f.Name == ItemName_ComboBox.SelectedItem.ToString()).FirstOrDefault().Price);
                    itm = new
                    {
                        Name = ItemName_ComboBox.SelectedItem.ToString(),
                        ItemCount = ItemCount_TextBox.Text,
                        Price = prc,
                        TotalPrice = Convert.ToString(Convert.ToInt32(ItemCount_TextBox.Text) * Convert.ToInt32(prc))
                    };

                    int tmp = Foods.FindIndex(f => f.Name == ItemName_ComboBox.SelectedItem.ToString());

                    SoledFoods.AddRange(Foods.GetRange(tmp, Convert.ToInt32(ItemCount_TextBox.Text) ));

                    Foods.RemoveRange(tmp, Convert.ToInt32(ItemCount_TextBox.Text) );
                }

                else if (ItemType.Glass.ToString() == selItm)
                {
                    string prc = Convert.ToString(Glasses.Where(f => f.Name == ItemName_ComboBox.SelectedItem.ToString()).FirstOrDefault().Price);
                    itm = new
                    {
                        Name = ItemName_ComboBox.SelectedItem.ToString(),
                        ItemCount = ItemCount_TextBox.Text,
                        Price = prc,
                        TotalPrice = Convert.ToString(Convert.ToInt32(ItemCount_TextBox.Text) * Convert.ToInt32(prc))
                    };

                    int tmp = Glasses.FindIndex(f => f.Name == ItemName_ComboBox.SelectedItem.ToString());

                    SoledGlasses.AddRange(Glasses.GetRange(tmp, Convert.ToInt32(ItemCount_TextBox.Text) ));

                    Glasses.RemoveRange(tmp, Convert.ToInt32(ItemCount_TextBox.Text) );
                }

                else if (ItemType.Cleaner.ToString() == selItm)
                {
                    string prc = Convert.ToString(Cleaners.Where(f => f.Name == ItemName_ComboBox.SelectedItem.ToString()).FirstOrDefault().Price);
                    itm = new
                    {
                        Name = ItemName_ComboBox.SelectedItem.ToString(),
                        ItemCount = ItemCount_TextBox.Text,
                        Price = prc,
                        TotalPrice = Convert.ToString(Convert.ToInt32(ItemCount_TextBox.Text) * Convert.ToInt32(prc))
                    };

                    int tmp = Cleaners.FindIndex(f => f.Name == ItemName_ComboBox.SelectedItem.ToString());

                    SoledCleaners.AddRange(Cleaners.GetRange(tmp, Convert.ToInt32(ItemCount_TextBox.Text) ));

                    Cleaners.RemoveRange(tmp, Convert.ToInt32(ItemCount_TextBox.Text) );
                }

                Items_ListView.Items.Add(itm);

                ClearTheForm();
            }
        }

        private void End_Button_Click(object sender, RoutedEventArgs e)
        {
            RequestString _ = new RequestString("أدخل اسم الزبون");
            _.ShowDialog();
            string CustomerName = "";
            if ((bool)_.DialogResult)
            {
                CustomerName = _.Result();
                Bill bill = new Bill();
                bill.CustomerName = CustomerName;
                bill.OutTime = DateTime.Now;

                using (AppDB appdb = new AppDB())
                {
                    appdb.Bills.Add(bill);

                    foreach (Food item in SoledFoods)
                    {
                        var __ = appdb.Foods.FirstOrDefault(f => f.ItemID == item.ItemID);
                        __.Sold = true;
                        __.Bill = bill;
                        __.OutTime = DateTime.Now;
                    }

                    foreach (Glass item in SoledGlasses)
                    {
                        var __ = appdb.Glasses.FirstOrDefault(f => f.ItemID == item.ItemID);
                        __.Sold = true;
                        __.Bill = bill;
                        __.OutTime = DateTime.Now;
                    }

                    foreach (Cleaner item in SoledCleaners)
                    {
                        var __ = appdb.Cleaners.FirstOrDefault(f => f.ItemID == item.ItemID);
                        __.Sold = true;
                        __.Bill = bill;
                        __.OutTime = DateTime.Now;
                    }

                    appdb.SaveChanges();
                }
                Close();
            }
        }

        private void ClearTheForm()
        {
            ItemType_ComboBox.SelectedIndex = -1;
            ItemName_ComboBox.SelectedIndex = -1;
            ItemName_ComboBox.IsEnabled = false;
            ItemCount_TextBox.IsEnabled = false;
            ItemCount_TextBox.Text = "";
            ItemCount_TextBlock.Text = "";
        }
    }
}
