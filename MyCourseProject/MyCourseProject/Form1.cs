using MyCourceProject;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace MyCourseProject
{
    public partial class Form1 : Form
    {
        public static string ConnectSTR = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MYTESTDB.mdb";
        private OleDbConnection dbCon;
        private string FIO;
        private string Spec;
        private string Otdel;
        private string Datatest;
        private string TypeTest;
        private string NameTest;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            textBox4.Text = Convert.ToString(date.ToShortDateString());
            dbCon = new OleDbConnection(ConnectSTR);
            dbCon.Open();
            DataTable tbls = dbCon.GetSchema("Tables", new string[] { null, null, null, "TABLE" }); //список всех таблиц
            foreach (DataRow row in tbls.Rows)
            {
                string TableName = row["TABLE_NAME"].ToString();
                if (TableName == "Registry")
                {
                    comboBox1.Items.Add(TableName);
                    comboBox1.Items.Remove("Registry");
                }
                else
                {
                    comboBox1.Items.Add(TableName);
                }
            }
            dbCon.Close();
        }
        private void button1_Click(object sender, EventArgs e) // Кнопка Тестирование
        {
            label1.Visible = true; label2.Visible = true; label3.Visible = true; label4.Visible = true; label5.Visible = true;
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
            textBox1.Visible = true; textBox2.Visible = true; textBox3.Visible = true; textBox4.Visible = true;
            button1.Visible = false; button2.Visible = false; button3.Visible = false; button4.Visible = false; button5.Visible = false; button6.Visible = false; button7.Visible = true; button8.Visible = true;
        }
        private void button7_Click(object sender, EventArgs e) // Кнопка Регистрация
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Одно из полей для заполнения пустое. Заполните все поля", "Пустые поля", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                FIO = textBox1.Text; Spec = textBox2.Text; Otdel = textBox3.Text; Datatest = textBox4.Text;  
                comboBox1.Visible = true;
                radioButton1.Visible = true; radioButton2.Visible = true;
                label1.Visible = false; label2.Visible = false; label3.Visible = false; label4.Visible = false; label5.Visible = false; label6.Visible = true; label7.Visible = true;
                button7.Visible = false; button8.Visible = false; button9.Visible = true;
                textBox1.Visible = false; textBox2.Visible = false; textBox3.Visible = false; textBox4.Visible = false;
            }
        }
        private void button2_Click(object sender, EventArgs e) // Кнопка Результаты
        {
            Form4 NewForm = new Form4();
            this.Hide();
            NewForm.Show();
        }
        private void button3_Click(object sender, EventArgs e) // Кнопка Обучение
        {
            Form3 NewForm = new Form3();
            this.Hide();
            NewForm.Show();
        }
        private void button4_Click(object sender, EventArgs e) // Кнопка Конструктор
        {
            Form5 NewForm = new Form5();
            this.Hide();
            NewForm.Show();
        }
        private void button5_Click(object sender, EventArgs e) // Кнопка О Программе
        {
            MessageBox.Show("Обучающе-тестирующее программное средство на базе языка программирования C# и СУБД Access. Данное ПО предназначенно для тестирования и экзаменации. Автор курсового проекта учащийся группы 181071 БГУИР Чиж Н. В. Все права защищены. 2022 год.", "О Программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button6_Click(object sender, EventArgs e) // Выход
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void button8_Click(object sender, EventArgs e) // Назад
        {
            label1.Visible = false; label2.Visible = false; label3.Visible = false; label4.Visible = false; label5.Visible = false;
            textBox1.Visible = false; textBox2.Visible = false; textBox3.Visible = false; textBox4.Visible = false;
            button1.Visible = true; button2.Visible = true; button3.Visible = true; button4.Visible = true; button5.Visible = true; button6.Visible = true; button7.Visible = false; button8.Visible = false;
        }
        private void button9_Click(object sender, EventArgs e) // Запуск тестирования
        {
            if ((comboBox1.SelectedIndex != -1) && (radioButton1.Checked == true || radioButton2.Checked == true))
            {
                if (radioButton1.Checked == true)
                {
                    TypeTest = "Тренировка";
                    NameTest = Convert.ToString(comboBox1.Text);
                }
                if (radioButton2.Checked == true)
                {
                    TypeTest = "Экзамен";
                    NameTest = Convert.ToString(comboBox1.Text);
                }
                Form2 NewForm = new Form2(FIO, Spec, Otdel, Datatest, NameTest, TypeTest, ConnectSTR);
                this.Hide();
                NewForm.Show();
            }
            else
            {
                MessageBox.Show("Не выбран тест из списка либо не выбран тип тестирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
