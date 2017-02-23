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
using System.Drawing.Drawing2D;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Collections;


namespace Maisha_Launcher
{
    public partial class Launcher : Form
    {
        String Version = "1.0.2.3";
        string apiUrl = "http://maisha.fr:8090/";
        string[] serverName_list;
        string[] serverIP_list;
        string[] serverPort_list;
        string[] serverid_list;
        string[] serverGame_list;
        bool maint;
        bool maintenance = false;
        private int count_sup;
        private long count;
        public string directory;
        const string FTP = "http://149.202.89.142/";
        const string serv = "http://37.187.114.51/launcher/";
        const string modfile = @"\@MaishaRP\addons\";
        bool CanDownload = false;
        bool Wait = false;
        private int counter;
        private string counte;
        private int counter2 = 0;
        private int counter_check = 0;
        string IParma3Serv;
        Thread Download_Mod;
        Thread UserData;
        Thread StatusServ;
        Thread Get_news;
        Thread backgroundlauncher;
        Stopwatch sw = new Stopwatch();
        private string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Maisha\\";
        Process page_web = new Process();
        int X;
        int Y;
        bool bool_news = false, bool_addons = false, bool_Site = false, bool_teamspeak = false, bool_Parameter = false;
        System.Drawing.Point PosPoint = new System.Drawing.Point();
        String[,] rssData = null;

        public Launcher()
        {
            InitializeComponent();
        }
        public void launchInit()
        {

            label_copyright.Text = "v " + Version + " | Copyright © 2017 Maisha. Créé par Couff";
            setarma3directory();
            checkmaint.RunWorkerAsync();
            InitServerData("64");
            InitUserData();
            Serverstatus();
            getnews();
            timer_News.Start();
            Menu_Button.Image = RotateImage(Properties.Resources.menu, 90);
        }

        private string getlistserver()
        {
            string id = "";
            /* VARIABLE DECLARATION */
            int total;


            var client = new RestClient(apiUrl);
            var request = new RestRequest("api/server/list", Method.GET);

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            dynamic res = JObject.Parse(content.ToString());

            if (res.status == "42")
            {
                total = res.total;
                serverName_list = new string[total];
                serverIP_list = new string[total];
                serverPort_list = new string[total];
                serverid_list = new string[total];
                serverGame_list = new string[total];
                for (int i = 0; i < total; i++)
                {
                    serverName_list[i] = res.servers[i].name;
                    serverid_list[i] = res.servers[i].id;
                    serverIP_list[i] = res.servers[i].ip;
                    serverPort_list[i] = res.servers[i].port;
                    serverGame_list[i] = res.servers[i].game;
                }

            }
            else
            {

            }
            return (id);
        }

        private void InitServerData(string id_server)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest("api/server/get", Method.POST);
            request.AddParameter("id", id_server);

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            dynamic res = JObject.Parse(content.ToString());
            string vmod = res.vmod;
            string tmp = res.ip + ":" + res.port;
            IParma3Serv = tmp;
            if (!File.Exists(appdata + "vmod.dat"))
            {
                /*    try
                    {
                        myProcesses = Process.GetProcesses();
                        foreach (Process myProcess in myProcesses)
                        {

                            if (myProcess.ProcessName == "arma3.exe" || myProcess.ProcessName == "arma3battleye.exe" || myProcess.ProcessName == "arma3launcher.exe")
                            {
                                myProcess.Kill();
                            }
                        }
                    }
                    catch
                    {
                    }*/
                File.WriteAllText(appdata + "vmod.dat", vmod);
                CheckMD5.RunWorkerAsync();
            }
            else
            {
                string vmod_client = File.ReadAllText(appdata + "vmod.dat");
                if (vmod_client != vmod)
                {
                    /*    try
                        {
                            myProcesses = Process.GetProcesses();
                            foreach (Process myProcess in myProcesses)
                            {

                                if (myProcess.ProcessName == "arma3.exe" || myProcess.ProcessName == "arma3battleye.exe" || myProcess.ProcessName == "arma3launcher.exe")
                                {
                                    myProcess.Kill();
                                }
                            }
                        }
                        catch
                        {
                        }*/
                    File.WriteAllText(appdata + "vmod.dat", vmod);
                    CheckMD5.RunWorkerAsync();
                }
            }
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
                        maintenance = true;
                        if (Play_button.Enabled == true)
                            maint = true;
                        else
                            maint = false;
                    }
                    else if (res.maintenance == "0")
                    {
                        maintenance = false;
                        if (Play_button.Enabled == false)
                            maint = false;
                        else
                            maint = true;
                    }
                });

            }
            catch
            { }
            System.Threading.Thread.Sleep(5000);
        }

        private void checkmaint_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (maintenance == false)
            {
                if (maint == false)
                {
                    Play_button.Invoke(new Action(() => { Play_button.Enabled = true; }));
                    panel1.Invoke(new Action(() => { panel1.BackColor = Color.FromArgb(5, 86, 147); }));
                    flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.BaseColor = Color.FromArgb(5, 86, 147); }));
                    flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.RectColor = Color.FromArgb(0, 192, 192); }));
                    flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.Text = ""; }));
                    panel2.Invoke(new Action(() => { panel2.Visible = true; }));
                    Play_button.Invoke(new Action(() => { Play_button.BaseColor = Color.FromArgb(5, 86, 147); }));
                    this.Refresh();
                }
            }
            else
            {
                if (maint == true)
                {
                    panel1.Invoke(new Action(() => { panel1.BackColor = Color.Orange; }));
                    flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.BaseColor = Color.Orange; }));
                    flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.RectColor = Color.Red; }));
                    flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.Text = "Maintenance en cours !"; }));
                    Play_button.Invoke(new Action(() => { Play_button.Enabled = false; }));
                    panel2.Invoke(new Action(() => { panel2.Visible = false; }));
                    Play_button.Invoke(new Action(() => { Play_button.BaseColor = Color.Orange; }));
                    this.Refresh();
                }
            }

            checkmaint.RunWorkerAsync();
        }

        private void InitUserData()
        {
            UserData = new Thread((ThreadStart)(() =>
            {

                var client = new RestClient("http://maisha.fr:8090");
                var request = new RestRequest("api/server/client/players/get", Method.POST);
                try
                {
                    string sessionToken = File.ReadAllText(appdata + "token.dat");
                    request.AddParameter("token", sessionToken);
                    request.AddParameter("id", 64);
                    IRestResponse response = client.Execute(request);
                    var content = response.Content;
                    dynamic res = JObject.Parse(content.ToString());
                    Resultat_name_label.Invoke(new Action(() => { Resultat_name_label.Text = res.lastname + " " + res.firstname; }));
                    Result_gender_label.Invoke(new Action(() => { Result_gender_label.Text = res.sexe; }));
                    Resultat_nationality_label.Invoke(new Action(() => { Resultat_nationality_label.Text = res.nationality; }));
                    Result_phone_label.Invoke(new Action(() => { Result_phone_label.Text = res.phone_number; }));
                    //Result_bank_label.Text = res.bank;
                    Result_bank_label.Invoke(new Action(() => { Result_bank_label.Text = res.bank; }));
                    Result_cash_label.Invoke(new Action(() => { Result_cash_label.Text = res.cash; }));

                    string tmp = res.birthday;
                    tmp = tmp.Trim(new Char[] { ' ', '[', '"', ']' });
                    tmp = tmp.Replace(",", " ");
                    Result_birthday_label.Invoke(new Action(() => { Result_birthday_label.Text = tmp; }));
                    string res_job = res.jobs;
                    string jobs_tmp = res.jobs_rank;
                    int res_rank_job = Int32.Parse(jobs_tmp);
                    #region switch Jobs
                    switch (res_job)
                    {
                        case "CIV":
                            #region switch
                            switch (res_rank_job)
                            {
                                case 1:
                                    res_job = "Citoyen";
                                    break;
                                case 2:
                                    res_job = "Rebelle";
                                    break;
                            }
                            #endregion
                            Result_jobs_label.Invoke(new Action(() => { Result_jobs_label.Text = res_job; }));
                            break;
                        case "Gendarmerie":
                            #region switch
                            switch (res_rank_job)
                            {
                                case 1:
                                    res_job = "Gendarme Adjoint";
                                    break;
                                case 2:
                                    res_job = "Gendarme";
                                    break;
                                case 3:
                                    res_job = "Maréchal des Logis";
                                    break;
                                case 4:
                                    res_job = "Adjudant";
                                    break;
                                case 5:
                                    res_job = "Adjudant-Chef";
                                    break;
                                case 6:
                                    res_job = "Major";
                                    break;
                                case 7:
                                    res_job = "Sous-Lieutenant";
                                    break;
                                case 8:
                                    res_job = "Lieutenant";
                                    break;
                                case 9:
                                    res_job = "Capitaine";
                                    break;
                                case 10:
                                    res_job = "Chef d'Escadron";
                                    break;
                                case 11:
                                    res_job = "Colonel";
                                    break;
                            }
                            #endregion
                            Result_jobs_label.Invoke(new Action(() => { Result_jobs_label.Text = res_job; }));
                            break;
                        case "Gouvernement":
                            #region switch
                            switch (res_rank_job)
                            {
                                case 1:
                                    res_job = "Maire";
                                    break;
                                case 2:
                                    res_job = "1er Ministre";
                                    break;
                                case 3:
                                    res_job = "Président";
                                    break;
                            }
                            #endregion
                            Result_jobs_label.Invoke(new Action(() => { Result_jobs_label.Text = res_job; }));
                            break;
                        case "medecin":
                            #region switch
                            switch (res_rank_job)
                            {
                                case 1:
                                    res_job = "Stagiaire Infirmier";
                                    break;
                                case 2:
                                    res_job = "Infirmier";
                                    break;
                                case 3:
                                    res_job = "Médecin Externe";
                                    break;
                                case 4:
                                    res_job = "Médecin Interne";
                                    break;
                                case 5:
                                    res_job = "Médecin Titulaire";
                                    break;
                                case 6:
                                    res_job = "Médecin Chef";
                                    break;
                            }
                            #endregion
                            Result_jobs_label.Invoke(new Action(() => { Result_jobs_label.Text = res_job; }));
                            break;

                    }
                    #endregion

                }
                catch { }
            }));
            UserData.Start();
        }

        private void Serverstatus()
        {
            bool boucle = true;
            StatusServ = new Thread((ThreadStart)(() =>
            {

                var client = new RestClient(apiUrl);
                var request = new RestRequest("api/server/status/get", Method.POST);
                do
                {
                    try
                    {
                        request.AddParameter("name", "MaishaRP");
                        request.AddParameter("arma_ip", IParma3Serv);

                        IRestResponse response = client.Execute(request);
                        var content = response.Content;

                        dynamic res = JObject.Parse(content.ToString());

                        if (res.status == "42")
                        {
                            if (res.online == true)
                            {
                                label_nom_serveur.Text = res.server_name;
                                label_status.Text = "En ligne";
                                label_status.ForeColor = Color.Green;
                                label_Mission.Text = "Mission: " + res.server_mission;
                                label_player.Text = "Joueur: " + res.server_onlineplayers + " / " + res.server_maxplayers;
                                label_map.Text = "Map: " + res.server_map;
                            }
                            else
                            {
                                label_nom_serveur.Text = "";
                                label_status.Text = "Hors ligne";
                                label_status.ForeColor = Color.Red;
                                label_Mission.Text = "";
                                label_player.Text = "";
                                label_map.Text = "";
                            }
                        }
                        else
                        {
                            label_nom_serveur.Text = "Erreur";
                            label_status.Text = "Erreur";
                            label_status.ForeColor = Color.Red;
                            label_Mission.Text = "Erreur";
                            label_player.Text = "Erreur";
                            label_map.Text = "Erreur";

                        }
                    }
                    catch { }
                    System.Threading.Thread.Sleep(10000);
                }
                while (boucle);
            }));
            StatusServ.Start();
        }

        protected void setarma3directory()                                                                  //Répertoire arma 3
        {
            try
            {
                RegistryKey rkLocalMachine = Registry.LocalMachine;
                RegistryKey rkey = rkLocalMachine.OpenSubKey("Software\\Wow6432Node\\Bohemia Interactive\\Arma 3", false);
                directory = (string)rkey.GetValue("main");
                if (directory == "")
                {
                    System.Windows.Forms.MessageBox.Show("vous devez spécifier le chemin de votre dossier arma 3", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    folderBrowserDialog1.ShowDialog();
                    directory = folderBrowserDialog1.SelectedPath;
                    if (directory.Equals(""))
                        System.Windows.Forms.MessageBox.Show("Vous devez avoir arma 3 pour pouvoir jouer sur le serveur", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                if (directory == "")
                {
                    System.Windows.Forms.MessageBox.Show("vous devez spécifier le chemin de votre dossier arma 3", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    folderBrowserDialog1.ShowDialog();
                    directory = folderBrowserDialog1.SelectedPath;
                    if (directory.Equals(""))
                        System.Windows.Forms.MessageBox.Show("Vous devez avoir arma 3 pour pouvoir jouer sur le serveur", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Catch ex As Exception;
            if (!Directory.Exists(directory + @"\@MaishaRP\addons\"))
                Directory.CreateDirectory(directory + @"\@MaishaRP\addons\");
            return;

        }

        #region Control_form
        #region Header  
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

        #region Close Button
        private void Close_Button_MouseEnter(object sender, EventArgs e)
        {
            Close_Button.Image = Properties.Resources.Close_red;
            Size size = new Size(21, 21);
            Close_Button.Size = size;
        }

        private void Close_Button_Click(object sender, EventArgs e)
        {
            try { if (UserData.IsAlive) { UserData.Abort(); } } catch { }
            try { if (StatusServ.IsAlive) { StatusServ.Abort(); } } catch { }
            try { if (Download_Mod.IsAlive) { Download_Mod.Abort(); } } catch { }
            try { if (Get_news.IsAlive) { Get_news.Abort(); } } catch { }
            try { if (timer_News.Enabled) { timer_News.Stop(); } } catch { }
            try { if (timer_UserData.Enabled) { timer_UserData.Stop(); } } catch { }
            this.Close();
            Application.Exit();
        }

        private void Close_Button_MouseLeave(object sender, EventArgs e)
        {
            Close_Button.Image = Properties.Resources.Close_white;
            Size size = new Size(20, 20);
            Close_Button.Size = size;
        }
        #endregion
        #region Minimize Button
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
        #endregion
        #endregion
        #region Menu
        #region New Button
        private void News_Button_Click(object sender, EventArgs e)
        {
            /* Panel Visibilité*/
            News_Panel.Visible = true;
            Addons_Panel.Visible = false;
            TeamSpeak_Panel.Visible = false;
            Parameter_Panel.Visible = false;
            /*Effet bouton*/
            News_Button.BackColor = Color.FromArgb(5, 86, 147);
            Addons_Button.BackColor = Color.FromArgb(26, 32, 40);
            Site_Button.BackColor = Color.FromArgb(26, 32, 40);
            TeamSpeak_Button.BackColor = Color.FromArgb(26, 32, 40);
            Parameter_Button.BackColor = Color.FromArgb(26, 32, 40);
            bool_news = true;
        }

        private void News_Button_MouseEnter(object sender, EventArgs e)
        {
            News_Button.BackColor = Color.FromArgb(5, 86, 147);
        }

        private void News_Button_MouseLeave(object sender, EventArgs e)
        {
            if (!bool_news)
                News_Button.BackColor = Color.FromArgb(26, 32, 40);

            bool_news = false;
        }
        #endregion
        #region Addons Buttons
        private void Addons_Button_Click(object sender, EventArgs e)
        {
            News_Panel.Visible = false;
            Addons_Panel.Visible = true;
            TeamSpeak_Panel.Visible = false;
            Parameter_Panel.Visible = false;
            /*Effet bouton*/
            News_Button.BackColor = Color.FromArgb(26, 32, 40);
            Addons_Button.BackColor = Color.FromArgb(5, 86, 147);
            Site_Button.BackColor = Color.FromArgb(26, 32, 40);
            TeamSpeak_Button.BackColor = Color.FromArgb(26, 32, 40);
            Parameter_Button.BackColor = Color.FromArgb(26, 32, 40);
            bool_addons = true;
        }

        private void Addons_Button_MouseEnter(object sender, EventArgs e)
        {
            Addons_Button.BackColor = Color.FromArgb(5, 86, 147);
        }

        private void Addons_Button_MouseLeave(object sender, EventArgs e)
        {
            if (!bool_addons)
                Addons_Button.BackColor = Color.FromArgb(26, 32, 40);

            bool_addons = false;
        }
        #endregion
        #region Site Button
        private void Site_Button_Click(object sender, EventArgs e)
        {
            page_web.StartInfo.FileName = "http://www.maisha.fr";
            page_web.Start();
        }

        private void Site_Button_MouseEnter(object sender, EventArgs e)
        {
            Site_Button.BackColor = Color.FromArgb(5, 86, 147);
        }

        private void Site_Button_MouseLeave(object sender, EventArgs e)
        {
            if (!bool_Site)
                Site_Button.BackColor = Color.FromArgb(26, 32, 40);

            bool_Site = false;
        }
        #endregion
        #region Teampseak Button
        private void TeamSpeak_Button_Click(object sender, EventArgs e)
        {
            News_Panel.Visible = false;
            Addons_Panel.Visible = false;
            TeamSpeak_Panel.Visible = true;
            Parameter_Panel.Visible = false;
            /*Effet bouton*/
            News_Button.BackColor = Color.FromArgb(26, 32, 40);
            Addons_Button.BackColor = Color.FromArgb(26, 32, 40);
            Site_Button.BackColor = Color.FromArgb(26, 32, 40);
            TeamSpeak_Button.BackColor = Color.FromArgb(5, 86, 147);
            Parameter_Button.BackColor = Color.FromArgb(26, 32, 40);
            bool_teamspeak = true;
            Process.Start("ts3server://ts3.maisha.fr");
        }

        private void TeamSpeak_Button_MouseEnter(object sender, EventArgs e)
        {
            TeamSpeak_Button.BackColor = Color.FromArgb(5, 86, 147);
        }

        private void TeamSpeak_Button_MouseLeave(object sender, EventArgs e)
        {
            if (!bool_teamspeak)
                TeamSpeak_Button.BackColor = Color.FromArgb(26, 32, 40);

            bool_teamspeak = false;
        }
        #endregion
        #region Parameter Button
        private void Parameter_Button_Click(object sender, EventArgs e)
        {
            News_Panel.Visible = false;
            Addons_Panel.Visible = false;
            TeamSpeak_Panel.Visible = false;
            Parameter_Panel.Visible = true;
            /*Effet bouton*/
            News_Button.BackColor = Color.FromArgb(26, 32, 40);
            Addons_Button.BackColor = Color.FromArgb(26, 32, 40);
            Site_Button.BackColor = Color.FromArgb(26, 32, 40);
            TeamSpeak_Button.BackColor = Color.FromArgb(26, 32, 40);
            Parameter_Button.BackColor = Color.FromArgb(5, 86, 147);
            bool_Parameter = true;
        }

        private void Parameter_Button_MouseEnter(object sender, EventArgs e)
        {
            Parameter_Button.BackColor = Color.FromArgb(5, 86, 147);
        }

        private void Parameter_Button_MouseLeave(object sender, EventArgs e)
        {
            if (!bool_Parameter)
                Parameter_Button.BackColor = Color.FromArgb(26, 32, 40);

            bool_Parameter = false;
        }
        #endregion
        #region Social network
        #region Twitter Button
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
        #endregion
        #region Facebook Button
        private void Facebook_Button_Click(object sender, EventArgs e)
        {
            page_web.StartInfo.FileName = "https://www.facebook.com/MaishaRolePlay";
            page_web.Start();
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
        #endregion
        #endregion
        private void Menu_Button_Click(object sender, EventArgs e)
        {
            int rotate = 0;
            int counter = Menu_Button.Location.X;
            if (Menu_Panel.Width == 50)
            {

                for (int count = Menu_Panel.Width; count <= 194; count++)
                {
                    rotate++;
                    counter++;
                    Menu_Panel.Width = Menu_Panel.Width + 1;
                    News_Button.Visible = false;
                    Addons_Button.Visible = false;
                    Site_Button.Visible = false;
                    TeamSpeak_Button.Visible = false;
                    Logo_Picture.Visible = false;
                    Logo_Picture.Visible = false;
                    Parameter_Button.Visible = false;
                    Twitter_Button.Visible = false;
                    Facebook_Button.Visible = false;
                    if (counter <= 164)
                    {
                        Menu_Button.Location = new Point(counter, 4);

                    }
                    Menu_Button.Image = RotateImage(Properties.Resources.menu, 90);


                }
                News_Button.Location = new Point(0, 110);
                Addons_Button.Location = new Point(0, 161);
                Site_Button.Location = new Point(0, 212);
                TeamSpeak_Button.Location = new Point(0, 263);
                Parameter_Button.Location = new Point(0, 484);
                Twitter_Button.Location = new Point(53, 448);
                Menu_Button.Location = new Point(counter, 4);
                News_Button.Visible = true;
                Addons_Button.Visible = true;
                Site_Button.Visible = true;
                TeamSpeak_Button.Visible = true;
                Logo_Picture.Visible = true;
                Parameter_Button.Visible = true;
                Twitter_Button.Visible = true;
                Facebook_Button.Visible = true;
                //   Menu_Button.Location = new Point(164, 4);

            }
            else
            {
                for (int count = Menu_Panel.Width; count > 50; count--)
                {
                    rotate--;
                    counter--;
                    Menu_Panel.Width = Menu_Panel.Width - 1;
                    News_Button.Visible = false;
                    Addons_Button.Visible = false;
                    Site_Button.Visible = false;
                    TeamSpeak_Button.Visible = false;
                    Logo_Picture.Visible = false;
                    Logo_Picture.Visible = false;
                    Parameter_Button.Visible = false;
                    Twitter_Button.Visible = false;
                    Facebook_Button.Visible = false;
                    if (counter > 20)
                    {
                        Menu_Button.Location = new Point(counter, 4);
                    }

                    Menu_Button.Image = RotateImage(Properties.Resources.menu, 180);

                    Logo_Picture.Visible = false;
                }
                News_Button.Location = new Point(-30, 110);
                Addons_Button.Location = new Point(-30, 161);
                Site_Button.Location = new Point(-30, 212);
                TeamSpeak_Button.Location = new Point(-30, 263);
                Parameter_Button.Location = new Point(-30, 484);
                Twitter_Button.Location = new Point(12, 408);
                News_Button.Visible = true;
                Addons_Button.Visible = true;
                Site_Button.Visible = true;
                TeamSpeak_Button.Visible = true;
                Parameter_Button.Visible = true;
                Twitter_Button.Visible = true;
                Facebook_Button.Visible = true;
                Menu_Button.Location = new Point(20, 4);

            }

        }
        #endregion
        #region Parameter Panel
        private void flatButton1_Click(object sender, EventArgs e)
        {
            Install_taskforce.RunWorkerAsync();
        }

        private void Force_Update_Click(object sender, EventArgs e)
        {
            CheckMD5.RunWorkerAsync();
        }
        #endregion
        #endregion

        private void DownloadStart()
        {

            Progressbar_dll.Visible = true;
            Progressbar_Total.Visible = true;

            Download_Mod = new Thread((ThreadStart)(() =>
            {

                XmlDocument List_download = new XmlDocument();

                List_download.Load(appdata + "listdownload.xml");

                XmlNodeList listdownload = List_download.GetElementsByTagName("addons");
                flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.Text = "Téléchargement des mods en cours !"; }));

                foreach (XmlNode down in listdownload)
                {
                    if (down.Attributes[1].Value == "true")
                    {
                        count++;
                    }
                    if (down.Attributes[1].Value == "del")
                    {
                        count_sup++;
                    }
                }
                counter = unchecked((int)count);
                counte = counter.ToString();
                Multi_label.Invoke(new Action(() => { Multi_label.Text = "mods téléchargés: 0" + "/" + counte; }));
                if (counter != 0)
                {
                    Progressbar_Total.Maximum = counter;
                }
                WebClient downloadmod = new WebClient();
                downloadmod.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.downloadmod_DownloadProgressChanged);
                downloadmod.DownloadFileCompleted += new AsyncCompletedEventHandler(this.downloadmod_DownloadFileCompleted);
                CanDownload = true;
                foreach (XmlNode down in listdownload)
                {
                    if (down.Attributes[1].Value == "true")
                    {
                        sw.Start();
                        Progressbar_dll.Value = 0;
                        string mod = down.Attributes[0].Value;
                        downloadmod.DownloadFileAsync(new Uri(FTP + "addons//" + mod), directory + modfile + mod);
                        CanDownload = false;
                    }
                    test:
                    while (CanDownload == false)
                    {
                        goto test;

                    }
                }
                foreach (XmlNode down in listdownload)
                {
                    if (down.Attributes[1].Value == "del")
                    {
                        flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.Text = "Suppression des mods obseletes en cours"; }));

                        string mod = down.Attributes[0].Value;
                        File.Delete(directory + modfile + mod);
                        count_sup--;

                    }

                }
                Progressbar_Total.Maximum = 100;
                Progressbar_Total.Value = Progressbar_Total.Maximum;
                Progressbar_dll.Value = 100;
                Progressbar_dll.Visible = true;
                Progressbar_Total.Visible = true;
                flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.Text = "Mise à jours des mods terminé !"; }));
                Play_button.Invoke(new Action(() => { Play_button.Enabled = false; }));

                /*   if (launcharma)
                   {
                       Process.Start(this.directory + "/arma3battleye.exe", "-mod=@MaishaRP -nopause -connect=149.202.87.25");
                   }*/
            }));
            Download_Mod.Start();

        }

        private void downloadmod_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Vitesse_Download.Invoke(new Action(() => { Vitesse_Download.Text = string.Format("{0} kb/s", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00")); }));
            Progressbar_dll.Invoke(new Action(() => { Progressbar_dll.Value = e.ProgressPercentage; }));
            //  label1.Text = e.ProgressPercentage.ToString();
            // Progressbar_dll.Value = e.ProgressPercentage;
            //roue2.Value = e.ProgressPercentage;
        }

        private void downloadmod_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();
            CanDownload = true;
            counter2++;
            string counte_mods_dll = counter2.ToString();
            string counte2 = counter.ToString();
            Multi_label.Text = "mods téléchargés: " + counte_mods_dll + "/" + counte2;
            Play_button.Enabled = true;
            // Progressbar_Total.Invoke(new Action(() => { Progressbar_Total.Value = Progressbar_Total.Value + 1; }));
            Progressbar_Total.Value = Progressbar_Total.Value + 1;

        }

        private void Install_taskforce_DoWork(object sender, DoWorkEventArgs e)
        {
            WebClient taskforcedwn = new WebClient();
            flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.Text = "Téléchargement de Task Force Radio en cours !"; }));
            taskforcedwn.DownloadFile(serv + @"config/task_force_radio.ts3_plugin", appdata + "task_force_radio.ts3_plugin");
            flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.Text = "Installation de Task Force Radio en cours !"; }));
            Process.Start(appdata + "task_force_radio.ts3_plugin");
            flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.Text = ""; }));
        }

        private void Play_button_Click(object sender, EventArgs e)
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest("api/server/get", Method.POST);
            request.AddParameter("id", 64);

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            dynamic res = JObject.Parse(content.ToString());
            string serverip = res.ip;
            string serverport = res.port;
            string serverpwd = res.password;
            if (CheckBox_Start.Checked == false)
                Process.Start(this.directory + "/arma3battleye.exe", "-mod=@MaishaRP -nosplash -world=empty -connect=" + serverip + ":" + serverport + " -password=" + serverpwd);
            else
                Process.Start(this.directory + "/arma3launcher.exe", "-mod=@MaishaRP -nosplash -world=empty -connect=" + serverip + ":" + serverport + " -password=" + serverpwd);
            flatStatusBar_message.Text = "Lancement d'arma 3 en cours !";
        }

        #region GESTION NEWS

        private void timer_News_Tick(object sender, EventArgs e)
        {
            getnews();
        }

        private void getnews()
        {
            Get_news = new Thread((ThreadStart)(() =>
            {
                while (true)
                {
                    rssData = getRssData("http://maisha.fr/rss");
                    for (int i = 0; i < rssData.GetLength(0); i++)
                    {
                        if (rssData[i, 0] != null)
                        {
                            try
                            {

                                #region News 1
                                label_news_1.Invoke(new Action(() => { label_news_1.Text = rssData[0, 0]; }));

                                if (label_news_1.Text != "")
                                {
                                    label_news_1.Invoke(new Action(() => { label_news_1.Visible = true; }));
                                    textBox_news_1.Invoke(new Action(() => { textBox_news_1.Text = rssData[0, 1]; }));
                                    textBox_news_1.Invoke(new Action(() => { textBox_news_1.Visible = true; }));
                                    pictureBox_news_1.Invoke(new Action(() => { pictureBox_news_1.ImageLocation = rssData[0, 3]; }));
                                    pictureBox_news_1.Invoke(new Action(() => { pictureBox_news_1.Visible = true; }));
                                    label_news_1.Invoke(new Action(() => { label_news_1.Parent = this.pictureBox_news_1; }));
                                    label_news_1.Invoke(new Action(() => { label_news_1.Dock = DockStyle.Bottom; }));
                                    label_news_1.Invoke(new Action(() => { label_news_1.BackColor = Color.FromArgb(70, Color.Blue); }));
                                    label_news_1.Invoke(new Action(() => { label_news_1.ForeColor = Color.White; }));
                                }
                                else
                                {
                                    label_news_1.Invoke(new Action(() => { label_news_1.Visible = false; }));
                                    pictureBox_news_1.Invoke(new Action(() => { pictureBox_news_1.Visible = false; }));
                                    textBox_news_1.Invoke(new Action(() => { textBox_news_1.Visible = false; }));
                                }

                                #endregion
                                #region News 2
                                label_news_2.Invoke(new Action(() => { label_news_2.Text = rssData[1, 0]; }));

                                if (label_news_2.Text != "")
                                {
                                    label_news_2.Invoke(new Action(() => { label_news_2.Visible = true; }));
                                    textBox_news_2.Invoke(new Action(() => { textBox_news_2.Text = rssData[1, 1]; }));
                                    textBox_news_2.Invoke(new Action(() => { textBox_news_2.Visible = true; }));
                                    pictureBox_news_2.Invoke(new Action(() => { pictureBox_news_2.ImageLocation = rssData[1, 3]; }));
                                    pictureBox_news_2.Invoke(new Action(() => { pictureBox_news_2.Visible = true; }));
                                    label_news_2.Invoke(new Action(() => { label_news_2.Parent = this.pictureBox_news_2; }));
                                    label_news_2.Invoke(new Action(() => { label_news_2.Dock = DockStyle.Bottom; }));
                                    label_news_2.Invoke(new Action(() => { label_news_2.BackColor = Color.FromArgb(70, Color.Blue); }));
                                    label_news_2.Invoke(new Action(() => { label_news_2.ForeColor = Color.White; }));
                                }
                                else
                                {
                                    label_news_2.Invoke(new Action(() => { label_news_2.Visible = false; }));
                                    pictureBox_news_2.Invoke(new Action(() => { pictureBox_news_2.Visible = false; }));
                                    textBox_news_2.Invoke(new Action(() => { textBox_news_2.Visible = false; }));
                                }

                                #endregion
                                #region News 3
                                label_news_3.Invoke(new Action(() => { label_news_3.Text = rssData[2, 0]; }));

                                if (label_news_3.Text != "")
                                {
                                    label_news_3.Invoke(new Action(() => { label_news_3.Visible = true; }));
                                    pictureBox_news_3.Invoke(new Action(() => { pictureBox_news_3.ImageLocation = rssData[2, 3]; }));
                                    pictureBox_news_3.Invoke(new Action(() => { pictureBox_news_3.Visible = true; }));
                                    label_news_3.Invoke(new Action(() => { label_news_3.Parent = this.pictureBox_news_3; }));
                                    label_news_3.Invoke(new Action(() => { label_news_3.Dock = DockStyle.Bottom; }));
                                    label_news_3.Invoke(new Action(() => { label_news_3.BackColor = Color.FromArgb(70, Color.Blue); }));
                                    label_news_3.Invoke(new Action(() => { label_news_3.ForeColor = Color.White; }));
                                }
                                else
                                {
                                    label_news_3.Invoke(new Action(() => { label_news_3.Visible = false; }));
                                    pictureBox_news_3.Invoke(new Action(() => { pictureBox_news_3.Visible = false; }));
                                }
                                #endregion
                                #region News 4
                                label_news_4.Invoke(new Action(() => { label_news_4.Text = rssData[3, 0]; }));

                                if (label_news_4.Text != "")
                                {
                                    label_news_4.Invoke(new Action(() => { label_news_4.Visible = true; }));
                                    pictureBox_news_4.Invoke(new Action(() => { pictureBox_news_4.ImageLocation = rssData[3, 3]; }));
                                    pictureBox_news_4.Invoke(new Action(() => { pictureBox_news_4.Visible = true; }));
                                    label_news_4.Invoke(new Action(() => { label_news_4.Parent = this.pictureBox_news_4; }));
                                    label_news_4.Invoke(new Action(() => { label_news_4.Dock = DockStyle.Bottom; }));
                                    label_news_4.Invoke(new Action(() => { label_news_4.BackColor = Color.FromArgb(70, Color.Blue); }));
                                    label_news_4.Invoke(new Action(() => { label_news_4.ForeColor = Color.White; }));
                                }
                                else
                                {
                                    label_news_4.Invoke(new Action(() => { label_news_4.Visible = false; }));
                                    pictureBox_news_4.Invoke(new Action(() => { pictureBox_news_4.Visible = false; }));
                                }
                                #endregion
                            }
                            catch
                            {
                                #region News 1
                                label_news_1.Text = rssData[0, 0];

                                if (label_news_1.Text != "")
                                {
                                    textBox_news_1.Text = rssData[0, 1];
                                    pictureBox_news_1.ImageLocation = rssData[0, 3];
                                    label_news_1.Parent = this.pictureBox_news_1;
                                    label_news_1.Dock = DockStyle.Bottom;
                                    label_news_1.BackColor = Color.FromArgb(70, Color.Blue);
                                    label_news_1.ForeColor = Color.White;
                                }
                                else
                                {
                                    label_news_1.Visible = false;
                                    pictureBox_news_1.Visible = false;
                                    textBox_news_1.Visible = false;
                                }

                                #endregion
                                #region News 2
                                label_news_2.Text = rssData[1, 0];

                                if (label_news_2.Text != "")
                                {
                                    textBox_news_2.Text = rssData[1, 1];
                                    pictureBox_news_2.ImageLocation = rssData[1, 3];
                                    label_news_2.Parent = this.pictureBox_news_2;
                                    label_news_2.Dock = DockStyle.Bottom;
                                    label_news_2.BackColor = Color.FromArgb(70, Color.Blue);
                                    label_news_2.ForeColor = Color.White;
                                }
                                else
                                {
                                    label_news_2.Visible = false;
                                    pictureBox_news_2.Visible = false;
                                    textBox_news_2.Visible = false;
                                }

                                #endregion
                                #region News 3
                                label_news_3.Text = rssData[2, 0];

                                if (label_news_3.Text != "")
                                {
                                    pictureBox_news_3.ImageLocation = rssData[2, 3];
                                    label_news_3.Parent = this.pictureBox_news_3;
                                    label_news_3.Dock = DockStyle.Bottom;
                                    label_news_3.BackColor = Color.FromArgb(70, Color.Blue);
                                    label_news_3.ForeColor = Color.White;
                                }
                                else
                                {
                                    label_news_3.Visible = false;
                                    pictureBox_news_3.Visible = false;
                                }
                                #endregion
                                #region News 4
                                label_news_4.Text = rssData[3, 0];

                                if (label_news_4.Text != "")
                                {
                                    pictureBox_news_4.ImageLocation = rssData[3, 3];
                                    label_news_4.Parent = this.pictureBox_news_4;
                                    label_news_4.Dock = DockStyle.Bottom;
                                    label_news_4.BackColor = Color.FromArgb(70, Color.Blue);
                                    label_news_4.ForeColor = Color.White;
                                }
                                else
                                {
                                    label_news_4.Visible = false;
                                    pictureBox_news_4.Visible = false;
                                }
                                #endregion
                            }

                        }
                    }
                }
            }));
            Get_news.Start();
        }

        private string[,] getRssData(string channel)
        {
            System.Net.WebRequest myRequest = System.Net.WebRequest.Create(channel);
            System.Net.WebResponse myResponse = myRequest.GetResponse();

            System.IO.Stream rssStream = myResponse.GetResponseStream();
            System.Xml.XmlDocument rssDoc = new System.Xml.XmlDocument();

            rssDoc.Load(rssStream);
            System.Xml.XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");

            string[,] tempRssData = new string[100, 4];

            for (int i = 0; i < rssItems.Count; i++)
            {
                System.Xml.XmlNode rssNode;
                rssNode = rssItems.Item(i).SelectSingleNode("title");
                if (rssNode != null)
                {
                    tempRssData[i, 0] = rssNode.InnerText;
                }
                else
                {
                    tempRssData[i, 0] = "";
                }

                rssNode = rssItems.Item(i).SelectSingleNode("description");
                if (rssNode != null)
                {
                    tempRssData[i, 1] = rssNode.InnerText;
                }
                else
                {
                    tempRssData[i, 1] = "";
                }

                rssNode = rssItems.Item(i).SelectSingleNode("link");
                if (rssNode != null)
                {
                    tempRssData[i, 2] = rssNode.InnerText;
                }
                else
                {
                    tempRssData[i, 2] = "";
                }
                rssNode = rssItems.Item(i).SelectSingleNode("image");
                if (rssNode != null)
                {
                    tempRssData[i, 3] = rssNode.InnerText;
                }
                else
                {
                    tempRssData[i, 3] = "";
                }
            }
            return tempRssData;
        }

        private void pictureBox_news_1_Click(object sender, EventArgs e)
        {
            Process.Start(rssData[0, 2]);
        }

        private void pictureBox_news_2_Click(object sender, EventArgs e)
        {
            Process.Start(rssData[1, 2]);
        }

        private void pictureBox_news_3_Click(object sender, EventArgs e)
        {
            Process.Start(rssData[2, 2]);
        }

        private void pictureBox_news_4_Click(object sender, EventArgs e)
        {
            Process.Start(rssData[3, 2]);
        }
        #endregion

        public void background()
        {
            backgroundlauncher = new Thread((ThreadStart)(() =>
            {
                try { if (UserData.IsAlive) { UserData.Abort(); } } catch { }
                try { if (StatusServ.IsAlive) { StatusServ.Abort(); } } catch { }
                try { if (Download_Mod.IsAlive) { Download_Mod.Abort(); } } catch { }
                try { if (Get_news.IsAlive) { Get_news.Abort(); } } catch { }
                try { if (timer_News.Enabled) { timer_News.Stop(); } } catch { }
                try { if (timer_UserData.Enabled) { timer_UserData.Stop(); } } catch { }
                this.Close();

                System.Threading.Thread.Sleep(10000);

                this.Show();
                this.launchInit();
            }));
            backgroundlauncher.Start();

        }
        private void timer_UserData_Tick(object sender, EventArgs e)
        {
            InitUserData();
        }

        private void CheckMD5_DoWork(object sender, DoWorkEventArgs e)
        {
            progress_indicator.Invoke(new Action(() => { progress_indicator.Visible = true; }));

            flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.Text = "Vérification des mods en cours ...."; }));
            Play_button.Invoke(new Action(() => { Play_button.Enabled = false; }));
            if (File.Exists(appdata + "listserver.xml"))
                File.Delete(appdata + "listserver.xml");
            if (File.Exists(appdata + "listclient.xml"))
                File.Delete(appdata + "listclient.xml");

            WebClient Downloadlist = new WebClient();
            Downloadlist.DownloadFile(serv + @"config/listserver.xml", appdata + "listserver.xml");
            System.IO.FileStream filed = System.IO.File.Create(appdata + "listclient.xml");

            XmlTextWriter xmltextwriter = new XmlTextWriter(filed, System.Text.Encoding.UTF8);

            xmltextwriter.Formatting = Formatting.Indented;

            xmltextwriter.WriteStartDocument(false);
            xmltextwriter.WriteStartElement("HASH");
            using (var md5 = MD5.Create())
            {
                var files = from filee in Directory.EnumerateFiles(directory + modfile, "*", SearchOption.TopDirectoryOnly)
                            select new
                            {
                                File = filee,
                                Files = filee,
                            };

                foreach (var f in files)
                {
                    using (var stream = File.OpenRead(f.File))
                    {
                        string Files = f.Files.Replace(directory + modfile, "");
                        string lines = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                        xmltextwriter.WriteStartElement("addons");
                        xmltextwriter.WriteAttributeString("Nom", Files);
                        xmltextwriter.WriteAttributeString("Hash", lines);
                        xmltextwriter.WriteEndElement(); ;
                        Multi_label.Invoke(new Action(() => { Multi_label.Text = lines; }));
                    }
                }
                xmltextwriter.WriteEndElement();

                xmltextwriter.Flush();

                xmltextwriter.Close();
                flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.Text = "Vérification des mods 50%"; }));
            }

            System.IO.FileStream file = System.IO.File.Create(appdata + "listdownload.xml");

            XmlTextWriter myXmlTextWriter = new XmlTextWriter(file, System.Text.Encoding.UTF8);

            myXmlTextWriter.Formatting = Formatting.Indented;

            myXmlTextWriter.WriteStartDocument(false);

            myXmlTextWriter.WriteStartElement("downloader");

            XmlDocument hashclient = new XmlDocument();

            hashclient.Load(appdata + "listclient.xml");

            XmlNodeList lstclient = hashclient.GetElementsByTagName("addons");

            XmlDocument hashserver = new XmlDocument();

            var nodees = new List<XmlNode>(lstclient.Cast<XmlNode>());

            hashserver.Load(appdata + "listserver.xml");

            XmlNodeList lst = hashserver.GetElementsByTagName("addons");
            if (hashclient.SelectSingleNode("//addons") == null)
            {
                foreach (XmlNode p in lst)
                {
                    myXmlTextWriter.WriteStartElement("addons");

                    myXmlTextWriter.WriteAttributeString("addons", p.Attributes[0].Value);

                    myXmlTextWriter.WriteAttributeString("download", "true");

                    myXmlTextWriter.WriteEndElement();
                }
            }
            foreach (XmlNode n in lst)
            {
                foreach (XmlNode b in lstclient)
                {

                    if (n.Attributes[0].Value == b.Attributes[0].Value)
                    {
                        if (n.Attributes[1].Value == b.Attributes[1].Value)
                        {
                            myXmlTextWriter.WriteStartElement("addons");

                            myXmlTextWriter.WriteAttributeString("addons", n.Attributes[0].Value);

                            myXmlTextWriter.WriteAttributeString("download", "false");

                            myXmlTextWriter.WriteEndElement();
                        }
                        else
                        {
                            myXmlTextWriter.WriteStartElement("addons");

                            myXmlTextWriter.WriteAttributeString("addons", n.Attributes[0].Value);

                            myXmlTextWriter.WriteAttributeString("download", "true");

                            myXmlTextWriter.WriteEndElement();
                        }

                        break;
                    }

                }
            }

            foreach (XmlNode h in lst)
            {
                foreach (XmlNode g in lstclient)
                {
                    if (g.Attributes[0] == null)
                    {
                        myXmlTextWriter.WriteStartElement("addons");

                        myXmlTextWriter.WriteAttributeString("addons", h.Attributes[0].Value);

                        myXmlTextWriter.WriteAttributeString("download", "true");

                        myXmlTextWriter.WriteEndElement();
                    }
                }
            }
            bool del = false;

            foreach (XmlNode v in lstclient)
            {

                foreach (XmlNode d in lst)
                {
                    del = false;

                    if (v.Attributes[0].Value == d.Attributes[0].Value)
                        break;

                    else
                        del = true;

                }
                if (del)
                {
                    myXmlTextWriter.WriteStartElement("addons");

                    myXmlTextWriter.WriteAttributeString("addons", v.Attributes[0].Value);

                    myXmlTextWriter.WriteAttributeString("download", "del");

                    myXmlTextWriter.WriteEndElement();
                }
            }
            foreach (XmlNode v in lst)
            {

                foreach (XmlNode d in lstclient)
                {
                    del = false;

                    if (v.Attributes[0].Value == d.Attributes[0].Value)
                        break;

                    else
                        del = true;

                }
                if (del)
                {
                    myXmlTextWriter.WriteStartElement("addons");

                    myXmlTextWriter.WriteAttributeString("addons", v.Attributes[0].Value);

                    myXmlTextWriter.WriteAttributeString("download", "true");

                    myXmlTextWriter.WriteEndElement();
                }
            }

            myXmlTextWriter.WriteEndElement();

            myXmlTextWriter.Flush();

            myXmlTextWriter.Close();

            XmlDocument List_download2 = new XmlDocument();

            List_download2.Load(appdata + "listdownload.xml");

            XmlNodeList listdownload = List_download2.GetElementsByTagName("addons");
            foreach (XmlNode down in listdownload)
            {
                if (down.Attributes[1].Value == "true" || down.Attributes[1].Value == "del")
                {
                    counter_check++;
                }
            }

        }

        private void CheckMD5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            flatStatusBar_message.Invoke(new Action(() => { flatStatusBar_message.Text = "Vérifcation des mods terminé !"; }));
            progress_indicator.Invoke(new Action(() => { progress_indicator.Visible = false; }));
            progress_indicator.Visible = false;
            if (counter_check == 0)
            {
                Progressbar_Total.Maximum = 100;
                Progressbar_Total.Value = Progressbar_Total.Maximum;
                Progressbar_dll.Value = 100;
                Play_button.Enabled = true;
                Multi_label.Text = "";
            }
            else
            {
                DownloadStart();
                Download_cpp.RunWorkerAsync();
            }
            CheckMD5.Dispose();
        }

        private void Download_cpp_DoWork(object sender, DoWorkEventArgs e)
        {
            while (Wait == true)
            { }
            WebClient download2 = new WebClient();
            if (!File.Exists(directory + @"/@MaishaRP/mod.cpp"))
            {
                download2.DownloadFile(FTP + @"/cpp/mod.cpp", directory + @"/@MaishaRP/mod.cpp");
            }
            if (!File.Exists(directory + @"/@MaishaRP/task_force_radio_pipe.dll"))
            {
                download2.DownloadFile(FTP + @"/cpp/task_force_radio_pipe.dll", directory + @"/@MaishaRP/task_force_radio_pipe.dll");
            }
            if (!File.Exists(directory + @"/@MaishaRP/MaishaRP.paa"))
            {
                download2.DownloadFile(FTP + @"/cpp/MaishaRP_Logo.paa", directory + @"/@MaishaRP/MaishaRP_Logo.paa");
            }
        }

        public static Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }
    }
}
