using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MOweb.Models
{
    public class GetData
    {
        public static string _Data_Source = "call2";
        public static string _Initial_Catalog = "callcenter";
        public static string CS_DB = "Initial Catalog=callcenter;Data Source=call2;Password=Sl(dfvnj72343ksdff;Persist Security Info=True;User ID=AutoCall_Check";

        private static string sqlConnectMO1;
        public static string SqlConnectMO1 { get => sqlConnectMO1; set => sqlConnectMO1 = value; }

        public static SqlConnection conn_db = new SqlConnection(CS_DB);
        public static SqlDataAdapter da_db = new SqlDataAdapter();

        public static DataSet GetDS()
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(CS_DB))
                {
                    connection.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("exec[dbo].[Ts1]", connection);
                    SqlDataAdapter da = new SqlDataAdapter("[dbo].[kpv_AutoCall_Check]", connection);
                    da.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        public static DataSet Get_user_uic(string user)
        {
            string sel = " @user@";

            sel = sel.Replace("@user@", user);
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(GetData.SqlConnectMO1))
                {
                    connection.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("exec[dbo].[Ts1]", connection);
                    SqlDataAdapter da = new SqlDataAdapter("exec [dbo].[cc_login_ListOfEmployees]" + sel, connection);
                    da.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        public static DataSet Get_MRP_Work_Schedule(string mrpid, string mes, string god)
        {
            string sel = " '2','@mrpid@', '@mes@', '@god@'";
            sel = sel.Replace("@mrpid@", mrpid);
            sel = sel.Replace("@mes@", mes);
            sel = sel.Replace("@god@", god);
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(GetData.SqlConnectMO1))
                {
                    connection.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("exec[dbo].[Ts1]", connection);
                    SqlDataAdapter da = new SqlDataAdapter("exec [dbo].[mrp_report_Work_Schedule]" + sel, connection);
                    da.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        public static DataSet MRP_Insert_Day(string mrpid, string day, string wid, string tbeg, string tend, string tcut, string log1, string log2)
        {
            string sel = " '@mrpid@', '@day@', '@wid@', '@tbeg@', '@tend@', '@tcut@', '@log1@', '@log2@'";
            sel = sel.Replace("@mrpid@", mrpid);
            sel = sel.Replace("@day@", day);
            sel = sel.Replace("@wid@", wid);
            sel = sel.Replace("@tbeg@", tbeg);
            sel = sel.Replace("@tend@", tend);
            sel = sel.Replace("@tcut@", tcut);
            sel = sel.Replace("@log1@", log1);
            sel = sel.Replace("@log2@", log2);
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(GetData.SqlConnectMO1))
                {
                    connection.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("exec[dbo].[Ts1]", connection);
                    SqlDataAdapter da = new SqlDataAdapter("exec [dbo].[mrp_WharehouseScheduler_basedays_insert]" + sel, connection);
                    da.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        public static DataSet MRP_Delete_Day(string mrpid, string day, string log1, string log2)
        {
            string sel = " '@mrpid@', '@day@', '@log1@', '@log2@'";
            sel = sel.Replace("@mrpid@", mrpid);
            sel = sel.Replace("@day@", day);
            sel = sel.Replace("@log1@", log1);
            sel = sel.Replace("@log2@", log2);
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(GetData.SqlConnectMO1))
                {
                    connection.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("exec[dbo].[Ts1]", connection);
                    SqlDataAdapter da = new SqlDataAdapter("exec [dbo].[mrp_WharehouseScheduler_basedays_delete]" + sel, connection);
                    da.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        public static DataSet MRP_Insert_Date(string mrpid, string dat, string wid, string tbeg, string tend, string tcut, string log1, string log2)
        {
            string sel = " '@mrpid@', '@dat@', '@wid@', '@tbeg@', '@tend@', '@tcut@', '@log1@', '@log2@'";
            sel = sel.Replace("@mrpid@", mrpid);
            sel = sel.Replace("@dat@", dat);
            sel = sel.Replace("@wid@", wid);
            sel = sel.Replace("@tbeg@", tbeg);
            sel = sel.Replace("@tend@", tend);
            sel = sel.Replace("@tcut@", tcut);
            sel = sel.Replace("@log1@", log1);
            sel = sel.Replace("@log2@", log2);
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(GetData.SqlConnectMO1))
                {
                    connection.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("exec[dbo].[Ts1]", connection);
                    SqlDataAdapter da = new SqlDataAdapter("exec [dbo].[mrp_WharehouseScheduler_specdays_insert]" + sel, connection);
                    da.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        public static DataSet MRP_Delete_Specdate(string mrpid, string dat, string log1, string log2)
        {
            string sel = " '@mrpid@', '@dat@', '@log1@', '@log2@'";
            sel = sel.Replace("@mrpid@", mrpid);
            sel = sel.Replace("@dat@", dat);
            sel = sel.Replace("@log1@", log1);
            sel = sel.Replace("@log2@", log2);
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(GetData.SqlConnectMO1))
                {
                    connection.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("exec[dbo].[Ts1]", connection);
                    SqlDataAdapter da = new SqlDataAdapter("exec [dbo].[mrp_WharehouseScheduler_specdays_delete]" + sel, connection);
                    da.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        public static DataSet MRP_Update_CitAlll(string mrpid, string tbeg, string tend, string tcut, string log1, string log2)
        {
            string sel = " '@mrpid@', '@tbeg@','@tend@','@tcut@', '@log1@', '@log2@'";
            sel = sel.Replace("@mrpid@", mrpid);
            sel = sel.Replace("@tbeg@", tbeg);
            sel = sel.Replace("@tend@", tend);
            sel = sel.Replace("@tcut@", tcut);
            sel = sel.Replace("@log1@", log1);
            sel = sel.Replace("@log2@", log2);
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(GetData.SqlConnectMO1))
                {
                    connection.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("exec[dbo].[Ts1]", connection);
                    SqlDataAdapter da = new SqlDataAdapter("exec [dbo].[mrp_WharehouseScheduler_cut_update]" + sel, connection);
                    da.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        public static DataSet Get_name_MRP()
        {
            string sel = " '1','1', '1', '2020'";
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(GetData.SqlConnectMO1))
                {
                    connection.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("exec[dbo].[Ts1]", connection);
                    SqlDataAdapter da = new SqlDataAdapter("exec [dbo].[mrp_report_Work_Schedule]" + sel, connection);
                    da.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        public static DataSet GetDS_For_Graph()
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(GetData.SqlConnectMO1))
                {
                    connection.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("exec[dbo].[Ts1]", connection);
                    SqlDataAdapter da = new SqlDataAdapter("exec [dbo].[GetDataForGraph_CallCheck] 2", connection);
                    da.Fill(ds);
                    connection.Close();
                }
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        public static string SecToStr(string str)
        {
            string s = str;
            double f = Convert.ToDouble(s.Replace(".", ","));
            int f1 = (int)f;
            double f2 = f - (int)f;
            TimeSpan TS = new TimeSpan(0, 0, f1);
            if (f2.ToString().IndexOf(",") > -1)
                return ((TS.ToString() + Math.Round(f2, 3).ToString().Replace("0,", ".").Replace("0.", ".")) + "000").Substring(0, 12);
            else
                return TS.ToString() + ".000";
        }
    }
}

