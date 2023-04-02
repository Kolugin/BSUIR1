using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace MyCourseProject
{
    public partial class Form4 : Form
    {
        List<string> Result = new List<string>();
        public static string ConnectSTR = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MYTESTDB.mdb";
        private OleDbConnection dbCon;
        string a = "";
        int PAns = 0;
        public Form4()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e) //Кнопка Назад
        {
            comboBox1.Items.Clear();
            Form1 NewForm = new Form1();
            this.Close();
            NewForm.Show();
        }
        private void button1_Click(object sender, EventArgs e) // Кнопка Просмотр
        {
            a = comboBox1.Text;
            String[] words = a.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            dbCon = new OleDbConnection(ConnectSTR);
            if (comboBox1.SelectedIndex != -1)
            {
                dbCon.Open();
                OleDbDataAdapter dbAdapter = new OleDbDataAdapter(@"SELECT Registry.Quest, Registry.ChtoOtvetil, Registry.PravOtvet FROM Registry WHERE (((Registry.FIO)='" + words[0] + "') AND ((Registry.Speciality)='" + words[1] + "') AND ((Registry.Otdel)='" + words[2] + "') AND ((Registry.DataTest)='" + words[3] + "') AND ((Registry.NameTest)='" + words[4] + "') AND ((Registry.TypeTest)='" + words[5] + "'))", dbCon);
                DataTable dataTable = new DataTable();
                dbAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns[0].HeaderText = "Вопрос";
                dataGridView1.Columns[1].HeaderText = "Что ответил";
                dataGridView1.Columns[2].HeaderText = "Правильный ответ";
                dbCon.Close();
                int rows = dataGridView1.Rows.Count;
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    if (Convert.ToString(dataGridView1.Rows[j].Cells[1].Value) != Convert.ToString(dataGridView1.Rows[j].Cells[2].Value))
                    {
                        dataGridView1.Rows[j].Cells[1].Style.BackColor = Color.Red;
                        dataGridView1.Rows[j].Cells[2].Style.BackColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[j].Cells[1].Style.BackColor = Color.Green;
                        dataGridView1.Rows[j].Cells[2].Style.BackColor = Color.Green;
                        PAns++;
                    }
                    if (Convert.ToString(dataGridView1.Rows[j].Cells[1].Value) == "" && Convert.ToString(dataGridView1.Rows[j].Cells[2].Value) == "")
                    {
                        dataGridView1.Rows[j].Cells[1].Style.BackColor = Color.White;
                        dataGridView1.Rows[j].Cells[2].Style.BackColor = Color.White;
                        PAns--;
                    }
                }
                if (PAns < rows / 2)
                {
                    MessageBox.Show("Данный тест был пройден на неудовлетворительную оценку. Количество правильных ответов меньше половины от общего числа вопросов", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (PAns >= rows / 2)
                {
                    MessageBox.Show("Данный тест был пройден на удовлетворительную оценку. Количество правильных ответов больше либо равно половине от общего числа вопросов", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                dbCon.Close();  
            }
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Не выбран результат для просмотра результатов.");
                return;
            }
        }

        private void Form4_Load(object sender, EventArgs e) //Загрузка формы
        {
            try
            {
                Result.Clear();
                dbCon = new OleDbConnection(ConnectSTR);
                dbCon.Open();
                using (dbCon)
                {
                    OleDbCommand cmd = new OleDbCommand("SELECT Registry.FIO, Registry.Speciality, Registry.Otdel, Registry.DataTest, Registry.NameTest, Registry.TypeTest FROM Registry;", dbCon);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Result.Add(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2) + " " + reader.GetString(3) + " " + reader.GetString(4) + " " + reader.GetString(5));
                    }
                }
                foreach (string item in Result)
                {
                    if (comboBox1.Items.Contains(item))
                    {

                    }
                    else
                    {
                        comboBox1.Items.Add(item);
                    }
                }
                dbCon.Close();
            }
            catch
            {
                MessageBox.Show("Просмотр результатов невозможен, так как их нету в таблице с результатами. Проведите хотя бы 1 тест либо экзамен.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Form1 NewForm = new Form1();
                this.Close();
                NewForm.Show();
            }
        }
    }
}