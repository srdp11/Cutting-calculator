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
        DataTable order;
        string OrderName;
        DateTime OrderDate;
        bool isOrderSaved;
        bool isConncectionOpened;
        DBconnection conn = new DBconnection();
        int currId = 1;
        List<bool> isEnough = new List<bool>();

        public Form1()
        {
            InitializeComponent();
           
            isConncectionOpened = conn.OpenConnection();
            string stm = "SELECT name FROM samtecs where name like '%-1%'";
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
            this.orderDataGridView.DataError +=
                new DataGridViewDataErrorEventHandler(orderDataGrid_DataError);
            this.orderDataGridView.CellValueChanged += new DataGridViewCellEventHandler(orderDataGridView_CellValueChanged);
            this.orderDataGridView.ColumnAdded += new DataGridViewColumnEventHandler(orderDataGridView_ColumnAdded);

            order = new DataTable();

            order.Columns.Add("ID", typeof(uint));
            order.Columns.Add("Part #", typeof(string));
            order.Columns.Add("Quantity", typeof(uint));
            order.PrimaryKey = new DataColumn[] { order.Columns["ID"] };

            order.Columns[0].AllowDBNull = false;
            order.Columns[1].AllowDBNull = false;
            order.Columns[2].AllowDBNull = false;

            order.Columns[1].Unique = true;

            orderDataGridView.DataSource = order;
            orderDataGridView.Columns[0].ReadOnly = true;
            orderDataGridView.Columns[1].ReadOnly = true;
            orderDataGridView.Columns[2].ReadOnly = false;
        }

        public DataTable getTable()
        {
           return this.order;
        }

        private void orderDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("Ошибка!\nПроверьте корректность данных!");
        }

        private void setCellCollor(int mode, int rowIdx)
        {
            if (mode == 1)
            {
                orderDataGridView.Rows[rowIdx].Cells[2].Style.BackColor = Color.LawnGreen;
            }
            else if (mode == 2)
            {
                orderDataGridView.Rows[rowIdx].Cells[2].Style.BackColor = Color.Yellow;
            }
            else if (mode == -1)
            {
                orderDataGridView.Rows[rowIdx].Cells[2].Style.BackColor = Color.OrangeRed;
            }
        }

        public int checkQuantity(int rowIdx)
        {
            int avalible = 0;
            int residue = 0;
            int expectation = 0;
            int orderQuantity = 0;
            int reserve = 0;
            int mode;


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable cutting = new DataTable();

            MySqlCommand cmd = conn.getMySqlConnection().CreateCommand();
            MySqlDataAdapter da = new MySqlDataAdapter();

            cmd.CommandText = "SELECT avalible, residue, expectation, reserve FROM samtecs where name = @Name";
            cmd.Parameters.AddWithValue("@Name", order.Rows[rowIdx][1]);

            cmd.Prepare();

            da.SelectCommand = cmd;

            da.Fill(ds, "samtecs");
            dt = ds.Tables["samtecs"];

            avalible = Int32.Parse(dt.Rows[0][0].ToString());
            residue = Int32.Parse(dt.Rows[0][1].ToString());
            expectation = Int32.Parse(dt.Rows[0][2].ToString());
            reserve = Int32.Parse(dt.Rows[0][3].ToString());
            orderQuantity = Int32.Parse(order.Rows[order.Rows.Count - 1][2].ToString());

            if (residue - reserve >= orderQuantity)
            {
                mode = 1;
            }
            else 
            {
                if ((expectation > 0) && (avalible >= orderQuantity))
                    mode = 2;
                else
                    mode = -1;
            }
            
            setCellCollor(mode, rowIdx);

            return mode;
        }

        private void addPossition_Click(object sender, EventArgs e)
        {
            try
            {
                order.Rows.Add(currId, partNumbersCmbx.SelectedValue.ToString(), Int32.Parse(quantityTextBox.Text.ToString()));
                int quantityCheckResult = checkQuantity(order.Rows.Count - 1);

                if (quantityCheckResult != -1)
                    isEnough.Add(true);
                else
                    isEnough.Add(false);
                currId++;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString());
            }
            quantityTextBox.Text = "";
        }

        private void resetId()
        {
            for (int i = 0; i < order.Rows.Count; i++)
            {
                order.Rows[i][0] = i + 1;
            }
            currId = order.Rows.Count + 1;
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
            foreach (DataGridViewRow currentRow in orderDataGridView.SelectedRows)
            {
                order.Rows[currentRow.Index].Delete();
            }
            resetId();
        }

        private void calculateCutting_Click(object sender, EventArgs e)
        {
            if (isOrderSaved)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                List<int> tempQuantity = new List<int>();
                DataTable cutting = new DataTable();

                int cuttingQty = 0;

                foreach(bool isEnoughQty in isEnough)
                {
                    if(!isEnoughQty)
                        cuttingQty++;
                }

                var parameters = new string[cuttingQty];
                MySqlCommand cmd = conn.getMySqlConnection().CreateCommand();
                MySqlDataAdapter da = new MySqlDataAdapter();
                
                int j = 0;
                try
                {
                  for (int i = 0; i < isEnough.Count; i++)
                  {
                    if (!isEnough[i])
                    {
                        parameters[j] = string.Format("@Name{0}", j);
                        cmd.Parameters.AddWithValue(parameters[j], order.Rows[i].ItemArray[1].ToString());
                        j++;
                   }
                }
                    cmd.CommandText = string.Format("SELECT article as 'id', name, residue, reserve, expectation, avalible, minbalance  FROM samtecs where name in ({0})", string.Join(",", parameters));

                    da.SelectCommand = cmd;
                    da.Fill(ds, "samtecs");
                    dt = ds.Tables["samtecs"];

                    ds.Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
               
                Form2 childForm2 = new Form2(dt);
                childForm2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Заявка не сохранена!");
            }
        }

        private void orderDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (orderDataGridView.Columns[e.ColumnIndex].Name == "Quantity")
            {
                int quantityCheckResult = checkQuantity(e.RowIndex);
                if (quantityCheckResult == -1)
                    isEnough[e.RowIndex] = false;
            }
        }

        private void orderDataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            orderDataGridView.Columns[e.Column.Index].SortMode = DataGridViewColumnSortMode.NotSortable;
        }


        private void partNumbersCmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void close_Click(object sender, EventArgs e)
        {
            conn.CloseConnection();
            this.Close();
        }

        private void orderDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }

}
