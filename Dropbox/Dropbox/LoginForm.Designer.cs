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
            this.parola.Location = new System.Drawing.Point(260, 217);
            this.parola.Name = "parola";
            this.parola.Size = new System.Drawing.Size(195, 20);
            this.parola.TabIndex = 0;
            // 
            // numeUtilizator
            // 
            this.numeUtilizator.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.numeUtilizator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numeUtilizator.Location = new System.Drawing.Point(260, 150);
            this.numeUtilizator.Name = "numeUtilizator";
            this.numeUtilizator.Size = new System.Drawing.Size(195, 20);
            this.numeUtilizator.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(316, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nume Utilizator";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(333, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Parolă";
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(309, 281);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(105, 53);
            this.login.TabIndex = 4;
            this.login.Text = "Logare";
            this.login.UseVisualStyleBackColor = true;
            this.login.Click += new System.EventHandler(this.login_Click);
            // 
            // register
            // 
            this.register.AutoSize = true;
            this.register.Location = new System.Drawing.Point(257, 354);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(226, 13);
            this.register.TabIndex = 5;
            this.register.Text = "Nu aveți un cont? Click aici pentru înregistrare";
            this.register.Click += new System.EventHandler(this.register_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.register);
            this.Controls.Add(this.login);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numeUtilizator);
            this.Controls.Add(this.parola);
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