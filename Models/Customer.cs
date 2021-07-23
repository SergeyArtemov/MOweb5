using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MOweb.Models
{

    public class Customer
    {
        private int? Customerid { get; set; }
        private int? Host { get; set; }
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string Surname { get; set; }
        private string Mobile { get; set; }
        private string Phone { get; set; }
        private string Email { get; set; }
        private int? Changed { get; set; }
        private string FullName { get; set; }
        private string CallOCallMobile { get; set; }
        private string CallOCallPhone { get; set; }
        private int? Region { get; set; }
        private int? PrefContact { get; set; }
        private int? PromoCall { get; set; }
        private int? PromoEmail { get; set; }
        private int? SpamDay { get; set; }
        private int? SpamWeek { get; set; }
        private int? SpamSpecial { get; set; }
        private int? SpamPartner { get; set; }
        private int? SpamSMS { get; set; }
        private string UserName { get; set; }
        private int? ValidEmailWeb { get; set; }
        private int? ValidEmailCC { get; set; }

        private double? Profitability { get; set; }
        private int? OrderCount { get; set; }
        private string NewSegment { get; set; }
        private double? deliveredOrdersSum { get; set; }
        private int? Upsell { get; set; }
        private DateTime? Birthday { get; set; }
        private int? AutoShipping { get; set; }

        private int? FirstOrder { get; set; }

        private int? LastConfirmWorkId { get; set; }

        private int? LastShippingWorkId { get; set; }

        private string EmailForNotify { get; set; }
        private int? LastShippedOrder { get; set; }

        private int? LastConfirmOrder { get; set; }

        private int? Sex { get; set; }
        private string ConfirmDescription { get; set; }

        private string IncomingDescription { get; set; }
        private string ShippingDescription { get; set; }
        private string EmailType { get; set; }
        private string CallOCallIncomingPhone { get; set; }
        private string DeliveryINN { get; set; }
        private int? NotSendToRobot { get; set; }

        private int? NotSendToSDEK { get; set; }
        private int? idAuto { get; set; }


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
