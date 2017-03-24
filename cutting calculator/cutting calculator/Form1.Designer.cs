namespace cutting_calculator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.orderTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateCutting = new System.Windows.Forms.ToolStripMenuItem();
            this.close = new System.Windows.Forms.ToolStripMenuItem();
            this.addPossition = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.positionsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.deletePosition = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.quantityTextBox = new System.Windows.Forms.TextBox();
            this.partNumbersCmbx = new System.Windows.Forms.ComboBox();
            this.dataLabel = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.orderDataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.positionsLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Заявка №";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // orderTextBox
            // 
            this.orderTextBox.Location = new System.Drawing.Point(78, 30);
            this.orderTextBox.Name = "orderTextBox";
            this.orderTextBox.Size = new System.Drawing.Size(283, 20);
            this.orderTextBox.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(383, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calculateCutting,
            this.close});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.programToolStripMenuItem.Text = "Программа";
            // 
            // calculateCutting
            // 
            this.calculateCutting.Name = "calculateCutting";
            this.calculateCutting.Size = new System.Drawing.Size(168, 22);
            this.calculateCutting.Text = "Рассчитать резку";
            this.calculateCutting.Click += new System.EventHandler(this.calculateCutting_Click);
            // 
            // close
            // 
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(168, 22);
            this.close.Text = "Закрыть";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // addPossition
            // 
            this.addPossition.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addPossition.Location = new System.Drawing.Point(281, 29);
            this.addPossition.Name = "addPossition";
            this.addPossition.Size = new System.Drawing.Size(23, 22);
            this.addPossition.TabIndex = 4;
            this.addPossition.Text = "+";
            this.addPossition.UseVisualStyleBackColor = true;
            this.addPossition.Click += new System.EventHandler(this.addPossition_Click);
            // 
            // Save
            // 
            this.Save.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Save.Location = new System.Drawing.Point(286, 458);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 8;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // positionsLayoutPanel
            // 
            this.positionsLayoutPanel.ColumnCount = 4;
            this.positionsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.03896F));
            this.positionsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.96104F));
            this.positionsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.positionsLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.positionsLayoutPanel.Controls.Add(this.deletePosition, 3, 1);
            this.positionsLayoutPanel.Controls.Add(this.label4, 1, 0);
            this.positionsLayoutPanel.Controls.Add(this.label3, 0, 0);
            this.positionsLayoutPanel.Controls.Add(this.addPossition, 2, 1);
            this.positionsLayoutPanel.Controls.Add(this.quantityTextBox, 1, 1);
            this.positionsLayoutPanel.Controls.Add(this.partNumbersCmbx, 0, 1);
            this.positionsLayoutPanel.Location = new System.Drawing.Point(12, 82);
            this.positionsLayoutPanel.Name = "positionsLayoutPanel";
            this.positionsLayoutPanel.RowCount = 2;
            this.positionsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.positionsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.positionsLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.positionsLayoutPanel.Size = new System.Drawing.Size(352, 54);
            this.positionsLayoutPanel.TabIndex = 6;
            // 
            // deletePosition
            // 
            this.deletePosition.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deletePosition.Location = new System.Drawing.Point(318, 29);
            this.deletePosition.Name = "deletePosition";
            this.deletePosition.Size = new System.Drawing.Size(23, 22);
            this.deletePosition.TabIndex = 13;
            this.deletePosition.Text = "-";
            this.deletePosition.UseVisualStyleBackColor = true;
            this.deletePosition.Click += new System.EventHandler(this.deletePosition_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(173, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 26);
            this.label4.TabIndex = 1;
            this.label4.Text = "Количество";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "Артикул";
            // 
            // quantityTextBox
            // 
            this.quantityTextBox.Location = new System.Drawing.Point(173, 29);
            this.quantityTextBox.Name = "quantityTextBox";
            this.quantityTextBox.Size = new System.Drawing.Size(93, 20);
            this.quantityTextBox.TabIndex = 11;
            // 
            // partNumbersCmbx
            // 
            this.partNumbersCmbx.FormattingEnabled = true;
            this.partNumbersCmbx.Location = new System.Drawing.Point(3, 29);
            this.partNumbersCmbx.Name = "partNumbersCmbx";
            this.partNumbersCmbx.Size = new System.Drawing.Size(164, 21);
            this.partNumbersCmbx.TabIndex = 14;
            this.partNumbersCmbx.SelectedIndexChanged += new System.EventHandler(this.partNumbersCmbx_SelectedIndexChanged);
            // 
            // dataLabel
            // 
            this.dataLabel.AutoSize = true;
            this.dataLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataLabel.Location = new System.Drawing.Point(33, 56);
            this.dataLabel.Name = "dataLabel";
            this.dataLabel.Size = new System.Drawing.Size(36, 16);
            this.dataLabel.TabIndex = 10;
            this.dataLabel.Text = "Дата";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(75, 56);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // orderDataGridView
            // 
            this.orderDataGridView.AllowUserToAddRows = false;
            this.orderDataGridView.AllowUserToDeleteRows = false;
            this.orderDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.orderDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderDataGridView.Location = new System.Drawing.Point(15, 142);
            this.orderDataGridView.Name = "orderDataGridView";
            this.orderDataGridView.Size = new System.Drawing.Size(349, 310);
            this.orderDataGridView.TabIndex = 12;
            this.orderDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.orderDataGrid_CellContentClick);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(383, 497);
            this.Controls.Add(this.orderDataGridView);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataLabel);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.positionsLayoutPanel);
            this.Controls.Add(this.orderTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(399, 535);
            this.MinimumSize = new System.Drawing.Size(399, 535);
            this.Name = "Form1";
            this.Text = "Заявка";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.positionsLayoutPanel.ResumeLayout(false);
            this.positionsLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox orderTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculateCutting;
        private System.Windows.Forms.Button addPossition;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.TableLayoutPanel positionsLayoutPanel;
        private System.Windows.Forms.TextBox quantityTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label dataLabel;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DataGridView orderDataGridView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button deletePosition;
        private System.Windows.Forms.ComboBox partNumbersCmbx;
        private System.Windows.Forms.ToolStripMenuItem close;
    }
}

