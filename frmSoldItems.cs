using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmSoldItems : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        public string suser; 
        public frmSoldItems() 
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            dt1.Value = DateTime.Now;
            dt2.Value = DateTime.Now;
            this.KeyPreview = true; 

            LoadRecord();
            LoadCashier(); 

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        public void LoadRecord()
        {
            dataGridView1.Rows.Clear();
            try
            {
                uint i = 0;
                double _total = 0;

                cn.Open();
                string dateFrom = $"{dt1.Value.Year}-{dt1.Value.Month}-{dt1.Value.Day}";
                string dateTo = $"{dt2.Value.Year}-{dt2.Value.Month}-{dt2.Value.Day}";

                string query;

                if (cboCashier.Text.Equals(frmSecurity.allCashier))
                {
                    query = $"SELECT c.id, c.transno, c.pcode, p.pdesc , p.price, c.qty, c.disc, c.total FROM tblCart AS c INNER JOIN tblProduct AS p ON c.pcode = p.pcode WHERE status LIKE 'Sold' AND sdate BETWEEN '{dateFrom}' AND '{dateTo}'; ";
                } else
                {
                    query = $"SELECT c.id, c.transno, c.pcode, p.pdesc , p.price, c.qty, c.disc, c.total FROM tblCart AS c INNER JOIN tblProduct AS p ON c.pcode = p.pcode WHERE status LIKE 'Sold' AND sdate BETWEEN '{dateFrom}' AND '{dateTo}' AND cashier LIKE '{cboCashier.Text}' ; ";
                }

                
                cm = new SqlCommand(query, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    ++i;
                    _total += double.Parse(dr["total"].ToString());
                    dataGridView1.Rows.Add(i, 
                        dr["id"].ToString(),
                        dr["transno"].ToString(),
                        dr["pcode"].ToString(),
                        dr["pdesc"].ToString(),
                        dr["price"].ToString(),
                        dr["qty"].ToString(),
                        dr["disc"].ToString(),
                        dr["total"].ToString()); 
                }
                dr.Close();
                lblTotal.Text = _total.ToString("#,##0.00");

            } catch(Exception ex)
            {

            } finally
            {
                cn.Close();
            }
        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dt2_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReportSold frm = new frmReportSold(this);
            frm.LoadReport();
            frm.ShowDialog();
        }

        private void cboCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }

        public void LoadCashier()
        {
            cboCashier.Items.Clear();
            cboCashier.Items.Add(frmSecurity.allCashier);

            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblUser WHERE role LIKE 'Cashier' ;", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboCashier.Items.Add(dr["username"].ToString()); 
            }
            
            dr.Close(); 
            cn.Close(); 
        }

        private void cboCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecord(); 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name; 

            if (colName.Equals("colCancel"))
            {
                frmCancelDetails frm = new frmCancelDetails(this);
                frm.txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtTransNo.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtPCode.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.txtQty.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                frm.txtDiscount.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                frm.txtTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                frm.txtCancel.Text = this.suser; 

                frm.ShowDialog(); 
            }
        }

        private void frmSoldItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Dispose();
            }
        }
    }
}
