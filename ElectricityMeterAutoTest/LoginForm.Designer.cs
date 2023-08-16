namespace BaylanElectricAutoTest
{
    partial class LoginForm
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
            this.GirisButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TboxUserName = new System.Windows.Forms.TextBox();
            this.TboxUserPw = new System.Windows.Forms.TextBox();
            this.AdminCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GirisButton
            // 
            this.GirisButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.GirisButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.GirisButton.Location = new System.Drawing.Point(328, 224);
            this.GirisButton.Name = "GirisButton";
            this.GirisButton.Size = new System.Drawing.Size(125, 55);
            this.GirisButton.TabIndex = 0;
            this.GirisButton.Text = "Giris";
            this.GirisButton.UseVisualStyleBackColor = false;
            this.GirisButton.Click += new System.EventHandler(this.GirisButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Kullanıcı Adı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(367, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sifre";
            // 
            // TboxUserName
            // 
            this.TboxUserName.Location = new System.Drawing.Point(328, 140);
            this.TboxUserName.Name = "TboxUserName";
            this.TboxUserName.Size = new System.Drawing.Size(125, 22);
            this.TboxUserName.TabIndex = 3;
            this.TboxUserName.TextChanged += new System.EventHandler(this.TboxUserName_TextChanged);
            // 
            // TboxUserPw
            // 
            this.TboxUserPw.Location = new System.Drawing.Point(328, 184);
            this.TboxUserPw.Name = "TboxUserPw";
            this.TboxUserPw.Size = new System.Drawing.Size(125, 22);
            this.TboxUserPw.TabIndex = 4;
            this.TboxUserPw.TextChanged += new System.EventHandler(this.TboxUserPw_TextChanged);
            // 
            // AdminCheckBox
            // 
            this.AdminCheckBox.AutoSize = true;
            this.AdminCheckBox.Location = new System.Drawing.Point(480, 142);
            this.AdminCheckBox.Name = "AdminCheckBox";
            this.AdminCheckBox.Size = new System.Drawing.Size(18, 17);
            this.AdminCheckBox.TabIndex = 5;
            this.AdminCheckBox.UseVisualStyleBackColor = true;
            this.AdminCheckBox.CheckedChanged += new System.EventHandler(this.AdminCheckBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(525, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AdminCheckBox);
            this.Controls.Add(this.TboxUserPw);
            this.Controls.Add(this.TboxUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GirisButton);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GirisButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TboxUserName;
        private System.Windows.Forms.TextBox TboxUserPw;
        private System.Windows.Forms.CheckBox AdminCheckBox;
        private System.Windows.Forms.Label label3;
    }
}