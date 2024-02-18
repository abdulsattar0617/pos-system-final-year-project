using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmBrandList : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        public frmBrandList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadRecords();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmBrand frm = new frmBrand(this);
            frm.lblID.Text = "NEW";
            frm.btnSave.Enabled = true;
            frm.btnUpdate.Enabled = false; 
            frm.ShowDialog();
        }

        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblBrand ORDER BY brand;", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                ++i;
                dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName.Equals("Edit"))
            {
                frmBrand frm = new frmBrand(this);
                frm.lblID.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                //frm.lblD.Visible = true;
                frm.txtBrand.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                frm.ShowDialog();

            }
            else if (colName.Equals("Delete"))
            {
                String question = "Are you sure you want to delete this record ?";
                if (MessageBox.Show(question, "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    cn.Open();
                    String id = dataGridView1[1, e.RowIndex].Value.ToString();
                    cm = new SqlCommand("DELETE FROM tblBrand WHERE id like '" + id + "';", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Brand has been successfully deleted.", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRecords();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }
    }
}
