using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmLookUp : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        frmPOS parentForm; 
        public frmLookUp(frmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.parentForm = frm;
            this.KeyPreview = true; 
                
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            String query = "SELECT " +
                            " p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty " +
                            " FROM tblProduct AS p " +
                            " INNER JOIN " +
                            " tblBrand AS b " +
                            " ON b.id = p.bid " +
                            " INNER JOIN tblCategory as c " +
                            " ON c.id = p.cid " +
                            " WHERE " +
                            $" p.pdesc like '%{txtSearch.Text}%' ORDER BY p.pdesc;";

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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName.Equals("Select"))
            {
                frmQty frm = new frmQty(parentForm);
                // dr["pcode"].ToString(), double.Parse(dr["price"].ToString()), lblTransno.Text
                frm.ProductDetails(
                    dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    double.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()),
                    parentForm.lblTransno.Text,
                    int.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString())
                    );
                frm.ShowDialog();
            }
        }

        private void frmLookUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Dispose();
            }
        }
    }
}
