using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmSearchProductStockin : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        String stitle = "POS SYSTEM";
        frmStockIn slist;  
        public frmSearchProductStockin(frmStockIn flist) 
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            slist = flist; 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        public void LoadProduct()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            String query = $"SELECT pcode, pdesc, qty FROM tblProduct WHERE pdesc like '%{txtSearch.Text}%' ORDER BY pdesc; ";
            cm = new SqlCommand(query, cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName.Equals("colSelect"))
            {
                if (slist.txtRefNo.Text.Equals(string.Empty))
                {
                    MessageBox.Show("Please enter 'reference no'".ToUpper(), stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slist.txtRefNo.Focus();
                    return;
                }

                if (slist.txtBy.Text.Equals(string.Empty))
                {
                    MessageBox.Show("Please enter 'stock in by'".ToUpper(), stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    slist.txtBy.Focus();
                    return;
                }



                if (MessageBox.Show("Add this item?".ToUpper(), stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    string pcode = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    //String query = $"SELECT * FROM tblProduct WHERE pcode like '{pcode}';";
                    //String query = $"INSERT INTO tblStockIn(refno, pcode, sdate, stockinby) VALUES (@refno, @pcode, @sdate, @stockinby);";
                    String query = $"INSERT INTO tblStockIn(refno, pcode, sdate, stockinby, vendorid) VALUES (@refno, @pcode, @sdate, @stockinby, @vendorid);";
                    cm = new SqlCommand(query, cn);
                    cm.Parameters.AddWithValue("@refno", slist.txtRefNo.Text);
                    cm.Parameters.AddWithValue("@pcode", pcode);
                    cm.Parameters.AddWithValue("@sdate", slist.dt1.Value);
                    cm.Parameters.AddWithValue("@stockinby", slist.txtBy.Text);
                    cm.Parameters.AddWithValue("@vendorid", slist.lblVendorID.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Successfully added!".ToUpper(), stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    slist.LoadStockIn();
                }


            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct(); 
        }
    }
}
