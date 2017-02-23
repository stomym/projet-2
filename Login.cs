using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.Permissions;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Input;
using System.Diagnostics;
using System.Media;
using System.Threading;
using RestSharp;
using Newtonsoft.Json.Linq;
namespace Maisha_Launcher
{
    public partial class Login : Form
    {
        bool maintenance = false;
        public bool check = false;
        const string serv = "http://37.187.114.51/launcher/";
        const string apiUrl = "http://maisha.fr:8090/";
        Launcher launcher = new Launcher();
        String Version = "1.0.2.3";
        public string auto_connect;
        private string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Maisha\\";
        public string sessionToken;
        public string user_login;
        Process page_web = new Process();
        bool Loginn = true;
        bool Pwd = true;
        int X;
        int Y;
        System.Drawing.Point PosPoint = new System.Drawing.Point();

        public Login()
        {
            InitializeComponent();
            label_copyright.Text = "v " + Version + " | Copyright © 2017 Maisha. Créé par Couff";
            checkupdate();
            if (!Directory.Exists(appdata))
                Directory.CreateDirectory(appdata);
            checkmaint.RunWorkerAsync();
            var client = new RestClient();

            try
            {
                auto_connect = File.ReadAllText(appdata + "auto_connect.dat");
                sessionToken = File.ReadAllText(appdata + "token.dat");
                user_login = File.ReadAllText(appdata + "infolog.dat");
            }

            catch { }
            if (auto_connect == "1")
            {
                autoconnect();
            }
            else
            {
                Login_Textbox.Text = "Identifiant";
                Login_Textbox.TextAlign = HorizontalAlignment.Center;
                pwd_TextBox.Text = "Mot de passe";
                pwd_TextBox.TextAlign = HorizontalAlignment.Center;
            }



            Login_Textbox.ForeColor = Color.Gray;
            Login_Textbox.Font = new Font(Login_Textbox.Font, FontStyle.Bold);



            pwd_TextBox.ForeColor = Color.Gray;
            pwd_TextBox.Font = new Font(pwd_TextBox.Font, FontStyle.Bold);


        }

        private void autoconnect()
        {
            Login_Textbox.Enabled = false;
            pwd_TextBox.Enabled = false;
            Check_Connect.Checked = true;
            Login_Textbox.Text = user_login;
            pwd_TextBox.Text = "*******";
            check = true;
        }

        private void checkmaint_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {

                var client = new RestClient(apiUrl);

                var request = new RestRequest("api/settings", Method.GET);

                IRestResponse response = client.Execute(request);
                var content = response.Content;

                dynamic res = JObject.Parse(content.ToString());
                //  string test = res.maintenance;
                // System.Windows.Forms.MessageBox.Show(test);
                this.BeginInvoke((MethodInvoker)delegate
                {
                    if (res.maintenance == "1")
                    {
                        panel1.BackColor = Color.Orange;
                        flatStatusBar1.BaseColor = Color.Orange;
                        flatStatusBar1.RectColor = Color.Red;
                        flatStatusBar1.Text = "Maintenance en cours !";
                        Connect_Button.Enabled = false;
                        pictureBox1.ImageLocation = res.maintenance_content;
                        maintenance = true;
                    }
                });
                if (res.maintenance == "0")
                {
                    maintenance = false;
                }
            }
            catch
            {

            }
            System.Threading.Thread.Sleep(5000);
        }

        protected void checkupdate()
        {
            try
            {
                WebClient chkupd = new WebClient();

                string Server_Version = chkupd.DownloadString(serv + @"/config/Version.txt");
                if (Server_Version == null)
                {
                    System.Windows.Forms.MessageBox.Show("Une erreur est survenue !", "Erreur MAJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Windows.Forms.Application.Exit();
                    this.Close();
                }
                if (Server_Version != Version)
                {
                    string message = "Une mise à jour est disponible !";
                    string caption = "MAJ";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    chkupd.DownloadFile(serv + @"config/Updatelauncher.exe", appdata + "Updatelauncher.exe");
                    result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        Process.Start(appdata + "Updatelauncher.exe");
                        this.Dispose();
                        this.Close();
                        Application.ExitThread();
                        Application.Exit();
                    }
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Une erreur est survenue !", "Erreur MAJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
                this.Close();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            X = Control.MousePosition.X - Location.X;
            Y = Control.MousePosition.Y - Location.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Control.MouseButtons == MouseButtons.Left)
            {
                PosPoint = Control.MousePosition;
                PosPoint.Y -= Y;
                PosPoint.X -= X;
                Location = PosPoint;
            }
        }

        private void Login_Textbox_Click(object sender, EventArgs e)
        {
            if (Loginn)
            {
                Loginn = false;
                Login_Textbox.Text = "";
                Login_Textbox.ForeColor = Color.Black;
                Login_Textbox.TextAlign = HorizontalAlignment.Left;
                Login_Textbox.Font = new Font(Login_Textbox.Font, FontStyle.Regular);
            }
        }

        private void Login_Textbox_Enter(object sender, EventArgs e)
        {
            if (Loginn)
            {
                Login_Textbox.Text = "";
                Login_Textbox.ForeColor = Color.Black;
                Loginn = false;
                Login_Textbox.TextAlign = HorizontalAlignment.Left;
                Login_Textbox.Font = new Font(Login_Textbox.Font, FontStyle.Regular);
            }
        }

        private void Login_Textbox_Leave(object sender, EventArgs e)
        {
            if (Login_Textbox.Text == "")
            {
                Login_Textbox.Text = "Identifiant";
                Login_Textbox.TextAlign = HorizontalAlignment.Center;
                Login_Textbox.ForeColor = Color.Gray;
                Login_Textbox.Font = new Font(Login_Textbox.Font, FontStyle.Bold);
                Loginn = true;
            }
        }

        private void pwd_TextBox_Click(object sender, EventArgs e)
        {

            if (Pwd)
            {
                pwd_TextBox.Text = "";
                pwd_TextBox.ForeColor = Color.Black;
                Pwd = false;
                pwd_TextBox.TextAlign = HorizontalAlignment.Left;
                pwd_TextBox.Font = new Font(pwd_TextBox.Font, FontStyle.Regular);
                pwd_TextBox.PasswordChar = '*';
            }
        }

        private void pwd_TextBox_Enter(object sender, EventArgs e)
        {
            if (Pwd)
            {
                pwd_TextBox.Text = "";
                pwd_TextBox.ForeColor = Color.Black;
                Pwd = false;
                pwd_TextBox.TextAlign = HorizontalAlignment.Left;
                pwd_TextBox.Font = new Font(pwd_TextBox.Font, FontStyle.Regular);
                pwd_TextBox.PasswordChar = '*';
            }
        }

        private void pwd_TextBox_Leave(object sender, EventArgs e)
        {
            if (pwd_TextBox.Text == "")
            {
                pwd_TextBox.Text = "Mot de passe";
                pwd_TextBox.TextAlign = HorizontalAlignment.Center;
                pwd_TextBox.ForeColor = Color.Gray;
                pwd_TextBox.Font = new Font(pwd_TextBox.Font, FontStyle.Bold);
                pwd_TextBox.PasswordChar = '\0';
                Pwd = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void create_account_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            page_web.StartInfo.FileName = "http://maisha.fr/inscription";
            page_web.Start();
        }

        private void flatLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Close_Button_MouseEnter(object sender, EventArgs e)
        {
            Close_Button.Image = Properties.Resources.Close_red;
            Size size = new Size(21, 21);
            Close_Button.Size = size;
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }

        private void Close_Button_MouseLeave(object sender, EventArgs e)
        {
            Close_Button.Image = Properties.Resources.Close_white;
            Size size = new Size(20, 20);
            Close_Button.Size = size;
        }

        private void Twitter_Button_Click(object sender, EventArgs e)
        {
            //  page_web.StartInfo.FileName = "";
            //   page_web.Start();
        }

        private void Twitter_Button_MouseEnter(object sender, EventArgs e)
        {
            Size size = new Size(33, 33);
            Twitter_Button.Size = size;
        }

        private void Twitter_Button_MouseLeave(object sender, EventArgs e)
        {
            Size size = new Size(30, 30);
            Twitter_Button.Size = size;
        }

        private void Facebook_Button_MouseEnter(object sender, EventArgs e)
        {
            Size size = new Size(33, 33);
            Facebook_Button.Size = size;
        }

        private void Facebook_Button_MouseLeave(object sender, EventArgs e)
        {
            Size size = new Size(30, 30);
            Facebook_Button.Size = size;
        }

        private void Facebook_Button_Click(object sender, EventArgs e)
        {
            page_web.StartInfo.FileName = "https://www.facebook.com/MaishaRolePlay";
            page_web.Start();
        }

        private void minimize_button_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void minimize_button_MouseEnter(object sender, EventArgs e)
        {
            minimize_button.Image = Properties.Resources.minimize_red;
            Size size = new Size(21, 21);
            minimize_button.Size = size;
        }

        private void minimize_button_MouseLeave(object sender, EventArgs e)
        {
            minimize_button.Image = Properties.Resources.minimize_white;
            Size size = new Size(20, 20);
            minimize_button.Size = size;

        }

        private void Connect_Button_Click(object sender, EventArgs e)
        {
            if (auto_connect == "1")
            {
                var client = new RestClient(apiUrl);

                var request = new RestRequest("api/users/client/get", Method.POST);

                request.AddParameter("token", sessionToken);

                IRestResponse response = client.Execute(request);
                var content = response.Content;

                dynamic res = JObject.Parse(content.ToString());
                if (res.status == "42")
                {
                    launcher.Show();
                    launcher.launchInit();
                    this.Close();
                }
                else
                {
                    flatStatusBar1.Text = "La connexion automatique a échouer, Veulliez vous entrer votre identifiant et mot de passe !";
                    flatStatusBar1.TextColor = Color.Red;
                    Login_Textbox.Enabled = true;
                    pwd_TextBox.Enabled = true;
                    auto_connect = "0";
                    label_Message.Text = "La connexion automatique a échouer !";
                    label_Message.ForeColor = Color.Red;
                }
            }
            else
            {

                var client = new RestClient(apiUrl);

                var request = new RestRequest("api/login", Method.POST);

                request.AddParameter("login", Login_Textbox.Text);
                request.AddParameter("password", pwd_TextBox.Text);
                request.AddParameter("launcher", 1);

                IRestResponse response = client.Execute(request);
                var content = response.Content;

                dynamic res = JObject.Parse(content.ToString());
                if (res.status == "42")
                {
                    string token = res.token;
                    File.WriteAllText(appdata + "token.dat", token);
                    if (Check_Connect.Checked == true)
                    {
                        File.WriteAllText(appdata + "Auto_connect.dat", "1");
                    }
                    else
                    {
                        File.WriteAllText(appdata + "Auto_connect.dat", "0");
                    }

                    string message = res.message;
                    label_Message.Text = message;
                    File.WriteAllText(appdata + "infolog.dat", Login_Textbox.Text);
                    launcher.Show();
                    launcher.launchInit();
                    this.Close();
                }
                else
                {
                    flatStatusBar1.Text = "Identifiant ou mot de passe incorrect";
                    flatStatusBar1.TextColor = Color.Red;
                    label_Message.Text = "Identifiant ou mot de passe incorrect";
                    label_Message.ForeColor = Color.Red;
                }

            }
        }

        private void Check_Connect_CheckedChanged(object sender, EventArgs e)
        {
            if (check == true)
            {
                if (Check_Connect.Checked == true)
                {
                    if (File.Exists(appdata + "Auto_connect.dat") == true)
                    {
                        File.WriteAllText(appdata + "Auto_connect.dat", "0");
                    }
                    Check_Connect.Checked = false;
                    pwd_TextBox.Enabled = true;
                    Login_Textbox.Enabled = true;

                }

            }
        }

        private void checkmaint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (maintenance == false)
            {

                Connect_Button.Enabled = true;
                pictureBox1.Image = Properties.Resources.Img_Form1;
                panel1.BackColor = Color.FromArgb(5, 86, 147);
                flatStatusBar1.BaseColor = Color.FromArgb(5, 86, 147);
                flatStatusBar1.RectColor = Color.FromArgb(0, 192, 192);
                flatStatusBar1.Text = "";
                this.Refresh();
                checkmaint.RunWorkerAsync();
            }
            else
                checkmaint.RunWorkerAsync();
        }

        private void pass_forgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            page_web.StartInfo.FileName = "http://maisha.fr/recuperation-mdp";
            page_web.Start();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            launcher.Show();
            launcher.launchInit();
        }
    }
}
