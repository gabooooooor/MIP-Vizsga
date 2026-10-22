namespace MIPVizsga
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRecipient = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxPriority = new System.Windows.Forms.ComboBox();
            this.buttonAddDelivery = new System.Windows.Forms.Button();
            this.buttonGenerateDataset = new System.Windows.Forms.Button();
            this.dataGridViewDeliveries = new System.Windows.Forms.DataGridView();
            this.buttonStartProcessing = new System.Windows.Forms.Button();
            this.buttonStopProcessing = new System.Windows.Forms.Button();
            this.labelCounts = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDeliveries)).BeginInit();
            this.SuspendLayout();

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Recipient:";

            // textBoxRecipient
            this.textBoxRecipient.Location = new System.Drawing.Point(86, 9);
            this.textBoxRecipient.Name = "textBoxRecipient";
            this.textBoxRecipient.Size = new System.Drawing.Size(200, 22);
            this.textBoxRecipient.TabIndex = 1;

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Priority:";

            // comboBoxPriority
            this.comboBoxPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPriority.FormattingEnabled = true;
            this.comboBoxPriority.Items.AddRange(new object[] { "1", "2", "3" });
            this.comboBoxPriority.Location = new System.Drawing.Point(352, 9);
            this.comboBoxPriority.Name = "comboBoxPriority";
            this.comboBoxPriority.Size = new System.Drawing.Size(50, 24);
            this.comboBoxPriority.TabIndex = 3;
            this.comboBoxPriority.SelectedIndex = 0;

            // buttonAddDelivery
            this.buttonAddDelivery.Location = new System.Drawing.Point(408, 9);
            this.buttonAddDelivery.Name = "buttonAddDelivery";
            this.buttonAddDelivery.Size = new System.Drawing.Size(120, 23);
            this.buttonAddDelivery.TabIndex = 4;
            this.buttonAddDelivery.Text = "Add Delivery";
            this.buttonAddDelivery.UseVisualStyleBackColor = true;
            this.buttonAddDelivery.Click += new System.EventHandler(this.ButtonAddDelivery_Click);

            // buttonGenerateDataset
            this.buttonGenerateDataset.Location = new System.Drawing.Point(534, 9);
            this.buttonGenerateDataset.Name = "buttonGenerateDataset";
            this.buttonGenerateDataset.Size = new System.Drawing.Size(120, 23);
            this.buttonGenerateDataset.TabIndex = 5;
            this.buttonGenerateDataset.Text = "Generate Dataset";
            this.buttonGenerateDataset.UseVisualStyleBackColor = true;
            this.buttonGenerateDataset.Click += new System.EventHandler(this.ButtonGenerateDataset_Click);

            // dataGridViewDeliveries
            this.dataGridViewDeliveries.AllowUserToAddRows = false;
            this.dataGridViewDeliveries.AllowUserToDeleteRows = false;
            this.dataGridViewDeliveries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewDeliveries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDeliveries.Location = new System.Drawing.Point(12, 50);
            this.dataGridViewDeliveries.Name = "dataGridViewDeliveries";
            this.dataGridViewDeliveries.ReadOnly = true;
            this.dataGridViewDeliveries.RowTemplate.Height = 24;
            this.dataGridViewDeliveries.Size = new System.Drawing.Size(776, 330);
            this.dataGridViewDeliveries.TabIndex = 6;

            // buttonStartProcessing
            this.buttonStartProcessing.Location = new System.Drawing.Point(12, 390);
            this.buttonStartProcessing.Name = "buttonStartProcessing";
            this.buttonStartProcessing.Size = new System.Drawing.Size(120, 23);
            this.buttonStartProcessing.TabIndex = 7;
            this.buttonStartProcessing.Text = "Start Processing";
            this.buttonStartProcessing.UseVisualStyleBackColor = true;
            this.buttonStartProcessing.Click += new System.EventHandler(this.ButtonStartProcessing_Click);

            // buttonStopProcessing
            this.buttonStopProcessing.Location = new System.Drawing.Point(138, 390);
            this.buttonStopProcessing.Name = "buttonStopProcessing";
            this.buttonStopProcessing.Size = new System.Drawing.Size(120, 23);
            this.buttonStopProcessing.TabIndex = 8;
            this.buttonStopProcessing.Text = "Stop Processing";
            this.buttonStopProcessing.UseVisualStyleBackColor = true;
            this.buttonStopProcessing.Enabled = false;
            this.buttonStopProcessing.Click += new System.EventHandler(this.ButtonStopProcessing_Click);

            // labelCounts
            this.labelCounts.AutoSize = true;
            this.labelCounts.Location = new System.Drawing.Point(400, 393);
            this.labelCounts.Name = "labelCounts";
            this.labelCounts.Size = new System.Drawing.Size(84, 16);
            this.labelCounts.TabIndex = 9;
            this.labelCounts.Text = "Status: Pending=0, Processing=0, Completed=0";

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 420);
            this.Controls.Add(this.labelCounts);
            this.Controls.Add(this.buttonStopProcessing);
            this.Controls.Add(this.buttonStartProcessing);
            this.Controls.Add(this.dataGridViewDeliveries);
            this.Controls.Add(this.buttonGenerateDataset);
            this.Controls.Add(this.buttonAddDelivery);
            this.Controls.Add(this.comboBoxPriority);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxRecipient);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "PonyExpress Delivery Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDeliveries)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRecipient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxPriority;
        private System.Windows.Forms.Button buttonAddDelivery;
        private System.Windows.Forms.Button buttonGenerateDataset;
        private System.Windows.Forms.DataGridView dataGridViewDeliveries;
        private System.Windows.Forms.Button buttonStartProcessing;
        private System.Windows.Forms.Button buttonStopProcessing;
        private System.Windows.Forms.Label labelCounts;
    }
}