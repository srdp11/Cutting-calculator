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
    public partial class Form2 : Form
    {
        DataTable cutting = new DataTable();
        DBconnection conn = new DBconnection();
        DataTable ord = new DataTable();

        public Form2()
        {
            InitializeComponent();
            bool isConncectionOpened = conn.OpenConnection();
            calculateCutting();
        }

        public void calculateCutting()
        {
            string quiry;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            
           
            quiry = "SELECT * FROM samtecs WHERE name = " + order.Rows[1].ItemArray[1].ToString();
            MySqlDataAdapter da = new MySqlDataAdapter(quiry, conn.getMySqlConnection());
            da.Fill(ds, "samtecs");
            dt = ds.Tables["samtecs"];
             cuttingDataGrid.DataSource = dt;
         
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
