using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace MOweb.Models.RefBookClasses
{
    public class OrderState
    {
        public int id;
        public string Description;
        public string Code;
        public int Blocked;
        public int Cancel;
        public int Confirm;
        public string AdditionalCode;
        public int CanChange;
        public int Shipping;
        public int Level;
        public int KindOfCancel;
        public string InteractionCode;
        public int CustomerCancel;
        public string Description2;
        public string SubStatusId;
        public int ReturnReason;

        public void FillObject(SqlDataReader Reader)
        {
            int idx;

            idx = Reader.GetOrdinal("id");              id = Reader.GetInt32(idx);
            idx = Reader.GetOrdinal("Description");     Description = Reader.GetString(idx);
            idx = Reader.GetOrdinal("Code");            Code = Reader.GetString(idx);
            idx = Reader.GetOrdinal("Blocked");         Blocked = Reader.GetByte(idx);
            idx = Reader.GetOrdinal("Cancel");          Cancel = Reader.GetByte(idx);
            idx = Reader.GetOrdinal("Confirm");         Confirm = Reader.GetByte(idx);
            idx = Reader.GetOrdinal("AdditionalCode");  AdditionalCode = Reader.GetString(idx);
            idx = Reader.GetOrdinal("CanChange");       CanChange = Reader.GetByte(idx);
            idx = Reader.GetOrdinal("Shipping");        Shipping = Reader.GetByte(idx);
            idx = Reader.GetOrdinal("Level");           Level = Reader.GetInt32(idx);
            idx = Reader.GetOrdinal("KindOfCancel");    KindOfCancel = Reader.GetByte(idx);
            idx = Reader.GetOrdinal("InteractionCode"); InteractionCode = Reader.GetString(idx);
            idx = Reader.GetOrdinal("CustomerCancel");  CustomerCancel = Reader.GetByte(idx);
            idx = Reader.GetOrdinal("Description2");    Description2 = Reader.GetString(idx);
            idx = Reader.GetOrdinal("SubStatusId");     SubStatusId = Reader.GetString(idx);
            idx = Reader.GetOrdinal("ReturnReason");    ReturnReason = Reader.GetInt32(idx);
        }

    } //OrderState


    public static class rbOrderStates
    {
        public static List<OrderState> States;
        public static void Init()//rbOrderStates()
        {
            States = new List<OrderState>();
            Load();
        }
        public static string QueryStr()
        {
            string s =
            "SELECT" + "\n" +
            "  id," + "\n" +
            "  Description," + "\n" +
            "  Code," + "\n" +
            "  Blocked," + "\n" +
            "  Cancel," + "\n" +
            "  Confirm," + "\n" +
            "  AdditionalCode," + "\n" +
            "  CanChange," + "\n" +
            "  Shipping," + "\n" +
            "  Level," + "\n" +
            "  ISNULL(KindOfCancel, 0) KindOfCancel," + "\n" +
            "  ISNULL(InteractionCode, '') InteractionCode," + "\n" +
            "  ISNULL(CustomerCancel, 0) CustomerCancel," + "\n" +
            "  ISNULL(Description2, '') Description2," + "\n" +
            "  ISNULL(SubStatusId, '') SubStatusId," + "\n" +
            "  ISNULL(ReturnReason, 0) ReturnReason" + "\n" +
            "FROM MO.dbo.cc_Ref_OrderStates" + "\n" +
            "ORDER BY id";

            return s;  
        }

        public static void Load()
        {
            using (SqlConnection connection = new SqlConnection(MOweb.Models.OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryStr(), connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderState os = new OrderState();
                        States.Add(os);
                        os.FillObject(reader);
                    }
                    reader.Close();
                }
            }
        }
    } //rbOrderStates

}   //namespace MOweb.Models.RefBookClasses
