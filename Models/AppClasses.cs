using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOweb.Infrastructure;
using MOUserClasses;
using ARMClasses;
using System.Data;
using System.Data.SqlClient;
using Utils.Common;
using SuperVisorToManager;


namespace AppClasses
{
    public class CustomerCardParam
    {
        public int Customer;
        public int Host;
        public int TypeClientCard;
        public int OrderNo;

    }

    public class AppInstance
    {
        public int Id;


        public int NeedConnectToCollOColl = 0;


        private Thread COCCmdThread;
        private bool LoopWork_flg = true;
        private MOUser FCurrentUser;
        public MOUser CurrentUser
        {
            get { return FCurrentUser; }
            set
            {
                FCurrentUser = value;
                FARMType = ARM_ARMTypeFullList.GetARMTypeById(FCurrentUser.ARMTypeId);

                COCCmdThread = new Thread(CollOCollCmdLoop);
                COCCmdThread.Start();
            }
        }

        public SuperVisorToManagerFormClass FSuperVisorToManagerFormClass;
        public SuperVisorToManagerFormClass GetSuperVisorToManagerFormClass()
        {
            if (FSuperVisorToManagerFormClass == null) { FSuperVisorToManagerFormClass = new SuperVisorToManagerFormClass(); }
            return FSuperVisorToManagerFormClass;
        }

        public ARM_ARMType FARMType;
        public bool ARMCheckAccess(int TaskId)
        {
            return FARMType.ChackTaskLink(TaskId);
        }


        public List<CustomerCardParam> CustomerCardParamList = new List<CustomerCardParam>();

        //ЭТО НАДО??
        private List<int> ViewDiscrList = new List<int>();

        private int FViewDiscrCntr = 0;
        public int GetNewViewDiscr()
        {
            ++FViewDiscrCntr;
            ViewDiscrList.Add(FViewDiscrCntr);
            return FViewDiscrCntr;
        }

        private void CollOCollCmdLoop()
        {
            while (LoopWork_flg)
            {
                string Q = "coscSingleCallOCall_cmd_proc";
                using (SqlConnection connection = new SqlConnection(cn.ConStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(Q, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@action", "GET");
                        command.Parameters.AddWithValue("@coscAppId", Id.ToString());
                        command.Parameters.AddWithValue("@Direction", "FOR_SRV");

                        SqlDataAdapter da = new SqlDataAdapter();
                        DataSet ds = new DataSet();
                        da = new SqlDataAdapter(command);
                        da.Fill(ds);

                        string Cmd;
                        string Params;

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Cmd = Convert.ToString(ds.Tables[0].Rows[i]["Cmd"]);
                            Params = Convert.ToString(ds.Tables[0].Rows[i]["Params"]);
                            CustomerCardParam cpp = null;

                            if (Cmd.Equals("ASK_USER_ID")) { ReturnUserId(Params); }
                            else if (Cmd.Equals("GET_CARD_FOR")) { GetCardFor(Params, ref cpp); }
                        }
                    }
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            } //while
        } //CollOCollCmdLoop

        private void ReturnUserId(string Params)
        {
            PutCmd("RETURN_USER_ID", "UserId=" + CurrentUser.AccountId.ToString());
        }  //ReturnUserId




        public void GetCardFor(string Params, ref CustomerCardParam ccp)
        {
            string[] pa = Params.Split(',');
            string Phone = "";
            string Line = "";
            foreach (string s in pa)
            {
                if (s.Split('=')[0] == "Phone") { Phone = s.Split('=')[1]; }
                else if (s.Split('=')[0] == "Line") { Line = s.Split('=')[1]; }
            }

            string Q = "cc_CallOCall_AutoCall_GetClientCard";
            //"cc_CallOCall_AutoCall_GetClientCard_with_Order";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@User", CurrentUser.AccountId.ToString());
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Line", Line);

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        bool with_return = false;
                        if (ccp == null)
                        {
                            ccp = new CustomerCardParam();
                            with_return = true;
                        }
                        ccp.Customer = Convert.ToInt32(ds.Tables[0].Rows[0]["Customer"]);
                        ccp.Host = Convert.ToInt32(ds.Tables[0].Rows[0]["Host"]);
                        ccp.TypeClientCard = Convert.ToInt32(ds.Tables[0].Rows[0]["TypeClientCard"]);
                        ccp.OrderNo = Convert.ToInt32(ds.Tables[0].Rows[0]["OrderNo"]);
                        if (with_return) CustomerCardParamList.Add(ccp);
                    }
                }
            }
        } //GetCardFor



/*

        private void GetCardFor(string Params)
        {
            string[] pa = Params.Split(',');
            string Phone = "";
            string Line = "";
            foreach (string s in pa)
            {
                if (s.Split('=')[0] == "Phone") { Phone = s.Split('=')[1]; }
                else if (s.Split('=')[0] == "Line") { Line = s.Split('=')[1]; }
            }

            string Q = "cc_CallOCall_AutoCall_GetClientCard";
            //"cc_CallOCall_AutoCall_GetClientCard_with_Order";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@User", CurrentUser.AccountId.ToString());
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Line", Line);

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        CustomerCardParam ccp = new CustomerCardParam();
                        ccp.Customer = Convert.ToInt32(ds.Tables[0].Rows[0]["Customer"]);
                        ccp.Host = Convert.ToInt32(ds.Tables[0].Rows[0]["Host"]);
                        ccp.TypeClientCard = Convert.ToInt32(ds.Tables[0].Rows[0]["TypeClientCard"]);
                        ccp.OrderNo = Convert.ToInt32(ds.Tables[0].Rows[0]["OrderNo"]);
                        CustomerCardParamList.Add(ccp);
                    }
                }
            }
        } //GetCardFor

*/

        public int ExistsCardForCall()
        {
            if (CustomerCardParamList.Count > 0)
            { return 1; }
            else { return 0; }
        }

        public void MakeCall(string Phone, string Line, int Customer = 0, int Host = 0, int TypeClientCard = 1)
        {
            PutCmd("CALL", "Phone=" + Phone + ",Line=" + Line +",Customer=" + Customer.ToString() +
                    ",Host=" + Host.ToString() + ",TypeClientCard=" + TypeClientCard.ToString());

        }  //MakeCall

        private void PutCmd(string Cmd, string Params)
        {
            string Q = "coscSingleCallOCall_cmd_proc";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@action", "PUT");
                    command.Parameters.AddWithValue("@coscAppId", Id.ToString());
                    command.Parameters.AddWithValue("@Direction", "FOR_CLN");
                    command.Parameters.AddWithValue("@Cmd", Cmd);
                    command.Parameters.AddWithValue("@Params", Params);

                    command.ExecuteNonQuery();
                }
            }
        }
    } //AppInstance

    public static class AppInstanceList
    {
        public static List<AppInstance> Items = new List<AppInstance>();
        public static AppInstance CheckUserPwd(string ALogin, string APwd)
        {
            AppInstance app = null;
            MOUser mu;
            for (int i = 0; i < MOUserList.Items.Count; i++)
            {
                mu = MOUserList.Items[i];
                if (mu.Login == ALogin)
                {
                    if (mu.CheckPwd(APwd))
                    {
                        app = AddApp();
                        app.CurrentUser = mu;
                    }
                    break;
                }
            }
            return app;
        }

        private static int FAppId = 0;
        private static int NewAppId { get { return ++FAppId; } }
        public static AppInstance AddApp()
        {
            AppInstance App = new AppInstance();
            Items.Add(App);
            App.Id = NewAppId;
            return App;
        }

        public static void vRemoveApp(int AppId)
        {
            Items.Remove(GetItemsById(AppId));
        }

        public static AppInstance GetItemsById(int ItemId)
        {
            for (int i = 0; i < Items.Count; i++){

                if (Items[i].Id == ItemId) { return Items[i]; }
            }
            return null;
        }
    }  //AppInstanceList

    public class LoginFrm
    {
        public List<string> LoginList = MOUserList.LoginList;

        public string login { get; set; }
        public string password { get; set; }  //md5
        public String ErrMsg = "";
    }


    public class ChangePasswordForm
    {
        public string login;
        public int AppId { get; set; } //Созданый экземпляр приложения но не положен в контекст
        public string old_password { get; set; }  //md5
        public string new_password {get; set; }  //md5
        public String ErrMsg = "";

    }



    public class ModelBase
    {
        public Controller ctrl
        {
            set
            {
                string app_id = value.HttpContext.Session.GetJson<string>("AppId");
                if (app_id != null){ App = AppInstanceList.GetItemsById(Convert.ToInt32(app_id)); }
            }
        }
        public AppInstance App = null; 
    }
}
