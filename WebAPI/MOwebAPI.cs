using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using MOweb.Models;

namespace MOweb.WebAPI
{
    public class MOwebAPI : IMOwebAPI
    {
        public async Task<string> GetMRPShedule(string vc, string sd, string cd)
        {
            return await Task.Run(() =>
            {
                try
                {

                    DataSet customDataSet = new DataSet();
                    using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
                    {
                        connection.Open();
                        SqlDataAdapter custAdapter = new SqlDataAdapter($"exec MO..cc_getWorkingDaysByVendor @VendorCode='{vc}', @StartDate='{sd}', @CountDays={cd}", connection);
                        custAdapter.Fill(customDataSet, "MRP_WorkSchedule");
                        connection.Close();
                    }
                    return JsonConvert.SerializeObject(customDataSet);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            });
        }
    }
}
