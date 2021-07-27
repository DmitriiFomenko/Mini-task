using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;
/*
 Окно содержит ListBox с меткой «Playlist», кнопки
«Play/Stop», «Up», «Down», NumericUpDown с меткой «Time»,
меню «Command» с «Add file», «Add folder», «Clear» и «Exit». Размеры списка меняются при изменении размеров окна.
«Add file», «Add folder» - с помощью диалогового окна пользователь выбирает один из wav-файлов; «Add file» - 1 файл, «Add folder» – все wav-файлы из папки. После добавления если в списке >= 1 элемент - кнопка «Play/Stop» становится доступной; если > 1, то доступными «Up» и «Down».
«Play/Stop» запускает/останавливает текущий файл из списка. Компонент-счетчик «Time» позволяет указать время воспроизведения каждого файла, после чего запуститься следующий файл по списку; если продолжительность файла меньше указанного времени, то файл воспроизводится циклически. 
«Up» и «Down» позволяют перемещать элемент по списку. 
Во время воспроизведения ListBox, компонент-счетчик и кнопки «Up» и «Down» недоступны.
При завершении работы программы список воспроизведения, позиция текущего элемента,
время воспроизведения, размеры и положение окна сохраняются в реестре. При последующих запусках программа должна восстанавливать сохраненное состояние. Указание. Для воспроизведения wav-файлов используйте класс System.Media.SoundPlayer.
 */

namespace 
{
    public partial class Form1 : Form
    {
        string s="";
        private SoundPlayer player;
        bool f = true;
        int n;
        private string regKeyName = "Software\\CSExamples\\REGISTRY";
        public Form1()
        {
            InitializeComponent();
            player = new SoundPlayer();
            player.LoadCompleted += new AsyncCompletedEventHandler(player_LoadCompleted);
            timer1.Interval = 10000;

            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(regKeyName);
                if (rk != null)
                {
                    Width = (int)rk.GetValue("FormWidth", Width);
                    Height = (int)rk.GetValue("FormHeight", Height);
                    s = rk.GetValue("listBox", s).ToString();
                    if (s.Length > 2)
                    {
                        s.Split(new char[] { '?' }).ToList().ForEach(f => listBox1.Items.Add(f));
                        n = (int)rk.GetValue("n", n);
                        listBox1.SetSelected(n, true);
                    }
                    int v = 0;
                    v = (int)rk.GetValue("Value", v);
                    numericUpDown1.Value = v;
                }
            }
            finally
            {
                if (rk != null)
                    rk.Close();
            }

            button1.Enabled = true;
            button2.Enabled = true;
            if (listBox1.Items.Count > 1)
            {
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void addCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;

            dlg.Filter = "WAV files (*.wav)|*.wav";
            dlg.DefaultExt = ".wav";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Add(dlg.FileName);
                listBox1.SetSelected(listBox1.Items.Count - 1, true);
            }
            EnablePlaybackControls(true);
        }
        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = true;
            dlg.RootFolder = System.Environment.SpecialFolder.MyComputer;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Directory
                         .GetFiles(dlg.SelectedPath, "*", SearchOption.TopDirectoryOnly)
                         .Where(f => Path.GetExtension(f).Equals(".wav"))
                         .ToList()
                         .ForEach(f => listBox1.Items.Add(dlg.SelectedPath + "\\" + Path.GetFileName(f)));
                if (listBox1.Items.Count > 0)
                    listBox1.SetSelected(listBox1.Items.Count - 1, true);
            }
            EnablePlaybackControls(true);
        }
        private void EnablePlaybackControls(bool enabled)
        {
            button1.Enabled = enabled;
            button2.Enabled = enabled;
            if (listBox1.Items.Count > 1)
            {
                button3.Enabled = enabled;
                button4.Enabled = enabled;
            }
            else
            {
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void player_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //EnablePlaybackControls(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            player.PlayLooping();
            listBox1.Enabled = false;
            n = listBox1.SelectedIndex;
            button3.Enabled = false;
            button4.Enabled = false;
            numericUpDown1.Enabled = false;
            timer1.Start();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            player.Stop();
            listBox1.Enabled = true;
            if (listBox1.Items.Count > 1)
            {
                button3.Enabled = true;
                button4.Enabled = true;
            }
            numericUpDown1.Enabled = true;
            timer1.Stop();
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            player.Stop();
            EnablePlaybackControls(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var t = listBox1.Items[listBox1.SelectedIndex];
            f = false;
            if (listBox1.SelectedIndex > 0)
            {

                listBox1.Items[listBox1.SelectedIndex] = listBox1.Items[listBox1.SelectedIndex - 1];
                listBox1.Items[listBox1.SelectedIndex - 1] = t;
                f = true;
                listBox1.SetSelected(listBox1.SelectedIndex - 1, true);
            }
            else
            {
                listBox1.Items[listBox1.SelectedIndex] = listBox1.Items[listBox1.Items.Count - 1];
                listBox1.Items[listBox1.Items.Count - 1] = t;
                f = true;
                listBox1.SetSelected(listBox1.Items.Count - 1, true);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var t = listBox1.Items[listBox1.SelectedIndex];
            f = false;
            if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
            {

                listBox1.Items[listBox1.SelectedIndex] = listBox1.Items[listBox1.SelectedIndex + 1];
                listBox1.Items[listBox1.SelectedIndex + 1] = t;
                f = true;
                listBox1.SetSelected(listBox1.SelectedIndex + 1, true);
            }
            else
            {
                listBox1.Items[listBox1.SelectedIndex] = listBox1.Items[0];
                listBox1.Items[0] = t;
                f = true;
                listBox1.SetSelected(0, true);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0 && f)
            {
                player.SoundLocation = listBox1.Items[0].ToString();
                player.Load();
            }
            n = listBox1.SelectedIndex;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            player.Stop();
            if (n < listBox1.Items.Count - 1)
            {
                listBox1.SetSelected(++n, true);
            }
            else
            {
                n = 0;
                if (listBox1.Items.Count > 0)
                    listBox1.SetSelected(n, true);
            }
            player.PlayLooping();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)((sender as NumericUpDown).Value) * 1000;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.CreateSubKey(regKeyName);
                if (rk == null)
                    return;
                rk.SetValue("FormWidth", Width);
                rk.SetValue("FormHeight", Height);
                rk.SetValue("n", n);
                rk.SetValue("Value", (int)numericUpDown1.Value);
                rk.SetValue("LocY", Location.Y);
                rk.SetValue("LocX", Location.X);
                s = "";
                if (listBox1.Items.Count > 0)
                {
                    for (int i = 0; i < listBox1.Items.Count - 1; i++)
                        s += listBox1.Items[i] + "?";
                    s += listBox1.Items[listBox1.Items.Count - 1];
                }
                rk.SetValue("listBox", s);
            }
            finally
            {
                if (rk != null)
                    rk.Close();
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryKey rk = null;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(regKeyName);
                if (rk != null)
                {
                    int y = 0;
                    y = (int)rk.GetValue("LocY", y);
                    int x = 0;
                    x = (int)rk.GetValue("LocX", x);
                    Location = new Point(x, y);
                }
            }
            finally
            {
                if (rk != null)
                    rk.Close();
            }
        }
    }
}
