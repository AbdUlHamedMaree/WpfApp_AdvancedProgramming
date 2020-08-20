using System;
using System.Collections.Generic;
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
    /// Interaction logic for RequestString.xaml
    /// </summary>
    public partial class RequestString : Window
    {
        public RequestString(string Tem)
        {
            InitializeComponent();
            (root_Grid.Children[0] as TextBlock).Text = Tem;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public string Result() => (root_Grid.Children[1] as TextBox).Text;
    }
}
