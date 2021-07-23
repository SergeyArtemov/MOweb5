using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOweb.Models.ViewModels
{
    public static class PassportViews
    {
        //public static Passport psp1;

        //public static PassportMap psp1map;

        public static List<PasspView> passpViewList = new List<PasspView>() { };

        public static PasspView psp1view;

        public class PasspView : Passport
        {
            //public int? key { get; set; }
            public int AccountId = -1;
            public PasspView(Passport psp) //: base(customerID, host)
            {
                Customerid = psp.Customerid;
                Host = psp.Host;
                Author = psp.Author;
                User = psp.User;
                Surname = psp.Surname;
                Name = psp.Name;
                Patronimic = psp.Patronimic;
                PassRange = psp.PassRange;
                PassNumber = psp.PassNumber;
                PassIssueDate = psp.PassIssueDate;
                Birthday = psp.Birthday;
                PassDepartmentCode = psp.PassDepartmentCode;
                PassDepartmentAddr = psp.PassDepartmentAddr;
                PassRegisteredAddress = psp.PassRegisteredAddress;
                CancelReason = psp.CancelReason;
                DocType = psp.DocType;
                SurnameRF = psp.SurnameRF;
                NameRF = psp.NameRF;
                PatronymicRF = psp.PatronymicRF;
                PassRangeRF = psp.PassRangeRF;
                PassNumberRF = psp.PassNumberRF;
                PassDepartmentAddrRF = psp.PassDepartmentAddrRF;
                PassIssueDateRF = psp.PassIssueDateRF;
                BirthdayRF = psp.BirthdayRF;
                SurnameNerez = psp.SurnameNerez;
                NameNerez = psp.NameNerez;
                PatronymicNerez = psp.PatronymicNerez;
                PassRangeNerez = psp.PassRangeNerez;
                PassNumberNerez = psp.PassNumberNerez;
                PassDepartmentAddrNerez = psp.PassDepartmentAddrNerez;
                PassIssueDateNerez = psp.PassIssueDateNerez;
                BirthdayNerez = psp.BirthdayNerez;
                SurnameZagr = psp.SurnameZagr;
                NameZagr = psp.NameZagr;
                PatronymicZagr = psp.PatronymicZagr;
                PassRangeZagr = psp.PassRangeZagr;
                PassNumberZagr = psp.PassNumberZagr;
                PassDepartmentAddrZagr = psp.PassDepartmentAddrZagr;
                PassIssueDateZagr = psp.PassIssueDateZagr;
                BirthdayZagr = psp.BirthdayZagr;
                SurnameUd = psp.SurnameUd;
                NameUd = psp.NameUd;
                PatronymicUd = psp.PatronymicUd;
                PassNumberUd = psp.PassNumberUd;
                PassDepartmentAddrUd = psp.PassDepartmentAddrUd;
                PassIssueDateUd = psp.PassIssueDateUd;
                BirthdayUd = psp.BirthdayUd;
                Inn = psp.Inn;
                LegalRF = psp.LegalRF;
                HomeAdress = psp.HomeAdress;
                LegalRF = psp.LegalRF;
                Key = psp.Key;
                LegalRF = psp.LegalRF;
                //key = rnd.Next();// this.Order();// base(orderno, host);
                //this.FillPassp();
            }

            public PasspView() //: base()
            {
                //key = rnd.Next();// this.Order();// base(orderno, host);
                //this.FillPassp();
            }

        }

        public static PasspView findPassp(int? customerid, int? host, int? key)
        {

            //if (order.Payment.PaymentType == OrdersMapper.initialOrderList.Find(ord1 => ord1.refOrd == order).Payment.PaymentType)

            return PassportViews.passpViewList.Find(psp1 => ((psp1.Customerid == customerid) && (psp1.Host == host) && (psp1.Key == key))); //.initialPasspList.//.initialOrderList

            //return //passpViewList[0];
        }


    }
}


