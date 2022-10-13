namespace Client
{
    partial class Recorder
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
            this.Record_Button = new System.Windows.Forms.Button();
            this.Stop_Button = new System.Windows.Forms.Button();
            this.Send_Button = new System.Windows.Forms.Button();
            this.Play_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Record_Button
            // 
            this.Record_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Record_Button.Location = new System.Drawing.Point(5, 5);
            this.Record_Button.Name = "Record_Button";
            this.Record_Button.Size = new System.Drawing.Size(66, 33);
            this.Record_Button.TabIndex = 0;
            this.Record_Button.Text = "Record";
            this.Record_Button.UseVisualStyleBackColor = false;
            this.Record_Button.Click += new System.EventHandler(this.Record_Button_Click);
            // 
            // Stop_Button
            // 
            this.Stop_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Stop_Button.Enabled = false;
            this.Stop_Button.Location = new System.Drawing.Point(74, 5);
            this.Stop_Button.Name = "Stop_Button";
            this.Stop_Button.Size = new System.Drawing.Size(66, 33);
            this.Stop_Button.TabIndex = 1;
            this.Stop_Button.Text = "Stop";
            this.Stop_Button.UseVisualStyleBackColor = false;
            this.Stop_Button.Click += new System.EventHandler(this.Stop_Button_Click);
            // 
            // Send_Button
            // 
            this.Send_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Send_Button.Location = new System.Drawing.Point(143, 5);
            this.Send_Button.Name = "Send_Button";
            this.Send_Button.Size = new System.Drawing.Size(66, 33);
            this.Send_Button.TabIndex = 3;
            this.Send_Button.Text = "Send";
            this.Send_Button.UseVisualStyleBackColor = false;
            this.Send_Button.Click += new System.EventHandler(this.Send_Button_Click);
            // 
            // Play_Button
            // 
            this.Play_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Play_Button.Enabled = false;
            this.Play_Button.Location = new System.Drawing.Point(5, 44);
            this.Play_Button.Name = "Play_Button";
            this.Play_Button.Size = new System.Drawing.Size(204, 30);
            this.Play_Button.TabIndex = 6;
            this.Play_Button.Text = "Preview Voice Note";
            this.Play_Button.UseVisualStyleBackColor = false;
            this.Play_Button.Click += new System.EventHandler(this.Play_Button_Click);
            // 
            // Recorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(214, 79);
            this.Controls.Add(this.Play_Button);
            this.Controls.Add(this.Send_Button);
            this.Controls.Add(this.Stop_Button);
            this.Controls.Add(this.Record_Button);
            this.Name = "Recorder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Recorder";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Record_Button;
        private System.Windows.Forms.Button Stop_Button;
        private System.Windows.Forms.Button Send_Button;
        private System.Windows.Forms.Button Play_Button;
    }
}