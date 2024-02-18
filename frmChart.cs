using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace POS_System
{
    public partial class frmChart : Form
    {
        SqlConnection cn = new SqlConnection();
        DBConnection dbcon = new DBConnection();
        public frmChart()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        public void LoadChartSoldItems(string sql) 
        {
            SqlDataAdapter dataAdapter;
            DataSet dataSet = new DataSet(); 

            cn.Open();
            dataAdapter = new SqlDataAdapter(sql, cn);
            dataAdapter.Fill(dataSet, "SOLD");
            chart.DataSource = dataSet.Tables["SOLD"];
            
            Series series = chart.Series[0];
            series.ChartType = SeriesChartType.Doughnut;

            series.Name = "SOLD ITEMS";
            chart.Series[0].XValueMember = "pdesc"; 
            chart.Series[0].YValueMembers = "total";
            chart.Series[0].LabelFormat = "{#,##0.00}";
            chart.Series[0].IsValueShownAsLabel = true;
            //chart.Series[0]["PieLabelStyle"] = "Outside";
            //chart.Series[0].BorderColor = Color.Gray;
            cn.Close();
        }
    }
}
