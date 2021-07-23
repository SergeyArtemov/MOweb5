using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MOweb.Models;
using Utils.Common;
using AppClasses;


namespace ARMClasses

//"Server=sqlsrv02;Database=mo;Trusted_Connection=True;"


{
 /*   public static class cn
    {
        public static string ConStr = MOweb.Models.OrdersMapper.SqlConnectMO;
    }
    */

    public enum ARM_ItemType { itNone, itTask, itRefReport, itFRReport };


    public class ARM_Item     //  "Элемент на которую могут быть даны права в АРМ-е
    {
        public ARM_ItemType ItemType = ARM_ItemType.itNone;
        public int id;
        public string Name;

    }
    public delegate void D_ARM_ItemSave(int ItemId, bool ACheck);

    public class ARM_Item_Checked
    {
        public ARM_Item Item;
        public bool FCheck = false;
        public D_ARM_ItemSave SaveInARMType;
        public bool Check
        {
            get { return FCheck; }
            set
            {
                if (FCheck != value)
                {
                    SaveInARMType(Item.id, value);
                    FCheck = value;
                }
            }
        }

        public bool CurCheck //Вознащаят значение с учетом не сохраненных изменений
        {
            get
            {
                return FCheck;
            }
        }
    }

    public class ARM_Task : ARM_Item //  Задача системы на которую могут быть даны права в АРМ-е 
    {
        public void FillObject(DataRow ARow)
        {
            id = Convert.ToInt32(ARow["id"]);
            Name = Convert.ToString(ARow["Name"]);
        }
    } //ARM_Task

    public class ARM_Report : ARM_Item  //Отчет системы на которую могут быть даны права в АРМ-е 
    {
        public void FillObject(DataRow ARow)
        {
            id = Convert.ToInt32(ARow["id"]);
            Name = Convert.ToString(ARow["ReportName"]);
        }
    } //ARM_Report


    public static class ARM_TaskFullList //  Полный список задач системы *********************************
    {
        private static LoadState LS = LoadState.lsNoLoad;
        private static List<ARM_Task> FItems = new List<ARM_Task>();
        public static List<ARM_Task> Items
        {
            get
            {
                if (LS == LoadState.lsNoLoad)
                {
                    LS = LoadState.lsLoading;
                    //  FItems = new List<ARM_Task>();
                    ARM_DataLoader.Load();
                    LS = LoadState.lsLoaded;
                }
                return FItems;
            }
        }
        public static void Filllist(DataTable tbl)
        {
            //int i = 
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                ARM_Task tsk = new ARM_Task();
                Items.Add(tsk);
                tsk.ItemType = ARM_ItemType.itTask;
                tsk.FillObject(tbl.Rows[i]);
            }
        }

 

    } //ARM_TaskFullList


    public static class ARM_Ref_ReportFullList //  Полный список Ref отчетов *********************************
    {
        private static LoadState LS = LoadState.lsNoLoad;
        private static List<ARM_Report> FItems = new List<ARM_Report>();
        public static List<ARM_Report> Items
        {
            get
            {
                if (LS == LoadState.lsNoLoad)
                {
                    LS = LoadState.lsLoading;
                    ARM_DataLoader.Load();
                    LS = LoadState.lsLoaded;
                }
                return FItems;
            }
        }
        public static void Filllist(DataTable tbl)
        {
            LS = LoadState.lsLoading;
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                ARM_Report rep = new ARM_Report();
                rep.ItemType = ARM_ItemType.itRefReport;
                Items.Add(rep);
                rep.FillObject(tbl.Rows[i]);
            }
            LS = LoadState.lsLoaded;
        }


    } //ARM_Ref_ReportFullList


    public static class ARM_FR_ReportFullList //  Полный список FR отчетов *********************************
    {
        private static LoadState LS = LoadState.lsNoLoad;
        private static List<ARM_Report> FItems = new List<ARM_Report>();
        public static List<ARM_Report> Items
        {
            get
            {
                if (LS == LoadState.lsNoLoad)
                {
                    LS = LoadState.lsLoading;
                    ARM_DataLoader.Load();
                    LS = LoadState.lsLoaded;
                }
                return FItems;
            }
        }
        public static void Filllist(DataTable tbl)
        {
            LS = LoadState.lsLoading;
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                ARM_Report rep = new ARM_Report();
                rep.ItemType = ARM_ItemType.itFRReport;
                Items.Add(rep);
                rep.FillObject(tbl.Rows[i]);
            }
            LS = LoadState.lsLoaded;
        }
    } //ARM_FR_ReportFullList


    public static class ARM_DataLoader
    {
        private static LoadState ls = LoadState.lsNoLoad;
        public static void Load()
        {
            if (ls != LoadState.lsNoLoad) { return; }
            ls = LoadState.lsLoading;

            string Q = "cc_ARM_GetDetail";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Author", "0");
                    command.Parameters.AddWithValue("@User", "0");
                    command.Parameters.AddWithValue("@ARM", "0");

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    ARM_TaskFullList.Filllist(ds.Tables[0]);
                    ARM_Ref_ReportFullList.Filllist(ds.Tables[1]);
                    ARM_FR_ReportFullList.Filllist(ds.Tables[2]);
                }
            }
        }
    }  //ARM_DataLoader


    public class ARM_ARMType //АРМ в системе
    {
        public int id;
        public string Name;

        public void ChangeName(string AName)
        {
            string Q = "cc_ARM_ChangeARMTypeName";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Author", "3");
                    command.Parameters.AddWithValue("@User", "0");
                    command.Parameters.AddWithValue("@Name", AName);
                    command.Parameters.AddWithValue("@ARMType", id.ToString());
                    command.ExecuteNonQuery();
                }
            }

        } //ChangeName

        public void FillObject(DataRow ARow)
        {
            id = Convert.ToInt32(ARow["id"]);
            Name = Convert.ToString(ARow["Name"]);
        }

     
        private List<Int32> FTaskLinkList;
        private List<Int32> TaskLinkList
        {
            get
            {
                if (FTaskLinkList == null) { LoadLinks(); }
                return FTaskLinkList;
            }
        }
        private List<Int32> FPefRepLinkList;
        private List<Int32> PefRepLinkList
        {
            get
            {
                if (FPefRepLinkList == null) { LoadLinks(); }
                return FPefRepLinkList;
            }
        }
        private List<Int32> FFRRepLinkList;
        private List<Int32> FRRepLinkList
        {
            get
            {
                if (FFRRepLinkList == null) { LoadLinks(); }
                return FFRRepLinkList;
            }
        }
        private void LoadLinks()
        {
            FTaskLinkList = new List<Int32>();
            FPefRepLinkList = new List<Int32>();
            FFRRepLinkList = new List<Int32>();

            string Q = "cc_ARM_GetLinksByType";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_ARMType", id.ToString());

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    DataTable tbl;
                    tbl = ds.Tables[0];
                    for (int i = 0; i < tbl.Rows.Count; i++) { FTaskLinkList.Add(Convert.ToInt32(tbl.Rows[i]["Task"])); }
                    tbl = ds.Tables[1];
                    for (int i = 0; i < tbl.Rows.Count; i++) { FPefRepLinkList.Add(Convert.ToInt32(tbl.Rows[i]["Report"])); }
                    tbl = ds.Tables[2];
                    for (int i = 0; i < tbl.Rows.Count; i++) { FFRRepLinkList.Add(Convert.ToInt32(tbl.Rows[i]["Report"])); }
                }
            }
        }//LoadLinks

        public bool ChackTaskLink(int id_Task)
        {
            for (int i = 0; i < TaskLinkList.Count; i++)
            {
                if (TaskLinkList[i] == id_Task) { return true; }
            }
            return false;
        }
        private bool ChackRefReportLink(int id_RefReport)
        {
            for (int i = 0; i < PefRepLinkList.Count; i++)
            {
                if (PefRepLinkList[i] == id_RefReport) { return true; }
            }
            return false;
        }
        private bool ChackFRReportLink(int id_FRReport)
        {
            for (int i = 0; i < FRRepLinkList.Count; i++)
            {
                if (FRRepLinkList[i] == id_FRReport) { return true; }
            }
            return false;
        }

        private List<ARM_Item_Checked> FTaskList;
        public List<ARM_Item_Checked> TaskList
        {
            get
            {
                if (FTaskList == null) { FillTaskList(); }
                return FTaskList;
            }
        }
        private void SaveTaskLink(int id_Task, bool ACheck)
        {
            //Сохраняем в базе даных
            string Enab = "0";
            if (ACheck) Enab = "1";
            string Q = "cc_ARM_ChangeARMTypeTask";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Author", "3");
                    command.Parameters.AddWithValue("@User", "0");
                    command.Parameters.AddWithValue("@Enabled", Enab);
                    command.Parameters.AddWithValue("@ARMType", id.ToString());
                    command.Parameters.AddWithValue("@Task", id_Task.ToString());
                    command.ExecuteNonQuery();
                }
            }

            //Вносим изменения в TaskLinkList
            if (ACheck) {
                FTaskLinkList.Add(id_Task);
            }
            else {
                for (int i = FTaskLinkList.Count - 1; i >= 0; i--)
                {
                    if (FTaskLinkList[i] == id_Task) { FTaskLinkList.RemoveAt(i); }
                }
            }
        }//SaveTaskLink
        private void FillTaskList()
        {
            FTaskList = new List<ARM_Item_Checked>();
            ARM_Item_Checked CHI;
            for (int i = 0; i < ARM_TaskFullList.Items.Count; i++)
            {
                CHI = new ARM_Item_Checked();
                FTaskList.Add(CHI);
                CHI.Item = ARM_TaskFullList.Items[i];
                CHI.FCheck = ChackTaskLink(CHI.Item.id); //При загрузке в обход проперти
                CHI.SaveInARMType = SaveTaskLink;
            }
        }//FillTaskList

        private List<ARM_Item_Checked> FRefReportList;
        public List<ARM_Item_Checked> RefReportList
        {
            get
            {
                if (FRefReportList == null) { FillRefReportList(); }
                return FRefReportList;
            }

        }
        private void SaveRefReportLink(int id_RefReport, bool ACheck)
        {
            string Enab = "0";
            if (ACheck) Enab = "1";
            
            //Сохранение в базе данных
            string Q = "cc_ARM_ChangeARMTypeReport";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Author", "3");
                    command.Parameters.AddWithValue("@User", "0");
                    command.Parameters.AddWithValue("@Enabled", Enab);
                    command.Parameters.AddWithValue("@ARMType", id.ToString());
                    command.Parameters.AddWithValue("@Report", id_RefReport.ToString());
                    command.ExecuteNonQuery();
                }
            }

            //

        }
        private void FillRefReportList()
        {
            FRefReportList = new List<ARM_Item_Checked>();
            ARM_Item_Checked CHI;
            for (int i = 0; i < ARM_Ref_ReportFullList.Items.Count; i++)
            {
                CHI = new ARM_Item_Checked();
                FRefReportList.Add(CHI);
                CHI.Item = ARM_Ref_ReportFullList.Items[i];
                CHI.FCheck = ChackRefReportLink(CHI.Item.id); //При загрузке в обход проперти
                CHI.SaveInARMType = SaveRefReportLink;
            }
        }


        private List<ARM_Item_Checked> FFRReportList;
        public List<ARM_Item_Checked> FRReportList
        {
            get
            {
                if (FFRReportList == null) { FillFRReportList(); }
                return FFRReportList;
            }
        }
        private void SaveFRReportLink(int id_FRReport, bool ACheck)
        {
            string Enab = "0";
            if (ACheck) { Enab = "1"; }

            string Q = "cc_ARM_FR_ChangeARMTypeReport";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Author", "3");
                    command.Parameters.AddWithValue("@User", "0");
                    command.Parameters.AddWithValue("@Enabled", Enab);
                    command.Parameters.AddWithValue("@ARMType", id.ToString());
                    command.Parameters.AddWithValue("@Report", id_FRReport.ToString());
                    command.ExecuteNonQuery();
                }
            }
        }
        private void FillFRReportList()
        {
            FFRReportList = new List<ARM_Item_Checked>();
            ARM_Item_Checked CHI;
            for (int i = 0; i < ARM_FR_ReportFullList.Items.Count; i++)
            {
                CHI = new ARM_Item_Checked();
                FFRReportList.Add(CHI);
                CHI.Item = ARM_FR_ReportFullList.Items[i];
                CHI.FCheck = ChackFRReportLink(CHI.Item.id); //При загрузке в обход проперти
                CHI.SaveInARMType = SaveFRReportLink;
            }
        }

        public void Save()
        {
            if (id == 0)  //Добавляем новый
            {
                string Q = "cc_ARM_AddARMType";
                using (SqlConnection connection = new SqlConnection(cn.ConStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(Q, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Author", "0");
                        command.Parameters.AddWithValue("@User", "0");
                        command.Parameters.AddWithValue("@Name", Name);

                        SqlDataAdapter da = new SqlDataAdapter();
                        DataSet ds = new DataSet();
                        da = new SqlDataAdapter(command);
                        da.Fill(ds);

                        id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                    }
                }
            }
            else  //Редактирование названия
            {
                string Q = "cc_ARM_ChangeARMTypeName";
                using (SqlConnection connection = new SqlConnection(cn.ConStr))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(Q, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Author", "0");
                        command.Parameters.AddWithValue("@User", "0");
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@ARMType", id.ToString());
                        command.ExecuteNonQuery();
                    }
                }
            }
        } //Save

        public void SaveLink(ARM_ItemType ItemType, int AitemId, bool ACheck)
        {
            int i;
            if (ItemType == ARM_ItemType.itTask)
            {
                for ( i = TaskList.Count - 1; i >= 0; i--) {
                    if (TaskList[i].Item.id == AitemId) {
                        TaskList[i].Check = ACheck;
                        break;
                    }
                }

                if (ACheck) { TaskLinkList.Add(AitemId); }
                else {
                    for (i = TaskLinkList.Count - 1; i >= 0; i--) {
                        if (TaskLinkList[i] == AitemId){
                            TaskLinkList.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            else if (ItemType == ARM_ItemType.itRefReport)
            {
                for (i = RefReportList.Count - 1; i >= 0; i--)
                {
                    if (RefReportList[i].Item.id == AitemId) {
                        RefReportList[i].Check = ACheck;
                        break;
                    }
                }

                if (ACheck) { PefRepLinkList.Add(AitemId); }
                else
                {
                    for (i = PefRepLinkList.Count - 1; i >= 0; i--)
                    {
                        if (PefRepLinkList[i] == AitemId) {
                            PefRepLinkList.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            else if (ItemType == ARM_ItemType.itFRReport)
            {

                for (i = FRReportList.Count - 1; i >= 0; i--)
                {
                    if (FRReportList[i].Item.id == AitemId) {
                        FRReportList[i].Check = ACheck;
                        break;
                    }
                }

                if (ACheck) { FFRRepLinkList.Add(AitemId); }
                else
                {
                    for (i = FFRRepLinkList.Count - 1; i >= 0; i--)
                    {
                        if (FFRRepLinkList[i] == AitemId)
                        {
                            FFRRepLinkList.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }

    }  //ARM_ARMType


    public static class ARM_ARMTypeFullList //  Полный список типов АРМов  *********************************
    {
        private static LoadState LS = LoadState.lsNoLoad;
        private static List<ARM_ARMType> FItems;
        public static List<ARM_ARMType> Items
        {
            get
            {
                if (LS == LoadState.lsNoLoad)
                {
                    LS = LoadState.lsLoading;
                    FItems = new List<ARM_ARMType>();
                    Load();
                    LS = LoadState.lsLoaded;
                }
                return FItems;
            }
        }
        private static void Load()
        {
            string Q = "cc_ARM_GetRefARMType";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);
                    Filllist(ds.Tables[0]);
                }
            }
        }
        private static void Filllist(DataTable tbl)
        {
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                ARM_ARMType AType = new ARM_ARMType();
                Items.Add(AType);
                AType.FillObject(tbl.Rows[i]);
            }
        }
        public static ARM_ARMType GetARMTypeById(Int32 id_ARMType)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].id == id_ARMType) { return Items[i]; }
            }
            return null;
        }

        public static ARM_ARMType AddARMType(string ARMTypeName)
        {
            ARM_ARMType Res = new ARM_ARMType();
            Res.Name = ARMTypeName;
            Res.Save();

            for (int i = 0; i < Items.Count; i++)
            {
                if (String.Compare(Res.Name, Items[i].Name) < 0) {
                    Items.Insert(i, Res);
                    break;
                }
            }
            Items.Add(Res);
            return Res;
        }
    } //ARM_ARMTypeFullList


    public class ARM_Manager : ModelBase
    {
        public List<ARM_Task> TaskList = ARM_TaskFullList.Items;
        public List<ARM_Report> RefRepkList = ARM_Ref_ReportFullList.Items;
        public List<ARM_Report> FRRepkList = ARM_FR_ReportFullList.Items;

        public List<ARM_ARMType> ARMTypeList = ARM_ARMTypeFullList.Items;
        public ARM_Manager()
        {
            CurARMTypeId = ARM_ARMTypeFullList.Items[0].id;
        }

        public ARM_ARMType FCurARMType;
        public ARM_ARMType CurARMType
        {
            get { return FCurARMType; }
        }

        private int FCurARMTypeId;
        public int CurARMTypeId
        {
            get { return FCurARMTypeId; }
            set
            {
                FCurARMTypeId = value;
                FCurARMType = ARM_ARMTypeFullList.GetARMTypeById(FCurARMTypeId);
            }
        }
        public int PageForShow { get; set; } = 1;


        public string NewARMName
        {
           // get; set;
            
            get { return null; }
            set
            {
                if (value != null)
                { CurARMTypeId = ARM_ARMTypeFullList.AddARMType(value).id; }
            }

        }
        public string EditingARMName
        {
            get { return null; }
            set
            {
                if (value != null)
                {
                    CurARMType.Name = value;
                    CurARMType.Save();
                }
            }
        }

        public string TaskStateChange { get; set; }
        public string refRepStateChange { get; set; }
        public string FRRepStateChange { get; set; }

        public bool ChBoxSave
        {
            get { return false; }
            set
            {
                if (value)
                {
                    int AitemId = 0;
                    bool ACheck = false;
                    ChangeLinksStrParser Parser = new ChangeLinksStrParser();

                    Parser.S = TaskStateChange;
                    while (Parser.GetNextItem(ref AitemId, ref ACheck))
                    {
                        CurARMType.SaveLink(ARM_ItemType.itTask, AitemId, ACheck);
                    }
                    TaskStateChange = "";

                    Parser.S = refRepStateChange;
                    while (Parser.GetNextItem(ref AitemId, ref ACheck))
                    {
                        CurARMType.SaveLink(ARM_ItemType.itRefReport, AitemId, ACheck);
                    }
                    refRepStateChange = "";

                    Parser.S = FRRepStateChange;
                    while (Parser.GetNextItem(ref AitemId, ref ACheck))
                    {
                        CurARMType.SaveLink(ARM_ItemType.itFRReport, AitemId, ACheck);
                    }
                    FRRepStateChange = "";
                }


            }
        }

        public bool ItemChecked(ARM_Item_Checked AItem)
        {
            string s = "";
            
           if (AItem.Item.ItemType == ARM_ItemType.itTask) { s = TaskStateChange; }
           else if (AItem.Item.ItemType == ARM_ItemType.itRefReport) { s = refRepStateChange; }
           else if (AItem.Item.ItemType == ARM_ItemType.itFRReport) { s = FRRepStateChange; }

           if (s == null) { return AItem.Check; }

            string FindStr = "[" + AItem.Item.id.ToString() + "]";
           Int32 pos = s.IndexOf(FindStr);

           if (pos >= 0)
           {
               if (s[pos + FindStr.Length + 1] == 0) { return false; } else { return true; }
           }
           else { return AItem.Check; }
        }

    } //ARM_Manager


    public class ChangeLinksStrParser
    {
        private int State;
        private int Pos;

        private bool NextChar(ref char ch)
        {
            Pos++;
            if (Pos < s_len)
            {
                ch = S[Pos];
                return true;
            }
            else { return false; }
        }

        private int s_len;
        public string FS;

        public string S
        {
            get { return FS; }
            set {
                State = 0;
                Pos = -1;
                FS = value;
                s_len = (FS == null) ? 0 : FS.Length;
            }
        }

        public bool GetNextItem(ref int AitemId, ref bool ACheck) {
            //Структура которую ищем и разбираем [id]check; пример [45]1;
            char ch = ' ';
            string id_str = "";
            string check_str = "";
            while (true)
            {
                if (!NextChar(ref ch)) { return false; }

                switch (State)
                {
                    case 0: //Вход
                        if (ch == '[') { State = 1; } //начало id
                        break;
                    case 1: //идет id
                        if (ch == ']') { State = 2; } //кончилось id
                        else { id_str += ch; } //идет id
                        break;
                    case 2: //идет check
                        if (ch == ';')
                        {  //конец блока нормальный выход из функции
                            AitemId = int.Parse(id_str);
                            //    ACheck = bool.Parse(check_str);
                            ACheck = (check_str == "1");
                            State = 0;
                            return true;
                        }
                        else { check_str += ch; }
                        break;
                }  //switch (State)
            } //while

        }


    }
}



