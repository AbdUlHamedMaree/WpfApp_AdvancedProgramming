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
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullName_TextBox.Text) || string.IsNullOrWhiteSpace(UserName_TextBox.Text) || string.IsNullOrWhiteSpace(Password_TextBox.Text)||BirthDate_DatePicker.SelectedDate == null)
                MessageBox.Show("يجب عدم ترك الحقول فارغة", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                using (AppDB appdb = new AppDB())
                {
                    Employee _ = appdb.Employees.Where(e => e.UserName == UserName_TextBox.Text).FirstOrDefault();
                    if (_ == null)
                    {
                        Employee emp = new Employee()
                        {
                            FullName = FullName_TextBox.Text,
                            UserName = UserName_TextBox.Text,
                            Password = Password_TextBox.Text,
                            BirthDate = BirthDate_DatePicker.SelectedDate.Value,
                            Admin = Admin_CheckBox.IsChecked.Value
                        };
                        appdb.Employees.Add(emp);
                        appdb.SaveChanges();
                        this.Close();
                    }
                    else
                        MessageBox.Show("اسم المستخدم موجود مسبقاً", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }
    }
}
