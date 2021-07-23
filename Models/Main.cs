using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace MOweb.Models
{
    public static class Main
    {
        public static string GetdMRPOrderInfo(int OrderNo)
        {
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter($"exec MO..GetMRPOrderInfo @OrderNo={OrderNo}", connection);
                custAdapter.Fill(customDataSet, "MRP_OrderInfo");
                connection.Close();
            }
            return JsonConvert.SerializeObject(customDataSet);
        }

        public static string GetMRPRouteRegistryDifference(DateTime OnDate)
        {
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter($"exec MO..cc_MRPRouteRegistryDifference @OnDate='{OnDate.ToString("yyyyMMdd")}'", connection);
                custAdapter.Fill(customDataSet, "MRPRouteRegistryDifference");
                connection.Close();
            }
            return JsonConvert.SerializeObject(customDataSet);
        }

        public static string GetMRPConfirmedHungOrders()
        {
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter($"exec MO..cc_MRPConfirmedHungOrders", connection);
                custAdapter.Fill(customDataSet, "MRPConfirmedHungOrders");
                connection.Close();
            }
            return JsonConvert.SerializeObject(customDataSet);
        }

        public static string AddPickUpDate(int OrderNo, int Host, string strDate, int UserId)
        {
            try {
                string queryString = String.Format($"exec MO..AddPickUpDateForMRP @OrderNo={OrderNo}, @Host={Host}, @strDate='{strDate}', @UserId={UserId}");
                using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                return "Дата успешно добавлена";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string AddPickUpDateToBaseForOrderList(string orderList, string strDate, int UserId)
        {
            try
            {
                DataSet customDataSet = new DataSet();
                string queryString = String.Format($"exec MO..AddPickUpDateForMRPOrderList @OrderList='{orderList}', @strDate='{strDate}', @UserId={UserId}");
                using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
                {
                    SqlDataAdapter custAdapter = new SqlDataAdapter(queryString, connection);                    
                    connection.Open();
                    custAdapter.Fill(customDataSet, "NotAddedForPickUp");
                    connection.Close();
                }
                if (customDataSet.Tables["NotAddedForPickup"].Rows.Count == 0)
                    return "Для всех заказов дата забора успешно добавлена";
                else
                {
                    string mes = "";
                    foreach (var elem in customDataSet.Tables["NotAddedForPickup"].Rows)
                    {
                        mes += ((System.Data.DataRow)elem).ItemArray[0].ToString() + ". " + ((System.Data.DataRow)elem).ItemArray[1].ToString() + "\n";
                    }
                    return mes;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string ReportGetList(int UserId)
        {
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter($"exec MO..cc_Get_UsedOrderTypesFullNames;" +
                    $" exec MO..cc_Ref_Report_GetList @Author = 3, @User={UserId}", connection);
                custAdapter.Fill(customDataSet, "RepParams");
                connection.Close();
            }
            return JsonConvert.SerializeObject(customDataSet);
        }

        public static DataSet GetDataSetFromCommand(string command)
        {
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.SelectCommand.CommandTimeout = 300;
                custAdapter.Fill(customDataSet, "DataSet");
                connection.Close();
            }
            return customDataSet;
        }
    }
}
