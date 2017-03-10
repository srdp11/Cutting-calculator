﻿using System;
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
        DataTable dt;

        public Form2(DataTable table)
        {
            InitializeComponent();
            dt = table.Copy();
            cuttingDataGrid.DataSource = dt;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
