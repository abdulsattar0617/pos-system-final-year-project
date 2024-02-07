using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace POS_System
{
    public partial class frmPOS : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr; 
        String stitle = "POS SOFTWARE";
        String selectedProductID;
        double selectedProductTotal;
        double selectedProductDiscount;
        public String transnoPlaceholder_frmPOS = "0000000000000000";
  
        public frmPOS(frmSecurity frm) 
        {
            InitializeComponent();
            lblDate.Text = DateTime.Now.ToLongDateString();
            lblTransno.Text = transnoPlaceholder_frmPOS;
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
            NotifyCriticalItems(); 
        }

        public void NotifyCriticalItems()
        {
            string count = "0";
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(*) FROM vwCriticalItems ;", cn);
            count = cm.ExecuteScalar().ToString();
            cn.Close();

            string critical = string.Empty;
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM vwCriticalItems ;", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                critical += $"{++i}. {dr["pdesc"].ToString()}{Environment.NewLine}";
            }
            dr.Close();
            cn.Close();

            if (int.Parse(count) > 0)
            {
                PopupNotifier popUp = new PopupNotifier();
                popUp.Image = Properties.Resources.error;
                popUp.TitleText = $"{count} CRITICAL ITEM(S)";
                popUp.ContentText = critical;
                popUp.Popup();
            }
        }

        public void GetTransno()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                string transno;
                int count; 
                cn.Open();
                string query = $"SELECT TOP 1 transno FROM tblCart WHERE transno LIKE '{sdate}%' ORDER BY id DESC;";
                cm = new SqlCommand(query, cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    lblTransno.Text = sdate + (count + 1);
                } else
                {
                    transno = sdate + "1001";
                    lblTransno.Text = transno;
                }
                dr.Close(); 
                cn.Close();
            } catch(Exception ex)
            {
                cn.Close(); 
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                return; 
            }

            GetTransno();
            txtSearch.Enabled = true;
            txtSearch.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text.Equals(String.Empty))
                {
                    return;
                }
                else
                {
                    // ----------------
                    string _pcode;
                    double _price;
                    int _qty;

                    cn.Open();
                    string query = $"SELECT * FROM tblProduct WHERE barcode LIKE '{txtSearch.Text.Trim()}'";
                    cm = new SqlCommand(query, cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        _pcode = dr["pcode"].ToString();
                        _price = double.Parse(dr["price"].ToString());
                        _qty = int.Parse(dr["qty"].ToString());
                        // _qty = int.Parse(txtQty.Text.Trim());

                        dr.Close();
                        cn.Close();

                        AddToCart(_pcode, _price, _qty); 
                    
                    //-----------------------
                        //frmQty frm = new frmQty(this);
                        //frm.ProductDetails(dr["pcode"].ToString(), double.Parse(dr["price"].ToString()), lblTransno.Text, int.Parse(dr["qty"].ToString()));
                        //frm.ShowDialog();
                    }
                    dr.Close();
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } finally
            {
                cn.Close();
                dr.Close();
            }
        }

        public void AddToCart(string _pcode, double _price, int _qtyMax)
        {
            bool found = false;
            int cartQty = 0;
            string query;
            string id = string.Empty; 

            cn.Open();
            query = "SELECT * FROM tblCart WHERE transno = @transno AND pcode = @pcode ;";
            cm = new SqlCommand(query, cn);
            cm.Parameters.AddWithValue("@transno", lblTransno.Text.Trim());
            cm.Parameters.AddWithValue("@pcode", _pcode);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                found = true;
                id = dr["id"].ToString();
                cartQty = int.Parse(dr["qty"].ToString());
            }
            dr.Close(); 
            cn.Close();

            if (found)
            {
                if (_qtyMax < (int.Parse(txtQty.Text) + cartQty))
                {
                    MessageBox.Show($"Unable to proceed. Remaining qty on hand is {_qtyMax}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cn.Open();
                query = $"UPDATE tblCart SET qty = (qty + {int.Parse(txtQty.Text)}) WHERE id = '{id}' ;";
                cm = new SqlCommand(query, cn);
                cm.ExecuteNonQuery();
                cn.Close();

            }
            else
            {
                if (_qtyMax < int.Parse(txtQty.Text))
                {
                    MessageBox.Show($"Unable to proceed. Remaining qty on hand is {_qtyMax}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cn.Open();
                query = $"INSERT INTO tblCart(transno, pcode, price, qty, sdate, cashier) VALUES(@transno, @pcode, @price, @qty, @sdate, @cashier);";
                cm = new SqlCommand(query, cn);
                cm.Parameters.AddWithValue("@transno", lblTransno.Text.Trim());
                cm.Parameters.AddWithValue("@pcode", _pcode);
                cm.Parameters.AddWithValue("@price", _price);
                cm.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                cm.Parameters.AddWithValue("@cashier", lblUser.Text);
                cm.ExecuteNonQuery();
                cn.Close();

            }

            txtSearch.SelectionStart = 0;
            txtSearch.SelectionLength = txtSearch.Text.Length; 
            LoadCart();
        }
        public void LoadCart()
        {
            try
            {
                Boolean hasRecord = false; 
                dataGridView1.Rows.Clear();
                int i = 0;
                double total = 0;
                double discount = 0;
                cn.Open();
                string query = $"SELECT c.id, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total FROM tblCart AS c INNER JOIN tblProduct AS p ON c.pcode = p.pcode WHERE transno LIKE '{lblTransno.Text}' AND status LIKE 'Pending'";
                cm = new SqlCommand(query, cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    total += Double.Parse(dr["total"].ToString());
                    discount += Double.Parse(dr["disc"].ToString());
                    dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), Double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
                    hasRecord = true; 
                }
                //dr.Close();
                //cn.Close();
                lblTotal.Text = total.ToString("#,##0.00");
                lblDiscount.Text = discount.ToString("#,##0.00");
                GetCartTotal();
                if (hasRecord)
                {
                    btnSettle.Enabled = true; 
                    btnDiscount.Enabled = true;
                    btnCancel.Enabled = true; 
                } else
                {
                    btnSettle.Enabled = false;
                    btnDiscount.Enabled = false;
                    btnCancel.Enabled = false;
                }

            } catch(Exception ex)
            {
                //cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            } finally
            {
                dr.Close();
                cn.Close();
            }

        }

       
        public void GetCartTotal()
        {
            double discount = Double.Parse(lblDiscount.Text);
            double sales = Double.Parse(lblTotal.Text);
            //double sales = subTotal - discount;
            double vat = sales * dbcon.GetGST();
            double vatable = sales - vat;
            lblVat.Text = vat.ToString("#,##0.00");
            lblVatable.Text = vatable.ToString("#,##0.00");
            lblDisplayTotal.Text = sales.ToString("#,##0.00");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (lblTransno.Text.Equals(transnoPlaceholder_frmPOS))
            {
                return;
            }

            frmLookUp frm = new frmLookUp(this);
            frm.LoadRecords();
            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName.Equals("Delete"))
            {
                if (MessageBox.Show("Remove this item?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    string id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string query = $"DELETE FROM tblCart WHERE id LIKE '{id}'";
                    cm = new SqlCommand(query, cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Item has successfully removed!".ToUpper(), stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCart(); 
                }

            } else if (colName.Equals("colAdd"))
            {
                int qtyMax = 0; 
                cn.Open();
                cm = new SqlCommand($"SELECT sum(qty) as qty FROM tblProduct WHERE pcode LIKE '{dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()}' GROUP BY pcode ;", cn);
                qtyMax = int.Parse(cm.ExecuteScalar().ToString()); 
                cn.Close(); 

                if ((int.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) + int.Parse(txtQty.Text)) <= (qtyMax))
                {
                    // update cart - increase qty 
                    cn.Open();
                    cm = new SqlCommand($"UPDATE tblCart SET qty = (qty + {int.Parse(txtQty.Text)}) WHERE transno LIKE '{lblTransno.Text}' AND pcode LIKE '{dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()}'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    LoadCart(); 

                } else
                {
                    MessageBox.Show($"Remaining qty on hand is {qtyMax} !".ToUpper(), "OUT OF STOCK", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }
            }
            else if (colName.Equals("colRemove"))
            {
                int qtyMax = 0;
                cn.Open();
                cm = new SqlCommand($"SELECT sum(qty) as qty FROM tblCart WHERE pcode LIKE '{dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()}' GROUP BY pcode ;", cn);
                qtyMax = int.Parse(cm.ExecuteScalar().ToString());
                cn.Close();

                if ((int.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) - int.Parse(txtQty.Text)) >= 1)
                {
                    // update cart - decrease qty 
                    cn.Open();
                    cm = new SqlCommand($"UPDATE tblCart SET qty = (qty - {int.Parse(txtQty.Text)}) WHERE transno LIKE '{lblTransno.Text}' AND pcode LIKE '{dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()}'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    LoadCart();

                }
                else
                {
                    MessageBox.Show($"Remaning qty on cart is {1}".ToUpper(), "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);  
                    return;
                }
            }
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            frmDiscount frm = new frmDiscount(this);
            frm.lblID.Text = this.selectedProductID;
            frm.txtTotalAmount.Text = this.selectedProductTotal.ToString();
            frm.txtAmount.Text = this.selectedProductDiscount.ToString();
            frm.txtPercent.Text = ((this.selectedProductDiscount / this.selectedProductTotal) * 100d).ToString();
            frm.ShowDialog();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            this.selectedProductID = dataGridView1[1, index].Value.ToString();
            this.selectedProductTotal = double.Parse(dataGridView1[4, index].Value.ToString()) * double.Parse(dataGridView1[5, index].Value.ToString());
            this.selectedProductDiscount = double.Parse(dataGridView1[6, index].Value.ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate1.Text = DateTime.Now.ToString("dddd, MMMM d, yyyy");
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            frmSettle frm = new frmSettle(this);
            frm.txtSale.Text = lblDisplayTotal.Text;
            frm.ShowDialog();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            frmSoldItems frm = new frmSoldItems();
            frm.dt1.Enabled = false;
            frm.dt2.Enabled = false;
            frm.cboCashier.Enabled = false;
            frm.cboCashier.Text = lblUser.Text;
            frm.suser = lblUser.Text; 
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("Unable to logout. Please cancel the transaction.".ToUpper(), "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            if (MessageBox.Show("LOGOUT APPLICATION?", stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                frmSecurity frm = new frmSecurity();
                frm.ShowDialog(); 
            }
        }

        private void frmPOS_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.F1:
                    btnNew.PerformClick();
                    break;

                case Keys.F2:
                    btnSearch.PerformClick();
                    break;

                case Keys.F3:
                    btnDiscount.PerformClick();
                    break;

                case Keys.F4:
                    btnSettle.PerformClick();
                    break;

                case Keys.F5:
                    btnCancel.PerformClick();
                    break;

                case Keys.F6:
                    btnSale.PerformClick();
                    break;

                case Keys.F7:
                    btnChangePass.PerformClick();
                    break;

                case Keys.F8:
                    txtSearch.SelectionStart = 0;
                    txtSearch.SelectionLength = txtSearch.Text.Length;
                    txtSearch.Focus();
                    break; 

                case Keys.F10:
                    btnLogout.PerformClick();
                    break;
            }

        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(this);
            frm.ShowDialog(); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("REMOVE ALL ITEMS FROM THE CART?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
            {
                // empty cart 
                cn.Open();
                cm = new SqlCommand($"DELETE FROM tblCart WHERE transno LIKE '{lblTransno.Text}'", cn);
                cm.ExecuteNonQuery(); 
                cn.Close();

                MessageBox.Show("ALL ITEMS HAS BEEN SUCCESSFULLY REMOVED!", "REMOVE ITEM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCart(); 
            }
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            btnNew.PerformClick();
        }
    }

    
}
