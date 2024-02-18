using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmVoid : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        frmCancelDetails frmCancelDetailRef; 
        public frmVoid(frmCancelDetails frm )
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            frmCancelDetailRef = frm; 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtPass.Equals(string.Empty))
                {
                    string username;
                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tblUser WHERE username = @username AND password = @password; ", cn);
                    cm.Parameters.AddWithValue("@username", txtUser.Text); 
                    cm.Parameters.AddWithValue("@password", txtPass.Text);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        username = dr["username"].ToString(); 
                        dr.Close();
                        cn.Close();
                        SaveCancelOrder(username);


                        string query; 
                        if (frmCancelDetailRef.cboAction.Text.Equals("Yes"))
                        {
                            // update product 
                            query = $"UPDATE tblProduct SET qty = (qty + {int.Parse(frmCancelDetailRef.txtCancelQty.Text)}) WHERE pcode = '{frmCancelDetailRef.txtPCode.Text}' ;";
                            UpdateData(query); 
                        }

                        // update cart 
                        query = $"UPDATE tblCart SET qty = (qty - {int.Parse(frmCancelDetailRef.txtCancelQty.Text)}) WHERE id LIKE '{int.Parse(frmCancelDetailRef.txtID.Text)}' ;";
                        UpdateData(query);

                        MessageBox.Show("Order transaction successfully cancelled!".ToUpper(), "CANCEL ORDER", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                        frmCancelDetailRef.RefreshList(); 
                        frmCancelDetailRef.Dispose(); 
                    }
                    dr.Close(); 
                    cn.Close(); 
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); 

            } finally
            {
                cn.Close(); 
            }
        }

        public void SaveCancelOrder(string username)
        {
            cn.Open();
            cm = new SqlCommand("INSERT INTO tblCancel(transno, pcode, price, qty,  sdate, voidby, cancelledby, reason, action) VALUES(@transno, @pcode, @price, @qty, @sdate, @voidby, @cancelledby, @reason, @action) ;", cn);
            cm.Parameters.AddWithValue("@transno", frmCancelDetailRef.txtTransNo.Text);
            cm.Parameters.AddWithValue("@pcode", frmCancelDetailRef.txtPCode.Text);
            cm.Parameters.AddWithValue("@price", double.Parse(frmCancelDetailRef.txtPrice.Text));
            cm.Parameters.AddWithValue("@qty", int.Parse(frmCancelDetailRef.txtCancelQty.Text));
            cm.Parameters.AddWithValue("@sdate", DateTime.Now); 
            cm.Parameters.AddWithValue("@voidby", username); 
            cm.Parameters.AddWithValue("@cancelledby", frmCancelDetailRef.txtCancel.Text); 
            cm.Parameters.AddWithValue("@reason", frmCancelDetailRef.txtReason.Text); 
            cm.Parameters.AddWithValue("@action", frmCancelDetailRef.cboAction.Text);
            cm.ExecuteNonQuery(); 
            cn.Close(); 
        }

        public void UpdateData(string query )
        {
            cn.Open();
            cm = new SqlCommand(query, cn);
            cm.ExecuteNonQuery(); 
            cn.Close(); 
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnLogin.PerformClick(); 
            }
        }
    }
}
