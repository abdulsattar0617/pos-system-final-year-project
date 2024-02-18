using System;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmCancelDetails : Form
    {
        frmSoldItems frmSoldItemsRef; 
        public frmCancelDetails(frmSoldItems frm)
        {
            InitializeComponent();
            frmSoldItemsRef = frm; 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void cboAction_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;  
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateFields())
                {
                    frmVoid frm = new frmVoid(this);
                    frm.ShowDialog();
                }

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
        }

        public void RefreshList()
        {
            this.frmSoldItemsRef.LoadRecord(); 
        }

        private bool ValidateFields()
        {
            if (txtCancelQty.Text.Equals(string.Empty)) return false; 
            if (cboAction.Text.Equals(string.Empty)) return false; 
            if (txtReason.Text.Equals(string.Empty)) return false;

            if ((int.Parse(txtQty.Text)) < (int.Parse(txtCancelQty.Text)) || (int.Parse(txtCancelQty.Text)) < 0)
            {
                MessageBox.Show("Invalid cancel quantity!".ToUpper(), "POS SYSTEM", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; 
            }

            return true; 
        }
    }
}
