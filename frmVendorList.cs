using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmVendorList : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        public frmVendorList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmVendor frm = new frmVendor(this);
            frm.lblID.Text = "NEW";
            frm.ShowDialog();
        }

        public void LoadVendorList()
        {
            int i = 0;
            dataGridViewVendorList.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblVendor ORDER BY vendor; ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                ++i;
                dataGridViewVendorList.Rows.Add(i, 
                        dr[0].ToString(), 
                        dr[1].ToString(), 
                        dr[2].ToString(), 
                        dr[3].ToString(), 
                        dr[4].ToString(), 
                        dr[5].ToString(), 
                        dr[6].ToString() 
                    );
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridViewVendorList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dataGridViewVendorList.Columns[e.ColumnIndex].Name;
            if (colName.Equals("Edit"))
            {
                frmVendor frm = new frmVendor(this); 
                frm.lblID.Text = dataGridViewVendorList.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtVendor.Text = dataGridViewVendorList.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtAddress.Text = dataGridViewVendorList.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.txtContactPerson.Text = dataGridViewVendorList.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.txtTelephoneNo.Text = dataGridViewVendorList.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.txtEmail.Text = dataGridViewVendorList.Rows[e.RowIndex].Cells[6].Value.ToString();
                frm.txtFax.Text = dataGridViewVendorList.Rows[e.RowIndex].Cells[7].Value.ToString();
                frm.ShowDialog();

            }
            else if (colName.Equals("Delete"))
            {
                if (MessageBox.Show("DELETE THIS RECORD? CLICK YES TO CONFIRM", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    String id = dataGridViewVendorList.Rows[e.RowIndex].Cells[1].Value.ToString();
                    cm = new SqlCommand($"DELETE FROM tblVendor WHERE id like '{id}'", cn); 
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("RECORD HAS BEEN SUCCESSFULLY DELETED!", "DELETE RECORD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadVendorList();
                }
            }
        }
    }
}
