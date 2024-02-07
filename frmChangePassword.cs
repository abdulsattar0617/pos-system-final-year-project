using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmChangePassword : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        DBConnection dataBase = new DBConnection();
        frmPOS frmPOSRef; 
        public frmChangePassword(frmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dataBase.MyConnection());
            this.frmPOSRef = frm; 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // fetch old password
                string oldPassword = dataBase.GetPassword(frmPOSRef.lblUser.Text);
                string newPassword = txtNewPass.Text.Trim();
                
                // verify old password
                if (!oldPassword.Equals(txtOldPass.Text))
                {
                    MessageBox.Show("Old password did not matched!".ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                } else if (!txtNewPass.Text.Equals(txtConfirmNewPass.Text))
                {
                    MessageBox.Show("Confirm new password did not matched!".ToUpper(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                } else
                {
                    if (MessageBox.Show("Change Password?".ToUpper(), "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Update password in DataBase 
                        cn.Open();
                        cm = new SqlCommand($"UPDATE tblUser SET password = '{newPassword}' WHERE username = '{frmPOSRef.lblUser.Text}'", cn);
                        cm.ExecuteNonQuery();
                        cn.Close();

                        MessageBox.Show("Password has been successfully saved!".ToUpper(), "SAVE CHANGES" , MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose(); 
                    }
                }


            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 

            } finally
            {
                cn.Close(); 
            }
        }

        private void txtConfirmNewPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSave.PerformClick(); 
            }
        }

        private void txtOldPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtNewPass.Focus(); 
            }
        }

        private void txtNewPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtConfirmNewPass.Focus();
            }
        }
    }
}
