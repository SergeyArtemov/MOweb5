using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace MOweb.Models
{
    public class AreaEditor
    {
        public static DataSet LoadAreaListFromDB()
        {
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection("Persist Security Info=False;User ID=mo2;Password=Ouy9gRplyFul0b9x;Server=sqldev01;Database=mo"/*OrdersMapper.SqlConnectMO*/))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter($"exec MO..map_GetAreasFullList_DataSet", connection);
                custAdapter.Fill(customDataSet, "AreaList");
                connection.Close();
            }
            return customDataSet;
        }

        public static DataSet GetAreaIntervals(int areaId)
        {
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection("Persist Security Info=False;User ID=mo2;Password=Ouy9gRplyFul0b9x;Server=sqldev01;Database=mo"/*OrdersMapper.SqlConnectMO*/))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter($"exec MO..map_GetAreaIntervals_desc @AreaId = {areaId}", connection);
                custAdapter.Fill(customDataSet, "AreaIntervals");
                connection.Close();
            }
            return customDataSet;
        }

        public static string LoadAreaListFromDB_XML()
        {
            string xmlResult = "";

            using (SqlConnection connection = new SqlConnection("Persist Security Info=False;User ID=mo2;Password=Ouy9gRplyFul0b9x;Server=sqldev01;Database=mo"/*OrdersMapper.SqlConnectMO*/))
            {
                connection.Open();
                string Query = String.Format($"exec MO..[map_GetAreasFullList_20181022] '<used>7</used><agent><id>1</id><id>13</id><id>8519</id></agent>'");
                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    int col;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("XMLBody");
                        xmlResult = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                    }
                }
                connection.Close();
            }
            return xmlResult;
        }
    }
}
