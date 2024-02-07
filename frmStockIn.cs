using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmStockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        String stitle = "POS System";
        public frmStockIn()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadVendor();
        }
        public void LoadStockIn()
        {
            int i = 0;
            dataGridViewStockEntryCart.Rows.Clear();
            cn.Open();
            String status = "Pending";
            String query = "SELECT * FROM vwStockIn;";
            query = $"SELECT * FROM vwStockIn WHERE refno like '{txtRefNo.Text}' AND status LIKE '{status}';";
            cm = new SqlCommand(query, cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridViewStockEntryCart.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[8].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dataGridViewStockEntryCart.Columns[e.ColumnIndex].Name;
            if (colName.Equals("colDelete"))
            {
                String question = "Remove this item?";
                if (MessageBox.Show(question, stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    string id = dataGridViewStockEntryCart.Rows[e.RowIndex].Cells[1].Value.ToString();
                    String query = $"DELETE FROM tblStockIn WHERE id = '{id}';";
                    cm = new SqlCommand(query, cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    MessageBox.Show("Item has been successfully removed.", stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadStockIn();
                }

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

       
        public void LoadVendor()
        {
            cboVendor.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblVendor; ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboVendor.Items.Add(dr["vendor"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewStockEntryCart.Rows.Count > 0)
                {

                    if (MessageBox.Show("Are you sure you want to save this records?".ToUpper(), stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dataGridViewStockEntryCart.Rows.Count; i++)
                        {
                            String query, status, id;
                            int newQty = int.Parse(dataGridViewStockEntryCart.Rows[i].Cells[5].Value.ToString());

                            // update tblProduct qty 
                            cn.Open();
                            String pcode = dataGridViewStockEntryCart.Rows[i].Cells[3].Value.ToString();
                            query = $"UPDATE tblProduct SET qty = qty + '{newQty}' WHERE pcode LIKE '{pcode}';";
                            cm = new SqlCommand(query, cn);
                            cm.ExecuteNonQuery();
                            cn.Close();

                            // update tblStockIn qty 
                            cn.Open();
                            status = "Done";
                            id = dataGridViewStockEntryCart.Rows[i].Cells[1].Value.ToString();
                            query = $"UPDATE tblStockIn SET qty = qty + '{newQty}', status = '{status}' WHERE id LIKE '{id}';";
                            cm = new SqlCommand(query, cn);
                            cm.ExecuteNonQuery();
                            cn.Close();
                        }


                        Clear();
                        LoadStockIn();
                    }
                }


            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadStockInHistory()
        {
            int i = 0;
            dateGridViewStockInHistory.Rows.Clear();
            cn.Open();
            String status = "Done";
            String query;

            String dateFrom = $"{date1.Value.Year}-{date1.Value.Month}-{date1.Value.Day}";
            String dateTo = $"{date2.Value.Year}-{date2.Value.Month}-{date2.Value.Day}";

            query = $"SELECT * FROM vwStockIn WHERE CAST(sdate as DATE) BETWEEN '{dateFrom}' AND '{dateTo}' AND status LIKE '{status}' ORDER BY sdate DESC;";

            cm = new SqlCommand(query, cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dateGridViewStockInHistory.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), DateTime.Parse(dr[5].ToString()).ToShortDateString(), dr[6].ToString(), dr[8].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void Clear()
        {
            txtBy.Clear();
            txtRefNo.Clear();
            dt1.Value = DateTime.Now;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSearchProductStockin frm = new frmSearchProductStockin(this);
            frm.LoadProduct();
            frm.ShowDialog();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadStockInHistory();
        }

        private void cboVendor_TextChanged(object sender, EventArgs e)
        {
            cn.Open();
            cm = new SqlCommand($"SELECT * FROM tblVendor WHERE vendor LIKE '{cboVendor.Text}';", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lblVendorID.Text = dr["id"].ToString();
                txtAddress.Text = dr["address"].ToString();
                txtPerson.Text = dr["contactperson"].ToString();
            }
            dr.Close();
            cn.Close();
        }

        private void linkGenerate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random random = new Random();
            txtRefNo.Text = random.Next().ToString();
            
        }
    }
}
