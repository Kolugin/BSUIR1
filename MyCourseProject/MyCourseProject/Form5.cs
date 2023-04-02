using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace MyCourseProject
{
    public partial class Form5 : Form
    {
        private string a;
        public static string ConnectSTR = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MYTESTDB.mdb";
        private OleDbConnection dbCon;
        OleDbCommand cmd;
        public Form5()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e) // Создание теста
        {
            if (textBox1.Text != "")
            {
                try
                {
                    dbCon = new OleDbConnection(ConnectSTR);
                    dbCon.Open();
                    cmd = new OleDbCommand("CREATE TABLE " + textBox1.Text.ToString() + "(TQ INTEGER, Q TEXT, A1 TEXT, A2 TEXT, A3 TEXT, A4 TEXT, A5 TEXT, A6 TEXT, A7 TEXT, A8 TEXT, A9 TEXT, A10 TEXT, PA1 TEXT, PA2 TEXT, PA3 TEXT, PA4 TEXT, PA5 TEXT, KPA INTEGER)", dbCon);
                    cmd.ExecuteNonQuery();
                    DataTable tbls = dbCon.GetSchema("Tables", new string[] { null, null, null, "TABLE" }); //список всех таблиц
                    comboBox1.Items.Clear();
                    foreach (DataRow row in tbls.Rows)
                    {
                        string TableName = row["TABLE_NAME"].ToString();
                        if (TableName == "Registry")
                        {
                            comboBox1.Items.Remove("Registry");
                        }
                        else
                        {
                            comboBox1.Items.Add(TableName);
                        }
                    }
                    DataTable dataTable = new DataTable();
                    dataGridView1.DataSource = dataTable;
                    dbCon.Close();
                    MessageBox.Show("Новая таблица для теста успешно создана.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                }
                catch (Exception)
                {
                    DialogResult result = MessageBox.Show("Вы пытаетесь присвоить тесту имя которое уже есть. Введите другое название в поле.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        textBox1.Text = "";
                    }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Вы не ввели название для теста. Введите название в поле.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    textBox1.Text = "";
                }
            }
        }
        private void button2_Click(object sender, EventArgs e) // Редактирование теста
        {
            if (comboBox1.SelectedIndex != -1)
            {
                dbCon = new OleDbConnection(ConnectSTR);
                dbCon.Open();
                OleDbDataAdapter dbAdapter = new OleDbDataAdapter(@"SELECT * FROM " + comboBox1.SelectedItem, dbCon);
                DataTable dataTable = new DataTable();
                dbAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.CurrentCell = null;
                dataGridView1.Columns[0].HeaderText = "Тип вопроса (1, 2 или 3)";
                dataGridView1.Columns[1].HeaderText = "Текст вопроса";
                dataGridView1.Columns[2].HeaderText = "Вариант ответа 1";
                dataGridView1.Columns[3].HeaderText = "Вариант ответа 2";
                dataGridView1.Columns[4].HeaderText = "Вариант ответа 3";
                dataGridView1.Columns[5].HeaderText = "Вариант ответа 4";
                dataGridView1.Columns[6].HeaderText = "Вариант ответа 5";
                dataGridView1.Columns[7].HeaderText = "Вариант ответа 6";
                dataGridView1.Columns[8].HeaderText = "Вариант ответа 7";
                dataGridView1.Columns[9].HeaderText = "Вариант ответа 8";
                dataGridView1.Columns[10].HeaderText = "Вариант ответа 9";
                dataGridView1.Columns[11].HeaderText = "Вариант ответа 10";
                dataGridView1.Columns[12].HeaderText = "Вариант верного ответа 1";
                dataGridView1.Columns[13].HeaderText = "Вариант верного ответа 2";
                dataGridView1.Columns[14].HeaderText = "Вариант верного ответа 3";
                dataGridView1.Columns[15].HeaderText = "Вариант верного ответа 4";
                dataGridView1.Columns[16].HeaderText = "Вариант верного ответа 5";
                dataGridView1.Columns[17].HeaderText = "Количество верных ответов (от 1 до 5)";
                dbCon.Close();
            }
            else
            {
                MessageBox.Show("Вы не выбрали тест для редактирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button6_Click(object sender, EventArgs e) // Добавление нового вопроса
        {
            if (comboBox1.SelectedIndex != -1)
            {
                a = comboBox1.Text;
                Form6 NewForm = new Form6(a);
                NewForm.Show();
            }
            else
            {
                MessageBox.Show("Вы не выбрали таблицу с тестом для добавления вопроса. Выберите таблицу из списка.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button4_Click(object sender, EventArgs e) //Выход
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите закрыть окно конструктор?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Form1 NewForm = new Form1();
                this.Close();
                NewForm.Show();
            }
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            dbCon = new OleDbConnection(ConnectSTR);
            dbCon.Open();
            DataTable tbls = dbCon.GetSchema("Tables", new string[] { null, null, null, "TABLE" }); //список всех таблиц
            foreach (DataRow row in tbls.Rows)
            {
                string TableName = row["TABLE_NAME"].ToString();
                if (TableName == "Registry")
                {
                    comboBox1.Items.Remove("Registry");
                }
                else
                {
                    comboBox1.Items.Add(TableName);
                }
            }
            dbCon.Close();
        }

        private void button3_Click(object sender, EventArgs e) //Удаление таблицы
        {
            if (comboBox1.SelectedIndex != -1)
            {
                try
                {
                    dbCon = new OleDbConnection(ConnectSTR);
                    dbCon.Open();
                    OleDbCommand com = new OleDbCommand("DROP TABLE " + comboBox1.SelectedItem.ToString(), dbCon);
                    com.ExecuteNonQuery();
                    DataTable tbls = dbCon.GetSchema("Tables", new string[] { null, null, null, "TABLE" }); //список всех таблиц
                    comboBox1.Items.Clear();
                    foreach (DataRow row in tbls.Rows)
                    {
                        string TableName = row["TABLE_NAME"].ToString();
                        if (TableName == "Registry")
                        {
                            comboBox1.Items.Remove("Registry");
                        }
                        else
                        {
                            comboBox1.Items.Add(TableName);
                        }
                    }
                    DataTable dataTable = new DataTable();
                    dataGridView1.DataSource = dataTable;
                    dbCon.Close();
                    MessageBox.Show("Выбранный тест успешно удален.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                }
                catch (Exception)
                {
                    DialogResult result = MessageBox.Show("Вы не выбрали тест для удаления. Выберите тест для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                    {
                        textBox1.Text = "";
                    }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show("Вы не выбрали тест для удаления. Выберите тест для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    textBox1.Text = "";
                }
            }
        }
    }
}