using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace POS_System
{
    public partial class frmRecords : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        string sortAttribute = "qty";
        string[,] sortOptions = { { "SORT BY QTY", "qty" }, { "SORT BY TOTAL AMOUNT", "total" } };
        public frmRecords()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        public void LoadSortByOptions()
        {
            cboSortBy.Items.Clear();

            for (int i=0; i < 2 ; i++ )
            {
                cboSortBy.Items.Add(sortOptions[i, 0]); 
            }
        }

        public void LoadTopSellingRecord()
        {
            int i = 0;
            cn.Open();
            dataGridViewTopSelling.Rows.Clear();
            string dateFrom = $"{dateFromTopSelling.Value.Year}-{dateFromTopSelling.Value.Month}-{dateFromTopSelling.Value.Day}";
            string dateTo = $"{dateToTopSelling.Value.Year}-{dateToTopSelling.Value.Month}-{dateToTopSelling.Value.Day}";
            
            if (cboSortBy.Text.Equals(this.sortOptions[0,0]))
            {
                sortAttribute = this.sortOptions[0, 1];

            } else if (cboSortBy.Text.Equals(this.sortOptions[1, 0]))
            {
                sortAttribute = this.sortOptions[1, 1];
            }

            string query = $"SELECT TOP 10 pcode, pdesc, ISNULL(SUM(qty), 0) as 'qty', ISNULL(SUM(total), 0) as 'total'  FROM vwSoldItems WHERE sdate BETWEEN '{dateFrom}' AND '{dateTo}' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY {sortAttribute} DESC;";
            cm = new SqlCommand(query, cn);
            dr = cm.ExecuteReader();

            while (dr.Read())
            {
                ++i;
                dataGridViewTopSelling.Rows.Add(i,
                    dr["pcode"].ToString(),
                    dr["pdesc"].ToString(),
                    dr["qty"].ToString(), 
                    double.Parse(dr["total"].ToString()).ToString("#,##0.00")
                );
            }
            dr.Close();
            cn.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadTopSellingRecord(); 
        }

        private void btnLoadSoldItems_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                cn.Open();
                dataGridViewSoldItems.Rows.Clear();
                string dateFrom = $"{dateFromSoldItems.Value.Year}-{dateFromSoldItems.Value.Month}-{dateFromSoldItems.Value.Day}";
                string dateTo = $"{dateToSoldItems.Value.Year}-{dateToSoldItems.Value.Month}-{dateToSoldItems.Value.Day}";
                string query = $"SELECT p.pcode, p.pdesc, c.price, ISNULL(sum(c.qty), 0) AS qty, ISNULL(sum(c.disc), 0) AS disc, ISNULL(sum(c.total), 0)  AS total FROM tblCart AS c  INNER JOIN tblProduct AS p ON c.pcode = p.pcode  WHERE c.status like 'Sold' AND sdate BETWEEN '{dateFrom}' AND '{dateTo}' GROUP BY p.pcode, p.pdesc, c.price, c.status;";
                cm = new SqlCommand(query, cn);
                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    ++i;
                    dataGridViewSoldItems.Rows.Add(i,
                        dr["pcode"].ToString(),
                        dr["pdesc"].ToString(),
                        double.Parse(dr["price"].ToString()).ToString("#,##0.00"),
                        dr["qty"].ToString(), 
                        double.Parse(dr["disc"].ToString()).ToString("#,##0.00"), 
                        double.Parse(dr["total"].ToString()).ToString("#,##0.00")
                        );
                }
                dr.Close();
                cn.Close();


                cn.Open(); 
                query = $"SELECT ISNULL(sum(total), 0) FROM tblCart  WHERE status like 'Sold' AND sdate BETWEEN '{dateFrom}' AND '{dateTo}' ;";
                cm = new SqlCommand(query, cn);
                lblTotal.Text = double.Parse(cm.ExecuteScalar().ToString()).ToString("#,##0.00");
                cn.Close(); 

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            } finally
            {
                cn.Close(); 
            }
        }

        public void LoadSoldItems()
        {
            try
            {
                // TABLE DATA 
                int i = 0;
                cn.Open();
                dataGridViewSoldItems.Rows.Clear();
                string dateFrom = $"{dateFromSoldItems.Value.Year}-{dateFromSoldItems.Value.Month}-{dateFromSoldItems.Value.Day}";
                string dateTo = $"{dateToSoldItems.Value.Year}-{dateToSoldItems.Value.Month}-{dateToSoldItems.Value.Day}";
                string query = $"SELECT p.pcode, p.pdesc, c.price, ISNULL(sum(c.qty), 0) AS qty, ISNULL(sum(c.disc), 0) AS disc, ISNULL(sum(c.total), 0)  AS total FROM tblCart AS c  INNER JOIN tblProduct AS p ON c.pcode = p.pcode  WHERE c.status like 'Sold' AND sdate BETWEEN '{dateFrom}' AND '{dateTo}' GROUP BY p.pcode, p.pdesc, c.price, c.status;";
                cm = new SqlCommand(query, cn);
                dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    ++i;
                    dataGridViewSoldItems.Rows.Add(i,
                        dr["pcode"].ToString(),
                        dr["pdesc"].ToString(),
                        double.Parse(dr["price"].ToString()).ToString("#,##0.00"),
                        dr["qty"].ToString(),
                        double.Parse(dr["disc"].ToString()).ToString("#,##0.00"),
                        double.Parse(dr["total"].ToString()).ToString("#,##0.00")
                        );
                }
                dr.Close();
                cn.Close();

                // TOTAL 
                cn.Open();
                query = $"SELECT ISNULL(sum(total), 0) FROM tblCart  WHERE status like 'Sold' AND sdate BETWEEN '{dateFrom}' AND '{dateTo}' ;";
                cm = new SqlCommand(query, cn);
                lblTotal.Text = double.Parse(cm.ExecuteScalar().ToString()).ToString("#,##0.00");
                cn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                cn.Close();
            }
        }

        public void LoadInventory()
        {
            int i = 0; 
            dataGridView4.Rows.Clear(); 
            cn.Open();
            string query = "SELECT pcode, barcode, pdesc, b.brand, c.category, price, reorder, qty FROM tblProduct AS p inner join tblBrand AS b ON b.id = p.bid INNER JOIN tblcategory AS c ON c.id = p.cid ;";
            cm = new SqlCommand(query, cn);
            dr = cm.ExecuteReader(); 
            while (dr.Read())
            {
                dataGridView4.Rows.Add(++i,
                    dr[0].ToString(),
                    dr[1].ToString(),
                    dr[2].ToString(),
                    dr[3].ToString(),
                    dr[4].ToString(),
                    double.Parse(dr[5].ToString()).ToString("#,##0.00"),
                    int.Parse(dr[6].ToString()),
                    int.Parse(dr[7].ToString())
                );

            }
            dr.Close(); 
            cn.Close(); 
        }

        public void LoadCriticalItems()
        {
            try
            {
                int i = 0; 
                cn.Open();
                dataGridView3.Rows.Clear(); 
                cm = new SqlCommand("SELECT * FROM vwCriticalItems ;", cn);
                dr = cm.ExecuteReader(); 
                while (dr.Read())
                {
                    dataGridView3.Rows.Add(++i,
                        dr[0].ToString(),
                        dr[1].ToString(),
                        dr[2].ToString(),
                        dr[3].ToString(),
                        dr[4].ToString(),
                        double.Parse(dr[5].ToString()).ToString("#,##0.00"),
                        int.Parse(dr[6].ToString()), 
                        int.Parse(dr[7].ToString()) 
                    ) ; 
                }
                cn.Close(); 

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            } finally
            {
                cn.Close(); 
            }
        }

        private void linkInventoryList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInventoryReport frm = new frmInventoryReport();
            frm.LoadInventory();
            frm.ShowDialog(); 
        }

        public void LoadCancelledOrders()
        {
            dataGridViewCancelledOrder.Rows.Clear();
            cn.Open();
            int i = 0; 
            string dateFrom = $"{dateFromCancelledOrder.Value.Year}-{dateFromCancelledOrder.Value.Month}-{dateFromCancelledOrder.Value.Day}"; 
            string dateTo = $"{dateToCancelledOrder.Value.Year}-{dateToCancelledOrder.Value.Month}-{dateToCancelledOrder.Value.Day}"; 
            cm = new SqlCommand($"SELECT * FROM vwCancelledOrder WHERE sdate BETWEEN '{dateFrom}' AND '{dateTo}' ;", cn);
            dr = cm.ExecuteReader(); 
            while (dr.Read())
            {
                ++i;
                dataGridViewCancelledOrder.Rows.Add(i,
                        dr["transno"].ToString(),
                        dr["pcode"].ToString(),
                        dr["pdesc"].ToString(),
                        double.Parse(dr["price"].ToString()).ToString("#,##0.00"),
                        int.Parse(dr["qty"].ToString()),
                        double.Parse(dr["total"].ToString()).ToString("#,##0.00"),
                        DateTime.Parse(dr["sdate"].ToString()).ToShortDateString(),
                        dr["voidby"].ToString(),
                        dr["cancelledby"].ToString(),
                        dr["reason"].ToString(),
                        dr["action"].ToString() 
                    );
            }
            dr.Close(); 
            cn.Close(); 
        }

        public void LoadStockInHistory()
        {
            int i = 0;
            dataGridViewTopSelling.Rows.Clear();
            cn.Open();
            String status = "Done";
            String query;

            String dateFrom = $"{dateFromStockInHistory.Value.Year}-{dateFromStockInHistory.Value.Month}-{dateFromStockInHistory.Value.Day}";
            String dateTo = $"{dateToStockInHistory.Value.Year}-{dateToStockInHistory.Value.Month}-{dateToStockInHistory.Value.Day}";

            query = $"SELECT * FROM vwStockIn WHERE CAST(sdate as DATE) BETWEEN '{dateFrom}' AND '{dateTo}' AND status LIKE '{status}';";
            cm = new SqlCommand(query, cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dateGridViewStockInHistory.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), DateTime.Parse(dr[5].ToString()).ToShortDateString(), dr[6].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnLoadCancelled_Click(object sender, EventArgs e)
        {
            LoadCancelledOrders(); 
        }

        private void btnLoadStockInHistory_Click(object sender, EventArgs e)
        {
            LoadStockInHistory(); 
        }

        private void linkTopSelling_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string dateFrom = $"{dateFromTopSelling.Value.Year}-{dateFromTopSelling.Value.Month}-{dateFromTopSelling.Value.Day}";
            string dateTo = $"{dateToTopSelling.Value.Year}-{dateToTopSelling.Value.Month}-{dateToTopSelling.Value.Day}";
            string query = $"SELECT TOP 10 pcode, pdesc, ISNULL(sum(qty), 0) as 'qty', ISNULL(sum(total), 0) as 'total'  FROM vwSoldItems WHERE sdate BETWEEN '{dateFrom}' AND '{dateTo}' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY {this.sortAttribute} DESC;";
            string sellingPeriod = $"FROM: {dateFromTopSelling.Value}    TO: {dateToTopSelling.Value}";

            frmInventoryReport frm = new frmInventoryReport();
            frm.LoadTopSelling(query, sellingPeriod, $"TOP SELLING ITEMS {this.sortOptions[cboSortBy.SelectedIndex, 0]}");
            frm.ShowDialog();
        }

        private void linkSoldItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string dateFrom = $"{dateFromSoldItems.Value.Year}-{dateFromSoldItems.Value.Month}-{dateFromSoldItems.Value.Day}";
            string dateTo = $"{dateToSoldItems.Value.Year}-{dateToSoldItems.Value.Month}-{dateToSoldItems.Value.Day}";
            string query = $"SELECT p.pcode, p.pdesc, c.price, ISNULL(sum(c.qty), 0) AS qty, ISNULL(sum(c.disc), 0) AS disc, ISNULL(sum(c.total), 0)  AS total FROM tblCart AS c  INNER JOIN tblProduct AS p ON c.pcode = p.pcode  WHERE c.status like 'Sold' AND sdate BETWEEN '{dateFrom}' AND '{dateTo}' GROUP BY p.pcode, p.pdesc, c.price, c.status;";
            string sellingPeriod = $"FROM: {dateFromSoldItems.Value}   TO: {dateToSoldItems.Value}";

            frmInventoryReport frm = new frmInventoryReport();
            frm.LoadSoldItems(query, sellingPeriod);
            frm.ShowDialog(); 
        }

        private void linkCriticalStocks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            string query = $"SELECT * FROM vwCriticalItems ;";
            string date = $"DATE: {DateTime.Now}";

            frmInventoryReport frm = new frmInventoryReport();
            frm.LoadCriticalStocks(query, date);
            frm.ShowDialog();
        }

        private void linkCancelledOrder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string query = $"SELECT * FROM vwCancelledOrder ;";
            string date = $"DATE: {DateTime.Now}";

            frmInventoryReport frm = new frmInventoryReport();
            frm.LoadCancelledOrder(query, date);
            frm.ShowDialog();
        }

        private void linkLoadDataTopSelling_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(cboSortBy.Text))
            {
                MessageBox.Show("PLEASE SELECT SORT ORDER FROM DROP DOWN LIST.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return; 
            }
            LoadTopSellingRecord();
            LoadChartTopSelling(); 
        }

        public void LoadChartTopSelling()
        {
            string dateFrom = $"{dateFromTopSelling.Value.Year}-{dateFromTopSelling.Value.Month}-{dateFromTopSelling.Value.Day}";
            string dateTo = $"{dateToTopSelling.Value.Year}-{dateToTopSelling.Value.Month}-{dateToTopSelling.Value.Day}";

            if (cboSortBy.Text.Equals(this.sortOptions[0, 0]))
            {
                sortAttribute = this.sortOptions[0, 1];
            }
            else if (cboSortBy.Text.Equals(this.sortOptions[1, 0]))
            {
                sortAttribute = this.sortOptions[1, 1];
            }

            string query = $"SELECT TOP 10 pcode, pdesc, ISNULL(SUM(qty), 0) as 'qty', ISNULL(SUM(total), 0) as 'total'  FROM vwSoldItems WHERE sdate BETWEEN '{dateFrom}' AND '{dateTo}' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY {sortAttribute} DESC;";

            cn.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, cn);

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "TOPSELLING");
            chartTopSelling.DataSource = dataSet.Tables["TOPSELLING"];
            Series series = chartTopSelling.Series[0];
            series.ChartType = SeriesChartType.Doughnut;

            series.Name = "TOP SELLING";

            var chart = chartTopSelling;
            chart.Series[0].XValueMember = "pcode";
            chart.Series[0].YValueMembers = this.sortOptions[cboSortBy.SelectedIndex, 1];

            if (cboSortBy.SelectedIndex == 0)
            {
                // format label for decimal 
                chart.Series[0].LabelFormat = "{#,##0}";

            } else if (cboSortBy.SelectedIndex == 1)
            {
                // format label for floating point 
                chart.Series[0].LabelFormat = "{#,##0.00}"; 
            }

            chart.Series[0].IsValueShownAsLabel = true;

            cn.Close(); 
        }

        private void linkLoadDataSoldItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadSoldItems();
        }

        private void linkLoadDataCancelledOrder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadCancelledOrders();
        }

        private void linkLoadDataStockInHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadStockInHistory();
        }

        private void cboTopSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; 
        }

        

        private void frmRecords_Load(object sender, EventArgs e)
        {
            LoadSortByOptions();
        }

        private void cboSortBy_TextChanged(object sender, EventArgs e)
        {
            this.sortAttribute = sortOptions[cboSortBy.SelectedIndex, 1];
            LoadTopSellingRecord();
            LoadChartTopSelling(); 
        }

        

        private void linkChartSoldItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmChart frm = new frmChart(); 
            frm.lblTitle.Text = $"SOLD ITEMS [{dateFromSoldItems.Value.ToShortDateString()} - {dateToSoldItems.Value.ToShortDateString()}]";

            string dateFrom = $"{dateFromSoldItems.Value.Year}-{dateFromSoldItems.Value.Month}-{dateFromSoldItems.Value.Day}";
            string dateTo = $"{dateToSoldItems.Value.Year}-{dateToSoldItems.Value.Month}-{dateToSoldItems.Value.Day}";
            string query = $"SELECT p.pcode, p.pdesc, ISNULL(sum(c.total), 0)  AS total FROM tblCart AS c  INNER JOIN tblProduct AS p ON c.pcode = p.pcode  WHERE c.status like 'Sold' AND sdate BETWEEN '{dateFrom}' AND '{dateTo}' GROUP BY p.pcode, p.pdesc ORDER BY total DESC;";

            frm.LoadChartSoldItems(query); 

            frm.ShowDialog(); 
        }

        private void linkStockInHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String status = "Done";
            String query;

            String dateFrom = $"{dateFromStockInHistory.Value.Year}-{dateFromStockInHistory.Value.Month}-{dateFromStockInHistory.Value.Day}";
            String dateTo = $"{dateToStockInHistory.Value.Year}-{dateToStockInHistory.Value.Month}-{dateToStockInHistory.Value.Day}";

            query = $"SELECT * FROM vwStockIn WHERE CAST(sdate as DATE) BETWEEN '{dateFrom}' AND '{dateTo}' AND status LIKE '{status}';";
            
            frmInventoryReport frm = new frmInventoryReport();
            frm.LoadStockIn(query, $"[ FROM: {dateFrom}    TO: {dateTo} ]");
            frm.ShowDialog();
        }
    }
}
