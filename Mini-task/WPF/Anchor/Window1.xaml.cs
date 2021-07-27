using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace 
{
    /*
        Главное окно содержит кнопку «Изменить выравнивание». Она выдаёт модальное окно с выпадающими списками, имеющим разное выравнивание.
    При нажатии кнопок «ОК» или «Применить» главное окно изменяет свое положение;
    При нажатии кнопки «Отмена» модальное окно закрывается без выполнения дополнительных
    действий.
    */
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            b1.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
            if (e.Key == Key.Enter)
            {
                b_Click(b1, null);
            }
        }

        private void b_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.Equals("Отмена"))
            {
                Close();
                return;
            }
            if (box1.Text.Length > 0)
                switch (box1.Text[3])
                {
                    case 'л':
                        (Owner.FindName("button") as Button).HorizontalAlignment = HorizontalAlignment.Left;
                        break;
                    case 'ц':
                        (Owner.FindName("button") as Button).HorizontalAlignment = HorizontalAlignment.Center;
                        break;
                    case 'п':
                        (Owner.FindName("button") as Button).HorizontalAlignment = HorizontalAlignment.Right;
                        break;
                }
            if (box2.Text.Length > 0)
                switch (box2.Text[3])
                {
                    case 'в':
                        (Owner.FindName("button") as Button).VerticalAlignment = VerticalAlignment.Top;
                        break;
                    case 'ц':
                        (Owner.FindName("button") as Button).VerticalAlignment = VerticalAlignment.Center;
                        break;
                    case 'н':
                        (Owner.FindName("button") as Button).VerticalAlignment = VerticalAlignment.Bottom;
                        break;
                }

            if (((Button)sender).Content.Equals("Ок"))
                Close();
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
    }
}
