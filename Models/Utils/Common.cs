using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Utils.Common
{

    public enum LoadState { lsNoLoad, lsLoading, lsLoaded };

    public static class cn
    {
        public static string ConStr = MOweb.Models.OrdersMapper.SqlConnectMO;
        //"Server=sqlsrv02;Database=mo;Trusted_Connection=True;";
    }


    public class NamedItem
    {
        public int id;
        public string Name;
        public void FillObjectByPos(DataRow ARow, int id_pos = 0, int Name_pos = 1)
        {
            if (!Convert.IsDBNull(ARow[id_pos])) { id = Convert.ToInt32(ARow[id_pos]); } else {id = default;}
            if (!Convert.IsDBNull(ARow[Name_pos])) { Name = Convert.ToString(ARow[Name_pos]); } else { Name = default; }
        }
    }


    public class ID_strNamedItem
    {
        public string id_str;
        public string Name;
        public void FillObjectByPos(DataRow ARow, int id_pos = 0, int Name_pos = 1)
        {
            if (!Convert.IsDBNull(ARow[id_pos])) { id_str = Convert.ToString(ARow[id_pos]); } else { id_str = default; }
            if (!Convert.IsDBNull(ARow[Name_pos])) { Name = Convert.ToString(ARow[Name_pos]); } else { Name = default; }
        }
    }



    public static class scv //Функйции конвертирования данных ADO с учетом значения NULL  
    {
        //Из текущей позиции SqlDataReader ====================================  
        public static string GetStringNull(string field, SqlDataReader reader)
        {
            var c = reader.GetOrdinal(field);
            if (reader.IsDBNull(c)) return null;
            else return reader.GetString(c);
        }
        public static int? GetInt32Null(string field, SqlDataReader reader)
        {
            var c = reader.GetOrdinal(field);
            if (reader.IsDBNull(c)) return null;
            else return reader.GetInt32(c);
        }
        public static double? GetDoubleNull(string field, SqlDataReader reader)
        {
            var c = reader.GetOrdinal(field);
            if (reader.IsDBNull(c)) return null;
            else return reader.GetDouble(c);
        }
        public static DateTime? GetDateTimeNull(string field, SqlDataReader reader)
        {
            var c = reader.GetOrdinal(field);
            if (reader.IsDBNull(c)) return null;
            else return reader.GetDateTime(c);
        }
        public static byte? GetByteNull(string field, SqlDataReader reader)
        {
            var c = reader.GetOrdinal(field);
            if (reader.IsDBNull(c)) return null;
            else return reader.GetByte(c);
        }

        //Из строки таблицы датасета DataRow ====================================  
        public static string GetStringNull(string field, DataRow ARow)
        {
            string s = (!Convert.IsDBNull(ARow[field])) ? Convert.ToString(ARow[field]) : null;
            return s;
        }
        public static int GetIntNull(string field, DataRow ARow)
        {
            if (!Convert.IsDBNull(ARow[field])) { return Convert.ToInt32(ARow[field]); }
            else { return default; }
        }

        public static int? GetInt32Null(string field, DataRow ARow)
        {
            if (!Convert.IsDBNull(ARow[field])) { return Convert.ToInt32(ARow[field]); }
            else { return default; }
        }

        public static double? GetDoubleNull(string field, DataRow ARow)
        {
            if (!Convert.IsDBNull(ARow[field])) { return Convert.ToDouble(ARow[field]); }
            else { return null; }
        }
        public static DateTime GetDateTimeNull(string field, DataRow ARow)
        {
            if (!Convert.IsDBNull(ARow[field])) { return Convert.ToDateTime(ARow[field]); }
            else { return default; }
        }
        public static byte? GetByteNull(string field, DataRow ARow)
        {
            if (!Convert.IsDBNull(ARow[field])) { return Convert.ToByte(ARow[field]); }
            else { return null; }
        }

        public static string GetParamInt(string param, int? v, int firstparam)
        {
            string str;
            if (v.HasValue) str = param + "=" + v.ToString() + " ";
            else str = "";
            
            if (firstparam == 0) { str = "," + str; }

            return str;
        }

        public static string GetParamStr(string param, string v, int firstparam)
        {
            string str;
            if (v != null) str = param + "='" + v + "' ";
            else str = "";

            if (firstparam == 0 && str != "") { str = "," + str; }

            return str;
        }

        public static string GetParamDate(string param, DateTime? v, int firstparam)
        {
            string str;
            if ((v != null) && (v != new DateTime(1900,01,01))) str = param + "='" + v.Value.ToString("yyyyMMdd") + "' ";
            else str = param + "='" +(new DateTime(1900, 01, 01)).ToString("yyyyMMdd")+"'";

            if (firstparam == 0 && str != "") { str = "," + str; }

            return str;
        }

        public static string Serialize<T>(this T value)  //https://stackoverflow.com/questions/4123590/serialize-an-object-to-xml
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    String S = stringWriter.ToString();
                    return S;

                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

    }
}
