using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using static System.Net.WebRequestMethods;
using System.Net;
using File = System.IO.File;
using System.Security.Cryptography.X509Certificates;

namespace RaidFarmer
{
    public partial class Form1 : Form
    {
        bool showcustomdelay = false;
        bool toggled = false;
        int clicks = 0;

        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(Keys ArrowKeys);


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
       

        public Form1()
        {
            InitializeComponent();
            
            int NotSoUniqueHotkeyId = 1;
            int NotSoFuckingUniqueHotkeyId = 2;

            int HotKeyCode = (int)Keys.F9;
            int HotKey10 = (int)Keys.F10;


            Boolean F10Registered = RegisterHotKey(
          this.Handle, NotSoFuckingUniqueHotkeyId, 0x0000, HotKey10);


           Boolean F9Registered = RegisterHotKey(
                this.Handle, NotSoUniqueHotkeyId, 0x0000, HotKeyCode
                
            );

            if (F9Registered)
            {
                label2.ForeColor = Color.Lime;
                label2.Text = "[Success] Hotkey to start / stop is F9";
            }
            else
            {
                label2.ForeColor = Color.Red;
                label2.Text = "[Fail] Hotkey can't load, check for a new Version on Github or End all other Process using F11 as Hotkey!";
                System.Diagnostics.Process.Start("explorer.exe", "https://github.com/ToxicSeinDad");
            }


            if (F10Registered)
            {
                label3.ForeColor = Color.Lime;
                label3.Text = "[Success] Hotkey to exit is F10";
            }
            else
            {
                label3.ForeColor = Color.Red;
                label3.Text = "[Fail] Hotkey can't load, check for a new Version on Github or End all other Process using F10 as Hotkey!";
                System.Diagnostics.Process.Start("explorer.exe", "https://github.com/ToxicSeinDad");
            }


        }

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();


                if (id == 1)
                {
                    
                    if (!toggled)
                    {
                        toggled = true;
                        button1.PerformClick();
                    }
                       
                    else
                    {
                        toggled = false;
                        button4.PerformClick();
                    }

                }

                if (id == 2)
                {
                   Application.Exit();

                }


            }




            base.WndProc(ref m);
        }


       
        
    
        private async void button1_Click(object sender, EventArgs e)
        {
            string sendcustom = textBox2.Text.ToUpper();
            if (checkBox1.Checked)
                this.WindowState = FormWindowState.Minimized;

            clicks = 0;
            toggled = true;

            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;

            if (textBox1.Text == "")
            {
                notification();

                if (checkBox4.Checked)
                {
                    await Task.Delay(3000);
                }

              

                while (toggled)
                {
                    if (checkBox2.Checked)
                       { 
                      
                        SendKeys.SendWait("{"+sendcustom+"}");
                       
                       }
                    await Task.Delay(1500);
                    clicks++;
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                }

            }
            else
            {


                int x = 0;

                if (Int32.TryParse(textBox1.Text, out x))
                {
                    notification();
                    if (checkBox4.Checked)
                    {
                        await Task.Delay(3000);
                    }
                  
                    while (toggled)
                    {

                        if (checkBox2.Checked)
                        {

                            SendKeys.Send("{" + sendcustom + "}");
                        }

                        await Task.Delay(x);
                        clicks++;
                        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                    }





                }
                else
                {
                    MessageBox.Show(textBox1.Text + " isn't a value");
                }
            }


        }
        public void notification()
        {
            if (checkBox3.Checked)
            {
                notifyIcon1.BalloonTipTitle = "Successfully Started!";
                string ico = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\RaidFarmer\toasticon.ico";
                notifyIcon1.Icon = Icon.ExtractAssociatedIcon(ico);
                notifyIcon1.BalloonTipText = "Service has been started! Await 3 Seconds to go into game. Click this message to cancel";
                notifyIcon1.ShowBalloonTip(2000);
            }

    
        }
        private void button2_Click(object sender, EventArgs e)
        {
         


            if (!showcustomdelay)
            {
                showcustomdelay = true;
                MessageBox.Show("Use this only if you know, what are you doing\n if your delay is under 750 it will probaly break the armor stand!");
                label4.Show();
                textBox1.Show();
            }

            else
            {
                showcustomdelay = false;
                timer1.Stop(); //just in case that the timer is still running
                label4.Hide();
                textBox1.Hide();
                label1.Show();
                panel1.Visible = true;
                timer1.Interval = 100;
                timer1.Start();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
      
            textBox1.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
    


            string firstfile = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\RaidFarmer\Accept.firstfile_RaidFarmer";
            string toasticon = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\RaidFarmer\ToastIcon.ico";
            Directory.CreateDirectory(@"C:\Users\"+Environment.UserName+@"\AppData\Roaming\RaidFarmer");

            if (!File.Exists(toasticon))
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFileAsync(

                        new System.Uri("https://cdn.discordapp.com/attachments/1021472474724057141/1022936217328812062/toasticon.ico"),

                        toasticon);
                }

            }


            if (!File.Exists(firstfile))
            {
                DialogResult dialogResult = MessageBox.Show("1.) I am not responsible for any Server bans caused by using this.\n 2.) I am not resposible for any damage to your farm using this.\n 3.) I am not responsible for a lost hardcore world.", firstfile, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    File.Create(firstfile);
                }
            
            else
                {
                    MessageBox.Show("You have to accept this!");
                    Application.Exit();
                }
                

                
            
            }

  
        }


        void stop() //idk for some reason button4 doesnt work and i dont have the time to figure this shit out.
        {
            toggled = false; ;
           
            if (checkBox3.Checked)
            {
                notifyIcon1.BalloonTipTitle = "Successfully Stopped!";
                string ico = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\RaidFarmer\toasticon.ico";
                notifyIcon1.Icon = Icon.ExtractAssociatedIcon(ico);
                notifyIcon1.BalloonTipText = "Service has been stopped, worked for " + clicks + " clicks";
                notifyIcon1.ShowBalloonTip(3000);
            }
            clicks = 0;

        }
        public void button4_Click(object sender, EventArgs e)
        {
            stop();
        }





        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {


        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {


        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {

            stop();
        }


                            
   

private void Form1_KeyDown(object sender, KeyEventArgs a)
        {
        
        }
             
            
   


private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
           
            
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            
        }

        private void button4_KeyDown(object sender, KeyEventArgs e)
        {
 
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //it's for verifying that you'll sneak if you have the sneak button bound to shift, in lowercase you woldn't sneak

            if (checkBox2.Checked)
            {
                textBox2.Show();
                button6.Show();
                MessageBox.Show("C# doesn't support sending Shift only, make sure you have nothing bound to V, (you can still edit the sended key)!");
            }

            else
            {
                textBox2.Hide();
                button6.Hide();
            } 
                

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        

        private void button6_Click(object sender, EventArgs e)
        {
            timer1.Stop(); //just in case that the timer is still running
            timer1.Interval = 100;
            textBox2.Hide();
            button6.Hide();
            
            
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!panel1.Visible)
            {
                panel1.Show();

            }
            panel1.Width -= 15;
            if (panel1.Width == 0)
            {
                panel1.Hide();
                panel1.Width = 438;
                timer1.Interval = 100;
                timer1.Stop();
            }
            

        }

  
    }
}
