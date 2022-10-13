namespace Client
{
    partial class AudioPlayer
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
            this.Pause_Button = new System.Windows.Forms.Button();
            this.Stop_Button = new System.Windows.Forms.Button();
            this.Play_Button = new System.Windows.Forms.Button();
            this.Volume_Slider = new NAudio.Gui.VolumeSlider();
            this.SuspendLayout();
            // 
            // Pause_Button
            // 
            this.Pause_Button.BackColor = System.Drawing.Color.White;
            this.Pause_Button.BackgroundImage = global::Client.Properties.Resources.newpause;
            this.Pause_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pause_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Pause_Button.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.Pause_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pause_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pause_Button.Location = new System.Drawing.Point(73, 1);
            this.Pause_Button.Name = "Pause_Button";
            this.Pause_Button.Size = new System.Drawing.Size(34, 32);
            this.Pause_Button.TabIndex = 4;
            this.Pause_Button.UseVisualStyleBackColor = false;
            this.Pause_Button.Click += new System.EventHandler(this.Pause_Button_Click);
            // 
            // Stop_Button
            // 
            this.Stop_Button.BackColor = System.Drawing.Color.White;
            this.Stop_Button.BackgroundImage = global::Client.Properties.Resources.newstop;
            this.Stop_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Stop_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Stop_Button.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.Stop_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Stop_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stop_Button.Location = new System.Drawing.Point(37, 1);
            this.Stop_Button.Name = "Stop_Button";
            this.Stop_Button.Size = new System.Drawing.Size(34, 32);
            this.Stop_Button.TabIndex = 3;
            this.Stop_Button.UseVisualStyleBackColor = false;
            this.Stop_Button.Click += new System.EventHandler(this.Stop_Button_Click);
            // 
            // Play_Button
            // 
            this.Play_Button.BackColor = System.Drawing.Color.White;
            this.Play_Button.BackgroundImage = global::Client.Properties.Resources.newplay;
            this.Play_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Play_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Play_Button.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.Play_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Play_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Play_Button.Location = new System.Drawing.Point(1, 1);
            this.Play_Button.Name = "Play_Button";
            this.Play_Button.Size = new System.Drawing.Size(34, 32);
            this.Play_Button.TabIndex = 0;
            this.Play_Button.UseVisualStyleBackColor = false;
            this.Play_Button.Click += new System.EventHandler(this.Play_Button_Click);
            // 
            // Volume_Slider
            // 
            this.Volume_Slider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Volume_Slider.Location = new System.Drawing.Point(115, 9);
            this.Volume_Slider.Name = "Volume_Slider";
            this.Volume_Slider.Size = new System.Drawing.Size(112, 16);
            this.Volume_Slider.TabIndex = 1;
            this.Volume_Slider.Click += new System.EventHandler(this.Volume_Slider_Click);
            // 
            // AudioPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.Pause_Button);
            this.Controls.Add(this.Stop_Button);
            this.Controls.Add(this.Volume_Slider);
            this.Controls.Add(this.Play_Button);
            this.Name = "AudioPlayer";
            this.Size = new System.Drawing.Size(237, 36);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Play_Button;
        private System.Windows.Forms.Button Stop_Button;
        private System.Windows.Forms.Button Pause_Button;
        private NAudio.Gui.VolumeSlider Volume_Slider;
    }
}
