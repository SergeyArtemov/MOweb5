using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOweb.Models.Case
{
    public class CaseMapper
    {
        public static List<CasMap> caseMapperList = new List<CasMap>() { };

        public static void addCasMap(CaseClasses case1, int key)  // в рамках конкретного key (ключ рабочей сессии)
        {

            caseMapperList.Add(new CasMap(case1, key));

            return;
        }

        public static CasMap findCasMap(CaseClasses case1, int key)  // Ищем в рамках конкретного key (ключ рабочей сессии)
        {

            return caseMapperList.Find(case11 => ((case11.Id == case1.Id) && (case11.key == key)));

        }

        public static void removeCasMap(CaseClasses case1, int key)  // Ищем в рамках конкретного key (ключ рабочей сессии)
        {

            caseMapperList.RemoveAll(case11 => ((case11.Id == case1.Id) && (case11.key == key)));
            return;

        }

        public static void updateCasMap(CaseClasses case1, int key)  // Ищем в рамках конкретного key (ключ рабочей сессии)
        {

            removeCasMap(case1,  key);
            addCasMap(case1, key);

            return;

        }

    }
    public class CasMap : CaseClasses
    {
        public int key { get; set; }
        public int AccountId { get; set; } = -1;
        public CasMap(CaseClasses case1, int key)
        {
            this.Id = case1.Id; ;
            this.AuthorTxt = case1.AuthorTxt;
            this.Author = case1.Author;
            this.User = case1.User;
            this.CreateDate = case1.CreateDate;
            this.Caption = case1.Caption;
            this.OrderTypeFullName = case1.OrderTypeFullName;
            this.OrderNo = case1.OrderNo;
            this.Host = case1.Host;
            this.Message = case1.Message;
            this.WorkTime = case1.WorkTime;
            this.CaseCustomerId = case1.CaseCustomerId;
            this.CaseReasonId = case1.CaseReasonId;
            this.CaseStateId = case1.CaseStateId;
            this.CaseRecallId = case1.CaseRecallId;
            this.ChangeDate = case1.ChangeDate;
            this.Source = case1.Source;
            this.Ok = case1.Ok;
            this.HowToResponse = case1.HowToResponse;
            this.deleted = case1.deleted;
            this.SendedAutoEMAil = case1.SendedAutoEMAil;
            this.GuiltyDept = case1.GuiltyDept;
            this.Reason = case1.Reason;
            this.CaseState = case1.CaseState;
            this.StateDate = case1.StateDate;
            this.dtable = case1.dtable;
            this.SourceName = case1.SourceName;
            this.ReasonName1 = case1.ReasonName1;
            this.ReasonName2 = case1.ReasonName2;
            this.ReasonName3 = case1.ReasonName3;


        }

    }
}
