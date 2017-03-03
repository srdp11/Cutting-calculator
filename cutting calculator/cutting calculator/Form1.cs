using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace cutting_calculator
{
    public partial class Form1 : Form
    {
        uint id = 1;
        DataTable order = new DataTable();
        string OrderName;
        DateTime OrderDate;
        bool isOrderSaved;
        DBconnection conn = new DBconnection();

        public Form1()
        {
            InitializeComponent();
            
            order.Columns.Add("ID", typeof(uint));
            order.Columns.Add("Part #", typeof(string));
            order.Columns.Add("Quantity", typeof(uint));
            order.PrimaryKey = new DataColumn[] { order.Columns["ID"] };
            
            order.Columns[0].AllowDBNull = false;
            order.Columns[1].AllowDBNull = false;
            order.Columns[2].AllowDBNull = false;

            orderDataGrid.DataSource = order;
            orderDataGrid.Columns[0].ReadOnly = true;
            orderDataGrid.Columns[1].ReadOnly = true;
            orderDataGrid.Columns[2].ReadOnly = false;

            bool isConncectionOpened = conn.OpenConnection();
            string stm = "SELECT name FROM samtecs";
            MySqlDataAdapter da = new MySqlDataAdapter(stm, conn.getMySqlConnection());
            DataSet ds = new DataSet();

            da.Fill(ds, "samtecs");
            DataTable dt = ds.Tables["samtecs"];

            //isConncectionOpened = conn.CloseConnection();

            partNumbersCmbx.DataSource = dt;
            partNumbersCmbx.DisplayMember = "name";
            partNumbersCmbx.ValueMember = "name";
            partNumbersCmbx.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            partNumbersCmbx.AutoCompleteSource = AutoCompleteSource.ListItems;

            isOrderSaved = false;

            this.Load += new EventHandler(Form1_Load);
        }


        private void Form1_Load(Object sender, EventArgs e)
        {
            this.orderDataGrid.DataError +=
                new DataGridViewDataErrorEventHandler(orderDataGrid_DataError);
        }

        private void orderDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("Ошибка!\nПроверьте корректность данных!");
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addPossition_Click(object sender, EventArgs e)
        {
            try
            {
                order.Rows.Add(id, partNumbersCmbx.SelectedValue.ToString(), Int32.Parse(quantityTextBox.Text.ToString()));
                id++;
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректные данные!");
            }
            quantityTextBox.Text = "";
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (orderTextBox.Text == "")
            {
                MessageBox.Show("Введите № заявки!");
                return;
            }
           
            OrderName = orderTextBox.Text.ToString();
            OrderDate = dateTimePicker1.Value;

            if (order.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных по позициям в заявке!");
                return;
            }
            else
            {
                isOrderSaved = true;
            }
        }

        private void deletePosition_Click(object sender, EventArgs e)
        {
            if (orderDataGrid.CurrentRow != null)
            {
                int deletedNum = orderDataGrid.CurrentRow.Index;
                orderDataGrid.Rows.Remove(orderDataGrid.CurrentRow);
            }  
        }

        private void calculateCutting_Click(object sender, EventArgs e)
        {
            if (isOrderSaved)
            {
                Form2 childForm = new Form2();
                childForm.ShowDialog();


            }
            else
            {
                MessageBox.Show("Заказ не сохранен!");
            }
        }

        private void partNumbersCmbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
