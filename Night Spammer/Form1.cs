using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tragedy_Spammer
{
    public partial class Form1 : Form
    {
        public Point mouseLocation;
        public Form1()
        {
            InitializeComponent();
        }

        public const uint WM_KEYDOWN = 0x100;
        public const uint WM_KEYUP = 0x0101;
        public TypeConverter converter = TypeDescriptor.GetConverter(typeof(Keys));
        ListViewItem lv;

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr Hwnd, int msg, int param, int lparam);

        [DllImport("user32.dll")]
        public static extern int ReleaseCapture();

        private void MoveWindow(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            // Version updater
            //WebClient webClient = new WebClient();
            //int crntVersion = 300; // Current growtopia version, it's for checking if some updates are available
            //string version = webClient.DownloadString("");
            ////string downloadLink = webClient.DownloadString("");
            //Int32.TryParse(version, out int intVersion);
            //if (crntVersion != intVersion)
            //{
            //    if (MessageBox.Show("Updates are available, do you want to download them? Without update you can't use this application.", "Tragedy Spammer Updater", MessageBoxButtons.YesNo) == DialogResult.Yes) // Updates are available
            //    {
            //        //Process.Start(""); // Start download link of that update
            //        this.Close();
            //    }
            //    else // Updates are not available
            //    {
            //        this.Close(); // Exit the app
            //    }
            //}
            //else
            //{
            //    // nothing
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void Spam(IntPtr Handle, string Text)
        {
            PostMessage(Handle, WM_KEYDOWN, (IntPtr)(Keys.Enter), IntPtr.Zero);
            PostMessage(Handle, WM_KEYUP, (IntPtr)(Keys.Enter), IntPtr.Zero);
            for (int i = 0; i < Text.Length; i++)
            {
                Keys keys1;
                switch (Text[i].ToString())
                {
                    case " ":
                        keys1 = Keys.Space;
                        break;
                    case "`":
                        keys1 = (Keys)0xC0;
                        break;
                    case @"":
                        keys1 = (Keys)0xDC;
                        break;
                    case "[":
                        keys1 = (Keys)0xDB;
                        break;
                    case "]":
                        keys1 = (Keys)0xDD;
                        break;
                    case "\t":
                    case "\n":
                        keys1 = Keys.Space;
                        break;
                    default:
                        keys1 = (Keys)converter.ConvertFromString(Text[i].ToString());
                        break;
                }
                PostMessage(Handle, WM_KEYDOWN, (IntPtr)keys1, IntPtr.Zero);
                PostMessage(Handle, WM_KEYUP, (IntPtr)keys1, IntPtr.Zero);
            }
            PostMessage(Handle, WM_KEYDOWN, (IntPtr)Keys.Enter, IntPtr.Zero);
            PostMessage(Handle, WM_KEYUP, (IntPtr)Keys.Enter, IntPtr.Zero);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            IntPtr winTitle = Process.GetProcessesByName("Growtopia")[0].MainWindowHandle;
            string spamText = textBox1.Text.ToString();
            Spam(winTitle, spamText.ToUpper());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int value;

            
            if (int.TryParse(textBox2.Text, out value))
            {
                
                if (value > 0)
                {
                    timer2.Interval = value;
                }
            }
            textBox2.Text = trackBar1.Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer2.Stop();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 21)
            {
                textBox2.Text = "3500";
                timer2.Interval = 3500;
                trackBar1.Value = 3500;
                checkBox1.Checked = true;
            }
            else if (textBox1.Text.Length < 41)
            {
                textBox2.Text = "6000";
                timer2.Interval = 6000;
                trackBar1.Value = 6000;
                checkBox1.Checked = true;
            }
            else if (textBox1.Text.Length < 71)
            {
                textBox2.Text = "9600";
                timer2.Interval = 9600;
                trackBar1.Value = 9600;
                checkBox1.Checked = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                timer3.Start();
            }
            else if (checkBox1.Checked == false)
            {
                timer3.Stop();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                timer1.Stop();
            }
            else if (checkBox2.Checked == false)
            {
                timer1.Start();
            }
        }
    }
}
