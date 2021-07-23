using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace MOweb.Models
{
    public class Mamsy
    {
        public static DataSet GetMamsyMonitoringValuesDataSet(string cmp, DateTime fromDate, DateTime toDate, int onlyErrors, int indId)
        {
            string command = $"exec MO..cc_MamsyMonitoring " +
                $"@Campaign={(String.IsNullOrEmpty(cmp) ? "null" : "'" + cmp + "'")}, " +
                $"@FromDate={(fromDate.Year > 2000 ? ("'" + fromDate.ToString("yyyyMMdd HH:mm:ss") + "'") : "null")}, " +
                $"@ToDate={(toDate.Year > 2000 ? ("'" + toDate.ToString("yyyyMMdd HH:mm:ss") + "'") : "null")}, " +
                $"@OnlyErrors={onlyErrors}, " +
                $"@IndicatorId={indId}";
            return GetDataSetFromCommand(command);
        }

        public static DataSet GetMamsyMonitoringValuesDataSet_State(int monitoringState)
        {
            string command = $"exec MO..cc_MamsyMonitoring_State @MonitoringState={monitoringState}";
            return GetDataSetFromCommand(command);
        }

        public static DataSet GetMamsyMonitoringValuesDataSet_Items(int paramId)
        {
            string command = $"exec MO..cc_MamsyMonitoring_Items @ParamId={paramId}";
            return GetDataSetFromCommand(command);
        }

        public static DataSet GetDataSetFromCommand(string command)
        {
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.SelectCommand.CommandTimeout = 300;
                custAdapter.Fill(customDataSet, "MamsyMonitoringValues");
                connection.Close();
            }
            return customDataSet;
        }

        public static string GetMamsyParams()
        {
            string command = $"exec MO..cc_GetMamsyParams";
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.Fill(customDataSet, "MamsyParams");
                connection.Close();
            }
            return JsonConvert.SerializeObject(customDataSet);
        }
    }
}
