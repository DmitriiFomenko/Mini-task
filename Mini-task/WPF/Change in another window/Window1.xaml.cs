using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace 
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Owner.IsVisible)
            {
                e.Cancel = true;
                Hide();
            }
            else
                Close();
        }

        private void checkBox1_Click(object sender, RoutedEventArgs e)
        {
            if ((Owner.FindName("textBox" + ((CheckBox)sender).Tag) as TextBox).FontWeight != FontWeights.Bold)
                (Owner.FindName("textBox" + ((CheckBox)sender).Tag) as TextBox).FontWeight = FontWeights.Bold;
            else
                (Owner.FindName("textBox" + ((CheckBox)sender).Tag) as TextBox).FontWeight = FontWeights.Normal;
        }
    }
}
