using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmVendor : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        frmVendorList frmVendorListRef;
        public frmVendor(frmVendorList frm) 
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true; 
            setControl();
            this.frmVendorListRef = frm; 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("SAVE THIS RECORD? CLICK YES TO CONFIRM.", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes) 
                {
                    cn.Open();
                    string query = $"INSERT INTO tblVendor(vendor, address, contactperson, telephone, email, fax) VALUES('{txtVendor.Text.Trim()}', '{txtAddress.Text.Trim()}', '{txtContactPerson.Text.Trim()}', '{txtTelephoneNo.Text.Trim()}', '{txtEmail.Text.Trim()}', '{txtFax.Text.Trim()}'); ";
                    cm = new SqlCommand(query, cn);
                    cm.ExecuteNonQuery(); 
                    cn.Close();

                    MessageBox.Show("RECORD HAS BEEN SUCCESSFULLY SAVED.", "SAVE RECORD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();

                    frmVendorListRef.LoadVendorList();
                    this.Dispose(); 
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
        }

        private void Clear()
        {
            // Clear fields
            txtVendor.Clear();
            txtAddress.Clear();
            txtContactPerson.Clear();
            txtTelephoneNo.Clear();
            txtEmail.Clear();
            txtFax.Clear();

            // Focus field
            txtVendor.Focus(); 

            // Set Control options 
            setControl(); 
        }

        private void setControl()
        {
            if (int.TryParse(lblID.Text, out _)) 
            {
                btnSave.Enabled = false;
                btnUpdate.Enabled = true; 
            }
            else 
            {
                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
            }
        }

        private void tglSwitchCase_CheckedChanged(object sender, EventArgs e)
        {
            if (tglSwitchCase.Checked)
            {
                // capital case
                txtVendor.Text = txtVendor.Text.Trim().ToUpper(); 
                txtAddress.Text = txtAddress.Text.Trim().ToUpper(); 
                txtContactPerson.Text = txtContactPerson.Text.Trim().ToUpper();
                

            } else
            {
                // lower case 
                txtVendor.Text = txtVendor.Text.Trim().ToLower();
                txtAddress.Text = txtAddress.Text.Trim().ToLower();
                txtContactPerson.Text = txtContactPerson.Text.Trim().ToLower();
            }

            txtTelephoneNo.Text = txtTelephoneNo.Text.Trim();
            txtFax.Text = txtFax.Text.Trim();
        }

        private void frmVendor_Load(object sender, EventArgs e)
        {
            setControl();
        }

        private void frmVendor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                if ((MessageBox.Show("CLOSE VENDOR DETAILS WINDOW?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                {
                    this.Dispose();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear(); 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("SAVE THIS RECORD? CLICK YES TO CONFIRM.", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand($"UPDATE tblVendor SET vendor = '{txtVendor.Text.Trim()}', address = '{txtAddress.Text.Trim()}', contactperson = '{txtContactPerson.Text.Trim()}', telephone = '{txtTelephoneNo.Text.Trim()}', email = '{txtEmail.Text.Trim()}', fax = '{txtFax.Text.Trim()}'  WHERE id LIKE '{lblID.Text}';", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("RECORD HAS BEEN SUCCESSFULLY UPDATED.", "UPDATE RECORD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // update vendor list 
                    frmVendorListRef.LoadVendorList(); 
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
