using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace POS_System
{
    public partial class frmReceipt : Form
    {
        SqlConnection cn = new SqlConnection();
        DBConnection dbcon = new DBConnection();
        frmPOS frmPOSInstance;
        public frmReceipt(frmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.frmPOSInstance = frm;
            this.KeyPreview = true; 
        }

        private void frmReceipt_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }

        public void LoadReport(string pcash, string pchange)
        {
            ReportDataSource rptDataSource;
            try
            {
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report1.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 dataSet = new DataSet1();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                cn.Open();
                string query = $"SELECT c.id, c.transno, c.pcode, c.price, c.qty, c.disc, c.total, c.sdate, c.status, p.pdesc FROM tblCart AS c INNER JOIN tblProduct AS p ON  c.pcode = p.pcode WHERE transno LIKE '{frmPOSInstance.lblTransno.Text}';"; 
                dataAdapter.SelectCommand = new SqlCommand(query, cn);

                dataAdapter.Fill(dataSet.Tables["dtSold"]);
                cn.Close();

                // Report Parameters 
                ReportParameter pAmount = new ReportParameter("pAmount", this.frmPOSInstance.lblAmount.Text);
                ReportParameter pGST = new ReportParameter("pGST", this.frmPOSInstance.lblGst.Text);
                ReportParameter pDiscount = new ReportParameter("pDiscount", this.frmPOSInstance.lblDiscount.Text);
                ReportParameter pTotal = new ReportParameter("pTotal", this.frmPOSInstance.lblTotal.Text);
                ReportParameter pCash = new ReportParameter("pCash", pcash);
                ReportParameter pChange = new ReportParameter("pChange", pchange);
                //ReportParameter pStore = new ReportParameter("pStore", this.store);
                ReportParameter pStore = new ReportParameter("pStore", dbcon.getStoreName());
                //ReportParameter pAddress = new ReportParameter("pAddress", this.address); 
                ReportParameter pAddress = new ReportParameter("pAddress", dbcon.getStoreAddress());
                ReportParameter pTransaction = new ReportParameter("pTransaction", $"Invoice #: {this.frmPOSInstance.lblTransno.Text}");
                ReportParameter pCashier = new ReportParameter("pCashier", this.frmPOSInstance.lblUser.Text);
                ReportParameter pGSTNumber = new ReportParameter("pGSTNumber", dbcon.getStoreGSTNo());

                // Setting Report Parameter
                reportViewer1.LocalReport.SetParameters(pAmount);
                reportViewer1.LocalReport.SetParameters(pGST);
                reportViewer1.LocalReport.SetParameters(pDiscount);
                reportViewer1.LocalReport.SetParameters(pTotal);
                reportViewer1.LocalReport.SetParameters(pCash);
                reportViewer1.LocalReport.SetParameters(pChange);
                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);
                reportViewer1.LocalReport.SetParameters(pTransaction);
                reportViewer1.LocalReport.SetParameters(pCashier);
                reportViewer1.LocalReport.SetParameters(pGSTNumber);

                rptDataSource = new ReportDataSource("DataSet1", dataSet.Tables["dtSold"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100; 

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                cn.Close();
            }
        }

        private void frmReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Dispose(); 
            }
        }
    }
}
