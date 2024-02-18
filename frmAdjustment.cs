using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace POS_System
{
    public partial class frmAdjustment : Form
    {
        Form1 form1Ref;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        int _stockQty = 0;
        string[] adjustmentCommands = { "REMOVE FROM INVENTORY", "ADD TO INVENTORY" }; 
        public frmAdjustment(Form1 frm) 
        {
            InitializeComponent();
            this.form1Ref = frm;
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true; 
        }

        public void ReferenceNo()
        {
            Random random = new Random();
            txtRef.Text = random.Next().ToString();
        }

        public void LoadCommands()
        {
            cboAction.Items.Clear(); 

            foreach(string command in adjustmentCommands)
            {
                cboAction.Items.Add(command); 
            }
        }

        public void Clear()
        {
            ReferenceNo();
            txtPcode.Clear();
            txtDescription.Clear();
            txtUser.Clear();
            txtRemarks.Clear();
            txtQty.Clear();
            cboAction.Text = string.Empty;

            // Disable controls 
            txtQty.Enabled = false;
            cboAction.Enabled = false;
            txtRemarks.Enabled = false;
            btnSave.Enabled = false; 
        }

        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            String query = $"SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty FROM tblProduct AS p INNER JOIN tblBrand AS b ON b.id = p.bid INNER JOIN tblCategory as c ON c.id = p.cid WHERE p.pdesc like '%{txtSearch.Text}%' ORDER BY p.pdesc;"; ;
            cm = new SqlCommand(query, cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                
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
                if (int.Parse(txtQty.Text) > this._stockQty)
                {
                    MessageBox.Show("STOCK QUNATITY SHOULD BE GREATER THAN FROM ADJUSTMENT QUANTITY.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }
                else if (!adjustmentCommands.Any(s => (s.Equals(cboAction.Text))))
                {
                    MessageBox.Show("INVALID COMMAND! CHOOSE COMMAND FROM GIVEN OPTIONS ONLY.", "WARNINING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }
                else if (string.IsNullOrEmpty(txtRemarks.Text))
                {
                    MessageBox.Show("PLEASE ENTER REMARKS!", "WARNINING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if ((MessageBox.Show("SAVE ADJUSTMENT?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                {
                    // Check Command 
                    if (cboAction.Text.Equals(this.adjustmentCommands[0]))
                    {
                        // REMOVE FROM INVENTORY
                        ExecuteSQL($"UPDATE tblProduct SET qty = (qty - {int.Parse(txtQty.Text)}) WHERE pcode LIKE '{txtPcode.Text}' ;");


                    } else if (cboAction.Text.Equals(this.adjustmentCommands[1]))
                    {
                        // ADD TO INVENTORY
                        ExecuteSQL($"UPDATE tblProduct SET qty = (qty + {int.Parse(txtQty.Text)}) WHERE pcode LIKE '{txtPcode.Text}' ;"); 
                    }

                    // UPDATE ADJUSTMENT TABLE 
                    ExecuteSQL($"INSERT INTO [dbo].[tblAdjustment] ([referenceno] ,[pcode] ,[qty] ,[action] ,[remarks] ,[sdate] ,[user]) VALUES('{txtRef.Text}', '{txtPcode.Text}', '{txtQty.Text}', '{cboAction.Text}', '{txtRemarks.Text}', '{DateTime.Now}', '{txtUser.Text}') ;");

                    MessageBox.Show("STOCK HAS BEEN SUCCESSFULLY ADJUSTED!", "PROCESS COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    LoadRecords(); 
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            } finally
            {
                cn.Close(); 
            }
        }

        private void ExecuteSQL(string sqlQuery)
        {
            cn.Open();
            cm = new SqlCommand(sqlQuery, cn);
            cm.ExecuteNonQuery();
            cn.Close(); 
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName.Equals("Select"))
            {
                // update data 
                txtUser.Text = form1Ref.lblUser.Text;
                txtPcode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                
                this._stockQty = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                
                // Enable controls 
                LoadCommands();
                txtQty.Enabled = true;
                cboAction.Enabled = true;
                txtRemarks.Enabled = true;
                btnSave.Enabled = true;
            }
            
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tglSwitchDynamicSearch.Checked || e.KeyChar == 13)
            {
                LoadRecords(); 
            } 
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cboAction.Focus();
            }
        }

        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSave.PerformClick();
            }
        }

        private void frmAdjustment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                if ((MessageBox.Show("CLOSE STOCK ADJUSTMENT WINDOW?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                {
                    this.Dispose();
                }
            }
        }

        private void frmAdjustment_Load(object sender, EventArgs e)
        {
            LoadRecords();
            Clear();
        }
    }
}
