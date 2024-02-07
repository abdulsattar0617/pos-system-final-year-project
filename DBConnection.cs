using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POS_System
{
    public class DBConnection
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public string MyConnection()
        {
            string con = @"Data Source=DESKTOP-S0DM0G4;Initial Catalog=POS_DB;Integrated Security=True";
            return con; 
        }

        public double GetGST()
        {
            double gst = 0; 
            cn.ConnectionString = MyConnection();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblGst", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                gst = Double.Parse(dr["gst"].ToString()); 
            }
            dr.Close();
            cn.Close();

            return gst; 
        }

        public double getDailySales()
        {
            double dailySales = 0; 
            cn.ConnectionString = MyConnection();
            cn.Open();
            string date = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}";
            cm = new SqlCommand($"SELECT ISNULL(sum(total), 0) AS 'total' FROM tblCart WHERE sdate BETWEEN '{date}' AND '{date}' AND status LIKE 'Sold'; ", cn);
            dailySales = double.Parse(cm.ExecuteScalar().ToString()); 
            cn.Close();
            return dailySales; 
        }

        public string[] GetUserData(string username)
        {
            SqlDataReader dr;
            string[] userData = new string[4];
            
            try
            {
                cn.ConnectionString = MyConnection();
                cn.Open();
                cm = new SqlCommand($"SELECT * FROM tblUser WHERE username = '{username}'", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    userData[0] = dr["password"].ToString();
                    userData[1] = dr["role"].ToString();
                    userData[2] = dr["name"].ToString();
                    userData[3] = dr["active"].ToString();
                }
                dr.Close();
                cn.Close();

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            } finally
            {
                cn.Close();
            }

            return userData; 
        }

        public string getStoreGSTNo()
        {
            string storeGstNo = string.Empty;
            try
            {

                cn.ConnectionString = MyConnection();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblStore ;", cn);
                SqlDataReader dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    storeGstNo = dr["gstnumber"].ToString();
                }
                cn.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                cn.Close();
            }

            return storeGstNo;
        }

        public string getStoreName()
        {
            string storeName = string.Empty;
            try
            {
                
                cn.ConnectionString = MyConnection();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblStore ;", cn);
                SqlDataReader dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    storeName = dr["store"].ToString();
                }
                cn.Close();

                
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            } finally
            {
                cn.Close(); 
            }

            return storeName;
        }

        public string getStoreAddress()
        {
            string storeAddress = string.Empty;
            try
            {

                cn.ConnectionString = MyConnection();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblStore ;", cn);
                SqlDataReader dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    storeAddress = dr["address"].ToString();
                }
                cn.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                cn.Close();
            }

            return storeAddress;
        }
        public int getProductLine()
        {
            int totalProducts = 0;
            cn.ConnectionString = MyConnection();
            cn.Open();
            cm = new SqlCommand($"SELECT ISNULL(COUNT(*), 0) FROM tblProduct;", cn);
            totalProducts = int.Parse(cm.ExecuteScalar().ToString());
            cn.Close();
            return totalProducts;
        }

        public int getStockOnHand()
        {
            int totalStock  = 0;
            cn.ConnectionString = MyConnection();
            cn.Open();
            cm = new SqlCommand($"SELECT ISNULL(sum(qty), 0) AS 'qty' FROM tblProduct;", cn);
            totalStock = int.Parse(cm.ExecuteScalar().ToString());
            cn.Close();
            return totalStock;
        }
        
        public int getCriticalItemsCount()
        {
            int totalCriticalItems  = 0;
            cn.ConnectionString = MyConnection();
            cn.Open();
            cm = new SqlCommand($"SELECT ISNULL(COUNT(*), 0) FROM vwCriticalItems;", cn);
            totalCriticalItems = int.Parse(cm.ExecuteScalar().ToString());
            cn.Close();
            return totalCriticalItems;
        }

        public string GetPassword(string username)
        {
            string password = string.Empty;

            cn.ConnectionString = MyConnection();
            cn.Open();
            cm = new SqlCommand($"SELECT password FROM tblUser WHERE username = '{username}'", cn);
            dr = cm.ExecuteReader();
            dr.Read(); 
            if (dr.HasRows)
            {
                password = dr["password"].ToString();
            }
            dr.Close();
            cn.Close();

            return password; 
        }
    }
}
