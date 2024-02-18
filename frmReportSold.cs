using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace POS_System
{
    public partial class frmReportSold : Form
    {
        SqlConnection cn = new SqlConnection();
        DBConnection dbcon = new DBConnection();
        frmSoldItems soldItemsForm;  
        public frmReportSold(frmSoldItems frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.soldItemsForm = frm; 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        
        public void LoadReport()
        {
            try
            {
                ReportDataSource rptDataSource; 

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report2.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 dataSet = new DataSet1();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                cn.Open();
                string dateFrom = $"{this.soldItemsForm.dt1.Value.Year}-{this.soldItemsForm.dt1.Value.Month}-{this.soldItemsForm.dt1.Value.Day}";
                string dateTo = $"{this.soldItemsForm.dt2.Value.Year}-{this.soldItemsForm.dt2.Value.Month}-{this.soldItemsForm.dt2.Value.Day}";
                string query; 

                if (this.soldItemsForm.cboCashier.Text.Equals(frmSecurity.allCashier))
                {
                    query = $"SELECT c.id, c.transno, c.pcode, p.pdesc , p.price, c.qty, c.disc AS discount, c.total FROM tblCart AS c INNER JOIN tblProduct AS p ON c.pcode = p.pcode WHERE status LIKE 'Sold' AND sdate BETWEEN '{dateFrom}' AND '{dateTo}' ; ";
                } else
                {
                    query = $"SELECT c.id, c.transno, c.pcode, p.pdesc , p.price, c.qty, c.disc AS discount, c.total FROM tblCart AS c INNER JOIN tblProduct AS p ON c.pcode = p.pcode WHERE status LIKE 'Sold' AND sdate BETWEEN '{dateFrom}' AND '{dateTo}' AND cashier LIKE '{soldItemsForm.cboCashier.Text}' ; ";
                }
                
                dataAdapter.SelectCommand = new SqlCommand(query, cn);
                dataAdapter.Fill(dataSet.Tables["dtSoldReport"]);

                cn.Close();

                // Creating Report Parameters 
                ReportParameter pDate = new ReportParameter("pDate", $"Date From: {soldItemsForm.dt1.Value.ToShortDateString()} To: {soldItemsForm.dt2.Value.ToShortDateString()}"); 
                ReportParameter pCashier = new ReportParameter("pCashier", $"{soldItemsForm.cboCashier.Text}"); 
                ReportParameter pHeader = new ReportParameter("pHeader", "SALES REPORT");
                ReportParameter pStore = new ReportParameter("pStore", dbcon.getStoreName());
                ReportParameter pAddress = new ReportParameter("pAddress", dbcon.getStoreAddress());

                // Setting Report Parameter 
                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pCashier);
                reportViewer1.LocalReport.SetParameters(pHeader);
                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);

                rptDataSource = new ReportDataSource("DataSet1", dataSet.Tables["dtSoldReport"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100; 

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                cn.Close(); 
            }
        }
    }
}
