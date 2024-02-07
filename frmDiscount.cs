using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmDiscount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        String stitle = "POS SYSTEM";
        frmPOS parentForm;
        public frmDiscount(frmPOS frm)
        {
            InitializeComponent();
            this.parentForm = frm;
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true; 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtPercent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Double.Parse(txtPercent.Text) > 100)
                {
                    txtPercent.Text = "100";
                }
                if (Double.Parse(txtPercent.Text) < 0)
                {
                    txtPercent.Text = "0";
                }
                double discount = (Double.Parse(txtTotalAmount.Text) * (Double.Parse(txtPercent.Text) / 100.0d));
                //double discount = Double.Parse(txtTotalAmount.Text) * Double.Parse(txtPercent.Text);
                txtAmount.Text = discount.ToString("#,##0.00");
            } catch(Exception ex)
            {
                txtAmount.Text = "0.00";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string question = "Add discount? Click yes to confirm.".ToUpper();
                if (MessageBox.Show(question, stitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    string query = "UPDATE tblCart SET disc = @disc, disc_percent = @disc_percent WHERE id = @id";
                    //string query = $"UPDATE tblCart SET disc = {Double.Parse(txtAmount.Text)} WHERE id = {int.Parse(lblID.Text)}";
                    cm = new SqlCommand(query, cn);
                    cm.Parameters.AddWithValue("@disc", Double.Parse(txtAmount.Text));
                    cm.Parameters.AddWithValue("@disc_percent", Double.Parse(txtPercent.Text));
                    cm.Parameters.AddWithValue("@id", int.Parse(lblID.Text));
                    cm.ExecuteNonQuery();
                    this.parentForm.LoadCart();
                    this.pictureBox1_Click(this, EventArgs.Empty); // close discount window
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, stitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } finally
            {
                cn.Close();
            }

        }

        private void txtPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46)
            {
                // accept dot (.) character 

            }
            else if (e.KeyChar == 8)
            {
                // accept backspace 
            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57))   // ascii code 48-57 between 0 - 9
            {
                e.Handled = true;
            }

            else if (e.KeyChar == 13)
            {
                if (txtPercent.Text.Equals(String.Empty))
                {
                    txtPercent.Text = "0";
                }
                btnSave.PerformClick();
            }
        }

        private void frmDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Dispose();
            } else if (e.KeyCode.Equals(Keys.Enter))
            {
                if (txtPercent.Text.Equals(String.Empty))
                {
                    txtPercent.Text = "0";
                }
                btnSave.PerformClick();
            }
        }
    }
}
