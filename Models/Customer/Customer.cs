using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MOweb.Models.Cust2
{

    public class Customer
    {
        public int? Customerid { get; set; }
        public int? Host { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Surname { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? Changed { get; set; }
        public string FullName { get; set; }
        public string CallOCallMobile { get; set; }
        public string CallOCallPhone { get; set; }
        public int? Region { get; set; }
        public int? PrefContact { get; set; }
        public int? PromoCall { get; set; }
        public int? PromoEmail { get; set; }
        public int? SpamDay { get; set; }
        public int? SpamWeek { get; set; }
        public int? SpamSpecial { get; set; }
        public int? SpamPartner { get; set; }
        public int? SpamSMS { get; set; }
        public string UserName { get; set; }
        public int? ValidEmailWeb { get; set; }
        public int? ValidEmailCC { get; set; }
        
        public double? Profitability { get; set; }
        public int? OrderCount { get; set; }
        public string NewSegment { get; set; }
        public double? deliveredOrdersSum { get; set; }
        public int? Upsell { get; set; }
        public DateTime? Birthday { get; set; }
        public int? AutoShipping { get; set; }
        
        public int? FirstOrder { get; set; }
        
        public int? LastConfirmWorkId { get; set; }
        
        public int? LastShippingWorkId { get; set; }
        
        public string EmailForNotify { get; set; }
        public int? LastShippedOrder { get; set; }
        
        public int? LastConfirmOrder { get; set; }
        
        public int? Sex { get; set; }
        public string ConfirmDescription { get; set; }
        
        public string IncomingDescription { get; set; }
        public string ShippingDescription { get; set; }
        public string EmailType { get; set; }
        public string CallOCallIncomingPhone { get; set; }
        public string DeliveryINN { get; set; }
        public int? NotSendToRobot { get; set; }
        
        public int? NotSendToSDEK { get; set; }
        public int? idAuto { get; set; }


        public Customer()
        {
            Customerid = 0;
            Host = 0;
        }

        public Customer(int id, int host)
        {
            Customerid = id;
            Host = host;
            FillCustomer();
        }

        private string GetStringNull(string field, SqlDataReader reader)
        {
            var c = reader.GetOrdinal(field);
            if (reader.IsDBNull(c)) return null;
                                    else return reader.GetString(c);
                          //else return "";
        }

        private int? GetInt32Null(string field, SqlDataReader reader)
        {
            var c = reader.GetOrdinal(field);
            if (reader.IsDBNull(c)) return null;
                                    else return reader.GetInt32(c);
        }

        private double? GetDoubleNull(string field, SqlDataReader reader)
        {
            var c = reader.GetOrdinal(field);
            if (reader.IsDBNull(c)) return null;
            else return reader.GetDouble(c);
        }

        private DateTime? GetDateTimeNull(string field, SqlDataReader reader)
        {
            var c = reader.GetOrdinal(field);
            if (reader.IsDBNull(c)) return null;
            else return reader.GetDateTime(c);
        }

        private byte? GetByteNull(string field, SqlDataReader reader)
        {
            var c = reader.GetOrdinal(field);
            if (reader.IsDBNull(c)) return null;
            else return reader.GetByte(c);
        }


        public void FillCustomer()
        {
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                string QueryOrders = "exec MO..cc_Customer_get " + Customerid.ToString() + "," + Host.ToString();
                using (SqlCommand command = new SqlCommand(QueryOrders, connection))
                {
                    var reader = command.ExecuteReader();
                    reader.Read();

                    {
                        FirstName = GetStringNull("FirstName", reader);
                        LastName = GetStringNull("LastName", reader);
                        Mobile = GetStringNull("Mobile", reader);
                        Phone = GetStringNull("Phone", reader);
                        Email = GetStringNull("Email", reader);
                        Changed = GetByteNull("Changed", reader);
                        FullName = GetStringNull("FullName", reader);
                        CallOCallMobile = GetStringNull("CallOCallMobile", reader);
                        CallOCallPhone = GetStringNull("CallOCallPhone", reader);
                        Region = GetInt32Null("Region", reader);
                        PrefContact = GetInt32Null("PrefContact", reader);
                        PromoCall = GetInt32Null("PromoCall", reader);
                        PromoEmail = GetInt32Null("PromoEmail", reader);
                        SpamDay = GetInt32Null("SpamDay", reader);
                        SpamWeek = GetInt32Null("SpamWeek", reader);
                        SpamSpecial = GetInt32Null("SpamSpecial", reader);
                        SpamPartner = GetInt32Null("SpamPartner", reader);
                        UserName = GetStringNull("UserName", reader);
                        ValidEmailWeb = GetByteNull("ValidEmailWeb", reader);
                        ValidEmailCC = GetByteNull("ValidEmailCC", reader);
                        Profitability = GetDoubleNull("Profitability", reader);

                        OrderCount = GetInt32Null("OrderCount", reader);

                        NewSegment = GetStringNull("NewSegment", reader);

                        deliveredOrdersSum = GetDoubleNull("deliveredOrdersSum", reader);

                        Upsell = GetInt32Null("Upsell", reader);
                        Birthday = GetDateTimeNull("Birthday", reader);

                        AutoShipping = GetInt32Null("AutoShipping", reader);
                        FirstOrder = GetInt32Null("FirstOrder", reader);
                        LastConfirmWorkId = GetInt32Null("LastConfirmWorkId", reader);
                        LastShippingWorkId = GetInt32Null("LastShippingWorkId", reader);
                        EmailForNotify = GetStringNull("EmailForNotify", reader);

                        LastShippedOrder = GetInt32Null("LastShippedOrder", reader);
                        LastConfirmOrder = GetInt32Null("LastConfirmOrder", reader);
                        Sex = GetInt32Null("Sex", reader);
                        ConfirmDescription = GetStringNull("ConfirmDescription", reader);
                        IncomingDescription = GetStringNull("IncomingDescription", reader);
                        ShippingDescription = GetStringNull("ShippingDescription", reader);
                        EmailType = GetStringNull("EmailType", reader);
                        CallOCallIncomingPhone = GetStringNull("CallOCallIncomingPhone", reader);
                        DeliveryINN = GetStringNull("DeliveryINN", reader);
                        NotSendToRobot = GetByteNull("NotSendToRobot", reader);
                        NotSendToSDEK = GetByteNull("NotSendToSDEK", reader);
                        idAuto = GetInt32Null("idAuto", reader);

                        reader.Close();
                    }

                }

            }
        }
    }
}
