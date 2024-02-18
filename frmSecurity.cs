using System;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace POS_System
{
    public partial class frmSecurity : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        public static string allCashier = "All Cashier";
        public static string loggedInUsername;
        public frmSecurity()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("EXIT APPLICATION?", "EXIT", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
            {
                Application.Exit(); 
            }

            //txtUser.Clear();
            //txtPass.Clear();
            //txtUser.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            string _username = string.Empty; 
            string _role = string.Empty; 
            string _name = string.Empty;
            bool _active = false; 
            try
            {
                Boolean found = false;
                cn.Open();
                string query = "SELECT * FROM tblUser WHERE username = @username AND password = @password";
                cm = new SqlCommand(query, cn);
                cm.Parameters.AddWithValue("@username", txtUser.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);
                dr = cm.ExecuteReader();
                dr.Read(); 
                if (dr.HasRows)
                {
                    found = true;
                    _username = dr["username"].ToString(); 
                    _role = dr["role"].ToString(); 
                    _name = dr["name"].ToString();
                    _active = bool.Parse(dr["active"].ToString());

                } else
                {
                    found = false; 
                }
                dr.Close();

                if (found)
                {
                    if (!_active)
                    {
                        MessageBox.Show("ACCOUNT IS INACTIVE. UNABLE TO LOGIN!", "INACTIVE ACCOUNT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (_role.Equals("Cashier"))
                    {
                        MessageBox.Show($"Welcome {_name}!".ToUpper(), "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUser.Clear();
                        txtPass.Clear();
                        this.Hide();
                        frmPOS frm = new frmPOS(this);
                        frm.lblUser.Text = _username;
                        frmSecurity.loggedInUsername = _username;
                        frm.lblName.Text = $"{_name} | {_role}";
                        frm.ShowDialog();

                    } else
                    {
                        MessageBox.Show($"Welcome {_name}!".ToUpper(), "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUser.Clear();
                        txtPass.Clear();
                        this.Hide();
                        Form1 frm = new Form1();
                        frm.lblUser.Text = _username;
                        frmSecurity.loggedInUsername = _username;
                        frm.lblName.Text = _name;
                        frm.lblRole.Text = _role; 

                        frm.ShowDialog(); 
                    }
                } else
                {
                    MessageBox.Show($"Invalid username or password!".ToUpper(), "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            } finally
            {
                cn.Close(); 
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin.PerformClick(); 
            }
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtUser.Text))
            {
                txtPass.Focus(); 
            }
        }

        private void frmSecurity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                btnExit.PerformClick(); 
            }
        }
    }
}
