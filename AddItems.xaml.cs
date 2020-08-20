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

namespace WpfApp_D_NrimanProject
{
    /// <summary>
    /// Interaction logic for AddItems.xaml
    /// </summary>
    public partial class AddItems : Window
    {
        string addType { get; set; }
        public AddItems()
        {
            InitializeComponent();
            ItemType_ComboBox.ItemsSource = Enum.GetNames(typeof(Model.ItemType));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(ItemName_TextBox.Text) || string.IsNullOrWhiteSpace(ItemCount_TextBox.Text) || string.IsNullOrWhiteSpace((Root_Grid.Children[0] as TextBox).Text))
                MessageBox.Show("يجب عدم ترك الحقول فارغة", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (addType == Model.ItemType.Food.ToString())
                {
                    if ((Root_Grid.Children[2] as DatePicker).SelectedDate == null)
                        MessageBox.Show("يجب عدم ترك الحقول فارغة", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        List<Model.Food> foods = new List<Model.Food>();
                        for (int i = 0; i < Convert.ToInt32(ItemCount_TextBox.Text); i++)
                            foods.Add(new Model.Food()
                            {
                                Name = ItemName_TextBox.Text,
                                InTime = DateTime.Now,
                                Price = Convert.ToDouble(ItemPrice_TextBox.Text),
                                FoodType = (Root_Grid.Children[0] as TextBox).Text,
                                EndTime = (Root_Grid.Children[2] as DatePicker).SelectedDate.Value,
                                Sold = false
                            });
                        using (Model.AppDB appdb = new Model.AppDB())
                        {
                            appdb.Foods.AddRange(foods);
                            appdb.SaveChanges();
                        }
                    }
                }

                else if (addType == Model.ItemType.Glass.ToString())
                {
                    if (string.IsNullOrWhiteSpace((Root_Grid.Children[2] as TextBox).Text))
                        MessageBox.Show("يجب عدم ترك الحقول فارغة", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        List<Model.Glass> glasses = new List<Model.Glass>();
                        for (int i = 0; i < Convert.ToInt32(ItemCount_TextBox.Text); i++)
                            glasses.Add(new Model.Glass()
                            {
                                Name = ItemName_TextBox.Text,
                                InTime = DateTime.Now,
                                Price = Convert.ToDouble(ItemPrice_TextBox.Text),
                                Width = Convert.ToDouble((Root_Grid.Children[0] as TextBox).Text),
                                Capacity = Convert.ToDouble((Root_Grid.Children[2] as TextBox).Text),
                                Sold = false
                            });
                        using (Model.AppDB appdb = new Model.AppDB())
                        {
                            appdb.Glasses.AddRange(glasses);
                            appdb.SaveChanges();
                        }
                    }
                }

                else if (addType == Model.ItemType.Cleaner.ToString())
                {
                    List<Model.Cleaner> cleaners = new List<Model.Cleaner>();
                    for (int i = 0; i < Convert.ToInt32(ItemCount_TextBox.Text); i++)
                        cleaners.Add(new Model.Cleaner()
                        {
                            Name = ItemName_TextBox.Text,
                            InTime = DateTime.Now,
                            Price = Convert.ToDouble(ItemPrice_TextBox.Text),
                            Company = (Root_Grid.Children[0] as TextBox).Text,
                            Sold = false
                        });
                    //not working
                    //cleaners = Enumerable.Repeat(cleaner, Convert.ToInt32(ItemCount_TextBox.Text)).ToList();

                    using (Model.AppDB appdb = new Model.AppDB())
                    {
                        appdb.Cleaners.AddRange(cleaners);
                        appdb.SaveChanges();
                    }
                }

                this.Close();
            }

        }

        private void ItemType_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selItm = (sender as ComboBox).SelectedItem.ToString();

            addType = selItm;

            List<UIElement> controls = new List<UIElement>();

            if (Model.ItemType.Food.ToString() == selItm)
            {
                TextBox textBox1 = new TextBox()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 150,
                    Name = "textBox1"
                };
                textBox1.SetValue(Grid.ColumnProperty, 0);
                textBox1.SetValue(Grid.RowProperty, 0);

                TextBlock textBlock_FoodType = new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = ":نوع الطعام"
                };
                textBlock_FoodType.SetValue(Grid.ColumnProperty, 1);
                textBlock_FoodType.SetValue(Grid.RowProperty, 0);

                DatePicker textBox2 = new DatePicker()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Name = "textBox2"
                };
                textBox2.SetValue(Grid.ColumnProperty, 0);
                textBox2.SetValue(Grid.RowProperty, 1);

                TextBlock textBlock2 = new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = ":تاريخ انتهاء\nالصلاحية"
                };
                textBlock2.SetValue(Grid.ColumnProperty, 1);
                textBlock2.SetValue(Grid.RowProperty, 1);

                controls = new List<UIElement>() { textBox1, textBlock_FoodType, textBox2, textBlock2 };
            }

            else if (Model.ItemType.Glass.ToString() == selItm)
            {
                TextBox textBox1 = new TextBox()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 150,
                    Name = "textBox1"
                };
                textBox1.SetValue(Grid.ColumnProperty, 0);
                textBox1.SetValue(Grid.RowProperty, 0);

                TextBlock textBlock_FoodType = new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = ":الوزن"
                };
                textBlock_FoodType.SetValue(Grid.ColumnProperty, 1);
                textBlock_FoodType.SetValue(Grid.RowProperty, 0);

                TextBox textBox2 = new TextBox()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 150,
                    Name = "textBox2"
                };
                textBox2.SetValue(Grid.ColumnProperty, 0);
                textBox2.SetValue(Grid.RowProperty, 1);

                TextBlock textBlock2 = new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = ":السعة"
                };
                textBlock2.SetValue(Grid.ColumnProperty, 1);
                textBlock2.SetValue(Grid.RowProperty, 1);

                controls = new List<UIElement>() { textBox1, textBlock_FoodType, textBox2, textBlock2 };
            }

            else if (Model.ItemType.Cleaner.ToString() == selItm)
            {
                TextBox textBox1 = new TextBox()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 150,
                    Name = "textBox1"
                };
                textBox1.SetValue(Grid.ColumnProperty, 0);
                textBox1.SetValue(Grid.RowProperty, 0);

                TextBlock textBlock_FoodType = new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = ":الشركة المصنعة"
                };
                textBlock_FoodType.SetValue(Grid.ColumnProperty, 1);
                textBlock_FoodType.SetValue(Grid.RowProperty, 0);

                controls = new List<UIElement>() { textBox1, textBlock_FoodType };
            }

            Root_Grid.Children.Clear();

            foreach (UIElement item in controls) { Root_Grid.Children.Add(item); }
        }
    }
}
