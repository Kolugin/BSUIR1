using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MyCourseProject
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)//Паскаль
        {
            Process.Start(Path.Combine(Application.StartupPath, "Material\\PascalABC.chm"));
        }
        private void button2_Click(object sender, EventArgs e)//Делфи
        {
            Process.Start(Path.Combine(Application.StartupPath, "Material\\Delphi7.chm"));
        }
        private void button3_Click(object sender, EventArgs e)//Плюсы
        {
            Process.Start(Path.Combine(Application.StartupPath, "Material\\CPlus.chm"));
        }
        private void button4_Click(object sender, EventArgs e)//Примеры плюсов
        {
            Process.Start(Path.Combine(Application.StartupPath, "Material\\CPlusPrim.chm"));
        }
        private void button5_Click(object sender, EventArgs e)//Шарпик
        {
            Process.Start(Path.Combine(Application.StartupPath, "Material\\CSharp.chm"));
        }
        private void button6_Click(object sender, EventArgs e)//Назад
        {
            Form1 NewForm = new Form1();
            this.Close();
            NewForm.Show();
        }
    }
}