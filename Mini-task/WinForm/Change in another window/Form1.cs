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
    public partial class Form1 : Form
    {
        private Form2 form2 = new Form2();
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            AddOwnedForm(form2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form2.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            form2.Controls["checkBox" + ((TextBox)sender).Tag].Text = ((TextBox)sender).Text + "";
        }
    }
}
