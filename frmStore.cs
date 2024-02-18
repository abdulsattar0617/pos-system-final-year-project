using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmStore : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        public frmStore()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        public void LoadRecords()
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblStore;", cn);
            dr = cm.ExecuteReader();
            dr.Read(); 
            if (dr.HasRows)
            {
                txtStore.Text = dr["store"].ToString();
                txtAddress.Text = dr["address"].ToString();
                txtGstNo.Text = dr["gstnumber"].ToString();

            } else
            {
                txtStore.Clear();
                txtAddress.Clear();
                txtGstNo.Clear();
            }
            cn.Close(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("SAVE STORE DETAILS?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                {
                    cn.Open();
                    int count = 0;
                    cm = new SqlCommand("SELECT COUNT(*) FROM tblStore; ", cn);
                    count = int.Parse(cm.ExecuteScalar().ToString()); 
                    cn.Close(); 

                    if (count > 0)
                    {
                        cn.Open();
                        string query = $"UPDATE tblStore set store = '{txtStore.Text.Trim()}', address = '{txtAddress.Text.Trim()}' ";
                        if (chbEditGst.Checked)
                        {
                            query += $" , gstnumber = '{txtGstNo.Text.Trim()}' ";
                        }
                        cm = new SqlCommand(query, cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }
                    else
                    {
                        cn.Open();
                        string query;
                        if (chbEditGst.Checked)
                        {
                            query = $"INSERT INTO tblStore(store, address, gstnumber) VALUES('{txtStore.Text.Trim()}', '{txtAddress.Text.Trim()}', '{txtGstNo.Text.Trim()}')"; 
                        } else
                        {
                            query = $"INSERT INTO tblStore(store, address) VALUES('{txtStore.Text.Trim()}', '{txtAddress.Text.Trim()}')";
                        }
                        cm = new SqlCommand(query, cn);
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }

                    MessageBox.Show("STORE DETAILS HAS BEEN SUCCESSFULLY SAVED!", "SAVE RECORD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose(); 
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning); 

            } finally
            {
                cn.Close(); 
            }
        }

        private void chbEditGst_CheckedChanged(object sender, EventArgs e)
        {
            if (chbEditGst.Checked)
            {
                // enable gst txt box
                txtGstNo.Enabled = true; 
            } else
            {
                txtGstNo.Enabled = false;
            }
        }
    }
}
