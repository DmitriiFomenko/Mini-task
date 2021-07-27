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

namespace 
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    /*
     Главное окно содержит шесть TextBox с текстом «1»–«6» и «Show». При нажатии «Show» появляется второе окно, содержащее
шесть CheckBox. При установке флажка текст соответствующего поля ввода выделяется полужирным шрифтом, при снятии отменяется. При изменении текста какого-либо поля ввода должна изменяться подпись соответствующего флажка. 
     */
    public partial class MainWindow : Window
    {
        Window1 win = new Window1();
        bool f = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(f)
            (win.FindName("checkBox" + ((TextBox)sender).Tag) as CheckBox).Content = ((TextBox)sender).Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            win.Show();
            win.Owner = this;
            if (!f)
            {
                f = true;
                for (int i = 1; i < 7; i++)
                    TextBox_TextChanged(FindName("textBox" + i) as TextBox, null);
            }
            
        }
    }
}
