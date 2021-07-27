using System;
using System.Drawing;
using System.Windows.Forms;

namespace 
{
	/*
        Главное окно содержит кнопку «Изменить выравнивание». Она выдаёт модальное окно с выпадающими списками, имеющим разное выравнивание.
    При нажатии кнопок «ОК» или «Применить» главное окно изменяет свое положение;
    При нажатии кнопки «Отмена» модальное окно закрывается без выполнения дополнительных
    действий.
    */

    public partial class Form1 : Form
    {
        private Form2 form2 = new Form2();
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            AddOwnedForm(form2);
            form2.MaximizeBox = false;
            form2.MinimizeBox = false;
            form2.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            form2.Show();
        }
    }
}
