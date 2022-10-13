using System;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.Flac;

namespace Client
{
    public partial class AudioPlayer : UserControl
    {
        string file;
        string ext;
        WaveOut waveOut = null;

        public AudioPlayer(string FilePath, AnchorStyles Anchor)
        {
            InitializeComponent();
            this.Anchor = Anchor;
            ext = Path.GetExtension(FilePath);
            file = FilePath;

            if (ext.CompareTo(".wav") == 0)
            {
                var reader = new WaveFileReader(file);
                waveOut = new WaveOut();
                waveOut.Init(reader);
            }
            if (ext.CompareTo(".mp3") == 0)
            {
                var reader = new Mp3FileReader(file);
                waveOut = new WaveOut();
                waveOut.Init(reader);
            }
            if (ext.CompareTo(".flac") == 0)
            {
                var reader = new FlacReader(file);
                waveOut = new WaveOut();
                waveOut.Init(reader);
            }
            if (ext.CompareTo(".AAC") == 0)
            {
                var reader = new FlacReader(file);
                waveOut = new WaveOut();
                waveOut.Init(reader);
            }

        }

        private void Play_Button_Click(object sender, EventArgs e)
        {
            Volume_Slider.Enabled = true;

            if (waveOut.PlaybackState == PlaybackState.Stopped)
            {
                if(ext.CompareTo(".wav") == 0)
            {
                    var reader = new WaveFileReader(file);
                    waveOut = new WaveOut();
                    waveOut.Init(reader);
                }
                if (ext.CompareTo(".mp3") == 0)
                {
                    var reader = new Mp3FileReader(file);
                    waveOut = new WaveOut();
                    waveOut.Init(reader);
                }
                if(ext.CompareTo(".flac")==0)
                {
                    var reader = new FlacReader(file);
                    waveOut = new WaveOut();
                    waveOut.Init(reader);
                }

            }
            waveOut.Play();

        }

        private void Stop_Button_Click(object sender, EventArgs e)
        {
            waveOut.Stop();
            Volume_Slider.Enabled = false;
        }

        private void Pause_Button_Click(object sender, EventArgs e)
        {
            waveOut.Pause();
        }
        
        private void Volume_Slider_Click(object sender, EventArgs e)
        {
            
                waveOut.Volume = Volume_Slider.Volume;
        }
    }
}
