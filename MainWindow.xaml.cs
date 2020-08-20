using System.Linq;
using System.Windows;
using WpfApp_D_NrimanProject.Model;

namespace WpfApp_D_NrimanProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserName_TextBox.Text, password = Password_TextBox.Text;
            Employee emp = new Employee();
            using (AppDB appdb = new AppDB())
            {
                emp = appdb.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();
            }
            if (userName == "admin" && password == "admin")
                ShowAdminWindow(new Employee() { });
            else if (userName == "user" && password == "user")
                ShowNonAdminWindow(new Employee() { });
            else if (emp != null)
            {
                if (emp.Admin)
                    ShowAdminWindow(emp);
                else
                    ShowNonAdminWindow(emp);
            }
            else
            {
                MessageBox.Show("اسم المستخدم أو كلمة المرور خاطئة","خطأ بالتسجيل",MessageBoxButton.OK,MessageBoxImage.Error);
                Password_TextBox.Text = "";
            }
            
        }

        private void ShowAdminWindow(Employee emp)
        {
            AdminWindow _ = new AdminWindow(emp);
            _.Show();
            Close();
        }

        private void ShowNonAdminWindow(Employee emp)
        {
            NonAdminWindow _ = new NonAdminWindow(emp);
            _.Show();
            Close();
        }
    }
}
