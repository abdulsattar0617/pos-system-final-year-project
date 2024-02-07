using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace POS_System
{
    public partial class frmDashboard : Form
    {
        SqlConnection cn = new SqlConnection();
        DBConnection dbcon = new DBConnection();

        public frmDashboard()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadChart(); 
        }

        private void frmDashboard_Resize(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2; 
        }

        public void LoadChart()
        {
            cn.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT YEAR(sdate) AS 'year', ISNULL(SUM(total), 0.0) AS 'total' FROM tblCart WHERE status LIKE 'Sold' GROUP BY YEAR(sdate) ;", cn);
            DataSet dataSet = new DataSet();
            
            dataAdapter.Fill(dataSet, "Sales");
            chart1.DataSource = dataSet.Tables["Sales"];
            Series series1 = chart1.Series["Series1"];
            series1.ChartType = SeriesChartType.Doughnut;

            series1.Name = "SALES";

            var chart = chart1;
            chart.Series[series1.Name].XValueMember = "year";
            chart.Series[series1.Name].YValueMembers = "total";
            chart.Series[0].IsValueShownAsLabel = true;  

            cn.Close();
        }
    }
}
