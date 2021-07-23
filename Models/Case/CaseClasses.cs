using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MOweb.Models.Case
{
    public class CaseClasses
    {
        public Int64 Id { get; set; }
        public String AuthorTxt { get; set; }
        public int Author { get; set; }
        public int User { get; set; }
        public DateTime CreateDate { get; set; }
        public String Caption { get; set; }
        public String OrderTypeFullName { get; set; }
        public Int64? OrderNo { get; set; }
        public int Host { get; set; }
        public String Message { get; set; }
        public DateTime WorkTime { get; set; }
        public int? CaseCustomerId { get; set; }
        public int? CaseReasonId { get; set; }
        public int? CaseStateId { get; set; }
        public int? CaseRecallId { get; set; }
        public DateTime? ChangeDate { get; set; }
        public String Source { get; set; }
        public int? Ok { get; set; }
        public int? HowToResponse { get; set; }
        public int? deleted { get; set; }

        public int? SendedAutoEMAil { get; set; }
        public int? GuiltyDept { get; set; }

        public String Reason { get; set; }
        public int? CaseState { get; set; }
        public DateTime? StateDate { get; set;}

        public DataTable dtable;
        public String SourceName;
        public String ReasonName1;
        public String ReasonName2;
        public String ReasonName3;

        public CaseClasses()
        {
        }

        public CaseClasses(int CaseId, long OrderNo22, int Host22 = 1)
        {

            if (CaseId != 0 && CaseId != null)
            {
                string strQuery = String.Format($"EXEC MO.[dbo].[Case_GetCaseData] "
                                   + $"@CaseId= {CaseId}, "
                                   + $"@Author = 2, "
                                   + $"@User = 3004,"
                                   + $"@OrderNo = null, "
                                   + $"@Customer = null, "
                                   + $"@Host = null ");

                DataSet customDataSet = new DataSet();

                using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
                {
                    connection.Open();
                    SqlDataAdapter custAdapter = new SqlDataAdapter(strQuery, connection);
                    custAdapter.Fill(customDataSet, "Case_GetCaseData");

                    connection.Close();
                }

                // Заголовочные поля Кейса.
                Id = Convert.ToInt64(customDataSet.Tables["Case_GetCaseData"].Rows[0]["CaseId"].ToString()); //Rows[0].ItemArray[0].ToString());
                Author = Convert.ToInt16(customDataSet.Tables["Case_GetCaseData"].Rows[0]["Author"].ToString());
                User = Convert.ToInt16(customDataSet.Tables["Case_GetCaseData"].Rows[0]["User"].ToString());
                AuthorTxt = customDataSet.Tables["Case_GetCaseData"].Rows[0]["AuthorTxt"].ToString(); //.Columns[3].Ro//ToString();//Rows["Auth].ToString();
                CreateDate = Convert.ToDateTime(customDataSet.Tables["Case_GetCaseData"].Rows[0]["CreateDate"].ToString());
                Caption = customDataSet.Tables["Case_GetCaseData"].Rows[0]["Caption"].ToString();
                OrderTypeFullName = customDataSet.Tables["Case_GetCaseData"].Rows[0]["OrderTypeFullName"].ToString();
                Reason = customDataSet.Tables["Case_GetCaseData"].Rows[0]["Reason"].ToString();
                HowToResponse = Convert.ToInt16(customDataSet.Tables["Case_GetCaseData"].Rows[0]["HowToResponse"].ToString());

                if (!Convert.IsDBNull(customDataSet.Tables["Case_GetCaseData"].Rows[0]["OrderNo"]))
                {
                    OrderNo = Convert.ToInt64(customDataSet.Tables["Case_GetCaseData"].Rows[0]["OrderNo"].ToString());
                };

                if (!Convert.IsDBNull(customDataSet.Tables["Case_GetCaseData"].Rows[0]["HostId"]))
                {
                    Host = Convert.ToInt32(customDataSet.Tables["Case_GetCaseData"].Rows[0]["HostId"].ToString());
                };

                if (!Convert.IsDBNull(customDataSet.Tables["Case_GetCaseData"].Rows[0]["Source"]))
                {
                    Source = customDataSet.Tables["Case_GetCaseData"].Rows[0]["Source"].ToString();
                };

                if (!Convert.IsDBNull(customDataSet.Tables["Case_GetCaseData"].Rows[0]["CustomerId"]))
                {
                    CaseCustomerId = Convert.ToInt32(customDataSet.Tables["Case_GetCaseData"].Rows[0]["CustomerId"].ToString());
                };


                // Табличка событий.
                //Id = Convert.ToInt64(customDataSet.Tables[1].Rows[0]["TypeMessage"].ToString());

                dtable = customDataSet.Tables[1];

            }  //  if (CaseId != 0 && CaseId != null)
            else
            {
                OrderNo = OrderNo22;
                Id = CaseId;

                string strQuery = String.Format($"select top 1 Customer from MO.dbo.cc_OrderHead(NOLOCK) where "
                                   + $"OrderNo= {OrderNo22} "
                                   + $"and Host = {Host22}");

                DataSet customDataSet = new DataSet();

                using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
                {
                    connection.Open();
                    SqlDataAdapter custAdapter = new SqlDataAdapter(strQuery, connection);
                    custAdapter.Fill(customDataSet, "Table1");

                    connection.Close();
                }

                CaseCustomerId = Convert.ToInt32(customDataSet.Tables["Table1"].Rows[0]["Customer"].ToString());


            }

                //------------------------------------------------------------------------------------------------------------------
                //------------------------------------------------------------------------------------------------------------------
                string strQuery2 = String.Format($"EXEC MO.[dbo].[Case_GetAllHierarchy] "
                              + $"@ReasonCode3= '{Reason}'");

            DataSet customDataSet2 = new DataSet();

            using (SqlConnection connection2 = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection2.Open();
                SqlDataAdapter custAdapter2 = new SqlDataAdapter(strQuery2, connection2);
                custAdapter2.Fill(customDataSet2, "Case_GetAllHierarchy");

                connection2.Close();
            }

            // Заголовочные поля Кейса.
            SourceName =    customDataSet2.Tables["Case_GetAllHierarchy"].Rows[0]["SourceName"].ToString(); //Rows[0].ItemArray[0].ToString());
            ReasonName1 =   customDataSet2.Tables["Case_GetAllHierarchy"].Rows[0]["ReasonName1"].ToString();
            ReasonName2 =   customDataSet2.Tables["Case_GetAllHierarchy"].Rows[0]["ReasonName2"].ToString();
            ReasonName3 =   customDataSet2.Tables["Case_GetAllHierarchy"].Rows[0]["ReasonName3"].ToString(); //.Columns[3].Ro//ToString();//Rows["Auth].ToString();



            //int i = dtable.Rows.Count

            //String s = dtable.Rows[0][0].ToString();
            //s = dtable.Rows[0][1].ToString();
            //s = dtable.Rows[1][0].ToString();
            //s = dtable.Rows[1][0].ToString();



            /*
                                public int Id { get; set; }
                    public String AuthorTxt { get; set; }
                    public int User { get; set; }
                    public DateTime Date { get; set; }
                    public String Caption { get; set; }
                    public int? OrderNo { get; set; }
                    public int Host { get; set; }
                    public String Message { get; set; }
                    public DateTime WorkTime { get; set; }
                    public int? CaseCustomerId { get; set; }
                    public int? CaseReasonId { get; set; }
                    public int? CaseStateId { get; set; }
                    public int? CaseRecallId { get; set; }
                    public DateTime? ChangeDate { get; set; }
                    public int? Source { get; set; }
                    public int? Ok { get; set; }
                    public int? HowToResponse { get; set; }
                    public int? deleted { get; set; }

                    public int? SendedAutoEMAil { get; set; }
                    public int? GuiltyDept { get; set; }

                    public String Reason1 { get; set; }
                    public int? CaseState { get; set; }
                    public DateTime? StateDate { get; set; }
            */

            /*
               public string GetAllARMCheckAccesses()
        {
            DataSet armAccesses = new DataSet();
            object[] values = new object[2];
            armAccesses.Tables.Add("ArmAccesses");
            armAccesses.Tables["ArmAccesses"].Columns.Add("T", typeof(int));    // Task
            armAccesses.Tables["ArmAccesses"].Columns.Add("V", typeof(int));    // Value 0 or 1 
            var tls = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).FARMType.TaskList;
            foreach (var tl in tls)
            {
                values.SetValue(tl.Item.id, 0);
                values.SetValue(tl.CurCheck ? 1 : 0, 1);
                armAccesses.Tables["ArmAccesses"].Rows.Add(values);
            }
            //foreach(var )
            return JsonConvert.SerializeObject(armAccesses);//AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).ARMCheckAccess(Task);

             */




            /*
            SqlDataAdapter  SqlDataAdapter custAdapter = new SqlDataAdapter(StrQuery, connection);
            custAdapter.Fill(customDataSet, "MRPConfirmedHungOrders");
            connection.Close();
            */

            //using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand(strQuery, connection))
            //    {
            //        var reader = command.ExecuteReader();
            //        reader.Close();
            //    }
            //    connection.Close();
            //}
        }


}
}
