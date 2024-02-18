using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS_System
{
    public partial class frmBrand : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        frmBrandList frmlist;
        public frmBrand(frmBrandList flist)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmlist = flist;
            setControl(); 
        }


        private void Clear()
        {
            txtBrand.Clear();
            txtBrand.Focus();
            setControl(); 
        }

        private void setControl()
        {
            if (lblID.Text.Equals("NEW"))
            {
                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
            }
            else
            {
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String question = "Are you sure you want to save this brand ?".ToUpper();
                if (MessageBox.Show(question, "POS SYSTEM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblBrand(Brand) VALUES (@brand) ", cn);
                    cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully saved.".ToUpper(), "POS SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    frmlist.LoadRecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                String question = "Are you sure you want to update this brand ?".ToUpper();
                if (MessageBox.Show(question, "UPDATE RECORD", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblBrand SET brand = @brand WHERE id LIKE '" + lblID.Text + "';", cn);
                    cm.Parameters.AddWithValue("@brand", txtBrand.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Brand has been successfully updated.".ToUpper(), "POS SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    frmlist.LoadRecords();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear(); 
        }
    }
}
