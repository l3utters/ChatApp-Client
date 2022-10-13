using System;
using System.Windows.Forms;
using NAudio.Wave;
using System.IO;

namespace Client
{
    public partial class Recorder : Form
    {
        public string VoiceFilePath = @"C:\Users\Public\Whazzup\Voice Notes\Sent\" + Path.GetRandomFileName() + ".wav";

        public Boolean active = true;

        public WaveIn waveSource = null;
        public WaveFileWriter waveFile = null;
        WaveOut waveOut = null;

        public Recorder()
        {
            
            InitializeComponent();
            Send_Button.Enabled = false;
        }

        private void Record_Button_Click(object sender, EventArgs e)
        {
            Record_Button.Enabled = false;
            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            try
            {
                
                waveFile = new WaveFileWriter(VoiceFilePath, waveSource.WaveFormat);
                
            }
            catch(Exception s)
            {
                MessageBox.Show(s.ToString());
            }
            waveSource.StartRecording();

            Stop_Button.Enabled = true;
        }

        private void Stop_Button_Click(object sender, EventArgs e)
        {
            waveSource.StopRecording();
            this.Height = 117;
            Play_Button.Enabled = true;
            Stop_Button.Enabled = false;

            Record_Button.Enabled = false;
            Record_Button.Text = "Recorded";
            Send_Button.Enabled = true;

        }

        public void Send_Button_Click(object sender, EventArgs e)
        {
            active = false;
            if(waveOut!=null)
                waveOut.Stop();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
            
        }

        private void Play_Button_Click(object sender, EventArgs e)
        {
            var reader = new WaveFileReader(VoiceFilePath);
            waveOut = new WaveOut();
            waveOut.Init(reader);
            waveOut.Play();
        }
    }
}
