using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
Окно содержит 4 панели и 4 радиокнопоки для выбора текущей панели. На
панелях в случайных местах по 3 метки. Реализуйте перетаскивание
меток с текущей панели на любую другую. Метки с на других панелях перетаскивать нельзя.
Меню окна содержит одно подменю «Command» с командой «Color» и «Exit». Команда «Color»
приводит к появлению диалогового окна ColorDialog для смены цвета всех меток активной панели.
 */


namespace 
{
    public partial class Form1 : Form
    {
        private Point p;
        private Size s;

        string tag = "1";
        public Form1()
        {
            InitializeComponent();

            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            Random rnd = new Random();
            int value = rnd.Next(0,10);

            for (int j = 0; j < 4; j++)
                for (int i = 1; i < 4; i++) 
                    Controls["panel" + (j+1)].Controls["label" + (j*3 + i)].Location = new Point(rnd.Next(741), rnd.Next(87));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            p = e.Location;
            Label a = sender as Label;
            a.BringToFront();
            s = a.Size;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((sender as Label).Tag.Equals(tag))
            {
                Label a = sender as Label;
                Size s0 = new Size(e.X - p.X, e.Y - p.Y);

                Size sP = Controls["panel" + (sender as Label).Tag].Size;

                if (e.Button == MouseButtons.Left)
                {
                    Point p0 = a.Location + s0;

                    a.Location = new Point(Math.Min(Math.Max(0, p0.X), sP.Width - (sender as Label).Width),
                        Math.Min(sP.Height - (sender as Label).Height, Math.Max(0, p0.Y)));

                    if (p0.Y - sP.Height - (sender as Label).Height > 5 && !(sender as Label).Tag.Equals("4"))//down
                    {
                        (sender as Label).Focus();
                        DoDragDrop(sender, DragDropEffects.Move);
                    }

                    if (p0.Y < -5 && !(sender as Label).Tag.Equals("1"))//up
                    {
                        (sender as Label).Focus();
                        DoDragDrop(sender, DragDropEffects.Move);
                    }
                    
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tag = (sender as RadioButton).Tag + "";
        }

        private void panel2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void panel2_DragDrop(object sender, DragEventArgs e)
        {
            ActiveControl.Tag = (sender as Panel).Tag;
            (sender as Panel).Controls.Add(ActiveControl);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();

            if (MyDialog.ShowDialog() == DialogResult.OK)
                foreach(Control cl in Controls["panel" + tag].Controls)
                    (cl as Label).ForeColor = MyDialog.Color;
        }
    }
}
