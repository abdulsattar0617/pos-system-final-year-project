using System;
using System.Windows.Forms;
using System.Data.SqlClient; 

namespace POS_System
{
    public partial class frmProduct : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        frmProductList flist;
        String stitle = "POS System"; 
        public frmProduct( frmProductList frm )
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            flist = frm;
        }

        public void LoadCategory()
        {
            cboCategory.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT category FROM tblCategory;", cn);
            dr = cm.ExecuteReader(); 
            while (dr.Read())
            {
                cboCategory.Items.Add(dr[0].ToString()); 
            }
            dr.Close(); 
            cn.Close();
        }

        public void LoadBrand()
        {
            cboBrand.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT brand FROM tblBrand;", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboBrand.Items.Add(dr[0].ToString());
            }
            dr.Close(); 
            cn.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String question = "Are you sure you want to save this product?";
                if (MessageBox.Show(question, "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String bid = "";
                    String cid = "";

                    // fetching brand id
                    cn.Open();
                    cm = new SqlCommand("SELECT id FROM tblBrand WHERE brand like '" + cboBrand.Text + "';", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows) { bid = dr[0].ToString(); } 
                    dr.Close(); 
                    cn.Close();

                    // fetching category id
                    cn.Open();
                    cm = new SqlCommand("SELECT id FROM tblCategory WHERE category like '" + cboCategory.Text + "';", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows) { cid = dr[0].ToString(); }
                    dr.Close();
                    cn.Close();

                    // inserting new product details 
                    cn.Open();
                    String query = " INSERT INTO tblProduct " +
                                    " (pcode, barcode, pdesc, bid, cid, cost_price, price, reorder) " + 
                                    " VALUES " +
                                    " (@pcode, @barcode, @pdesc, @bid, @cid, @cost_price, @price, @reorder); ";

                    cm = new SqlCommand(query, cn);
                    cm.Parameters.AddWithValue("@pcode", txtPcode.Text);
                    cm.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    cm.Parameters.AddWithValue("@pdesc", txtPdesc.Text);
                    cm.Parameters.AddWithValue("@bid", bid);
                    cm.Parameters.AddWithValue("@cid", cid);
                    cm.Parameters.AddWithValue("@cost_price", double.Parse(txtCostPrice.Text));
                    cm.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                    cm.Parameters.AddWithValue("@reorder", int.Parse(txtReorder.Text));
                    cm.ExecuteNonQuery(); 
                    cn.Close();
                    MessageBox.Show("Product has been successfully saved.");
                    Clear();
                    flist.LoadRecords(); 
                }

            } catch(Exception ex)
            {
                cn.Close(); 
                MessageBox.Show(ex.Message); 
            }
        }

        public void Clear()
        {
            txtPcode.Clear();
            txtPdesc.Clear();
            txtPrice.Clear();
            txtBarcode.Clear(); 
            cboBrand.Text = "";
            cboCategory.Text = "";
            txtPcode.Focus();
            
            if (this.Tag != null && this.Tag.Equals("NEW"))
            {
                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
            } else
            {
                btnSave.Enabled = false;
                btnUpdate.Enabled = true ;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear(); 
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                String question = "Are you sure you want to update this product?".ToUpper();
                if (MessageBox.Show(question, "SAVE PRODUCT", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String bid = "";
                    String cid = "";

                    // fetching brand id
                    cn.Open();
                    cm = new SqlCommand("SELECT id FROM tblBrand WHERE brand like '" + cboBrand.Text + "';", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows) { bid = dr[0].ToString(); }
                    dr.Close();
                    cn.Close();

                    // fetching category id
                    cn.Open();
                    cm = new SqlCommand("SELECT id FROM tblCategory WHERE category like '" + cboCategory.Text + "';", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows) { cid = dr[0].ToString(); }
                    dr.Close();
                    cn.Close();

                    // update product details 
                    cn.Open();
                    String query = " UPDATE tblProduct SET barcode = @barcode, pdesc = @pdesc, bid = @bid, cid = @cid, cost_price = @cost_price, price = @price, reorder = @reorder WHERE pcode like @pcode ;";


                    cm = new SqlCommand(query, cn);
                    cm.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    cm.Parameters.AddWithValue("@pcode", txtPcode.Text);
                    cm.Parameters.AddWithValue("@pdesc", txtPdesc.Text);
                    cm.Parameters.AddWithValue("@bid", bid);
                    cm.Parameters.AddWithValue("@cid", cid);
                    cm.Parameters.AddWithValue("@cost_price", double.Parse(txtCostPrice.Text));
                    cm.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                    cm.Parameters.AddWithValue("@reorder", int.Parse(txtReorder.Text));
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully update.".ToUpper(), stitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    flist.LoadRecords();
                    this.Dispose(); 
                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46)
            {
                // accept dot (.) character 

            } else if (e.KeyChar == 8)
            {
                // accept backspace 
            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57))   // ascii code 48-57 between 0 - 9
            {
                e.Handled = true; 
            }
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {

        }
    }
}
