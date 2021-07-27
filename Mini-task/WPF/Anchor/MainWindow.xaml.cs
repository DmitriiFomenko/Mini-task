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
    /*
        Главное окно содержит кнопку «Изменить выравнивание». Она выдаёт модальное окно с выпадающими списками, имеющим разное выравнивание.
    При нажатии кнопок «ОК» или «Применить» главное окно изменяет свое положение;
    При нажатии кнопки «Отмена» модальное окно закрывается без выполнения дополнительных
    действий.
    */

    public partial class MainWindow : Window
    {
        Window1 win = new Window1();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            win.Show();
            win.Owner = this;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            win.Close();
        }
    }
}
