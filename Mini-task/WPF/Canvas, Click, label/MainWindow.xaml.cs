using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace 
{
    /*
     Окно содержит поле ввода с заметкой «Время» и значением «0,0», пустой ListBox с меткой «Результаты», Canvas c метками «1»–«6».
Меню содержит «Command» с командами «Start» и «Exit».
«Start» - все метки изменяют свое положение(не пересекались) на панели и заливаются
красным цветом, а в «Время» отсчет времени. Требуется быстро нажать на все метки в указанном порядке. Нажатие делает ее фон зеленым. Как только все окажутся нажатыми, время останавливается. Если «Время» меньшее менее 10 с, то сообщение «Вы выиграли» и результат заносится в начальную строку списка
результатов; иначе сообщение «Вы проиграли».
Список результатов должен храниться на диске и считываться при каждом запуске программы.
     */

    public partial class MainWindow : Window
    {
        DispatcherTimer timer1 = new DispatcherTimer();
        DateTime startTime;
        TimeSpan pauseSpan;
        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists("Wpf-Task5.txt"))
            {
                List<string> lines = File.ReadAllLines("Wpf-Task5.txt", Encoding.Default).ToList();
                foreach (string str in lines)
                    times.Items.Add(str);
            }

            timer1.Interval = TimeSpan.FromMilliseconds(5);
            timer1.Tick += timer1_Tick;
            

            for (int i = 1; i < 7; i++)
                (FindName("label" + i) as Label).Tag = i;
        }

        private void MenuItem_Start(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            int value = rnd.Next(0,Convert.ToInt32(canva.ActualWidth));

            int[] rX = new int[] { 2, 3, 4, 5, 6, 7 };
            for (int i = 5; i >= 1; i--)
            {
                int j = rnd.Next(i + 1);
                int tmp = rX[j];
                rX[j] = rX[i];
                rX[i] = tmp;
            }
            int[] rY = new int[] { 2, 3, 4, 5, 6, 7 };
            for (int i = 5; i >= 1; i--)
            {
                int j = rnd.Next(i + 1);
                int tmp = rY[j];
                rY[j] = rY[i];
                rY[i] = tmp;
            }
            for (int i = 1; i < 7; i++)
            {
                Label l = FindName("label" + i) as Label;
                l.Background = new SolidColorBrush(Colors.Red);
                l.SetValue(Canvas.LeftProperty, 0.0 + rnd.Next((int)(canva.ActualWidth / 7 * (rX[i-1] - 1) - label1.ActualWidth), (int)(canva.ActualWidth / 7 * rX[i-1] - label1.ActualWidth)));
                l.SetValue(Canvas.TopProperty, 0.0 + rnd.Next((int)(canva.ActualHeight / 7 * (rY[i - 1] - 1) - label1.ActualHeight), (int)(canva.ActualHeight / 7 * rY[i - 1] - label1.ActualHeight)));
                l.IsEnabled = false;
            }
            label1.IsEnabled = true;
            start.IsEnabled = false;
            res.Content = "";
            timer.Text = "0,0";
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
        void timer1_Tick(object sender, EventArgs e)
        {
            pauseSpan = DateTime.Now - startTime;
            timer.Text = string.Format("{0},{1}", pauseSpan.Minutes * 60 + pauseSpan.Seconds, pauseSpan.Milliseconds / 100);
        }
        private void label1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label l = sender as Label;
            if (l.Tag.Equals(1))
            {
                startTime = DateTime.Now;
                pauseSpan = TimeSpan.Zero;
                timer1.Start();
            }

            if (l.Tag.Equals(6))
            {
                start.IsEnabled = true;
                timer1.Stop();
                if (pauseSpan.Minutes * 60 + pauseSpan.Seconds < 10)
                {
                    res.Content = "Вы выиграли";
                    times.Items.Insert(0,(string.Format("{0},{1}", pauseSpan.Minutes * 60 + pauseSpan.Seconds, pauseSpan.Milliseconds / 100)));

                    List<string> l1 = new List<string>();
                    foreach (string s in times.Items)
                        l1.Add(s);
                    File.WriteAllLines("Wpf-Task5.txt", l1);
                }
                else
                    res.Content = "Вы проиграли";
            }
            else
                (FindName("label" + ((int)(l.Tag) + 1)) as Label).IsEnabled = true;

            l.Background = new SolidColorBrush(Colors.Green);
            l.IsEnabled = false;
        }
    }
}
