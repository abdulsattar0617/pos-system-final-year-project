using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Tulpep.NotificationWindow; 

namespace POS_System
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr; 
        public Form1()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            NotifyCriticalItems();
            this.KeyPreview = true; 
        }

        public void NotifyCriticalItems()
        {
            int count = 0;  
            cn.Open();
            cm = new SqlCommand("SELECT ISNULL(COUNT(*), 0) FROM vwCriticalItems ;", cn);
            count = int.Parse(cm.ExecuteScalar().ToString());
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

            if (count > 0)
            {
                PopupNotifier popUp = new PopupNotifier();
                popUp.Image = Properties.Resources.error;
                popUp.TitleText = $"{count} CRITICAL ITEM(S)";
                popUp.ContentText = critical;
                popUp.Popup();
            }
        }
        private void btnBrand_Click(object sender, EventArgs e)
        {
            frmBrandList frm = new frmBrandList();
            frm.TopLevel = false;
            panel4.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            frmCategoryList frm = new frmCategoryList();
            frm.TopLevel = false;
            panel4.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadCategory();
            frm.Show();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            frmProductList frm = new frmProductList();
            frm.TopLevel = false;
            panel4.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadRecords(); 
            frm.Show();
        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            frmStockIn frm = new frmStockIn();
            frm.TopLevel = false;
            panel4.Controls.Add(frm);
            frm.BringToFront(); 
            frm.Show(); 
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmUserAccount frm = new frmUserAccount();
            frm.TopLevel = false;
            panel4.Controls.Add(frm);
            frm.BringToFront();
            frm.Show(); 
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            frmSoldItems frm = new frmSoldItems();
            frm.ShowDialog(); 
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            frmRecords frm = new frmRecords();
            frm.TopLevel = false;
            frm.LoadCriticalItems();
            frm.LoadInventory(); 
            frm.LoadCancelledOrders(); 
            frm.LoadStockInHistory(); 
            panel4.Controls.Add(frm);
            frm.BringToFront();
            frm.Show(); 
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("LOGOUT APPLICATION?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                frmSecurity frm = new frmSecurity();
                frm.ShowDialog();
            }
        }

        private void btnStoreSetting_Click(object sender, EventArgs e)
        {
            frmStore frm = new frmStore();
            frm.LoadRecords(); 
            frm.ShowDialog(); 
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            frmDashboard frm = new frmDashboard();
            frm.TopLevel = false;
            panel4.Controls.Add(frm);
            frm.lblDailySales.Text = dbcon.getDailySales().ToString("#,##0.00");
            frm.lblProductLine.Text = dbcon.getProductLine().ToString();
            frm.lblStockOnHand.Text = dbcon.getStockOnHand().ToString();
            frm.lblCriticalItems.Text = dbcon.getCriticalItemsCount().ToString(); 
            frm.BringToFront();
            frm.Show();
        }

        private void btnVendor_Click(object sender, EventArgs e)
        {
            frmVendorList frm = new frmVendorList();
            frm.TopLevel = false;
            panel4.Controls.Add(frm);
            frm.LoadVendorList(); 
            frm.BringToFront();
            frm.Show();
        }

        private void btnAdjustment_Click(object sender, EventArgs e)
        {
            frmAdjustment frm = new frmAdjustment(this); 
            frm.ShowDialog(); 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                btnLogout.PerformClick();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            btnDashboard.PerformClick();
        }
    }
}
