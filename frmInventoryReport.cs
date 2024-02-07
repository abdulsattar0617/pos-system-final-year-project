using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace POS_System
{
    public partial class frmInventoryReport : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        public frmInventoryReport()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Dispose(); 
        }

        private void frmInventoryReport_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        public void LoadInventory()
        {
            try
            {
                ReportDataSource rptDataSource;

                // Fetch report format file 
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report3.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 dataSet = new DataSet1();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                // Fetch data from DB into data adapter 
                cn.Open();
                string query = "SELECT pcode, barcode, pdesc, b.brand, c.category, price, reorder, qty FROM tblProduct AS p inner join tblBrand AS b ON b.id = p.bid INNER JOIN tblcategory AS c ON c.id = p.cid ;";
                dataAdapter.SelectCommand = new SqlCommand(query, cn);
                // fill data into data set table 
                dataAdapter.Fill(dataSet.Tables["dtInventory"]); 
                cn.Close();

                // Initialize report parameters
                ReportParameter pStore = new ReportParameter("pStore", dbcon.getStoreName());
                ReportParameter pAddress = new ReportParameter("pAddress", dbcon.getStoreAddress());
                // Setting Report Parameter 
                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);

                // Prepare report data source
                rptDataSource = new ReportDataSource("DataSet1", dataSet.Tables["dtInventory"]);
                // Set Data set for the Report 
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);
                // Set display mode 
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100; 

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                cn.Close(); 
            }
        }

        public void LoadTopSelling(string sql, string sellingPeriod, string header)
        {
            try
            {
                // Declare report data source 
                ReportDataSource rptDataSource;

                // initialize report format 
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\rptTop.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 dataSet = new DataSet1();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                cn.Open();
                dataAdapter.SelectCommand = new SqlCommand(sql, cn);
                dataAdapter.Fill(dataSet.Tables["dtTopSelling"]);
                cn.Close();

                // Initialize report parameters 
                ReportParameter pHeader = new ReportParameter("pHeader", header);
                ReportParameter pDate = new ReportParameter("pDate", sellingPeriod);
                ReportParameter pStore = new ReportParameter("pStore", dbcon.getStoreName());
                ReportParameter pAddress = new ReportParameter("pAddress", dbcon.getStoreAddress());
                // Setting Report Parameter 
                reportViewer1.LocalReport.SetParameters(pHeader);
                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);

                // Initliaze report data source 
                rptDataSource = new ReportDataSource("DataSet1", dataSet.Tables["dtTopSelling"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cn.Close();
            }
        }

        public void LoadSoldItems(string sql, string sellingPeriod)
        {
            try
            {
                // Declare report data source 
                ReportDataSource rptDataSource;

                // initialize report format 
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\rptSoldItems.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 dataSet = new DataSet1();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                cn.Open();
                dataAdapter.SelectCommand = new SqlCommand(sql, cn);
                dataAdapter.Fill(dataSet.Tables["dtSoldItems"]);
                cn.Close();

                // Initialize report parameters 
                ReportParameter pDate = new ReportParameter("pDate", sellingPeriod);
                ReportParameter pStore = new ReportParameter("pStore", dbcon.getStoreName());
                ReportParameter pAddress = new ReportParameter("pAddress", dbcon.getStoreAddress());
                // Setting Report Parameter 
                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);

                // Initliaze report data source 
                rptDataSource = new ReportDataSource("DataSet1", dataSet.Tables["dtSoldItems"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cn.Close();
            }
        }

        public void LoadCriticalStocks(string sql, string date)
        {
            try
            {
                // Declare report data source 
                ReportDataSource rptDataSource;

                // initialize report format 
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\rptCriticalStocks.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 dataSet = new DataSet1();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                cn.Open();
                dataAdapter.SelectCommand = new SqlCommand(sql, cn);
                dataAdapter.Fill(dataSet.Tables["dtCriticalStocks"]);
                cn.Close();

                // Initialize report parameters 
                ReportParameter pDate = new ReportParameter("pDate", date);
                ReportParameter pStore = new ReportParameter("pStore", dbcon.getStoreName());
                ReportParameter pAddress = new ReportParameter("pAddress", dbcon.getStoreAddress());
                // Setting Report Parameter 
                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);

                // Initliaze report data source 
                rptDataSource = new ReportDataSource("DataSet1", dataSet.Tables["dtCriticalStocks"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cn.Close();
            }
        }

        public void LoadCancelledOrder(string sql, string date)
        {
            try
            {
                // Declare report data source 
                ReportDataSource rptDataSource;

                // initialize report format 
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\rptCancelledOrders.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 dataSet = new DataSet1();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                cn.Open();
                dataAdapter.SelectCommand = new SqlCommand(sql, cn);
                dataAdapter.Fill(dataSet.Tables["dtCancelledOrders"]);
                cn.Close();

                // Initialize report parameters 
                ReportParameter pDate = new ReportParameter("pDate", date);
                ReportParameter pStore = new ReportParameter("pStore", dbcon.getStoreName());
                ReportParameter pAddress = new ReportParameter("pAddress", dbcon.getStoreAddress());
                // Setting Report Parameter 
                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);

                // Initliaze report data source 
                rptDataSource = new ReportDataSource("DataSet1", dataSet.Tables["dtCancelledOrders"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 75;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cn.Close();
            }
        }

        public void LoadStockIn(string sql, string date)
        {
            try
            {
                // Declare report data source 
                ReportDataSource rptDataSource;

                // initialize report format 
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\rptStockIn.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 dataSet = new DataSet1();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                cn.Open();
                dataAdapter.SelectCommand = new SqlCommand(sql, cn);
                dataAdapter.Fill(dataSet.Tables["dtStockIn"]);
                cn.Close();

                // Initialize report parameters 
                ReportParameter pDate = new ReportParameter("pDate", date);
                ReportParameter pStore = new ReportParameter("pStore", dbcon.getStoreName());
                ReportParameter pAddress = new ReportParameter("pAddress", dbcon.getStoreAddress());
                // Setting Report Parameter 
                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pStore);
                reportViewer1.LocalReport.SetParameters(pAddress);

                // Initliaze report data source 
                rptDataSource = new ReportDataSource("DataSet1", dataSet.Tables["dtStockIn"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSource);
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
