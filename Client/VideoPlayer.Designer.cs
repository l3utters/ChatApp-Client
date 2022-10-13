namespace Client
{
    partial class VideoPlayer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.Volume_Slider = new NAudio.Gui.VolumeSlider();
            this.Pause_Button = new System.Windows.Forms.Button();
            this.Stop_Button = new System.Windows.Forms.Button();
            this.Play_Button = new System.Windows.Forms.Button();
            this.VidHost = new System.Windows.Forms.Integration.ElementHost();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.Volume_Slider);
            this.panel1.Controls.Add(this.Pause_Button);
            this.panel1.Controls.Add(this.Stop_Button);
            this.panel1.Controls.Add(this.Play_Button);
            this.panel1.Location = new System.Drawing.Point(3, 187);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(231, 37);
            this.panel1.TabIndex = 1;
            // 
            // Volume_Slider
            // 
            this.Volume_Slider.Location = new System.Drawing.Point(115, 10);
            this.Volume_Slider.Name = "Volume_Slider";
            this.Volume_Slider.Size = new System.Drawing.Size(112, 16);
            this.Volume_Slider.TabIndex = 9;
            this.Volume_Slider.Click += new System.EventHandler(this.Volume_Slider_Click);
            // 
            // Pause_Button
            // 
            this.Pause_Button.BackColor = System.Drawing.Color.Silver;
            this.Pause_Button.BackgroundImage = global::Client.Properties.Resources.newpause;
            this.Pause_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pause_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Pause_Button.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.Pause_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pause_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pause_Button.Location = new System.Drawing.Point(76, 2);
            this.Pause_Button.Name = "Pause_Button";
            this.Pause_Button.Size = new System.Drawing.Size(34, 32);
            this.Pause_Button.TabIndex = 8;
            this.Pause_Button.UseVisualStyleBackColor = false;
            this.Pause_Button.Click += new System.EventHandler(this.Pause_Button_Click);
            // 
            // Stop_Button
            // 
            this.Stop_Button.BackColor = System.Drawing.Color.Silver;
            this.Stop_Button.BackgroundImage = global::Client.Properties.Resources.newstop;
            this.Stop_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Stop_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Stop_Button.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.Stop_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Stop_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stop_Button.Location = new System.Drawing.Point(40, 2);
            this.Stop_Button.Name = "Stop_Button";
            this.Stop_Button.Size = new System.Drawing.Size(34, 32);
            this.Stop_Button.TabIndex = 7;
            this.Stop_Button.UseVisualStyleBackColor = false;
            this.Stop_Button.Click += new System.EventHandler(this.Stop_Button_Click);
            // 
            // Play_Button
            // 
            this.Play_Button.BackColor = System.Drawing.Color.Silver;
            this.Play_Button.BackgroundImage = global::Client.Properties.Resources.newplay;
            this.Play_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Play_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Play_Button.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.Play_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Play_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Play_Button.Location = new System.Drawing.Point(4, 2);
            this.Play_Button.Name = "Play_Button";
            this.Play_Button.Size = new System.Drawing.Size(34, 32);
            this.Play_Button.TabIndex = 5;
            this.Play_Button.UseVisualStyleBackColor = false;
            this.Play_Button.Click += new System.EventHandler(this.Play_Button_Click);
            // 
            // VidHost
            // 
            this.VidHost.BackColor = System.Drawing.Color.Black;
            this.VidHost.Location = new System.Drawing.Point(4, 3);
            this.VidHost.Name = "VidHost";
            this.VidHost.Size = new System.Drawing.Size(223, 175);
            this.VidHost.TabIndex = 2;
            this.VidHost.Text = "elementHost1";
            this.VidHost.Child = null;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.VidHost);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(231, 182);
            this.panel2.TabIndex = 3;
            // 
            // VideoPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "VideoPlayer";
            this.Size = new System.Drawing.Size(237, 225);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Pause_Button;
        private System.Windows.Forms.Button Stop_Button;
        private System.Windows.Forms.Button Play_Button;
        private NAudio.Gui.VolumeSlider Volume_Slider;
        private System.Windows.Forms.Integration.ElementHost VidHost;
        private System.Windows.Forms.Panel panel2;
    }
}
