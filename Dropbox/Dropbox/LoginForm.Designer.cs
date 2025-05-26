namespace Dropbox
{
    partial class LoginForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.parola = new System.Windows.Forms.TextBox();
            this.numeUtilizator = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.login = new System.Windows.Forms.Button();
            this.register = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // parola
            // 
            this.parola.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.parola.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.parola.Location = new System.Drawing.Point(347, 267);
            this.parola.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.parola.Name = "parola";
            this.parola.PasswordChar = '●';
            this.parola.Size = new System.Drawing.Size(259, 22);
            this.parola.TabIndex = 0;
            // 
            // numeUtilizator
            // 
            this.numeUtilizator.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.numeUtilizator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numeUtilizator.Location = new System.Drawing.Point(347, 185);
            this.numeUtilizator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numeUtilizator.Name = "numeUtilizator";
            this.numeUtilizator.Size = new System.Drawing.Size(259, 22);
            this.numeUtilizator.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(421, 165);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nume Utilizator";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(444, 247);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Parolă";
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(412, 346);
            this.login.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(140, 65);
            this.login.TabIndex = 4;
            this.login.Text = "Logare";
            this.login.UseVisualStyleBackColor = true;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // register
            // 
            this.register.AutoSize = true;
            this.register.Location = new System.Drawing.Point(343, 436);
            this.register.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(274, 16);
            this.register.TabIndex = 5;
            this.register.Text = "Nu aveți un cont? Click aici pentru înregistrare";
            this.register.Click += new System.EventHandler(this.register_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.register);
            this.Controls.Add(this.login);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numeUtilizator);
            this.Controls.Add(this.parola);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox parola;
        private System.Windows.Forms.TextBox numeUtilizator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button login;
        private System.Windows.Forms.Label register;
    }
}