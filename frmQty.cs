using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmQty : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        private string pcode;
        private double price;
        private string transno; 
        private int qty; 

        frmPOS fpos; 
        public frmQty(frmPOS frmpos)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true; 
            fpos = frmpos; 
        }

        public void ProductDetails(string pcode, double price, string transno, int qty) 
        {
            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
            this.qty = qty; 
        }

       

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ( (e.KeyChar == 13) && (!txtQty.Text.Equals(String.Empty)))
            {
                

                bool found = false;
                int cartQty = 0; 
                String id = string.Empty; 
                String query; 
                // if product already present, update quantity
                cn.Open();
                query = "SELECT * FROM tblCart WHERE transno = @transno AND pcode = @pcode ;";
                cm = new SqlCommand(query, cn);
                cm.Parameters.AddWithValue("@transno", this.transno);
                cm.Parameters.AddWithValue("@pcode", this.pcode);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    id = dr["id"].ToString() ;
                    cartQty = int.Parse(dr["qty"].ToString()); 
                }
                cn.Close(); 

                if (found)
                {
                    if (this.qty < (int.Parse(txtQty.Text) + cartQty))
                    {
                        MessageBox.Show($"Unable to proceed. Remaining qty on hand is {this.qty}".ToUpper(), "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cn.Open();
                    query = $"UPDATE tblCart SET qty = (qty + {int.Parse(txtQty.Text)}) WHERE id = '{id}' ;";
                    cm = new SqlCommand(query, cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    fpos.txtSearch.Clear();
                    fpos.txtSearch.Focus();
                    fpos.LoadCart();

                } else
                {
                    if (this.qty < int.Parse(txtQty.Text))
                    {
                        MessageBox.Show($"Unable to proceed. Remaining qty on hand is {this.qty}".ToUpper(), "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cn.Open();
                    query = $"INSERT INTO tblCart(transno, pcode, price, qty, sdate, cashier) VALUES(@transno, @pcode, @price, @qty, @sdate, @cashier);";
                    cm = new SqlCommand(query, cn);
                    cm.Parameters.AddWithValue("@transno", this.transno);
                    cm.Parameters.AddWithValue("@pcode", this.pcode);
                    cm.Parameters.AddWithValue("@price", this.price);
                    cm.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                    cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cm.Parameters.AddWithValue("@cashier", fpos.lblUser.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    fpos.txtSearch.Clear();
                    fpos.txtSearch.Focus();
                    fpos.LoadCart();
                }
                

                this.Dispose();
            }
            else if (e.KeyChar == 8)
            {
                // allow backspace
            }
            else if (e.KeyChar.Equals(Keys.Escape))
            {
                // allow escape key
            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57))   
            {
                // mark handled to all keys except digits (0 - 9)
                e.Handled = true;
            }
        }

        private void frmQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Dispose(); 
            }
        }
    }
}
