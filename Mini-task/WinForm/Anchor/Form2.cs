using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 
{
	/*
        Главное окно содержит кнопку «Изменить выравнивание». Она выдаёт модальное окно с выпадающими списками, имеющим разное выравнивание.
    При нажатии кнопок «ОК» или «Применить» главное окно изменяет свое положение;
    При нажатии кнопки «Отмена» модальное окно закрывается без выполнения дополнительных
    действий.
    */

    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            ActiveControl = button1;
            string[] format = { "По левому краю", "По центру", "По правому краю" };
            string[] format2 = { "По верхнему краю", "По центру", "По нижнему краю" };
            comboBox1.Items.AddRange(format);
            comboBox2.Items.AddRange(format2);
        }

        private void button1_Click(object sender, EventArgs e)//ok
        {
            if (comboBox1.Text.Length > 0)
                switch (comboBox1.Text[3])
                {
                    case 'л':
                        Owner.Controls["button1"].Left = 0;
                        Owner.Controls["button1"].Anchor = AnchorStyles.Left;
                        break;
                    case 'ц':
                        Owner.Controls["button1"].Left = Owner.ClientSize.Width / 2 - Owner.Controls["button1"].Width / 2;
                        Owner.Controls["button1"].Anchor = AnchorStyles.None;
                        break;
                    case 'п':
                        Owner.Controls["button1"].Left = Owner.ClientSize.Width - Owner.Controls["button1"].Width;
                        Owner.Controls["button1"].Anchor = AnchorStyles.Right;
                        break;
                }
            if (comboBox2.Text.Length > 0)
                switch (comboBox2.Text[3])
                {
                    case 'в':
                        Owner.Controls["button1"].Top = 0;
                        Owner.Controls["button1"].Anchor = Owner.Controls["button1"].Anchor | AnchorStyles.Top;
                        break;
                    case 'ц':
                        Owner.Controls["button1"].Top = Owner.ClientSize.Height / 2 - Owner.Controls["button1"].Height / 2;
                        Owner.Controls["button1"].Anchor = Owner.Controls["button1"].Anchor | AnchorStyles.None;
                        break;
                    case 'н':
                        Owner.Controls["button1"].Top = Owner.ClientSize.Height - Owner.Controls["button1"].Height;
                        Owner.Controls["button1"].Anchor = Owner.Controls["button1"].Anchor | AnchorStyles.Bottom;
                        break;
                }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)//cancel
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)//apply
        {
            if (comboBox1.Text.Length > 0)
                switch (comboBox1.Text[3])
                {
                    case 'л':
                        Owner.Controls["button1"].Left = 0;
                        Owner.Controls["button1"].Anchor = AnchorStyles.Left;
                        break;
                    case 'ц':
                        Owner.Controls["button1"].Left = Owner.ClientSize.Width / 2 - Owner.Controls["button1"].Width / 2;
                        Owner.Controls["button1"].Anchor = AnchorStyles.None;
                        break;
                    case 'п':
                        Owner.Controls["button1"].Left = Owner.ClientSize.Width - Owner.Controls["button1"].Width;
                        Owner.Controls["button1"].Anchor = AnchorStyles.Right;
                        break;
                }
            if (comboBox2.Text.Length > 0)
                switch (comboBox2.Text[3])
                {
                    case 'в':
                        Owner.Controls["button1"].Top = 0;
                        Owner.Controls["button1"].Anchor = Owner.Controls["button1"].Anchor | AnchorStyles.Top;
                        break;
                    case 'ц':
                        Owner.Controls["button1"].Top = Owner.ClientSize.Height / 2 - Owner.Controls["button1"].Height / 2;
                        Owner.Controls["button1"].Anchor = Owner.Controls["button1"].Anchor | AnchorStyles.None;
                        break;
                    case 'н':
                        Owner.Controls["button1"].Top = Owner.ClientSize.Height - Owner.Controls["button1"].Height;
                        Owner.Controls["button1"].Anchor = Owner.Controls["button1"].Anchor | AnchorStyles.Bottom;
                        break;
                }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }
    }
}
