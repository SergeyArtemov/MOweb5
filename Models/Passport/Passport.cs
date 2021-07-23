using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Utils.Common;
using AppClasses;

namespace MOweb.Models
{
    public class Passport
    {

            //public int? Customerid { get; set; }
            //public int? Host { get; set; }
//            public string FirstName { get; set; }
            //public string LastName { get; set; }
            //public string Surname { get; set; }
            public string Mobile { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public int PageForShow { get; set; }
            public int PageNext { get; set; }


            public int? Customerid { get; set; }
            public int? Host { get; set; }
            public string Author { get; set; }
            public string User { get; set; }
            public string CancelReason { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string Patronimic { get; set; }
            //public string LastName { get; set; }

            public string PassRange { get; set; }
            public string PassNumber { get; set; }
            public DateTime? PassIssueDate { get; set; }
            public DateTime? Birthday { get; set; }

            public string PassDepartmentCode { get; set; }
            public string PassDepartmentAddr { get; set; }
            public string PassRegisteredAddress { get; set; }
            public string DocType { get; set; }

            public string SurnameRF { get; set; }
            public string NameRF { get; set; }
            public string PatronymicRF { get; set; }
            public string PassRangeRF { get; set; }
            public string PassNumberRF { get; set; }
            public string PassDepartmentAddrRF { get; set; }
            public DateTime? PassIssueDateRF { get; set; }
            public DateTime? BirthdayRF { get; set; }
            public string SurnameNerez { get; set; }
            public string NameNerez { get; set; }
            public string PatronymicNerez { get; set; }
            public string PassRangeNerez { get; set; }
            public string PassNumberNerez { get; set; }
            public string PassDepartmentAddrNerez { get; set; }
            public DateTime? PassIssueDateNerez { get; set; }
            public DateTime? BirthdayNerez { get; set; }

            public string SurnameZagr { get; set; }
            public string NameZagr { get; set; }
            public string PatronymicZagr { get; set; }
            public string PassRangeZagr { get; set; }
            public string PassNumberZagr { get; set; }
            public string PassDepartmentAddrZagr { get; set; }
            public DateTime? PassIssueDateZagr { get; set; }
            public DateTime? BirthdayZagr { get; set; }

            public string SurnameUd { get; set; }
            public string NameUd { get; set; }
            public string PatronymicUd { get; set; }
            public string PassNumberUd { get; set; }
            public string PassDepartmentAddrUd { get; set; }
            public DateTime? PassIssueDateUd { get; set; }
            public DateTime? BirthdayUd { get; set; }
            public string DeliveryINN { get; set; }

            public string CustomerPostCode { get; set; }
            public string CustomerCity { get; set; }
            public string CustomerAdressStreet { get; set; }
	        public string CustomerAdressHouse { get; set; }
	        public string CustomerAdressCorps { get; set; }
	        public string CustomerAdressApartment { get; set; }
            public string Inn { get; set; }
            public string HomeAdress { get; set; }
            public int? LegalRF { get; set; }
            public int? Key { get; set; }



        public Passport()
            {
                Customerid = 0;
                Host = 0;
                PageForShow = 1;
                PageNext = 1;
                //FillPassp();
        }

            public Passport(int? id, int? host)
            {
                Customerid = id;
                Host = host;
                this.PageForShow = 1;
                PageNext = 1;
                //FillPassp();
            }

        public void FillPassp()
        {
            if (1 == 1)
            {
                using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
                {
                    connection.Open();
                    string QueryOrders = "EXEC GateStore..PD_Select 'CallCenter',2," + Customerid.ToString() + "," + Host.ToString();
                    using (SqlCommand command = new SqlCommand(QueryOrders, connection))
                    {
                        var reader = command.ExecuteReader();
                        reader.Read();

                        {

                           //Customerid               =
                           //Host                     =
                           //Author                   =
                           //User                     =
                           Surname	                = scv.GetStringNull     ("Surname"	                , reader);
                           Name	                    = scv.GetStringNull     ("Name"	                    , reader);
                           Patronimic	            = scv.GetStringNull     ("Patronymic"	            , reader);
                           PassRange	            = scv.GetStringNull     ("PassRange"	            , reader);
                           PassNumber	            = scv.GetStringNull     ("PassNumber"	            , reader);
                           PassIssueDate	        = scv.GetDateTimeNull   ("PassIssueDateRF"	        , reader);
                           Birthday	                = scv.GetDateTimeNull   ("Birthday"	                , reader);
                           PassDepartmentCode	    = scv.GetStringNull     ("PassDepartmentCode"	    , reader);
                           PassDepartmentAddr	    = scv.GetStringNull     ("PassDepartmentAddr"	    , reader);
                           PassRegisteredAddress    = scv.GetStringNull     ("PassRegisteredAddress"    , reader);
                           CancelReason	            = scv.GetStringNull     ("CancelReason"	            , reader);
                           DocType	                = scv.GetStringNull     ("DocType"	                , reader);
                           SurnameRF	            = scv.GetStringNull     ("SurnameRF"	            , reader);
                           NameRF	                = scv.GetStringNull     ("NameRF"	                , reader);
                           PatronymicRF	            = scv.GetStringNull     ("PatronymicRF"	            , reader);
                           PassRangeRF	            = scv.GetStringNull     ("PassRangeRF"	            , reader);
                           PassNumberRF	            = scv.GetStringNull     ("PassNumberRF"	            , reader);
                           PassDepartmentAddrRF	    = scv.GetStringNull     ("PassDepartmentAddrRF"	    , reader);
                           PassIssueDateRF	        = scv.GetDateTimeNull   ("PassIssueDateRF"	        , reader);
                           BirthdayRF	            = scv.GetDateTimeNull   ("BirthdayRF"	            , reader);
                           SurnameNerez	            = scv.GetStringNull     ("SurnameNerez"	            , reader);
                           NameNerez	            = scv.GetStringNull     ("NameNerez"	            , reader);
                           PatronymicNerez	        = scv.GetStringNull     ("PatronymicNerez"	        , reader);
                           PassRangeNerez	        = scv.GetStringNull     ("PassRangeNerez"	        , reader);
                           PassNumberNerez	        = scv.GetStringNull     ("PassNumberNerez"	        , reader);
                           PassDepartmentAddrNerez  = scv.GetStringNull     ("PassDepartmentAddrNerez"  , reader);
                           PassIssueDateNerez	    = scv.GetDateTimeNull   ("PassIssueDateNerez"	    , reader);
                           BirthdayNerez            = scv.GetDateTimeNull   ("BirthdayNerez"            , reader);
                           SurnameZagr	            = scv.GetStringNull     ("SurnameZagr"	            , reader);
                           NameZagr	                = scv.GetStringNull     ("NameZagr"	                , reader);
                           PatronymicZagr	        = scv.GetStringNull     ("PatronymicZagr"	        , reader);
                           PassRangeZagr	        = scv.GetStringNull     ("PassRangeZagr"	        , reader);
                           PassNumberZagr	        = scv.GetStringNull     ("PassNumberZagr"	        , reader);
                           PassDepartmentAddrZagr   = scv.GetStringNull     ("PassDepartmentAddrZagr"	, reader);
                           PassIssueDateZagr	    = scv.GetDateTimeNull   ("PassIssueDateZagr"	    , reader);
                           BirthdayZagr	            = scv.GetDateTimeNull   ("BirthdayZagr"	            , reader);
                           SurnameUd	            = scv.GetStringNull     ("SurnameUd"	            , reader);
                           NameUd	                = scv.GetStringNull     ("NameUd"	                , reader);
                           PatronymicUd	            = scv.GetStringNull     ("PatronymicUd"	            , reader);
                           PassNumberUd	            = scv.GetStringNull     ("PassNumberUd"	            , reader);
                           PassDepartmentAddrUd	    = scv.GetStringNull     ("PassDepartmentAddrUd"	    , reader);
                           PassIssueDateUd	        = scv.GetDateTimeNull   ("PassIssueDateUd"	        , reader);
                           BirthdayUd	            = scv.GetDateTimeNull   ("BirthdayUd"	            , reader);
                           Inn	                    = scv.GetStringNull     ("Inn"	                    , reader);
                           HomeAdress	            = scv.GetStringNull     ("HomeAdress"	            , reader);
                           LegalRF                  = scv.GetInt32Null       ("LegalRF"                  , reader);

                           reader.Close();
                        }

                    }

                }

                //FirstName = PassportsMapper.psp1.FirstName;
                //LastName = PassportsMapper.psp1.LastName;
                //Surname = PassportsMapper.psp1.Surname;
                //Mobile = PassportsMapper.psp1.Mobile;
                //Phone = PassportsMapper.psp1.Phone;
                //Email = PassportsMapper.psp1.Email;



            }
            else
            {
                //FirstName = "AAAAA";
                //LastName = "BBBBBB";
                Surname = "CCCCCCCCC";
                Mobile = "76786786868";
                Phone = "8788787";
                Email = "ghfghf@gh.ut";
            }
        }
    }
}
