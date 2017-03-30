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
using System.Text.RegularExpressions;
using System.Collections;

namespace cutting_calculator
{
    public partial class Form1 : Form
    {
        DataTable order;
        DataTable cutting;
        DataTable samtecs;
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

            string queryCuttingData = "SELECT * FROM cutting";
            MySqlDataAdapter da1 = new MySqlDataAdapter(queryCuttingData, conn.getMySqlConnection());

            string querySamtecsData = "SELECT * FROM samtec where name LIKE '%-1%'";
            MySqlDataAdapter da2 = new MySqlDataAdapter(querySamtecsData, conn.getMySqlConnection());

            DataSet cuttingDataSet = new DataSet();
            DataSet samtecsDataSet = new DataSet();

            da1.Fill(cuttingDataSet, "cutting");
            da2.Fill(samtecsDataSet, "samtec");

            samtecs = samtecsDataSet.Tables["samtec"];
            cutting = cuttingDataSet.Tables["cutting"];

            partNumbersCmbx.DataSource = samtecs;
            partNumbersCmbx.DisplayMember = "name";
            partNumbersCmbx.ValueMember = "name";
            partNumbersCmbx.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            partNumbersCmbx.AutoCompleteSource = AutoCompleteSource.ListItems;

            isOrderSaved = false;

            this.Load += new EventHandler(Form1_Load);
            //test();
        }

        public class PartsComparer : IComparer
        {
            public int Compare(Object x, Object y)
            {
                DataRow a = (DataRow)x;
                DataRow b = (DataRow)y;
                
                Int32 aFrom = getPinsNumber(a.ItemArray[1].ToString());
                Int32 bFrom = getPinsNumber(b.ItemArray[1].ToString());

                Int32 to = getPinsNumber(a.ItemArray[2].ToString());

                if (aFrom % to == 0 && bFrom % to == 0)
                {
                    return aFrom.CompareTo(bFrom);
                }
                else if (aFrom % to == 0 && bFrom % to != 0)
                {
                    return -1;
                }
                else if (aFrom % to != 0 && bFrom % to == 0)
                {
                    return 1;
                }
                else
                {
                    return aFrom.CompareTo(bFrom);
                }
            }
        }

        public void test()
        {
            DataView view = new DataView(cutting);
            DataTable uniqueParts = view.ToTable(true, "cutting_result_name");
            int i = 0;

            foreach (DataRow uniqueRow in uniqueParts.Rows)
            {
                String partName = uniqueRow.ItemArray[0].ToString();

                String query = String.Format("cutting_result_name='{0}'", partName);
                DataRow[] partsForCutting = cutting.Select(query);

                Array.Sort(partsForCutting, new PartsComparer());

                Console.WriteLine(String.Format("{0}/{1}", i++, uniqueParts.Rows.Count));

                Console.WriteLine("From:");
                foreach (DataRow row in partsForCutting)
                {
                    Console.WriteLine(row.ItemArray[1]);
                }

                Console.WriteLine("To:");
                foreach (DataRow row in partsForCutting)
                {
                    Console.WriteLine(row.ItemArray[2]);
                }

                Console.ReadLine();
                Console.Clear();
            }
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
            //order.PrimaryKey = new DataColumn[] { order.Columns["ID"] };

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

            Console.WriteLine(order.Rows[rowIdx][1].ToString());

            DataRow[] part1 = samtecs.Select(String.Format("name = '{0}'", order.Rows[rowIdx][1].ToString()));
            DataRow part = part1[0];

            avalible = Int32.Parse(part["avalible"].ToString());
            residue = Int32.Parse(part["residue"].ToString());
            expectation = Int32.Parse(part["expectation"].ToString());
            reserve = Int32.Parse(part["reserve"].ToString());
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
                //DataSet ds = new DataSet();
                //DataTable dt = new DataTable();
                //DataTable dt1 = new DataTable();

                //List<int> tempQuantity = new List<int>();
                //DataTable cutting = new DataTable();

                //int cuttingQty = 0;

                //foreach(bool isEnoughQty in isEnough)
                //{
                //    if(!isEnoughQty)
                //        cuttingQty++;
                //}

                //var parameters = new string[cuttingQty];
                //MySqlCommand cmd = conn.getMySqlConnection().CreateCommand();
                //MySqlDataAdapter da = new MySqlDataAdapter();

                //int j = 0;
                try
                {
                    for (int i = 0; i < isEnough.Count; i++)
                    {
                        if (!isEnough[i])
                        {
                            PartInfo partInfo = getCuttingPlan(order.Rows[i]);
                            // TODO: remove break
                            break;
                        }
                    }
                }
                    //cmd.CommandText = string.Format("SELECT article as 'id', name, residue, reserve, expectation, avalible, minbalance  FROM samtec where name in ({0})", string.Join(",", parameters));

                    //da.SelectCommand = cmd;
                    //da.Fill(ds, "samtec");
                    //dt = ds.Tables["samtec"];

                    //ds.Clear();

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
               
                Form2 childForm2 = new Form2(new DataTable());
                childForm2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Заявка не сохранена!");
            }
        }

        private static Int32 getPinsNumber(String partName)
        {
            Regex regex = new Regex("\\d+");
            Int32 contactOption = Int32.Parse(regex.Match(partName).Value);
            return contactOption % 100;
        }

        private Boolean isReusable(String partResidueName)
        {
            DataRow[] residueNames = cutting.Select(String.Format("cutting_result_name = '{0}'", partResidueName));
            return residueNames.Length != 0;
        }

        private String getResiduePartName(String sourcePart, Int32 residuePinsNum)
        {
            Regex regex = new Regex("\\d+");
            Int32 newPins = 100 + residuePinsNum;
            String residuePartName = regex.Replace(sourcePart, newPins.ToString(), 1);
            return residuePartName;
        }

        private PartInfo getCuttingPlan(DataRow partRow)
        {
            DataTable plan = new DataTable();
            plan.Columns.Add("Part #");
            plan.Columns.Add("Quantity");
            plan.Columns.Add("Avalible");
            plan.Columns.Add("CuttingNumber");
            plan.Columns.Add("CuttingPart");
            plan.Columns.Add("ResiduePartName");
            plan.Columns.Add("Residue");

            DataRow mainRow = plan.NewRow();

            String partName = partRow["Part #"].ToString();
            Int32 reqQuantity = Int32.Parse(partRow["Quantity"].ToString());
            Int32 reqPinsNum = getPinsNumber(partName);

            DataRow partInSamtec = samtecs.Select(String.Format("name = '{0}'", partName))[0];
            Int32 avalible = Int32.Parse(partInSamtec["avalible"].ToString());
            Int32 cuttingNumber;

            if (avalible > 0)
            {
                cuttingNumber = reqQuantity - avalible;
            }
            else
            {
                cuttingNumber = reqQuantity;
            }

            PartInfo partInfo = new PartInfo(partName, reqQuantity, cuttingNumber);
            DataRow[] partsForCutting = cutting.Select(String.Format("cutting_result_name = '{0}'", partName));

            Array.Sort(partsForCutting, new PartsComparer());

            foreach (DataRow partsPair in partsForCutting)
            {
                DataRow part = samtecs.Select(String.Format("name = '{0}'", partsPair["cutting_name"]))[0];
                String cutPartName = part["name"].ToString();
                Int32 pinsNum = getPinsNumber(cutPartName);
                Int32 pinsResidue = reqPinsNum % pinsNum;

                if (!isReusable(getResiduePartName(cutPartName, pinsResidue)))
                {
                    continue;
                }

                Int32 quantity = Int32.Parse(part["avalible"].ToString());

                if (quantity <= 0)
                {
                    continue;
                }

                cuttingNumber -= quantity * (reqPinsNum / pinsNum);

                partInfo.AddCutStep(cuttingNumber, cutPartName, quantity * (reqPinsNum / pinsNum), getResiduePartName(cutPartName, pinsResidue));

                // update data in table 'samtec' 
                part["avalible"] = Int32.Parse(part["avalible"].ToString()) - quantity * (reqPinsNum / pinsNum);

               

                if (cuttingNumber <= 0)
                {
                    break;
                }
            }

            if (cuttingNumber <= 0)
            {
                partInfo.IsCutted = true;
            }

            return partInfo;
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
