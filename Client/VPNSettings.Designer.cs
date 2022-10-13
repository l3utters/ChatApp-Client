namespace Client
{
    partial class VPNSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.Address_Value = new System.Windows.Forms.TextBox();
            this.Port_Value = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Prox_Type = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.UsernameText = new System.Windows.Forms.TextBox();
            this.PasswordText = new System.Windows.Forms.TextBox();
            this.LoginDetailsCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Proxy Address";
            // 
            // Address_Value
            // 
            this.Address_Value.Location = new System.Drawing.Point(170, 56);
            this.Address_Value.Name = "Address_Value";
            this.Address_Value.Size = new System.Drawing.Size(155, 20);
            this.Address_Value.TabIndex = 1;
            // 
            // Port_Value
            // 
            this.Port_Value.Location = new System.Drawing.Point(170, 97);
            this.Port_Value.Name = "Port_Value";
            this.Port_Value.Size = new System.Drawing.Size(155, 20);
            this.Port_Value.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Proxy Port";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(47, 238);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(170, 238);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 30);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Proxy Type";
            // 
            // Prox_Type
            // 
            this.Prox_Type.FormattingEnabled = true;
            this.Prox_Type.Items.AddRange(new object[] {
            "SOCKS4",
            "SOCKS5",
            "HTTP",
            "HTTPS"});
            this.Prox_Type.Location = new System.Drawing.Point(170, 14);
            this.Prox_Type.Name = "Prox_Type";
            this.Prox_Type.Size = new System.Drawing.Size(155, 21);
            this.Prox_Type.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "Username*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "Password*";
            // 
            // UsernameText
            // 
            this.UsernameText.Location = new System.Drawing.Point(170, 138);
            this.UsernameText.Name = "UsernameText";
            this.UsernameText.Size = new System.Drawing.Size(155, 20);
            this.UsernameText.TabIndex = 10;
            // 
            // PasswordText
            // 
            this.PasswordText.Location = new System.Drawing.Point(170, 179);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.Size = new System.Drawing.Size(155, 20);
            this.PasswordText.TabIndex = 11;
            // 
            // LoginDetailsCheck
            // 
            this.LoginDetailsCheck.AutoSize = true;
            this.LoginDetailsCheck.Location = new System.Drawing.Point(16, 215);
            this.LoginDetailsCheck.Name = "LoginDetailsCheck";
            this.LoginDetailsCheck.Size = new System.Drawing.Size(113, 17);
            this.LoginDetailsCheck.TabIndex = 13;
            this.LoginDetailsCheck.Text = "*Use Login Details";
            this.LoginDetailsCheck.UseVisualStyleBackColor = true;
            // 
            // VPNSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 294);
            this.Controls.Add(this.LoginDetailsCheck);
            this.Controls.Add(this.PasswordText);
            this.Controls.Add(this.UsernameText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Prox_Type);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Port_Value);
            this.Controls.Add(this.Address_Value);
            this.Controls.Add(this.label1);
            this.Name = "VPNSettings";
            this.Text = "VPNSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Address_Value;
        private System.Windows.Forms.TextBox Port_Value;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Prox_Type;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox UsernameText;
        private System.Windows.Forms.TextBox PasswordText;
        public System.Windows.Forms.CheckBox LoginDetailsCheck;
    }
}