namespace Maisha_Launcher
{
    partial class Login
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_copyright = new System.Windows.Forms.Label();
            this.minimize_button = new System.Windows.Forms.PictureBox();
            this.Close_Button = new System.Windows.Forms.PictureBox();
            this.flatStatusBar1 = new theme.FlatStatusBar();
            this.pwd_TextBox = new System.Windows.Forms.TextBox();
            this.Check_Connect = new System.Windows.Forms.CheckBox();
            this.Connect_Button = new theme.FlatButton();
            this.flatLabel1 = new theme.FlatLabel();
            this.create_account = new System.Windows.Forms.LinkLabel();
            this.pass_forgot = new System.Windows.Forms.LinkLabel();
            this.Login_Textbox = new System.Windows.Forms.TextBox();
            this.label_Message = new System.Windows.Forms.Label();
            this.Twitter_Button = new System.Windows.Forms.PictureBox();
            this.Facebook_Button = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkmaint = new System.ComponentModel.BackgroundWorker();
            this.iTalk_Panel1 = new iTalk.iTalk_Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minimize_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Close_Button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Twitter_Button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Facebook_Button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(86)))), ((int)(((byte)(147)))));
            this.panel1.Controls.Add(this.label_copyright);
            this.panel1.Controls.Add(this.minimize_button);
            this.panel1.Controls.Add(this.Close_Button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 24);
            this.panel1.TabIndex = 7;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // label_copyright
            // 
            this.label_copyright.AutoSize = true;
            this.label_copyright.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_copyright.ForeColor = System.Drawing.Color.White;
            this.label_copyright.Location = new System.Drawing.Point(5, 3);
            this.label_copyright.Name = "label_copyright";
            this.label_copyright.Size = new System.Drawing.Size(0, 18);
            this.label_copyright.TabIndex = 83;
            // 
            // minimize_button
            // 
            this.minimize_button.Image = global::Maisha_Launcher.Properties.Resources.minimize_white;
            this.minimize_button.Location = new System.Drawing.Point(649, 2);
            this.minimize_button.Name = "minimize_button";
            this.minimize_button.Size = new System.Drawing.Size(20, 20);
            this.minimize_button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.minimize_button.TabIndex = 26;
            this.minimize_button.TabStop = false;
            this.minimize_button.Click += new System.EventHandler(this.minimize_button_Click);
            this.minimize_button.MouseEnter += new System.EventHandler(this.minimize_button_MouseEnter);
            this.minimize_button.MouseLeave += new System.EventHandler(this.minimize_button_MouseLeave);
            // 
            // Close_Button
            // 
            this.Close_Button.Image = global::Maisha_Launcher.Properties.Resources.Close_white;
            this.Close_Button.Location = new System.Drawing.Point(679, 2);
            this.Close_Button.Name = "Close_Button";
            this.Close_Button.Size = new System.Drawing.Size(20, 20);
            this.Close_Button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Close_Button.TabIndex = 25;
            this.Close_Button.TabStop = false;
            this.Close_Button.Click += new System.EventHandler(this.Close_Button_Click);
            this.Close_Button.MouseEnter += new System.EventHandler(this.Close_Button_MouseEnter);
            this.Close_Button.MouseLeave += new System.EventHandler(this.Close_Button_MouseLeave);
            // 
            // flatStatusBar1
            // 
            this.flatStatusBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(86)))), ((int)(((byte)(147)))));
            this.flatStatusBar1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(86)))), ((int)(((byte)(147)))));
            this.flatStatusBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flatStatusBar1.Font = new System.Drawing.Font("Baskerville Old Face", 9.75F);
            this.flatStatusBar1.ForeColor = System.Drawing.Color.White;
            this.flatStatusBar1.Location = new System.Drawing.Point(0, 379);
            this.flatStatusBar1.Name = "flatStatusBar1";
            this.flatStatusBar1.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.flatStatusBar1.ShowTimeDate = false;
            this.flatStatusBar1.Size = new System.Drawing.Size(704, 23);
            this.flatStatusBar1.TabIndex = 9;
            this.flatStatusBar1.TextColor = System.Drawing.Color.White;
            // 
            // pwd_TextBox
            // 
            this.pwd_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pwd_TextBox.Font = new System.Drawing.Font("Baskerville Old Face", 9.75F);
            this.pwd_TextBox.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.pwd_TextBox.Location = new System.Drawing.Point(505, 120);
            this.pwd_TextBox.MaxLength = 99;
            this.pwd_TextBox.Name = "pwd_TextBox";
            this.pwd_TextBox.Size = new System.Drawing.Size(140, 15);
            this.pwd_TextBox.TabIndex = 3;
            this.pwd_TextBox.Click += new System.EventHandler(this.pwd_TextBox_Click);
            this.pwd_TextBox.Enter += new System.EventHandler(this.pwd_TextBox_Enter);
            this.pwd_TextBox.Leave += new System.EventHandler(this.pwd_TextBox_Leave);
            // 
            // Check_Connect
            // 
            this.Check_Connect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.Check_Connect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Check_Connect.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Check_Connect.FlatAppearance.BorderSize = 5;
            this.Check_Connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Check_Connect.Font = new System.Drawing.Font("Berlin Sans FB", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Check_Connect.ForeColor = System.Drawing.Color.White;
            this.Check_Connect.Location = new System.Drawing.Point(528, 153);
            this.Check_Connect.Name = "Check_Connect";
            this.Check_Connect.Size = new System.Drawing.Size(97, 24);
            this.Check_Connect.TabIndex = 4;
            this.Check_Connect.Text = "Se souvenir";
            this.Check_Connect.UseVisualStyleBackColor = false;
            this.Check_Connect.CheckedChanged += new System.EventHandler(this.Check_Connect_CheckedChanged);
            // 
            // Connect_Button
            // 
            this.Connect_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.Connect_Button.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(86)))), ((int)(((byte)(147)))));
            this.Connect_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Connect_Button.Font = new System.Drawing.Font("Baskerville Old Face", 12F);
            this.Connect_Button.Location = new System.Drawing.Point(505, 246);
            this.Connect_Button.Name = "Connect_Button";
            this.Connect_Button.Rounded = false;
            this.Connect_Button.Size = new System.Drawing.Size(140, 36);
            this.Connect_Button.TabIndex = 5;
            this.Connect_Button.Text = "Connexion";
            this.Connect_Button.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Connect_Button.Click += new System.EventHandler(this.Connect_Button_Click);
            // 
            // flatLabel1
            // 
            this.flatLabel1.AutoSize = true;
            this.flatLabel1.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel1.Font = new System.Drawing.Font("Baskerville Old Face", 9F);
            this.flatLabel1.ForeColor = System.Drawing.Color.White;
            this.flatLabel1.Location = new System.Drawing.Point(531, 303);
            this.flatLabel1.Name = "flatLabel1";
            this.flatLabel1.Size = new System.Drawing.Size(84, 14);
            this.flatLabel1.TabIndex = 21;
            this.flatLabel1.Text = "Pas de compte ?";
            this.flatLabel1.Click += new System.EventHandler(this.flatLabel1_Click);
            // 
            // create_account
            // 
            this.create_account.AutoSize = true;
            this.create_account.Font = new System.Drawing.Font("Baskerville Old Face", 9F);
            this.create_account.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.create_account.Location = new System.Drawing.Point(551, 321);
            this.create_account.Name = "create_account";
            this.create_account.Size = new System.Drawing.Size(49, 14);
            this.create_account.TabIndex = 7;
            this.create_account.TabStop = true;
            this.create_account.Text = "S\'inscrire";
            this.create_account.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.create_account.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.create_account_LinkClicked);
            // 
            // pass_forgot
            // 
            this.pass_forgot.AutoSize = true;
            this.pass_forgot.Font = new System.Drawing.Font("Baskerville Old Face", 9F);
            this.pass_forgot.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pass_forgot.Location = new System.Drawing.Point(525, 194);
            this.pass_forgot.Name = "pass_forgot";
            this.pass_forgot.Size = new System.Drawing.Size(104, 14);
            this.pass_forgot.TabIndex = 6;
            this.pass_forgot.TabStop = true;
            this.pass_forgot.Text = "Mot de passe oublier";
            this.pass_forgot.VisitedLinkColor = System.Drawing.Color.Blue;
            this.pass_forgot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.pass_forgot_LinkClicked);
            // 
            // Login_Textbox
            // 
            this.Login_Textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Login_Textbox.Font = new System.Drawing.Font("Baskerville Old Face", 9.75F);
            this.Login_Textbox.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.Login_Textbox.Location = new System.Drawing.Point(505, 78);
            this.Login_Textbox.MaxLength = 99;
            this.Login_Textbox.Name = "Login_Textbox";
            this.Login_Textbox.Size = new System.Drawing.Size(140, 15);
            this.Login_Textbox.TabIndex = 2;
            this.Login_Textbox.Click += new System.EventHandler(this.Login_Textbox_Click);
            this.Login_Textbox.Enter += new System.EventHandler(this.Login_Textbox_Enter);
            this.Login_Textbox.Leave += new System.EventHandler(this.Login_Textbox_Leave);
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.ForeColor = System.Drawing.Color.White;
            this.label_Message.Location = new System.Drawing.Point(502, 138);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(0, 13);
            this.label_Message.TabIndex = 28;
            // 
            // Twitter_Button
            // 
            this.Twitter_Button.Image = ((System.Drawing.Image)(resources.GetObject("Twitter_Button.Image")));
            this.Twitter_Button.Location = new System.Drawing.Point(63, 325);
            this.Twitter_Button.Name = "Twitter_Button";
            this.Twitter_Button.Size = new System.Drawing.Size(30, 30);
            this.Twitter_Button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Twitter_Button.TabIndex = 26;
            this.Twitter_Button.TabStop = false;
            this.Twitter_Button.Click += new System.EventHandler(this.Twitter_Button_Click);
            this.Twitter_Button.MouseEnter += new System.EventHandler(this.Twitter_Button_MouseEnter);
            this.Twitter_Button.MouseLeave += new System.EventHandler(this.Twitter_Button_MouseLeave);
            // 
            // Facebook_Button
            // 
            this.Facebook_Button.Image = ((System.Drawing.Image)(resources.GetObject("Facebook_Button.Image")));
            this.Facebook_Button.Location = new System.Drawing.Point(22, 325);
            this.Facebook_Button.Name = "Facebook_Button";
            this.Facebook_Button.Size = new System.Drawing.Size(30, 30);
            this.Facebook_Button.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Facebook_Button.TabIndex = 25;
            this.Facebook_Button.TabStop = false;
            this.Facebook_Button.Click += new System.EventHandler(this.Facebook_Button_Click);
            this.Facebook_Button.MouseEnter += new System.EventHandler(this.Facebook_Button_MouseEnter);
            this.Facebook_Button.MouseLeave += new System.EventHandler(this.Facebook_Button_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Maisha_Launcher.Properties.Resources.Img_Form1;
            this.pictureBox1.Location = new System.Drawing.Point(22, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(379, 220);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // checkmaint
            // 
            this.checkmaint.DoWork += new System.ComponentModel.DoWorkEventHandler(this.checkmaint_DoWork);
            this.checkmaint.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.checkmaint_RunWorkerCompleted);
            // 
            // iTalk_Panel1
            // 
            this.iTalk_Panel1.BackColor = System.Drawing.Color.Transparent;
            this.iTalk_Panel1.Location = new System.Drawing.Point(429, 30);
            this.iTalk_Panel1.Name = "iTalk_Panel1";
            this.iTalk_Panel1.Padding = new System.Windows.Forms.Padding(5);
            this.iTalk_Panel1.Size = new System.Drawing.Size(1, 343);
            this.iTalk_Panel1.TabIndex = 20;
            this.iTalk_Panel1.Text = "iTalk_Panel1";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.ClientSize = new System.Drawing.Size(704, 402);
            this.Controls.Add(this.label_Message);
            this.Controls.Add(this.Twitter_Button);
            this.Controls.Add(this.Facebook_Button);
            this.Controls.Add(this.Login_Textbox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pass_forgot);
            this.Controls.Add(this.create_account);
            this.Controls.Add(this.flatLabel1);
            this.Controls.Add(this.iTalk_Panel1);
            this.Controls.Add(this.Connect_Button);
            this.Controls.Add(this.Check_Connect);
            this.Controls.Add(this.pwd_TextBox);
            this.Controls.Add(this.flatStatusBar1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(126, 39);
            this.Name = "Login";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minimize_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Close_Button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Twitter_Button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Facebook_Button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private theme.FlatStatusBar flatStatusBar1;
        private System.Windows.Forms.TextBox pwd_TextBox;
        private System.Windows.Forms.CheckBox Check_Connect;
        private theme.FlatButton Connect_Button;
        private iTalk.iTalk_Panel iTalk_Panel1;
        private theme.FlatLabel flatLabel1;
        private System.Windows.Forms.LinkLabel create_account;
        private System.Windows.Forms.LinkLabel pass_forgot;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox Login_Textbox;
        private System.Windows.Forms.PictureBox Close_Button;
        private System.Windows.Forms.PictureBox Facebook_Button;
        private System.Windows.Forms.PictureBox Twitter_Button;
        private System.Windows.Forms.PictureBox minimize_button;
        private System.Windows.Forms.Label label_Message;
        private System.ComponentModel.BackgroundWorker checkmaint;
        private System.Windows.Forms.Label label_copyright;
    }
}

