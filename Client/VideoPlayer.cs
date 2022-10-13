using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    
    public partial class VideoPlayer : UserControl
    {
        System.Windows.Controls.MediaElement test = null;

        
        public VideoPlayer(string FilePath, AnchorStyles Side)
        {
            InitializeComponent();
            this.Anchor = Side;

            panel2.Controls.Add(VidHost);

            VidHost.Dock = DockStyle.Fill;

            CreatePlayer(FilePath);


        }

        private void Play_Button_Click(object sender, EventArgs e)
        {
            test.Play();
        }

        private void Stop_Button_Click(object sender, EventArgs e)
        {
            test.Stop();
        }

        private void Pause_Button_Click(object sender, EventArgs e)
        {
            test.Pause();
        }

        private void Volume_Slider_Click(object sender, EventArgs e)
        {
            
            float val = Volume_Slider.Volume;
            val = val * 100;
            double temp = Convert.ToDouble(val);
            test.Volume = temp;
        }

        [STAThread]
        private void CreatePlayer(string path)
        {
            try
            {
                test = new System.Windows.Controls.MediaElement
                {
                    Source = new Uri(path),
                    LoadedBehavior = System.Windows.Controls.MediaState.Manual,
                    UnloadedBehavior = System.Windows.Controls.MediaState.Manual
                };
                VidHost.Child = test;
            }
            catch (Exception s)
            {
                MessageBox.Show(s.ToString());
            }
        }
    }
}
