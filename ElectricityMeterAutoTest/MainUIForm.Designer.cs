namespace BaylanElectricAutoTest
{
    partial class MainUIForm
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
            this.components = new System.ComponentModel.Container();
            this.IndexTestingButton = new System.Windows.Forms.Button();
            this.DaylightTestingButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SelectPort = new System.Windows.Forms.ComboBox();
            this.BdPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.LblHour = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LblMinute = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LblSecond = new System.Windows.Forms.Label();
            this.LongCutOff = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IndexTestingButton
            // 
            this.IndexTestingButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.IndexTestingButton.Location = new System.Drawing.Point(31, 144);
            this.IndexTestingButton.Name = "IndexTestingButton";
            this.IndexTestingButton.Size = new System.Drawing.Size(195, 26);
            this.IndexTestingButton.TabIndex = 0;
            this.IndexTestingButton.Text = "Kısa Kesinti Testi";
            this.IndexTestingButton.UseVisualStyleBackColor = false;
            this.IndexTestingButton.Click += new System.EventHandler(this.ShortCutOffTestingButton_Click);
            // 
            // DaylightTestingButton
            // 
            this.DaylightTestingButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.DaylightTestingButton.Location = new System.Drawing.Point(31, 302);
            this.DaylightTestingButton.Name = "DaylightTestingButton";
            this.DaylightTestingButton.Size = new System.Drawing.Size(195, 75);
            this.DaylightTestingButton.TabIndex = 1;
            this.DaylightTestingButton.Text = "Yaz Saati Testi";
            this.DaylightTestingButton.UseVisualStyleBackColor = false;
            this.DaylightTestingButton.Click += new System.EventHandler(this.DaylightTestingButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(274, 1);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(522, 452);
            this.listBox1.TabIndex = 2;
            // 
            // SelectPort
            // 
            this.SelectPort.FormattingEnabled = true;
            this.SelectPort.Location = new System.Drawing.Point(152, 17);
            this.SelectPort.Name = "SelectPort";
            this.SelectPort.Size = new System.Drawing.Size(74, 24);
            this.SelectPort.TabIndex = 3;
            this.SelectPort.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // BdPort
            // 
            this.BdPort.FormattingEnabled = true;
            this.BdPort.Location = new System.Drawing.Point(152, 67);
            this.BdPort.Name = "BdPort";
            this.BdPort.Size = new System.Drawing.Size(74, 24);
            this.BdPort.TabIndex = 4;
            this.BdPort.SelectedIndexChanged += new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "ComPort";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Baud Rate";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // LblHour
            // 
            this.LblHour.AutoSize = true;
            this.LblHour.Location = new System.Drawing.Point(15, 422);
            this.LblHour.Name = "LblHour";
            this.LblHour.Size = new System.Drawing.Size(44, 16);
            this.LblHour.TabIndex = 7;
            this.LblHour.Text = "label3";
            this.LblHour.Click += new System.EventHandler(this.LblHour_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 422);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = ".";
            // 
            // LblMinute
            // 
            this.LblMinute.AutoSize = true;
            this.LblMinute.Location = new System.Drawing.Point(81, 422);
            this.LblMinute.Name = "LblMinute";
            this.LblMinute.Size = new System.Drawing.Size(44, 16);
            this.LblMinute.TabIndex = 9;
            this.LblMinute.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(131, 422);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = ".";
            // 
            // LblSecond
            // 
            this.LblSecond.AutoSize = true;
            this.LblSecond.Location = new System.Drawing.Point(147, 422);
            this.LblSecond.Name = "LblSecond";
            this.LblSecond.Size = new System.Drawing.Size(44, 16);
            this.LblSecond.TabIndex = 11;
            this.LblSecond.Text = "label7";
            // 
            // LongCutOff
            // 
            this.LongCutOff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LongCutOff.Location = new System.Drawing.Point(31, 176);
            this.LongCutOff.Name = "LongCutOff";
            this.LongCutOff.Size = new System.Drawing.Size(195, 26);
            this.LongCutOff.TabIndex = 12;
            this.LongCutOff.Text = "Uzun Kesinti Testi";
            this.LongCutOff.UseVisualStyleBackColor = false;
            this.LongCutOff.Click += new System.EventHandler(this.LongCutOffTesting_Click);
            // 
            // MainUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 458);
            this.Controls.Add(this.LongCutOff);
            this.Controls.Add(this.LblSecond);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LblMinute);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LblHour);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BdPort);
            this.Controls.Add(this.SelectPort);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.DaylightTestingButton);
            this.Controls.Add(this.IndexTestingButton);
            this.Name = "MainUIForm";
            this.Text = "MainUIForm";
            this.Load += new System.EventHandler(this.MainUIForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button IndexTestingButton;
        private System.Windows.Forms.Button DaylightTestingButton;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ComboBox SelectPort;
        private System.Windows.Forms.ComboBox BdPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.IO.Ports.SerialPort serialPort2;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label LblHour;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LblMinute;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LblSecond;
        private System.Windows.Forms.Button LongCutOff;
    }
}