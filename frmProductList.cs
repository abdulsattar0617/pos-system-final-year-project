using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmProductList : Form  
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr; 
        public frmProductList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection()); 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmProduct frm = new frmProduct(this);
            frm.btnSave.Enabled = true;
            frm.btnUpdate.Enabled = false;
            frm.LoadCategory();
            frm.LoadBrand();
            frm.Tag = "NEW"; 
            frm.ShowDialog();
        }

        public void LoadRecords()
        {
            int i = 0; 
            dataGridView1.Rows.Clear();
            cn.Open();
            String query = "SELECT " +
                            " p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.reorder " +
                            " FROM tblProduct AS p " +
                            " INNER JOIN " +
                            " tblBrand AS b " +
                            " ON b.id = p.bid " +
                            " INNER JOIN tblCategory as c " +
                            " ON c.id = p.cid " +
                            $" WHERE p.pdesc like '%{txtSearch.Text}%' ORDER BY p.pdesc;";

            cm = new SqlCommand(query, cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());

            }
            dr.Close(); 
            cn.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords(); 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dataGridView1.Columns[e.ColumnIndex].Name; 
            if (colName.Equals("Edit"))
            {
                frmProduct frm = new frmProduct(this);
                frm.btnSave.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.txtPcode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtBarcode.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtPdesc.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.cboBrand.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.cboCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                frm.txtReorder.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                frm.LoadCategory();
                frm.LoadBrand();
                frm.ShowDialog(); 

            } else if (colName.Equals("Delete")) 
            {
                string question = "Are you sure you want to delete this record?"; 
                if (MessageBox.Show(question, "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    String pcode = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    String query = $" DELETE FROM tblProduct WHERE pcode like '{pcode}';";
                    cm = new SqlCommand(query, cn);
                    cm.ExecuteNonQuery(); 
                    cn.Close();
                    LoadRecords(); 
                }
            }
        }
    }
}
