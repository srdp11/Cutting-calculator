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
        int id = 1;
        DataTable order;
        string OrderName;
        DateTime OrderDate;
        bool isOrderSaved;
        bool isConncectionOpened;
        DBconnection conn = new DBconnection();
        List<bool> isEnough = new List<bool>();
        List<int> quantity = new List<int>();

        public Form1()
        {
            InitializeComponent();
           
            isConncectionOpened = conn.OpenConnection();
            string stm = "SELECT name FROM samtecs";
            MySqlDataAdapter da = new MySqlDataAdapter(stm, conn.getMySqlConnection());
            DataSet ds = new DataSet();

            da.Fill(ds, "samtecs");
            DataTable dt = ds.Tables["samtecs"];

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
            order = new DataTable();

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

            this.orderDataGrid.DataError +=
                new DataGridViewDataErrorEventHandler(orderDataGrid_DataError);
        }

        public DataTable getTable()
        {
           return this.order;
        }

        private void orderDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("Ошибка!\nПроверьте корректность данных!");
        }


        private void addPossition_Click(object sender, EventArgs e)
        {
            try
            {
                order.Rows.Add(id, partNumbersCmbx.SelectedValue.ToString(), Int32.Parse(quantityTextBox.Text.ToString()));
                bool isQuantityEnough = false;
                int avalibleQuantity = 0;
                
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataTable cutting = new DataTable();

                MySqlCommand cmd = conn.getMySqlConnection().CreateCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();

                cmd.CommandText = "SELECT avalible FROM samtecs where name = @Name";
                cmd.Parameters.AddWithValue("@Name", order.Rows[id - 1].ItemArray[1].ToString());

                cmd.Prepare();

                da.SelectCommand = cmd;

                da.Fill(ds, "samtecs");
                dt = ds.Tables["samtecs"];

                avalibleQuantity = Int32.Parse(dt.Rows[0][0].ToString());
                
                quantity.Add(avalibleQuantity);

                if (avalibleQuantity >= Int32.Parse(order.Rows[id - 1][2].ToString()))
                {
                    isQuantityEnough = true;
                    isEnough.Add(isQuantityEnough);
                }
                else
                {
                    isEnough.Add(isQuantityEnough);
                }

                if (isQuantityEnough)
                {
                   orderDataGrid.Rows[id - 1].Cells[2].Style.BackColor = Color.MediumSpringGreen;
                }
                else
                {
                    orderDataGrid.Rows[id - 1].Cells[2].Style.BackColor = Color.Tomato;
                }
                id++;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString());
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
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                List<int> tempQuantity = new List<int>();
                DataTable cutting = new DataTable();

                var parameters = new string[order.Rows.Count];
                MySqlCommand cmd = conn.getMySqlConnection().CreateCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
               
                for (int i = 0; i < order.Rows.Count; i++)
                {
                    parameters[i] = string.Format("@Name{0}", i);
                    cmd.Parameters.AddWithValue(parameters[i], order.Rows[i].ItemArray[1].ToString());
                }

                cmd.CommandText = string.Format("SELECT article as 'id', name, residue, reserve, expectation, avalible, minbalance  FROM samtecs where name in ({0})", string.Join(", ", parameters));
              
                da.SelectCommand = cmd;
                da.Fill(ds, "samtecs");
                dt = ds.Tables["samtecs"];

                for (int i = 0; i < order.Rows.Count; i++)
                {
                    
                }

                Form2 childForm2 = new Form2(dt);
                childForm2.Owner = this;
                childForm2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Заказ не сохранен!");
            }
        }

        private void partNumbersCmbx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void close_Click_1(object sender, EventArgs e)
        {
            conn.CloseConnection();
            this.Close();
        }

        private void orderDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }

}
