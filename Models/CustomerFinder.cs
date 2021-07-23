using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AppClasses;
using MOweb.Models;

namespace CustomerFinder
{
    public class CustomerInfo
    {
        public int CI_Customer;
        public string CI_HostName;
        public int CI_Host;
        public string CI_Email;
        public string CI_Name;
        public string CI_Mobile;
        public string CI_Region;
        public string CI_ComMethod;
        public string CI_Active;
        public int CI_AccCount;
        public int CI_Order;


        public CustomerInfo(int id, 
                            string hostname, 
                            int host, 
                            string email, 
                            string name, 
                            string mobile, 
                            string region,
                            string commethod, 
                            string active, 
                            int acccnt, 
                            int order)
        {
            CI_Customer = id;
            CI_HostName = hostname;
            CI_Host = host;
            CI_Email = email;
            CI_Name = name;
            CI_Mobile = mobile;
            CI_Region = region;
            CI_ComMethod = commethod;
            CI_Active = active;
            CI_AccCount = acccnt;
            CI_Order = order;
        }
        
     }

    public class CustomerFind : ModelBase
    {
        public string FindMobile;
        public int FindCustomer;
        public int FindOrder;
        public string FindEmail;
        public List<CustomerInfo> Customers;


        
        public void FindCustomerId(int customer)
        {
            string stringSQLFindCustomer = "declare @Customer int = " + (customer == 0? "null": customer.ToString())
                                            + "\n" + "																	"
                                            + "\n" + "select rc.id, rc.FullName, rc.Host, rc.email, rc.CallOCallMobile,	"
                                            + "\n" + "	isnull(rr.Name, '') [regName],									"
                                            + "\n" + "	isnull(rprefcont.Name, '') [prefContact],						"
                                            + "\n" + "	CASE															"
                                            + "\n" + "		WHEN GSA.[AccountID] IS NULL THEN 'Активен'					"
                                            + "\n" + "		ELSE 'Не активен' 											"
                                            + "\n" + "    END [AccActive],												"
                                            + "\n" + "	rh.Name HostName, 0 [OrderNo]									"
                                            + "\n" + "from MO..cc_Ref_Customer (nolock) rc 								"
                                            + "\n" + "left join MO..cc_Ref_Host (nolock) rh								"
                                            + "\n" + "	on rh.id = rc.Host												"
                                            + "\n" + "left join MO..cc_Ref_Region (nolock) rr							"
                                            + "\n" + "	on rr.id = rc.Region											"
                                            + "\n" + "left join MO..[cc_Ref_PrefContact] (nolock) rprefcont				"
                                            + "\n" + "	on rprefcont.id = rc.PrefContact								"
                                            + "\n" + "left join [GateStore].[dbo].[gs_spr_AccountsDeleted] GSA			"
                                            + "\n" + "	ON GSA.[AccountID] = rc.[id]									"
                                            + "\n" + "		and GSA.HostId = rc.Host									"
                                            + "\n" + "where (@Customer is null or rc.id = @Customer)					";
            FillCustomerFind(stringSQLFindCustomer);

        }

        public void FindCustomerEmail(string email)
        {
            string stringSQLFindCustomer = "declare @email varchar(50) = '" + (email == "" ? "null" : email) + "'"
                                            + "\n" + "																	"
                                            + "\n" + "select rc.id, rc.FullName, rc.Host, rc.email, rc.CallOCallMobile,	"
                                            + "\n" + "	isnull(rr.Name, '') [regName],									"
                                            + "\n" + "	isnull(rprefcont.Name, '') [prefContact],						"
                                            + "\n" + "	CASE															"
                                            + "\n" + "		WHEN GSA.[AccountID] IS NULL THEN 'Активен'					"
                                            + "\n" + "		ELSE 'Не активен' 											"
                                            + "\n" + "    END [AccActive],												"
                                            + "\n" + "	rh.Name HostName, 0 [OrderNo]									"
                                            + "\n" + "from MO..cc_Ref_Customer (nolock) rc 								"
                                            + "\n" + "left join MO..cc_Ref_Host (nolock) rh								"
                                            + "\n" + "	on rh.id = rc.Host												"
                                            + "\n" + "left join MO..cc_Ref_Region (nolock) rr							"
                                            + "\n" + "	on rr.id = rc.Region											"
                                            + "\n" + "left join MO..[cc_Ref_PrefContact] (nolock) rprefcont				"
                                            + "\n" + "	on rprefcont.id = rc.PrefContact								"
                                            + "\n" + "left join [GateStore].[dbo].[gs_spr_AccountsDeleted] GSA			"
                                            + "\n" + "	ON GSA.[AccountID] = rc.[id]									"
                                            + "\n" + "		and GSA.HostId = rc.Host									"
                                            + "\n" + "where (@email is null or rc.email = @email)					   ";
            FillCustomerFind(stringSQLFindCustomer);
        }

        public void FindCustomerPhone(string phone)
        {
            string stringSQLFindCustomer = "declare @phone varchar(50) = '" + (phone == "" ? "null" : phone) + "'"
                                            + "\n" + "																	"
                                            + "\n" + "select rc.id, rc.FullName, rc.Host, rc.email, rc.CallOCallMobile,	"
                                            + "\n" + "	isnull(rr.Name, '') [regName],									"
                                            + "\n" + "	isnull(rprefcont.Name, '') [prefContact],						"
                                            + "\n" + "	CASE															"
                                            + "\n" + "		WHEN GSA.[AccountID] IS NULL THEN 'Активен'					"
                                            + "\n" + "		ELSE 'Не активен' 											"
                                            + "\n" + "    END [AccActive],												"
                                            + "\n" + "	rh.Name HostName, 0 [OrderNo]									"
                                            + "\n" + "from MO..cc_Ref_Customer (nolock) rc 								"
                                            + "\n" + "left join MO..cc_Ref_Host (nolock) rh								"
                                            + "\n" + "	on rh.id = rc.Host												"
                                            + "\n" + "left join MO..cc_Ref_Region (nolock) rr							"
                                            + "\n" + "	on rr.id = rc.Region											"
                                            + "\n" + "left join MO..[cc_Ref_PrefContact] (nolock) rprefcont				"
                                            + "\n" + "	on rprefcont.id = rc.PrefContact								"
                                            + "\n" + "left join [GateStore].[dbo].[gs_spr_AccountsDeleted] GSA			"
                                            + "\n" + "	ON GSA.[AccountID] = rc.[id]									"
                                            + "\n" + "		and GSA.HostId = rc.Host									"
                                            + "\n" + "where (@phone is null or rc.CallOCallMobile = @phone)					   ";
            FillCustomerFind(stringSQLFindCustomer);
        }

        public void FindCustomerOrder(int order)
        {
            string stringSQLFindCustomer = "declare @order varchar(50) = " + order.ToString()
                                            + "\n" + "																				"
                                            + "\n" + "select cst.*, oh.OrderNo														"
                                            + "\n" + "from MO..cc_OrderHead (nolock) oh												"
                                            + "\n" + "left join (select rc.id, rc.FullName, rc.Host, rc.email, rc.CallOCallMobile,	"
                                            + "\n" + "	isnull(rr.Name, '') [regName],		                    					"
                                            + "\n" + "	isnull(rprefcont.Name, '') [prefContact],	                    			"
                                            + "\n" + "	CASE											                    		"
                                            + "\n" + "		WHEN GSA.[AccountID] IS NULL THEN 'Активен'	                    		"
                                            + "\n" + "		ELSE 'Не активен' 							                    		"
                                            + "\n" + "    END [AccActive],			                                    			"
                                            + "\n" + "	rh.Name HostName            					                    		"
                                            + "\n" + "from MO..cc_Ref_Customer (nolock) rc 					                    	"
                                            + "\n" + "left join MO..cc_Ref_Host (nolock) rh						                    "
                                            + "\n" + "	on rh.id = rc.Host										                    "
                                            + "\n" + "left join MO..cc_Ref_Region (nolock) rr					                    "
                                            + "\n" + "	on rr.id = rc.Region									                    "
                                            + "\n" + "left join MO..[cc_Ref_PrefContact] (nolock) rprefcont		                    "
                                            + "\n" + "	on rprefcont.id = rc.PrefContact						                    "
                                            + "\n" + "left join [GateStore].[dbo].[gs_spr_AccountsDeleted] GSA	                    "
                                            + "\n" + "ON GSA.[AccountID] = rc.[id]) cst							                    "
                                            + "\n" + "	on cst.id = oh.Customer 													"
                                            + "\n" + "		and cst.Host = oh.Host 										  			"
                                            + "\n" + "where isnull(@order, 0) > 0 and oh.OrderNo = @order							";
            FillCustomerFind(stringSQLFindCustomer);
        }


        public void FillCustomerFind(string sqlCommand)
        {
            Customers = new List<CustomerInfo> { };
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                {

                    var reader = command.ExecuteReader();
                    int col;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("id");
                        int sqlCust = reader.GetInt32(col);

                        col = reader.GetOrdinal("Host");
                        int sqlHost = reader.GetInt32(col);

                        col = reader.GetOrdinal("HostName");
                        string sqlHostName = reader.GetString(col);

                        col = reader.GetOrdinal("FullName");
                        string sqlCustName = reader.GetString(col);

                        col = reader.GetOrdinal("email");
                        string sqlCustEmail = reader.GetString(col);

                        col = reader.GetOrdinal("CallOCallMobile");
                        string sqlCustPhone = reader.GetString(col);

                        col = reader.GetOrdinal("regName");
                        string sqlCustRegion= reader.GetString(col);

                        col = reader.GetOrdinal("OrderNo");
                        int sqlCustOrder = reader.GetInt32(col);

                        col = reader.GetOrdinal("prefContact");
                        string sqlCustPrefCont = reader.GetString(col);

                        col = reader.GetOrdinal("AccActive");
                        string sqlCustAccActive = reader.GetString(col);
                        Customers.Add(new CustomerInfo(sqlCust, sqlHostName, sqlHost, sqlCustEmail, sqlCustName, 
                                                        sqlCustPhone, sqlCustRegion, sqlCustPrefCont, sqlCustAccActive, 
                                                        1, sqlCustOrder));
                    }
                }
            }
        }


    }


}