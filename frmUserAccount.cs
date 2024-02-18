using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmUserAccount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        string[] userData;
        public frmUserAccount()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmUserAccount_Resize(object sender, EventArgs e)
        {
            metroTabControl1.Left = (this.Width - metroTabControl1.Width) / 2; 
            metroTabControl1.Top = (this.Height - metroTabControl1.Height) / 2; 
        }

        private void ClearCreateAccountTab()
        {
            txtUser.Clear(); 
            txtPass.Clear();
            txtRetype.Clear();
            cboRole.Text = "";
            txtName.Clear();
            txtUser.Focus(); 
        }

        private void ClearChangePasswordTab()
        {
            
            txtOldPassChangePass.Clear();
            txtNewPassChangePass.Clear(); 
            txtRetypeNewPassChangePass.Clear();

            txtOldPassChangePass.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearCreateAccountTab();
        }

        private bool ValidateFieldsCreateAccountTab()
        {
            if (string.IsNullOrEmpty(txtUser.Text)) return false;
            if (string.IsNullOrEmpty(txtPass.Text)) return false;
            if (string.IsNullOrEmpty(txtRetype.Text)) return false;
            if (string.IsNullOrEmpty(txtName.Text)) return false;
            if (string.IsNullOrEmpty(cboRole.Text)) return false;
            if (!cboRole.Text.Equals("System Administrator") || !cboRole.Text.Equals("Cashier")) return false;

            return true; 
        }

        private bool ValidateFieldsChangePasswordTab()
        {
            if (string.IsNullOrEmpty(txtUserChangePass.Text)) return false;
            if (string.IsNullOrEmpty(txtOldPassChangePass.Text))
            {
                MessageBox.Show("ENTER OLD PASSWORD!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtNewPassChangePass.Text))
            {
                MessageBox.Show("ENTER NEW PASSWORD!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txtRetypeNewPassChangePass.Text))
            {
                MessageBox.Show("ENTER 'CONFIRM NEW PASSWORD'!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (string.IsNullOrEmpty(cboRole.Text)) return false;
            if (cbChangeName.Checked && string.IsNullOrEmpty(txtNameChangePass.Text))
            {
                MessageBox.Show("ENTER NEW 'NAME'!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //if (!cboRole.Text.Equals("System Administrator") || !cboRole.Text.Equals("Cashier")) return false;
        
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFieldsCreateAccountTab())
                {
                    MessageBox.Show("ENTER ALL FIELDS PROPERLY!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (!txtPass.Text.Equals(txtRetype.Text))
                {
                    MessageBox.Show("Password did not match!".ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                cn.Open();
                string query = "INSERT INTO tblUser(username, password, role, name) VALUES(@username, @password, @role, @name);";
                cm = new SqlCommand(query, cn);
                cm.Parameters.AddWithValue("@username", txtUser.Text);
                cm.Parameters.AddWithValue("@password", txtPass.Text);
                cm.Parameters.AddWithValue("@role", cboRole.Text);
                cm.Parameters.AddWithValue("@name", txtName.Text);
                cm.ExecuteNonQuery();

                MessageBox.Show("NEW ACCOUNT HAS SAVED!", "POS SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCreateAccountTab(); 

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); 

            } finally
            {
                cn.Close(); 
            }
        }

        private void LoadUserData()
        {
            this.userData = dbcon.GetUserData(frmSecurity.loggedInUsername);
            // Data Format 
            // 0  = password
            // 1  = role 
            // 2  = name 

            txtUserChangePass.Text = frmSecurity.loggedInUsername;
            txtNameChangePass.Text = this.userData[2];
        }

        private void btnSaveChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                // ALL FIELDS ARE FILLED OR NOT 
                if (!ValidateFieldsChangePasswordTab())
                {
                    //MessageBox.Show("ENTER ALL FIELDS PROPERLY!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // VERIFY OLD PASSWORD 
                if (!txtOldPassChangePass.Text.Equals(this.userData[0]))
                {
                    MessageBox.Show("OLD PASSWORD DID NOT MATCHED!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                } 

                // MATCH NEW PASSWORD 
                if (!txtNewPassChangePass.Text.Equals(txtRetypeNewPassChangePass.Text))
                {
                    MessageBox.Show("NEW Password did not matched!".ToUpper(), "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                

                if ((MessageBox.Show("SAVE CHANGES?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                {
                    // UPDATE DATABASE 
                    cn.Open();
                    string query = "UPDATE tblUser SET ";
                    // add parameters 
                    // change password 
                    query += $" password = '{txtRetypeNewPassChangePass.Text.Trim()}' ";
                    // change name 
                    if (cbChangeName.Checked && !string.IsNullOrEmpty(txtNameChangePass.Text))
                    {
                        query += ", " + $"name = '{txtNameChangePass.Text.Trim()}' ";
                    }
                    // where clause
                    query += $" WHERE username = '{frmSecurity.loggedInUsername}' ;";

                    cm = new SqlCommand(query, cn); 
                    cm.ExecuteNonQuery();

                    MessageBox.Show("ACCOUNT HAS BEEN SUCCESSFULLY UPDATED!", "POS SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearChangePasswordTab();
                    LoadUserData(); 
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }finally
            {
                cn.Close();
            }
        }


        private void frmUserAccount_Load(object sender, EventArgs e)
        {
            LoadUserData();
            metroTabControl1.Left = (this.Width - metroTabControl1.Width) / 2;
            metroTabControl1.Top = (this.Height - metroTabControl1.Height) / 2;

            LoadUserList();
        }

        private void cbChangeName_CheckedChanged(object sender, EventArgs e)
        {
            if (cbChangeName.Checked)
            {
                txtNameChangePass.Enabled = true;
            }
            else
            {
                txtNameChangePass.Enabled = false;
            }
        }

        private void btnCancelChangePass_Click(object sender, EventArgs e)
        {
            ClearChangePasswordTab();
        }

        private void txtOldPassChangePass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtNewPassChangePass.Focus(); 
            }
        }

        private void txtNewPassChangePass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtRetypeNewPassChangePass.Focus();
            }
        }

        private void txtRetypeNewPassChangePass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && txtName.Enabled)
            {
                txtName.Focus();
            } else if (e.KeyChar == 13)
            {
                btnSaveChangePass.PerformClick();
            }
        }

        private void txtNameChangePass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSaveChangePass.PerformClick(); 
            }
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked)
            {
                txtOldPassChangePass.PasswordChar = '\0';
                txtNewPassChangePass.PasswordChar = '\0';
                txtRetypeNewPassChangePass.PasswordChar = '\0';

            } else
            {
                txtOldPassChangePass.PasswordChar = '•';
                txtNewPassChangePass.PasswordChar = '•';
                txtRetypeNewPassChangePass.PasswordChar = '•';
            }
        }


        public void LoadUserList()
        {
            int i = 0;
            dataGridViewUserList.Rows.Clear();
            cn.Open();
            cm = new SqlCommand($"SELECT * FROM tbluser WHERE name LIKE '%{txtUserSearch.Text}%'; ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                // Data Format 
                // 0 = username
                // 1 = password
                // 2 = role 
                // 3 = name 
                // 4 = active (account state)
                System.Drawing.Bitmap accountStateIcon;
                if (bool.Parse(dr["active"].ToString()))
                {
                    accountStateIcon = Properties.Resources.green_circle;
                } else
                {
                    accountStateIcon = Properties.Resources.red_circle;
                }

                ++i;
                dataGridViewUserList.Rows.Add(i, accountStateIcon , dr[0].ToString(), dr[3].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void txtUserSearch_TextChanged(object sender, EventArgs e)
        {
            LoadUserList();
        }

        private void dataGridViewUserList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dataGridViewUserList.Columns[e.ColumnIndex].Name;

            if (colName.Equals("Select"))
            {
                string username = dataGridViewUserList.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.userData = dbcon.GetUserData(username);

                txtUserADA.Text = username;
                if (bool.Parse(userData[3]))
                {
                    btnActivate.Enabled = false;
                    btnDeactivate.Enabled = true;
                }
                else
                {
                    btnActivate.Enabled = true;
                    btnDeactivate.Enabled = false;
                }

            }
        }

      

        private void btnActivate_Click(object sender, EventArgs e)
        {
            setAccountStatus(1, "ACTIVATE");
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            setAccountStatus(0, "DEACTIVATE"); 
        }

        private void setAccountStatus(byte state, string msg) 
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserADA.Text))
                {
                    MessageBox.Show("PLEASE ENTER USERNAME OR SELECT FROM THE LIST!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if ((MessageBox.Show($"{msg} ACCOUNT?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand($"UPDATE tbluser SET active = {state} WHERE username = '{txtUserADA.Text}' ; ", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show($"ACCOUNT {msg}D!", "UPDATE STATUS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadUserList();

                    txtUserADA.Clear();
                    txtUserADA.Focus();
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            } finally
            {
                cn.Close(); 
            }
        }

        private void cbShowPassCreateAcc_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassCreateAcc.Checked)
            {
                txtPass.PasswordChar = '\0';
                txtRetype.PasswordChar = '\0';
            }
            else
            {
                txtPass.PasswordChar = '•';
                txtRetype.PasswordChar = '•';
            }
        }
    }
}
