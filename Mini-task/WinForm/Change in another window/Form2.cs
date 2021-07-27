using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
     Главное окно содержит шесть TextBox с текстом «1»–«6» и «Show». При нажатии «Show» появляется второе окно, содержащее
шесть CheckBox. При установке флажка текст соответствующего поля ввода выделяется полужирным шрифтом, при снятии отменяется. При изменении текста какого-либо поля ввода должна изменяться подпись соответствующего флажка. 
     */

namespace 
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Owner.Controls["textBox" + ((CheckBox)sender).Tag].Font.Style != FontStyle.Bold)
                Owner.Controls["textBox" + ((CheckBox)sender).Tag].Font = new Font(FontFamily.GenericSansSerif,
            8.25F, FontStyle.Bold);
            else
                Owner.Controls["textBox" + ((CheckBox)sender).Tag].Font = new Font(FontFamily.GenericSansSerif,
            8.25F, FontStyle.Regular);
        }
    }
}
