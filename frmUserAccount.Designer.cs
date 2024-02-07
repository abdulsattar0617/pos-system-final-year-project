
namespace POS_System
{
    partial class frmUserAccount
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbShowPassCreateAcc = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRetype = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbShowPassword = new System.Windows.Forms.CheckBox();
            this.cbChangeName = new System.Windows.Forms.CheckBox();
            this.txtRetypeNewPassChangePass = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancelChangePass = new System.Windows.Forms.Button();
            this.btnSaveChangePass = new System.Windows.Forms.Button();
            this.txtNameChangePass = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNewPassChangePass = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOldPassChangePass = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtUserChangePass = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtUserSearch = new MetroFramework.Controls.MetroTextBox();
            this.dataGridViewUserList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACTIVE = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtUserADA = new MetroFramework.Controls.MetroTextBox();
            this.btnDeactivate = new System.Windows.Forms.Button();
            this.btnActivate = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.metroTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserList)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 40);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::POS_System.Properties.Resources.remove__1_;
            this.pictureBox1.Location = new System.Drawing.Point(954, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "USER ACCOUNT";
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.tabPage1);
            this.metroTabControl1.Controls.Add(this.tabPage2);
            this.metroTabControl1.Controls.Add(this.tabPage3);
            this.metroTabControl1.Location = new System.Drawing.Point(178, 150);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(633, 330);
            this.metroTabControl1.TabIndex = 4;
            this.metroTabControl1.UseSelectable = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(625, 288);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "CREATE ACCOUNT";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.cbShowPassCreateAcc);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.txtName);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.cboRole);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtRetype);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtPass);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtUser);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(625, 288);
            this.panel2.TabIndex = 0;
            // 
            // cbShowPassCreateAcc
            // 
            this.cbShowPassCreateAcc.AutoSize = true;
            this.cbShowPassCreateAcc.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbShowPassCreateAcc.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowPassCreateAcc.Location = new System.Drawing.Point(459, 39);
            this.cbShowPassCreateAcc.Name = "cbShowPassCreateAcc";
            this.cbShowPassCreateAcc.Size = new System.Drawing.Size(120, 17);
            this.cbShowPassCreateAcc.TabIndex = 74;
            this.cbShowPassCreateAcc.Text = "SHOW PASSWORD";
            this.cbShowPassCreateAcc.UseVisualStyleBackColor = true;
            this.cbShowPassCreateAcc.CheckedChanged += new System.EventHandler(this.cbShowPassCreateAcc_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(512, 217);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 32);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "CANCEL";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(435, 217);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(67, 32);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(223, 186);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(356, 25);
            this.txtName.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "NAME";
            // 
            // cboRole
            // 
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Items.AddRange(new object[] {
            "System Administrator",
            "Cashier"});
            this.cboRole.Location = new System.Drawing.Point(223, 155);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(356, 25);
            this.cboRole.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "ROLE";
            // 
            // txtRetype
            // 
            this.txtRetype.Location = new System.Drawing.Point(223, 124);
            this.txtRetype.Name = "txtRetype";
            this.txtRetype.PasswordChar = '•';
            this.txtRetype.Size = new System.Drawing.Size(356, 25);
            this.txtRetype.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "CONFIRM PASSWORD";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(223, 93);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '•';
            this.txtPass.Size = new System.Drawing.Size(356, 25);
            this.txtPass.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "PASSWORD";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(223, 62);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(356, 25);
            this.txtUser.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "USERNAME";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(625, 288);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CHANGE PASSWORD";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.cbShowPassword);
            this.panel3.Controls.Add(this.cbChangeName);
            this.panel3.Controls.Add(this.txtRetypeNewPassChangePass);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.btnCancelChangePass);
            this.panel3.Controls.Add(this.btnSaveChangePass);
            this.panel3.Controls.Add(this.txtNameChangePass);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.txtNewPassChangePass);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.txtOldPassChangePass);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.txtUserChangePass);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(625, 288);
            this.panel3.TabIndex = 1;
            // 
            // cbShowPassword
            // 
            this.cbShowPassword.AutoSize = true;
            this.cbShowPassword.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbShowPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbShowPassword.Location = new System.Drawing.Point(459, 39);
            this.cbShowPassword.Name = "cbShowPassword";
            this.cbShowPassword.Size = new System.Drawing.Size(120, 17);
            this.cbShowPassword.TabIndex = 73;
            this.cbShowPassword.Text = "SHOW PASSWORD";
            this.cbShowPassword.UseVisualStyleBackColor = true;
            this.cbShowPassword.CheckedChanged += new System.EventHandler(this.cbShowPassword_CheckedChanged);
            // 
            // cbChangeName
            // 
            this.cbChangeName.AutoSize = true;
            this.cbChangeName.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbChangeName.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbChangeName.Location = new System.Drawing.Point(349, 39);
            this.cbChangeName.Name = "cbChangeName";
            this.cbChangeName.Size = new System.Drawing.Size(104, 17);
            this.cbChangeName.TabIndex = 72;
            this.cbChangeName.Text = "CHANGE NAME";
            this.cbChangeName.UseVisualStyleBackColor = true;
            this.cbChangeName.CheckedChanged += new System.EventHandler(this.cbChangeName_CheckedChanged);
            // 
            // txtRetypeNewPassChangePass
            // 
            this.txtRetypeNewPassChangePass.Location = new System.Drawing.Point(223, 155);
            this.txtRetypeNewPassChangePass.Name = "txtRetypeNewPassChangePass";
            this.txtRetypeNewPassChangePass.PasswordChar = '•';
            this.txtRetypeNewPassChangePass.Size = new System.Drawing.Size(356, 25);
            this.txtRetypeNewPassChangePass.TabIndex = 71;
            this.txtRetypeNewPassChangePass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRetypeNewPassChangePass_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(45, 158);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(169, 17);
            this.label8.TabIndex = 70;
            this.label8.Text = "CONFIRM NEW PASSWORD";
            // 
            // btnCancelChangePass
            // 
            this.btnCancelChangePass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnCancelChangePass.FlatAppearance.BorderSize = 0;
            this.btnCancelChangePass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelChangePass.ForeColor = System.Drawing.Color.White;
            this.btnCancelChangePass.Location = new System.Drawing.Point(512, 217);
            this.btnCancelChangePass.Name = "btnCancelChangePass";
            this.btnCancelChangePass.Size = new System.Drawing.Size(67, 32);
            this.btnCancelChangePass.TabIndex = 69;
            this.btnCancelChangePass.Text = "CANCEL";
            this.btnCancelChangePass.UseVisualStyleBackColor = false;
            this.btnCancelChangePass.Click += new System.EventHandler(this.btnCancelChangePass_Click);
            // 
            // btnSaveChangePass
            // 
            this.btnSaveChangePass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnSaveChangePass.FlatAppearance.BorderSize = 0;
            this.btnSaveChangePass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveChangePass.ForeColor = System.Drawing.Color.White;
            this.btnSaveChangePass.Location = new System.Drawing.Point(435, 217);
            this.btnSaveChangePass.Name = "btnSaveChangePass";
            this.btnSaveChangePass.Size = new System.Drawing.Size(67, 32);
            this.btnSaveChangePass.TabIndex = 68;
            this.btnSaveChangePass.Text = "SAVE";
            this.btnSaveChangePass.UseVisualStyleBackColor = false;
            this.btnSaveChangePass.Click += new System.EventHandler(this.btnSaveChangePass_Click);
            // 
            // txtNameChangePass
            // 
            this.txtNameChangePass.Enabled = false;
            this.txtNameChangePass.Location = new System.Drawing.Point(223, 186);
            this.txtNameChangePass.Name = "txtNameChangePass";
            this.txtNameChangePass.Size = new System.Drawing.Size(356, 25);
            this.txtNameChangePass.TabIndex = 67;
            this.txtNameChangePass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNameChangePass_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 17);
            this.label7.TabIndex = 66;
            this.label7.Text = "NAME";
            // 
            // txtNewPassChangePass
            // 
            this.txtNewPassChangePass.Location = new System.Drawing.Point(223, 124);
            this.txtNewPassChangePass.Name = "txtNewPassChangePass";
            this.txtNewPassChangePass.PasswordChar = '•';
            this.txtNewPassChangePass.Size = new System.Drawing.Size(356, 25);
            this.txtNewPassChangePass.TabIndex = 65;
            this.txtNewPassChangePass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewPassChangePass_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(45, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 17);
            this.label9.TabIndex = 64;
            this.label9.Text = "NEW PASSWORD";
            // 
            // txtOldPassChangePass
            // 
            this.txtOldPassChangePass.Location = new System.Drawing.Point(223, 93);
            this.txtOldPassChangePass.Name = "txtOldPassChangePass";
            this.txtOldPassChangePass.PasswordChar = '•';
            this.txtOldPassChangePass.Size = new System.Drawing.Size(356, 25);
            this.txtOldPassChangePass.TabIndex = 63;
            this.txtOldPassChangePass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOldPassChangePass_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(45, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 17);
            this.label10.TabIndex = 62;
            this.label10.Text = "OLD PASSWORD";
            // 
            // txtUserChangePass
            // 
            this.txtUserChangePass.Enabled = false;
            this.txtUserChangePass.Location = new System.Drawing.Point(223, 62);
            this.txtUserChangePass.Name = "txtUserChangePass";
            this.txtUserChangePass.Size = new System.Drawing.Size(356, 25);
            this.txtUserChangePass.TabIndex = 61;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(45, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 17);
            this.label11.TabIndex = 60;
            this.label11.Text = "USERNAME";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.panel4);
            this.tabPage3.Location = new System.Drawing.Point(4, 38);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(625, 288);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ACTIVATE / DEACTIVATE ACCOUNT";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(625, 288);
            this.panel4.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtUserSearch);
            this.panel6.Controls.Add(this.dataGridViewUserList);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 60);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(625, 228);
            this.panel6.TabIndex = 72;
            // 
            // txtUserSearch
            // 
            // 
            // 
            // 
            this.txtUserSearch.CustomButton.Image = null;
            this.txtUserSearch.CustomButton.Location = new System.Drawing.Point(513, 2);
            this.txtUserSearch.CustomButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUserSearch.CustomButton.Name = "";
            this.txtUserSearch.CustomButton.Size = new System.Drawing.Size(20, 18);
            this.txtUserSearch.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUserSearch.CustomButton.TabIndex = 1;
            this.txtUserSearch.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUserSearch.CustomButton.UseSelectable = true;
            this.txtUserSearch.CustomButton.Visible = false;
            this.txtUserSearch.DisplayIcon = true;
            this.txtUserSearch.Icon = global::POS_System.Properties.Resources.search__5_;
            this.txtUserSearch.Lines = new string[0];
            this.txtUserSearch.Location = new System.Drawing.Point(0, 14);
            this.txtUserSearch.MaxLength = 32767;
            this.txtUserSearch.Name = "txtUserSearch";
            this.txtUserSearch.PasswordChar = '\0';
            this.txtUserSearch.PromptText = "Search user by name";
            this.txtUserSearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserSearch.SelectedText = "";
            this.txtUserSearch.SelectionLength = 0;
            this.txtUserSearch.SelectionStart = 0;
            this.txtUserSearch.ShortcutsEnabled = true;
            this.txtUserSearch.Size = new System.Drawing.Size(625, 28);
            this.txtUserSearch.TabIndex = 5;
            this.txtUserSearch.UseSelectable = true;
            this.txtUserSearch.WaterMark = "Search user by name";
            this.txtUserSearch.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUserSearch.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserSearch.TextChanged += new System.EventHandler(this.txtUserSearch_TextChanged);
            // 
            // dataGridViewUserList
            // 
            this.dataGridViewUserList.AllowUserToAddRows = false;
            this.dataGridViewUserList.AllowUserToResizeRows = false;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.AliceBlue;
            this.dataGridViewUserList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridViewUserList.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewUserList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewUserList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUserList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewUserList.ColumnHeadersHeight = 30;
            this.dataGridViewUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewUserList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.ACTIVE,
            this.Column2,
            this.Column3,
            this.Select});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.MintCream;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(205)))), ((int)(((byte)(235)))));
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewUserList.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridViewUserList.EnableHeadersVisualStyles = false;
            this.dataGridViewUserList.Location = new System.Drawing.Point(0, 48);
            this.dataGridViewUserList.Name = "dataGridViewUserList";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUserList.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridViewUserList.RowHeadersVisible = false;
            this.dataGridViewUserList.RowTemplate.Height = 25;
            this.dataGridViewUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUserList.Size = new System.Drawing.Size(625, 187);
            this.dataGridViewUserList.TabIndex = 4;
            this.dataGridViewUserList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUserList_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.HeaderText = "#";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            this.Column1.Width = 41;
            // 
            // ACTIVE
            // 
            this.ACTIVE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ACTIVE.HeaderText = "ACTIVE";
            this.ACTIVE.Image = global::POS_System.Properties.Resources.checked__1_;
            this.ACTIVE.Name = "ACTIVE";
            this.ACTIVE.Width = 53;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "USERNAME";
            this.Column2.Name = "Column2";
            this.Column2.Width = 99;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "NAME";
            this.Column3.Name = "Column3";
            // 
            // Select
            // 
            this.Select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Select.HeaderText = "";
            this.Select.Image = global::POS_System.Properties.Resources.right_arrow;
            this.Select.Name = "Select";
            this.Select.Width = 5;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.txtUserADA);
            this.panel5.Controls.Add(this.btnDeactivate);
            this.panel5.Controls.Add(this.btnActivate);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(625, 60);
            this.panel5.TabIndex = 71;
            // 
            // txtUserADA
            // 
            // 
            // 
            // 
            this.txtUserADA.CustomButton.Image = null;
            this.txtUserADA.CustomButton.Location = new System.Drawing.Point(269, 2);
            this.txtUserADA.CustomButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUserADA.CustomButton.Name = "";
            this.txtUserADA.CustomButton.Size = new System.Drawing.Size(20, 18);
            this.txtUserADA.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtUserADA.CustomButton.TabIndex = 1;
            this.txtUserADA.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtUserADA.CustomButton.UseSelectable = true;
            this.txtUserADA.CustomButton.Visible = false;
            this.txtUserADA.DisplayIcon = true;
            this.txtUserADA.Enabled = false;
            this.txtUserADA.Icon = global::POS_System.Properties.Resources.image__2_;
            this.txtUserADA.Lines = new string[0];
            this.txtUserADA.Location = new System.Drawing.Point(25, 15);
            this.txtUserADA.MaxLength = 32767;
            this.txtUserADA.Name = "txtUserADA";
            this.txtUserADA.PasswordChar = '\0';
            this.txtUserADA.PromptText = "select username from below list";
            this.txtUserADA.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtUserADA.SelectedText = "";
            this.txtUserADA.SelectionLength = 0;
            this.txtUserADA.SelectionStart = 0;
            this.txtUserADA.ShortcutsEnabled = true;
            this.txtUserADA.Size = new System.Drawing.Size(340, 28);
            this.txtUserADA.TabIndex = 73;
            this.txtUserADA.UseSelectable = true;
            this.txtUserADA.WaterMark = "select username from below list";
            this.txtUserADA.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUserADA.WaterMarkFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDeactivate
            // 
            this.btnDeactivate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnDeactivate.Enabled = false;
            this.btnDeactivate.FlatAppearance.BorderSize = 0;
            this.btnDeactivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeactivate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeactivate.ForeColor = System.Drawing.Color.White;
            this.btnDeactivate.Location = new System.Drawing.Point(487, 15);
            this.btnDeactivate.Name = "btnDeactivate";
            this.btnDeactivate.Size = new System.Drawing.Size(110, 28);
            this.btnDeactivate.TabIndex = 72;
            this.btnDeactivate.Text = "DEACTIVATE";
            this.btnDeactivate.UseVisualStyleBackColor = false;
            this.btnDeactivate.Click += new System.EventHandler(this.btnDeactivate_Click);
            // 
            // btnActivate
            // 
            this.btnActivate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnActivate.Enabled = false;
            this.btnActivate.FlatAppearance.BorderSize = 0;
            this.btnActivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivate.ForeColor = System.Drawing.Color.White;
            this.btnActivate.Location = new System.Drawing.Point(371, 15);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(110, 28);
            this.btnActivate.TabIndex = 71;
            this.btnActivate.Text = "ACTIVATE";
            this.btnActivate.UseVisualStyleBackColor = false;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::POS_System.Properties.Resources.checked__1_;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = global::POS_System.Properties.Resources.right_arrow;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            // 
            // frmUserAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(989, 644);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmUserAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmUserAccount";
            this.Load += new System.EventHandler(this.frmUserAccount_Load);
            this.Resize += new System.EventHandler(this.frmUserAccount_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.metroTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserList)).EndInit();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRetype;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox cbShowPassword;
        private System.Windows.Forms.CheckBox cbChangeName;
        private System.Windows.Forms.TextBox txtRetypeNewPassChangePass;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Button btnCancelChangePass;
        public System.Windows.Forms.Button btnSaveChangePass;
        private System.Windows.Forms.TextBox txtNameChangePass;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNewPassChangePass;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOldPassChangePass;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtUserChangePass;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView dataGridViewUserList;
        private MetroFramework.Controls.MetroTextBox txtUserSearch;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn ACTIVE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewImageColumn Select;
        private MetroFramework.Controls.MetroTextBox txtUserADA;
        public System.Windows.Forms.Button btnDeactivate;
        public System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.CheckBox cbShowPassCreateAcc;
    }
}