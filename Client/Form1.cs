using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using Starksoft.Aspen.Proxy;

namespace Client
{
    public partial class Wazzup_Client : Form
    {
        public Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        byte[] ReceiveBuffer = new byte[50000000];
        public string FileLocation = null;
        private string IPaddress = "192.168.50.46";

        public Wazzup_Client()
        {
            InitializeComponent();
            TrayNotify.Icon = SystemIcons.Application;
            Chat.HorizontalScroll.Visible = false;
            Chat.HorizontalScroll.Maximum = -1;
            Chat.AutoScroll = false;
            Chat.VerticalScroll.Visible = false;
            Chat.AutoScroll = true;
            System.IO.Directory.CreateDirectory(@"C:\Users\Public\Whazzup\Videos");
            System.IO.Directory.CreateDirectory(@"C:\Users\Public\Whazzup\Audio");
            System.IO.Directory.CreateDirectory(@"C:\Users\Public\Whazzup\Images");
            System.IO.Directory.CreateDirectory(@"C:\Users\Public\Whazzup\Voice Notes\Sent");
            System.IO.Directory.CreateDirectory(@"C:\Users\Public\Whazzup\Voice Notes\Received");
            System.IO.Directory.CreateDirectory(@"C:\Users\Public\Whazzup\Voice Notes");
            System.IO.Directory.CreateDirectory(@"C:\Users\Public\Whazzup\Chat History");
            UserName.Text = Environment.UserName;

            File.Decrypt(@"C:\Users\Public\Whazzup\Chat History");
            
            RetreiveChat("allchat",Chat);
        }

        public void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = UserName.Text + ": " + TextField.Text;
                if (UserList.SelectedIndex != -1)
                {
                    if (ChatTabs.SelectedTab.Text.CompareTo("All Chat")==0)
                    {
                        OwnChat("You: " + TextField.Text, AnchorStyles.Right);
                    }
                    else
                    {
                        RichTextBox temp = CreateChatBox(AnchorStyles.Right, "You: " + TextField.Text);
                        
                        foreach (TabPage tab in ChatTabs.TabPages)
                        {
                            if (tab.Name.CompareTo(ChatTabs.SelectedTab.Text) == 0)
                            {
                                Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                                foreach(Control child in children)
                                {
                                    Console.WriteLine(child.Name);
                                }

                                TableLayoutPanel Temp_Panel = (TableLayoutPanel)children[0];

                                this.Invoke(new Action(() => Temp_Panel.Controls.Add(temp, 0, Temp_Panel.RowCount - 1)));
                                this.Invoke(new Action(() => Temp_Panel.RowCount = Temp_Panel.RowCount + 1));
                                this.Invoke(new Action(() => Temp_Panel.ScrollControlIntoView(temp)));
                                SaveChat("You: " + TextField.Text, ChatTabs.SelectedTab.Text);
                                break;
                            }
                            msg = msg + "@" + ChatTabs.SelectedTab.Text;
                            
                        }
                        
                    }

                    byte[] SendBuffer = Convert.FromBase64String(Base64Encode(msg));

                    SendBuffer = Encryption.encryptStream(SendBuffer);
                    SendBuffer = Compression.CompressBytes(SendBuffer);
                    

                    ClientSocket.BeginSend(SendBuffer, 0, SendBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), ClientSocket);

                    TextField.Text = "";
                }
            }
            catch(Exception s)
            {
                MessageBox.Show(s.ToString());
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConnectButton.Text.CompareTo("Connect") == 0)
                {
                    if (UserName.Text.CompareTo("") == 0)
                    {
                        MessageBox.Show("Please Enter a Name", "Username ERROR");
                    }
                    else
                    {
                        ClientSocket.Connect(IPaddress, 8888);

                        ClientSocket.BeginReceive(ReceiveBuffer, 0, ReceiveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), ClientSocket);
                        string msg = "OnlineConnected8888$$" + UserName.Text;

                        byte[] SendBuffer = Convert.FromBase64String(Base64Encode(msg));


                        SendBuffer = Encryption.encryptStream(SendBuffer);
                        SendBuffer = Compression.CompressBytes(SendBuffer);
                        
                        ClientSocket.BeginSend(SendBuffer, 0, SendBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), ClientSocket);

                        ConnectButton.Text = "Connected";
                        ConnectButton.Enabled = false;
                        UserName.Enabled = false;
                        SendButton.Enabled = true;
                        FileButton.Enabled = true;
                        Voice_Button.Enabled = true;
                    }
                }
            }
            catch(Exception s)
            {
                MessageBox.Show(s.ToString());
            }

        }

        private void SendCallBack(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }

        [STAThread]
        private void ReceiveCallBack(IAsyncResult AR)
        {
            try
            {
                Socket socket = (Socket)AR.AsyncState;
                int received = socket.EndReceive(AR);
                byte[] tempBuffer = new byte[received];
                Array.Copy(ReceiveBuffer, tempBuffer, received);

               
                tempBuffer = Compression.DecompressBytes(tempBuffer);
                byte[] dec = Encryption.mydec(tempBuffer);

                string msg = Convert.ToBase64String(dec);

                msg = Base64Decode(msg);

                Stream str = Properties.Resources.Wassup;

                System.Media.SoundPlayer rp = new System.Media.SoundPlayer(str);

                //rp.Play();

                //rp.Dispose();

                if (msg.Contains("CurrentOnline$$"))
                {
                    System.Media.SystemSounds.Exclamation.Play();
                    UserList.Invoke(new Action(() => UserList.Items.Clear()));
                    UserList.Invoke(new Action(() => UserList.Items.Add("(All)")));
                    UserList.Invoke(new Action(() => UserList.SetSelected(0, true)));
                    msg = msg.Substring(15);
                    string[] users = msg.Split(';');
                    foreach (string name in users)
                    {
                        if (name.Contains(UserName.Text))
                            ;
                        else
                            UserList.Invoke(new Action(() => UserList.Items.Add(name)));
                    }

                }
                else if (msg.Contains("CON$#$"))
                {
                    System.Media.SystemSounds.Exclamation.Play();
                    if (msg.Contains(UserName.Text))
                        ;
                    else
                    {
                        msg = msg.Substring(6);
                        OwnChat(msg, AnchorStyles.Left);

                        TrayNotify.ShowBalloonTip(1000, "User Connected! ", msg, ToolTipIcon.Info);

                    }
                }
                else if (msg.CompareTo("DISC&&$ALL&&$USERS") == 0)
                {
                    if (ClientSocket.Connected)
                    {
                        byte[] SendBuffer = new byte[1500];
                        string disc = "!Disconnect" + UserName.Text;

                        SendBuffer = Convert.FromBase64String(Base64Encode(disc));

                        SendBuffer = Encryption.encryptStream(SendBuffer);
                        SendBuffer = Compression.CompressBytes(SendBuffer);
                        

                        ClientSocket.BeginSend(SendBuffer, 0, SendBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), ClientSocket);

                        ClientSocket.Disconnect(false);

                        MessageBox.Show("Server Disconnected");

                        this.Close();
                    }
                }
                else if (msg.Contains("NEWPRIVATECHAT&&$"))
                {
                    string no_flag = msg.Substring(17);
                    string[] messages = no_flag.Split(':');
                    string sender = messages[1].Substring(4);

                    Boolean found = false;

                    foreach (TabPage tab in ChatTabs.TabPages)
                    {
                        if (tab.Name.CompareTo("[PRIVATE] " + sender) == 0)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        TabPage temp = new TabPage("[PRIVATE] " + sender);
                        temp.Name = "[PRIVATE] " + sender;

                        TableLayoutPanel temp_chat = new TableLayoutPanel
                        {
                            ColumnCount = 1,
                            RowCount = 1,
                            Name = "Private_Chat_Panel",
                            AccessibleName = "Private_Chat_Panel",
                            BackColor = Color.FromArgb(64, 64, 64),
                            Anchor = AnchorStyles.Top |
                                     AnchorStyles.Bottom |
                                     AnchorStyles.Left |
                                     AnchorStyles.Right,
                        };

                        temp_chat.HorizontalScroll.Visible = false;
                        temp_chat.HorizontalScroll.Maximum = -1;
                        temp_chat.AutoScroll = false;
                        temp_chat.VerticalScroll.Visible = false;
                        temp_chat.AutoScroll = true;

                        temp.Controls.Add(temp_chat);
                        ChatTabs.Invoke(new Action(() => ChatTabs.TabPages.Add(temp)));

                        if (File.Exists(@"C:\Users\Public\Whazzup\Chat History\" + temp.Name + ".txt")) 
                            RetreiveChat(temp.Name, temp_chat);
                    }

                }
                else if (msg.Contains("VOICE"))
                {
                    msg = msg.Replace("VOICE", "");
                    byte[] VoiceArray = Convert.FromBase64String(msg);

                    string filePath = @"C:\Users\Public\Whazzup\Voice Notes\Received\" + Path.GetRandomFileName() + ".wav";
                    File.WriteAllBytes(filePath, VoiceArray);
                    AudioPlayer temp = new AudioPlayer(filePath, AnchorStyles.Left);

                    AddtoChatUI(Chat,temp);


                    SaveChat("VOICE&&$ " + filePath, "allchat");
                }
                else if (msg.Contains("[PRIVATE]VNT@"))
                {
                    msg = msg.Replace("[PRIVATE]VNT@", "");
                    string[] message = msg.Split('@');

                    foreach (TabPage tab in ChatTabs.TabPages)
                    {
                        if (tab.Name.CompareTo("[PRIVATE] " + message[0]) == 0)
                        {
                            Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                            TableLayoutPanel Temp_Panel = (TableLayoutPanel)children[0];

                            try
                            {

                                try
                                {
                                    byte[] VoiceArray = Convert.FromBase64String(message[1]);
                                    string filePath = @"C:\Users\Public\Whazzup\Voice Notes\Received\" + Path.GetRandomFileName() + ".wav";
                                    File.WriteAllBytes(filePath, VoiceArray);

                                    AudioPlayer temp = new AudioPlayer(filePath, AnchorStyles.Left);
                                    AddtoChatUI(Temp_Panel, temp);

                                    SaveChat("VOICENOTE&&$ " + filePath, "[PRIVATE] " + message[0]);
                                }
                                catch (Exception s)
                                {
                                    MessageBox.Show(s.ToString());
                                }

                            }
                            catch (Exception s)
                            {
                                MessageBox.Show(s.ToString());
                            }

                            break;
                        }
                    }
                }
                else if (msg.Contains("[PRIVATE]VID@"))
                {
                    msg = msg.Replace("[PRIVATE]VID@", "");
                    string[] message = msg.Split('@');

                    foreach (TabPage tab in ChatTabs.TabPages)
                    {
                        if (tab.Name.CompareTo("[PRIVATE] " + message[0]) == 0)
                        {
                            Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                            TableLayoutPanel Temp_Panel = (TableLayoutPanel)children[0];

                            try
                            {

                                try
                                {
                                    byte[] VidArray = Convert.FromBase64String(message[1]);
                                    string filePath = null;
                                    string type = message[1].Substring(0, 30);

                                    if (type.Contains("GkXfowEAAAAAAAA"))
                                    {
                                        filePath = @"C:\Users\Public\Whazzup\Videos\" + Path.GetRandomFileName() + ".mkv";
                                        File.WriteAllBytes(filePath, VidArray);
                                    }
                                    else if (type.Contains("AAAAGGZ0eXBtcDQyAAAAAGl"))
                                    {
                                        filePath = @"C:\Users\Public\Whazzup\Videos\" + Path.GetRandomFileName() + ".mp4";
                                        File.WriteAllBytes(filePath, VidArray);
                                    }
                                    VideoPlayer temp = null;

                                    this.Invoke(new Action(() => temp = new VideoPlayer(filePath,AnchorStyles.Left)));
                                    AddtoChatUI(Temp_Panel, temp);
                                    
                                    SaveChat("VIDEO&&$ " + filePath,"[PRIVATE] " + message[0]);
                                }
                                catch (Exception s)
                                {
                                    MessageBox.Show(s.ToString());
                                }

                            }
                            catch (Exception s)
                            {
                                MessageBox.Show(s.ToString());
                            }

                            break;
                        }
                    }
                }
                else if (msg.Contains("AAAAGGZ0eXBtcDQyAAAAAGl") || msg.Contains("GkXfowEAAAAAAAA"))
                {
                    try
                    {
                        byte[] VidArray = Convert.FromBase64String(msg);
                        string filePath = null;
                        string type = msg.Substring(0, 30);

                        if (type.Contains("GkXfowEAAAAAAAA"))
                        {
                            filePath = @"C:\Users\Public\Whazzup\Videos\" + Path.GetRandomFileName() + ".mkv";
                            File.WriteAllBytes(filePath, VidArray);
                        }
                        else if (type.Contains("AAAAGGZ0eXBtcDQyAAAAAGl"))
                        {
                            filePath = @"C:\Users\Public\Whazzup\Videos\" + Path.GetRandomFileName() + ".mp4";
                            File.WriteAllBytes(filePath, VidArray);
                        }
                        
                        VideoPlayer temp = null;

                        this.Invoke(new Action(() => temp = new VideoPlayer(filePath, AnchorStyles.Left)));

                        AddtoChatUI(Chat, temp);

                        SaveChat("VIDEO&&$ " + filePath, "allchat");
                    }
                    catch (Exception s)
                    {
                        MessageBox.Show(s.ToString());
                    }


                }
                else if (msg.Contains("[PRIVATE]IMG@"))
                {
                    msg = msg.Replace("[PRIVATE]IMG@", "");
                    string[] message = msg.Split('@');

                    foreach (TabPage tab in ChatTabs.TabPages)
                    {
                        if (tab.Name.CompareTo("[PRIVATE] " + message[0]) == 0)
                        {
                            Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                            TableLayoutPanel Temp_Panel = (TableLayoutPanel)children[0];

                            try
                            {

                                byte[] ImgArray = Convert.FromBase64String(message[1]);
                                string filePath = "";
                                string type = message[1].Substring(0, 20);

                                if (type.Contains("/9j/4AAQSkZJRgABA"))
                                    filePath = @"C:\Users\Public\Whazzup\Images\" + Path.GetRandomFileName() + ".jpg";
                                else if (type.Contains("iVBORw0KGgo"))
                                    filePath = @"C:\Users\Public\Whazzup\Images\" + Path.GetRandomFileName() + ".png";

                                File.WriteAllBytes(filePath, ImgArray);
                                using (MemoryStream ms = new MemoryStream(ImgArray, 0, ImgArray.Length))
                                {
                                    Image img = Image.FromStream(ms, false);

                                    Panel Temp_IMGPanel = new Panel()
                                    {
                                        Width = 150,
                                        Height = 150,
                                        Anchor = AnchorStyles.Left,
                                        BackColor = System.Drawing.Color.White
                                    };

                                    PictureBox temp1 = null;

                                    this.Invoke(new Action(() => temp1 = new PictureBox
                                    {
                                        Anchor = AnchorStyles.Left,
                                        Image = img,
                                        SizeMode = PictureBoxSizeMode.StretchImage,
                                        Width = 150,
                                        Height = 150,
                                        Padding = new Padding(2)
                                    }));

                                    Temp_IMGPanel.Controls.Add(temp1);

                                    AddtoChatUI(Temp_Panel, Temp_IMGPanel);

                                    SaveChat("IMAGE&&$ " + filePath, "[PRIVATE] " + message[0]);
                                }
                            }
                            catch (Exception s)
                            {
                                MessageBox.Show(s.ToString());
                            }

                            break;
                        }
                    }
                }

                else if (msg.Contains("/9j/4AAQSkZJRgABA") || msg.Contains("iVBORw0KGgo"))
                {
                    try
                    {
                        byte[] ImgArray = Convert.FromBase64String(msg);
                        string filePath = "";
                        string type = msg.Substring(0, 20);

                        if (type.Contains("/9j/4AAQSkZJRgABA"))
                            filePath = @"C:\Users\Public\Whazzup\Images\" + Path.GetRandomFileName() + ".jpg";
                        else if (type.Contains("iVBORw0KGgo"))
                            filePath = @"C:\Users\Public\Whazzup\Images\" + Path.GetRandomFileName() + ".png";

                        File.WriteAllBytes(filePath, ImgArray);
                        using (MemoryStream ms = new MemoryStream(ImgArray, 0, ImgArray.Length))
                        {
                            Image img = Image.FromStream(ms, false);

                            Panel Temp_Panel = new Panel()
                            {
                                Width = 150,
                                Height = 150,
                                Anchor = AnchorStyles.Left,
                                BackColor = Color.White
                            };

                            PictureBox temp1 = new PictureBox
                            {
                                Anchor = AnchorStyles.Left,
                                Image = img,
                                SizeMode = PictureBoxSizeMode.StretchImage,
                                Width = 150,
                                Height = 150,
                                Padding = new Padding(2)
                            };

                            Temp_Panel.Controls.Add(temp1);

                            AddtoChatUI(Chat, Temp_Panel);

                            SaveChat("IMAGE&&$ " + filePath, "allchat");
                        }
                    }
                    catch (Exception s)
                    {
                        MessageBox.Show(s.ToString());
                    }
                }
                else if (msg.Contains("[PRIVATE]AUD@"))
                {
                    msg = msg.Replace("[PRIVATE]AUD@", "");
                    string[] message = msg.Split('@');

                    foreach (TabPage tab in ChatTabs.TabPages)
                    {
                        if (tab.Name.CompareTo("[PRIVATE] " + message[0]) == 0)
                        {
                            Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                            TableLayoutPanel Temp_Panel = (TableLayoutPanel)children[0];

                            try
                            {
                                byte[] AudioArray = Convert.FromBase64String(message[1]);

                                string filePath = "";

                                string type = message[0].Substring(0, 20);

                                if (type.Contains("//FQg"))
                                {
                                    filePath = @"C:\Users\Public\Whazzup\Audio\" + Path.GetRandomFileName() + ".aac";
                                    File.WriteAllBytes(filePath, AudioArray);
                                }
                                else if (type.Substring(10).Contains("ZkxhQwAAAC"))
                                {
                                    filePath = @"C:\Users\Public\Whazzup\Audio\" + Path.GetRandomFileName() + ".flac";
                                    File.WriteAllBytes(filePath, AudioArray);
                                }
                                else if (type.Substring(4).Contains("SUQz"))
                                {
                                    filePath = @"C:\Users\Public\Whazzup\Audio\" + Path.GetRandomFileName() + ".mp3";
                                    File.WriteAllBytes(filePath, AudioArray);
                                }
                                else if (type.Substring(5).Contains("UklGR"))
                                {
                                    filePath = @"C:\Users\Public\Whazzup\Audio\" + Path.GetRandomFileName() + ".wav";
                                    File.WriteAllBytes(filePath, AudioArray);
                                }
                                

                                AudioPlayer temp = new AudioPlayer(filePath, AnchorStyles.Left);

                                AddtoChatUI(Temp_Panel, temp);

                                SaveChat("AUDIO&&$ " + filePath, "[PRIVATE] " + message[0]);

                            }
                            catch (Exception s)
                            {
                                MessageBox.Show(s.ToString());
                            }

                            break;
                        }
                    }
                }
                else if (msg.Contains("SUQz") || msg.Contains("ZkxhQwAAAC") || msg.Contains("UklGR") || msg.Contains("//FQg"))
                {
                    try
                    {
                        byte[] AudioArray = Convert.FromBase64String(msg);
                        string filePath = "";

                        string type = msg.Substring(0,20);

                        if (type.Contains("//FQg"))
                        {
                            filePath = @"C:\Users\Public\Whazzup\Audio\" + Path.GetRandomFileName() + ".aac";
                            File.WriteAllBytes(filePath, AudioArray);
                        }
                        else if (type.Contains("ZkxhQwAAAC"))
                        {
                            filePath = @"C:\Users\Public\Whazzup\Audio\" + Path.GetRandomFileName() + ".flac";
                            File.WriteAllBytes(filePath, AudioArray);
                        }
                        else if (type.Contains("SUQz"))
                        {
                            filePath = @"C:\Users\Public\Whazzup\Audio\" + Path.GetRandomFileName() + ".mp3";
                            File.WriteAllBytes(filePath, AudioArray);
                        }
                        else if (type.Contains("UklGR"))
                        {
                            filePath = @"C:\Users\Public\Whazzup\Audio\" + Path.GetRandomFileName() + ".wav";
                            File.WriteAllBytes(filePath, AudioArray);
                        }
                        SaveChat("AUDIO&&$ " + filePath, "allchat");

                        AudioPlayer temp = new AudioPlayer(filePath, AnchorStyles.Left);

                        AddtoChatUI(Chat, temp);
                        
                    }
                    catch (Exception s)
                    {
                        MessageBox.Show(s.ToString());
                    }
                }
                

                else
                {
                    if (msg.Contains("IMG&&$") || msg.Contains("AUD&&$") || msg.Contains("VID&&$") || msg.Contains("VNT&&$"))
                    {

                        if (msg.Contains("[PRIVATE]"))
                        {
                            string sender = msg.Substring(10);

                            string filetype = sender.Substring(0, 6);
                            sender = sender.Substring(6);
                            string[] message = sender.Split(':');

                            RichTextBox temp = CreateChatBox(AnchorStyles.Left, sender);

                            foreach (TabPage tab in ChatTabs.TabPages)
                            {
                                if (tab.Name.CompareTo("[PRIVATE] " + message[0]) == 0)
                                {
                                    Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                                    TableLayoutPanel Temp_Panel = (TableLayoutPanel)children[0];

                                    AddtoChatUI(Temp_Panel, temp);
                                    SaveChat(sender, "[PRIVATE] " + message[0]);
                                    break;
                                }
                            }

                            if (filetype.CompareTo("VNT&&$") == 0)
                            {
                                TrayNotify.ShowBalloonTip(1000, "Private Voice From " + message[0], message[1], ToolTipIcon.Info);
                            }
                            if (filetype.CompareTo("IMG&&$") == 0)
                            {
                                TrayNotify.ShowBalloonTip(1000, "Private Image From " + message[0], message[1], ToolTipIcon.Info);
                            }
                            if (filetype.CompareTo("VID&&$") == 0)
                            {
                                TrayNotify.ShowBalloonTip(1000, "Private Video From " + message[0], message[1], ToolTipIcon.Info);
                            }
                            if (filetype.CompareTo("AUD&&$") == 0)
                            {
                                TrayNotify.ShowBalloonTip(1000, "Private Audio From " + message[0], message[1], ToolTipIcon.Info);
                            }
                        }
                        else
                        {

                            string filetype = msg.Substring(0, 6);
                            //MessageBox.Show(filetype);
                            msg = msg.Substring(6);
                            string[] message = msg.Split(':');
                            OwnChat(message[0] + ": " + message[1], AnchorStyles.Left);

                            using (StreamWriter sw = File.AppendText(@"C:\Users\Public\Whazzup\Chat History\allchat.txt"))
                            {
                                sw.WriteLine(message[0] + ": " + message[1]);
                            }
                            if (filetype.CompareTo("VNT&&$") == 0)
                            {
                                TrayNotify.ShowBalloonTip(1000, "New Voice From " + message[0], message[1], ToolTipIcon.Info);
                            }
                            if (filetype.CompareTo("IMG&&$") == 0)
                            {
                                TrayNotify.ShowBalloonTip(1000, "New Image From " + message[0], message[1], ToolTipIcon.Info);
                            }
                            if (filetype.CompareTo("AUD&&$") == 0)
                            {
                                TrayNotify.ShowBalloonTip(1000, "New Audio From " + message[0], message[1], ToolTipIcon.Info);
                            }
                            if (filetype.CompareTo("VID&&$") == 0)
                            {
                                TrayNotify.ShowBalloonTip(1000, "New Video From " + message[0], message[1], ToolTipIcon.Info);
                            }

                        }
                    }
                    else if (msg.Contains("[PRIVATE]"))
                    {
                        string sender = msg.Substring(10);
                        string[] message = sender.Split(':');

                        RichTextBox temp = CreateChatBox(AnchorStyles.Left, sender);

                        foreach (TabPage tab in ChatTabs.TabPages)
                        {
                            if (tab.Name.CompareTo("[PRIVATE] " + message[0]) == 0)
                            {
                                Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                                TableLayoutPanel Temp_Panel = (TableLayoutPanel)children[0];

                                AddtoChatUI(Temp_Panel, temp);

                                break;
                            }
                        }
                        SaveChat(sender, "[PRIVATE] " + message[0]);

                        TrayNotify.ShowBalloonTip(1000, "Private Message From " + message[0], message[1], ToolTipIcon.Info);
                    }
                    else if (msg.Contains(":"))
                    {
                        string[] message = msg.Split(':');
                        OwnChat(message[0] + ": " + message[1], AnchorStyles.Left);

                        TrayNotify.ShowBalloonTip(1000, "New Message From " + message[0], message[1], ToolTipIcon.Info);
                    }
                    else if (msg.CompareTo("Connected") == 0)
                    {
                        OwnChat(msg, AnchorStyles.Left);

                    }
                    else if (msg.Contains("Disconnected"))
                    {
                        OwnChat(msg, AnchorStyles.Left);

                        TrayNotify.ShowBalloonTip(1000, "User Disconnected", msg, ToolTipIcon.Info);

                        string disc = msg.Substring(5);
                        disc = disc.Replace(" Disconnected", "");

                        RichTextBox temp = CreateChatBox(AnchorStyles.Left, msg);

                        foreach (TabPage tab in ChatTabs.TabPages)
                        {
                            if (tab.Name.CompareTo("[PRIVATE] " + disc) == 0)
                            {
                                Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                                TableLayoutPanel Temp_Panel = (TableLayoutPanel)children[0];

                                AddtoChatUI(Temp_Panel, temp);

                                break;
                            }
                        }
                    }
                }
                ClientSocket.BeginReceive(ReceiveBuffer, 0, ReceiveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), ClientSocket);
            }
            catch(Exception s)
            {
                Console.WriteLine(s.ToString());
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ClientSocket.Connected)
            {
                byte[] SendBuffer = new byte[1500];
                string disc = "!Disconnect" + UserName.Text;

                SendBuffer = Convert.FromBase64String(Base64Encode(disc));

                SendBuffer = Encryption.encryptStream(SendBuffer);
                SendBuffer = Compression.CompressBytes(SendBuffer);
                

                ClientSocket.BeginSend(SendBuffer, 0, SendBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), ClientSocket);
                ClientSocket.Shutdown(SocketShutdown.Both);
                ClientSocket.Disconnect(true);
            }

            File.Encrypt(@"C:\Users\Public\Whazzup\Chat History");
        }

        private void FileButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            FileLocation = openFileDialog1.FileName;
            
            string ext = Path.GetExtension(FileLocation);
            if (ext.CompareTo(".bmp") == 0 || ext.CompareTo(".jpg") == 0 || ext.CompareTo(".jpeg") == 0 || ext.CompareTo(".png") == 0 || ext.CompareTo(".gif") == 0)
            {

                MessageRelay("IMG&&$");
                

                Panel Temp_Panel = new Panel()
                {
                    Width = 150,
                    Height = 150,
                    Anchor = AnchorStyles.Right,
                    BackColor = System.Drawing.Color.White
                };

                Image IMG = Image.FromFile(FileLocation);
                PictureBox temp = new PictureBox
                {
                    Anchor = AnchorStyles.None,
                    Image = IMG,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Width = 150,
                    Height = 150,
                    Padding = new Padding(2)
                };

                Temp_Panel.Controls.Add(temp);
                
                byte[] ImageBuffer = File.ReadAllBytes(FileLocation);

                string test;

                if (ChatTabs.SelectedTab.Text.CompareTo("All Chat") == 0)
                {
                    test = Convert.ToBase64String(ImageBuffer);

                    AddtoChatUI(Chat, Temp_Panel);

                    SaveChat("You: IMAGE&&$ " + FileLocation, "allchat");
                }
                else
                {
                    foreach (TabPage tab in ChatTabs.TabPages)
                    {
                        if (tab.Name.CompareTo(ChatTabs.SelectedTab.Text) == 0)
                        {
                            Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                            foreach (Control child in children)
                            {
                                Console.WriteLine(child.Name);
                            }

                            TableLayoutPanel Temp_Panel1 = (TableLayoutPanel)children[0];

                            AddtoChatUI(Temp_Panel1, Temp_Panel);

                            SaveChat("You: IMAGE&&$ " + FileLocation, ChatTabs.SelectedTab.Text);
                            break;
                        }
                    }
                    

                    test = UserName.Text+"@IMG"+ChatTabs.SelectedTab.Text+"@" + Convert.ToBase64String(ImageBuffer);

                }
                
                byte[] SendBuffer2 = Convert.FromBase64String(Base64Encode(test));

                SendBuffer2 = Encryption.encryptStream(SendBuffer2);
                SendBuffer2 = Compression.CompressBytes(SendBuffer2);
                

                ClientSocket.BeginSend(SendBuffer2, 0, SendBuffer2.Length, SocketFlags.None, new AsyncCallback(SendCallBack), ClientSocket);

            }
            else if(ext.CompareTo(".mp3") == 0 || ext.CompareTo(".flac") == 0 || ext.CompareTo(".aac") == 0 || ext.CompareTo(".wav") == 0)
            {
                MessageRelay("AUD&&$");
                

                byte[] AudioBuffer = File.ReadAllBytes(FileLocation);
                string audio;
                

                if (ChatTabs.SelectedTab.Text.CompareTo("All Chat") == 0)
                {
                    audio = Convert.ToBase64String(AudioBuffer);
                    AudioPlayer temp = new AudioPlayer(FileLocation, AnchorStyles.Right);

                    AddtoChatUI(Chat, temp);
                    
                    SaveChat("You: AUDIO&&$ " + FileLocation, "allchat");
                }
                else
                {
                    foreach (TabPage tab in ChatTabs.TabPages)
                    {
                        if (tab.Name.CompareTo(ChatTabs.SelectedTab.Text) == 0)
                        {
                            Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                            foreach (Control child in children)
                            {
                                Console.WriteLine(child.Name);
                            }

                            TableLayoutPanel Temp_Panel1 = (TableLayoutPanel)children[0];

                            AudioPlayer temp = new AudioPlayer(FileLocation, AnchorStyles.Right);

                            AddtoChatUI(Temp_Panel1, temp);

                            SaveChat("You: AUDIO&&$ " + FileLocation, ChatTabs.SelectedTab.Text);
                            break;
                        }
                    }


                    audio = UserName.Text + "@AUD" + ChatTabs.SelectedTab.Text + "@" + Convert.ToBase64String(AudioBuffer);

                }

                byte[] SendBuffer2 = Convert.FromBase64String(Base64Encode(audio));
                SendBuffer2 = Encryption.encryptStream(SendBuffer2);
                SendBuffer2 = Compression.CompressBytes(SendBuffer2);
                

                ClientSocket.BeginSend(SendBuffer2, 0, SendBuffer2.Length, SocketFlags.None, new AsyncCallback(SendCallBack), ClientSocket);

            }
            else if(ext.CompareTo(".mp4") == 0 || ext.CompareTo(".mkv") == 0 || ext.CompareTo(".avi") == 0 || ext.CompareTo(".m4v") == 0)
            {
                MessageRelay("VID&&$");
                

                byte[] VideoBuffer = File.ReadAllBytes(FileLocation);
                string video;

                if (ChatTabs.SelectedTab.Text.CompareTo("All Chat") == 0)
                {
                    video = Convert.ToBase64String(VideoBuffer);

                    VideoPlayer temp = null;
                    this.Invoke(new Action(() => temp = new VideoPlayer(FileLocation, AnchorStyles.Right)));

                    AddtoChatUI(Chat, temp);
                    
                    SaveChat("You: VIDEO&&$ " + FileLocation, "allchat");
                }
                else
                {
                    foreach (TabPage tab in ChatTabs.TabPages)
                    {
                        if (tab.Name.CompareTo(ChatTabs.SelectedTab.Text) == 0)
                        {
                            Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                            foreach (Control child in children)
                            {
                                Console.WriteLine(child.Name);
                            }

                            TableLayoutPanel Temp_Panel1 = (TableLayoutPanel)children[0];

                            VideoPlayer temp = null;

                            this.Invoke(new Action(() => temp = new VideoPlayer(FileLocation, AnchorStyles.Right)));

                            AddtoChatUI(Temp_Panel1, temp);
                            
                            SaveChat("You: VIDEO&&$ " + FileLocation, ChatTabs.SelectedTab.Text);
                            break;
                        }
                    }
                    
                    video = UserName.Text + "@VID" + ChatTabs.SelectedTab.Text + "@" + Convert.ToBase64String(VideoBuffer);

                }

                byte[] SendBuffer2 = Convert.FromBase64String(Base64Encode(video));
                //MessageBox.Show(SendBuffer2.Length.ToString(),"Initial");
                SendBuffer2 = Encryption.encryptStream(SendBuffer2);
                //MessageBox.Show(SendBuffer2.Length.ToString(), "After encryption");
                SendBuffer2 = Compression.CompressBytes(SendBuffer2);
                //MessageBox.Show(SendBuffer2.Length.ToString(), "After Compression");
                
                ClientSocket.BeginSend(SendBuffer2, 0, SendBuffer2.Length, SocketFlags.None, new AsyncCallback(SendCallBack), ClientSocket);

            }
        }

        private void Voice_Button_Click(object sender, EventArgs e)
        {
            using (Recorder temp = new Recorder())
            {
                var result = temp.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string FilePath = temp.VoiceFilePath;

                    byte[] VoiceBuffer = File.ReadAllBytes(FilePath);

                    string voice = "";

                    if (ChatTabs.SelectedTab.Text.CompareTo("All Chat") == 0)
                    {
                        voice = "VOICE" + Convert.ToBase64String(VoiceBuffer);

                        AudioPlayer temp_audio = new AudioPlayer(FilePath,AnchorStyles.Right);

                        AddtoChatUI(Chat, temp_audio);

                        SaveChat("You: VOICENOTE&&$ " + FilePath, "allchat");
                    }
                    else
                    {
                        foreach (TabPage tab in ChatTabs.TabPages)
                        {
                            if (tab.Name.CompareTo(ChatTabs.SelectedTab.Text) == 0)
                            {
                                Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                                foreach (Control child in children)
                                {
                                    Console.WriteLine(child.Name);
                                }

                                TableLayoutPanel Temp_Panel1 = (TableLayoutPanel)children[0];

                                AudioPlayer temp_audio = new AudioPlayer(FilePath, AnchorStyles.Right);

                                AddtoChatUI(Temp_Panel1, temp_audio);

                                SaveChat("You: VOICENOTE&&$ " + FilePath, ChatTabs.SelectedTab.Text);

                                break;
                            }
                        }

                        voice = UserName.Text + "@VNT" + ChatTabs.SelectedTab.Text + "@" + Convert.ToBase64String(VoiceBuffer);

                    }

                    byte[] SendBuffer2 = Convert.FromBase64String(Base64Encode(voice));
                    SendBuffer2 = Encryption.encryptStream(SendBuffer2);
                    SendBuffer2 = Compression.CompressBytes(SendBuffer2);
                    
                    ClientSocket.BeginSend(SendBuffer2, 0, SendBuffer2.Length, SocketFlags.None, new AsyncCallback(SendCallBack), ClientSocket);
                }
            }
        }

        //CREATE PRIVATE CHAT WINDOW
        private void UserList_DoubleClick(object sender, EventArgs e)
        {
            if (UserList.SelectedIndex != -1)
            {
                if (UserList.SelectedItem.ToString().CompareTo("(All)") == 0 || UserList.SelectedItem.ToString().CompareTo("") == 0)
                {
                    ;
                }
                else
                {
                    Boolean found = false;
                    string Selected_User = UserList.SelectedItem.ToString();
                    foreach (TabPage tab in ChatTabs.TabPages)
                    {
                        if (tab.Name.CompareTo("[PRIVATE] " + Selected_User) == 0)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        TabPage temp = new TabPage("[PRIVATE] " + Selected_User);
                        temp.Name = "[PRIVATE] " + Selected_User;

                        TableLayoutPanel temp_chat = new TableLayoutPanel
                        {
                            ColumnCount = 1,
                            RowCount = 1,
                            Name = "Private_Chat_Panel",
                            AccessibleName = "Private_Chat_Panel",
                            BackColor = Color.FromArgb(64,64,64),
                            Anchor = AnchorStyles.Top |
                                     AnchorStyles.Bottom |
                                     AnchorStyles.Left |
                                     AnchorStyles.Right,
                        };

                        temp_chat.HorizontalScroll.Visible = false;
                        temp_chat.HorizontalScroll.Maximum = -1;
                        temp_chat.AutoScroll = false;
                        temp_chat.VerticalScroll.Visible = false;
                        temp_chat.AutoScroll = true;
                        
                        temp.Controls.Add(temp_chat);
                        ChatTabs.TabPages.Add(temp);

                        if (File.Exists(@"C:\Users\Public\Whazzup\Chat History\" + temp.Name + ".txt")) ;
                            RetreiveChat(temp.Name, temp_chat);
                    }
                    
                    string msg = "NEWPRIVATECHAT&&$"+Selected_User+":FROM"+UserName.Text;

                    byte[] SendBuffer = Convert.FromBase64String(Base64Encode(msg));

                    SendBuffer = Encryption.encryptStream(SendBuffer);
                    SendBuffer = Compression.CompressBytes(SendBuffer);
                    

                    ClientSocket.BeginSend(SendBuffer, 0, SendBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), ClientSocket);
                }
            }
        }
        
        private void OwnChat(string message, AnchorStyles AnchorPoint)
        {

            RichTextBox temp = CreateChatBox(AnchorPoint, message);

            temp.ReadOnly = true;

            AddtoChatUI(Chat, temp);

            using (StreamWriter sw = File.AppendText(@"C:\Users\Public\Whazzup\Chat History\allchat.txt"))
            {
                sw.WriteLine(message);
            }
        }

        private void MessageRelay(string filetype)
        {
            string send;

            if (ChatTabs.SelectedTab.Text.CompareTo("All Chat") == 0)
            {
                OwnChat("You: " + TextField.Text, AnchorStyles.Right);
                send = filetype + UserName.Text + ": " + TextField.Text;

            }
            else
            {
                RichTextBox tempOwn = CreateChatBox(AnchorStyles.Right, "You: " + TextField.Text);

                foreach (TabPage tab in ChatTabs.TabPages)
                {
                    if (tab.Name.CompareTo(ChatTabs.SelectedTab.Text) == 0)
                    {
                        Control[] children = tab.Controls.Find("Private_Chat_Panel", true);

                        TableLayoutPanel Temp_Panel_Own = (TableLayoutPanel)children[0];

                        AddtoChatUI(Temp_Panel_Own, tempOwn);

                        break;
                    }
                }

                string SendTo = ChatTabs.SelectedTab.Text;
                send = filetype + UserName.Text + ": " + TextField.Text + "@" + SendTo;
            }

            byte[] SendBuffer = Convert.FromBase64String(Base64Encode(send));

            SendBuffer = Encryption.encryptStream(SendBuffer);
            SendBuffer = Compression.CompressBytes(SendBuffer);
            

            ClientSocket.BeginSend(SendBuffer, 0, SendBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), ClientSocket);

            TextField.Text = "";
        }

        private RichTextBox CreateChatBox(AnchorStyles side, string text)
        {

            RichTextBox TempChatBox = new RichTextBox
            {
                Text = text,
                BorderStyle = BorderStyle.None,
                WordWrap = true,
                Width = Chat.Width / 3,
                BackColor = System.Drawing.Color.White,
                Anchor = side,
                ReadOnly = true,
                //Padding = new Padding(5),
                Visible = true,
                
            };

            //TempChatBox.SelectAll();
            //Font currentFont = TempChatBox.SelectionFont;
            //FontStyle newFontStyle = (FontStyle)(currentFont.Style);
            //TempChatBox.SelectionFont = new Font(currentFont.FontFamily, 10, newFontStyle);

            using (Graphics g = CreateGraphics())
            {
                TempChatBox.Height = (int)g.MeasureString(TempChatBox.Text, TempChatBox.Font, TempChatBox.Width).Height + 1;
            }
            
            return TempChatBox;
        }

        private void SaveChat(string msg, string chat)
        {
            using (StreamWriter sw = File.AppendText(@"C:\Users\Public\Whazzup\Chat History\"+chat+".txt"))
            {
                sw.WriteLine(msg);
            }
        }

        [STAThread]
        private void RetreiveChat(string ChatName, TableLayoutPanel temppanel)
        {
            try
            {
                string FilePath = @"C:\Users\Public\Whazzup\Chat History\" + ChatName + ".txt";
                string file = "";
                string[] messages = null;
                if (File.Exists(FilePath))
                {
                    File.Decrypt(FilePath);
                    messages = File.ReadAllLines(FilePath);
                    File.Delete(FilePath);

                    foreach (string line in messages)
                    {
                        if (line.CompareTo("Connected") == 0)
                            ;
                        else if (line.Contains("You:"))
                        {
                            if (line.Contains("IMAGE&&$"))
                            {
                                file = line.Replace("You: IMAGE&&$ ", "");
                                if (File.Exists(file))
                                {
                                    Panel Temp_Panel = new Panel()
                                    {
                                        Width = 150,
                                        Height = 150,
                                        Anchor = AnchorStyles.Right,
                                        BackColor = System.Drawing.Color.White
                                    };

                                    Image IMG = Image.FromFile(file);
                                    PictureBox temp = new PictureBox
                                    {
                                        Anchor = AnchorStyles.None,
                                        Image = IMG,
                                        SizeMode = PictureBoxSizeMode.StretchImage,
                                        Width = 150,
                                        Height = 150,
                                        Padding = new Padding(2)
                                    };

                                    Temp_Panel.Controls.Add(temp);

                                    AddtoChatUI(temppanel, Temp_Panel);
                                    
                                    SaveChat("You: IMAGE&&$ " + file, ChatName);
                                }
                            }
                            else if (line.Contains("VIDEO&&$") || line.Contains("AUDIO&&$") || line.Contains("VOICENOTE&&$"))
                            {
                                AudioPlayer temp_Aud = null;
                                VideoPlayer temp_Vid = null;
                                if (line.Contains("VIDEO&&$"))
                                {
                                    SaveChat(line, ChatName);
                                    file = line.Replace("You: VIDEO&&$ ", "");
                                    if (File.Exists(file))
                                    {
                                        this.Invoke(new Action(() => temp_Vid = new VideoPlayer(file, AnchorStyles.Right)));

                                        AddtoChatUI(temppanel, temp_Vid);
                                    }
                                    
                                }
                                if (line.Contains("AUDIO&&$"))
                                {
                                    SaveChat(line, ChatName);
                                    file = line.Replace("You: AUDIO&&$ ", "");
                                    if (File.Exists(file))
                                    {
                                        temp_Aud = new AudioPlayer(file, AnchorStyles.Right);

                                        AddtoChatUI(temppanel, temp_Aud);
                                    }
                                }
                                if (line.Contains("VOICENOTE&&$"))
                                {
                                    SaveChat(line, ChatName);
                                    file = line.Replace("VOICENOTE&&$ ", "");
                                    if (File.Exists(file))
                                    {
                                        temp_Aud = new AudioPlayer(file, AnchorStyles.Right);

                                        AddtoChatUI(temppanel, temp_Aud);
                                    }
                                }
                            }
                            else
                            {
                                RichTextBox temp = CreateChatBox(AnchorStyles.Right, line);

                                AddtoChatUI(temppanel, temp);

                                SaveChat(line, ChatName);
                            }
                        }
                        else
                        {
                            if (line.Contains("IMAGE&&$"))
                            {
                                file = line.Replace("IMAGE&&$ ", "");

                                Panel Temp_Panel = new Panel()
                                {
                                    Width = 150,
                                    Height = 150,
                                    Anchor = AnchorStyles.Left,
                                    BackColor = System.Drawing.Color.White
                                };

                                Image IMG = Image.FromFile(file);
                                PictureBox temp = new PictureBox
                                {
                                    Anchor = AnchorStyles.None,
                                    Image = IMG,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Width = 150,
                                    Height = 150,
                                    Padding = new Padding(2)
                                };

                                Temp_Panel.Controls.Add(temp);

                                AddtoChatUI(temppanel, Temp_Panel);

                                SaveChat("IMAGE&&$ " + file, ChatName);
                            }
                            else if (line.Contains("VIDEO&&$") || line.Contains("AUDIO&&$") || line.Contains("VOICE&&$"))
                            {
                                AudioPlayer temp = null;
                                VideoPlayer temp_vid = null;
                                if (line.Contains("VIDEO&&$"))
                                {
                                    SaveChat(line, ChatName);
                                    file = line.Replace("You: VIDEO&&$ ", "");
                                    if (File.Exists(file))
                                    {
                                        this.Invoke(new Action(() => temp_vid = new VideoPlayer(file, AnchorStyles.Left)));

                                        AddtoChatUI(temppanel, temp_vid);
                                    }
                                }
                                if (line.Contains("AUDIO&&$"))
                                {
                                    SaveChat(line, ChatName);
                                    file = line.Replace("AUDIO&&$ ", "");
                                    temp = new AudioPlayer(file, AnchorStyles.Left);

                                    AddtoChatUI(temppanel, temp);
                                }
                                if (line.Contains("VOICE&&$"))
                                {
                                    SaveChat(line, ChatName);
                                    file = line.Replace("VOICE&&$ ", "");
                                    temp = new AudioPlayer(file, AnchorStyles.Left);

                                    AddtoChatUI(temppanel, temp);
                                }
                            }
                            else
                            {
                                RichTextBox temp = CreateChatBox(AnchorStyles.Left, line);

                                AddtoChatUI(temppanel, temp);

                                SaveChat(line, ChatName);
                            }
                        }
                    }
                    try
                    {
                        //RichTextBox temp_box1 = new RichTextBox
                        //{
                        //    Text = "Previous Messages",
                        //    Anchor = AnchorStyles.None,
                        //    Height = 22,
                        //    Width = 500,
                        //    BorderStyle = BorderStyle.FixedSingle,
                            
                        //};

                        RichTextBox temp_test = CreateChatBox(AnchorStyles.None, "Previous Messages");

                        temp_test.Width = 500;
                        temp_test.SelectAll();
                        temp_test.SelectionAlignment = HorizontalAlignment.Center;

                        AddtoChatUI(temppanel, temp_test);
                    }
                    catch (Exception s)
                    {
                        Console.WriteLine(s.ToString());
                    }

                }
            }
            catch(Exception s)
            {
                MessageBox.Show(s.ToString());
            }
        }

        //RETURN BASE64STRING FROM NORMAL STRING
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        //RETURN NORMAL STRING FROM BASE64STRING
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void vPNSettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(ClientSocket.IsBound)
            {
                MessageBox.Show("Please Disconnect Before Changing VPN Settings","VPN Error",MessageBoxButtons.OK);
            }
            else
            {
                using (VPNSettings temp = new VPNSettings())
                {
                    

                    var result = temp.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string ProxyAddress = temp.address;
                        int  ProxyPort = Convert.ToInt32(temp.port);
                        string type = temp.type;
                        string username = null;
                        string password = null;
                        if (temp.LoginDetailsCheck.Checked)
                        {
                            username = temp.username;
                            password = temp.password;
                        }

                        try
                        {
                            TcpClient tcpClient = null;
                            if(type.CompareTo("SOCKS4") == 0)
                            {
                                if (username == null)
                                {
                                    Socks4ProxyClient ClientProxy = new Socks4ProxyClient(ProxyAddress, ProxyPort);
                                    tcpClient = ClientProxy.CreateConnection(IPaddress, 8888);
                                }
                                else
                                {
                                    Socks4ProxyClient ClientProxy = new Socks4ProxyClient(ProxyAddress, ProxyPort, username);
                                    tcpClient = ClientProxy.CreateConnection(IPaddress, 8888);
                                }
                            }
                            if(type.CompareTo("SOCKS5") == 0)
                            {
                                if (username == null)
                                {
                                    Socks5ProxyClient ClientProxy = new Socks5ProxyClient(ProxyAddress, ProxyPort);
                                    tcpClient = ClientProxy.CreateConnection(IPaddress, 8888);
                                }
                                else
                                {
                                    Socks5ProxyClient ClientProxy = new Socks5ProxyClient(ProxyAddress, ProxyPort, username, password);
                                    tcpClient = ClientProxy.CreateConnection(IPaddress, 8888);
                                }
                            }
                            if(type.CompareTo("HTTP") == 0 || type.CompareTo("HTTPS") == 0)
                            {
                                if (username == null)
                                {
                                    HttpProxyClient ClientProxy = new HttpProxyClient(ProxyAddress, ProxyPort);
                                    tcpClient = ClientProxy.CreateConnection(IPaddress, 8888);
                                }
                                else
                                {
                                    HttpProxyClient ClientProxy = new HttpProxyClient(ProxyAddress, ProxyPort, username, password);
                                    tcpClient = ClientProxy.CreateConnection(IPaddress, 8888);
                                }
                            }
                            
                            ClientSocket = tcpClient.Client;

                            MessageBox.Show("Connected to Proxy Server","Proxy Notice",MessageBoxButtons.OK);

                            ClientSocket.BeginReceive(ReceiveBuffer, 0, ReceiveBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), ClientSocket);
                            string msg = "OnlineConnected8888$$" + UserName.Text;

                            byte[] SendBuffer = Convert.FromBase64String(Base64Encode(msg));

                            SendBuffer = Encryption.encryptStream(SendBuffer);
                            SendBuffer = Compression.CompressBytes(SendBuffer);
                            

                            ClientSocket.BeginSend(SendBuffer, 0, SendBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), ClientSocket);

                            ConnectButton.Text = "Connected";
                            ConnectButton.Enabled = false;
                            UserName.Enabled = false;
                            SendButton.Enabled = true;
                            FileButton.Enabled = true;
                            Voice_Button.Enabled = true;
                        }
                        catch(Exception s)
                        {
                            Console.WriteLine(s.ToString());
                            MessageBox.Show("Invalid Proxy Settings or Connection Error","Proxy Error",MessageBoxButtons.OK);
                        }

                    }
                }
            }
        }

        private void clearMainChatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(File.Exists(@"C:\Users\Public\Whazzup\Chat History\allchat.txt"))
            {
                File.Delete(@"C:\Users\Public\Whazzup\Chat History\allchat.txt");

                Chat.Controls.Clear();

                MessageBox.Show("Chat History for Main Chat Deleted", "Chat History", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("There Is No Chat To Delete", "Chat History Error", MessageBoxButtons.OK);
            }
        }

        private void clearPrivateChatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(@"C: \Users\Public\Whazzup\Chat History\");

            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Name.CompareTo("allchat.txt") == 0)
                    ;
                else
                    file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                if (dir.Name.CompareTo("allchat.txt") == 0)
                    ;
                else
                    dir.Delete(true);
            }

            MessageBox.Show("Private Chat Histories Deleted", "Chat History", MessageBoxButtons.OK);
        }

        private void AddtoChatUI(TableLayoutPanel temppanel, Control temp)
        {
            temppanel.Invoke(new Action(() => temppanel.Controls.Add(temp, 0, temppanel.RowCount-1)));
            temppanel.Invoke(new Action(() => temppanel.RowCount = temppanel.RowCount + 1));
            temppanel.Invoke(new Action(() => temppanel.ScrollControlIntoView(temp)));
        }

        private void UserList_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.SetToolTip(UserList, "Double click on a users name to open a private conversation");
        }
    }
}