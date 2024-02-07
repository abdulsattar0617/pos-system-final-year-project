using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmSettle : Form
    {
        frmPOS parentForm;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection(); 
        public frmSettle(frmPOS frm)
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

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double sale = double.Parse(txtSale.Text);
                double cash = double.Parse(txtCash.Text);
                double change = cash - sale;
                txtChange.Text = change.ToString("#,##0.00");

            } catch(Exception ex)
            {
                txtChange.Text = "0.00";
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn9.Text;
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn6.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn0.Text;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn3.Text;
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            txtCash.Text += btn00.Text;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtCash.Text) || (Double.Parse(txtChange.Text) < 0))
                {
                    string message = "Insufficient amount. Please enter the correct amount!".ToUpper();
                    MessageBox.Show(message, "POS SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }else
                {
                    int totalRows = this.parentForm.dataGridView1.Rows.Count;
                    for (int i=0; i<totalRows; i++)
                    {
                        // update product table 
                        cn.Open();
                        int soldQty = int.Parse(this.parentForm.dataGridView1.Rows[i].Cells[5].Value.ToString());
                        string pcode = this.parentForm.dataGridView1.Rows[i].Cells[2].Value.ToString();
                        string query = $"UPDATE tblProduct SET qty = (qty - {soldQty}) WHERE pcode = '{pcode}'";
                        cm = new SqlCommand(query, cn);
                        cm.ExecuteNonQuery();
                        cn.Close();

                        // update cart table 
                        cn.Open();
                        int id = int.Parse(this.parentForm.dataGridView1.Rows[i].Cells[1].Value.ToString());
                        query = $"UPDATE tblCart SET status = 'Sold' WHERE id LIKE '{id}'";
                        cm = new SqlCommand(query, cn);
                        cm.ExecuteNonQuery();
                        cn.Close();

                        
                    }

                    // Receipt
                    frmReceipt frm = new frmReceipt(this.parentForm);
                    frm.LoadReport(txtCash.Text, txtChange.Text);
                    frm.ShowDialog();

                    MessageBox.Show("Payment successfully saved!".ToUpper(), "PAYMENT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.parentForm.GetTransno();
                    this.parentForm.LoadCart();
                    this.Dispose();

                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46)
            {
                // accept dot (.) character 
            }
            else if (e.KeyChar == 8)
            {
                // accept backspace 

            } else if (e.KeyChar == 13)
            {
                // allow enter 
                btnEnter.PerformClick();
            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57))   // ascii code 48-57 between 0 - 9
            {
                e.Handled = true;
            }
        }

        private void frmSettle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Dispose();

            } 
        }
    }
}
