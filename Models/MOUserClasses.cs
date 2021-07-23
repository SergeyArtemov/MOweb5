using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ARMClasses;
using Utils.Common;
using AppClasses;

namespace MOUserClasses
{

    public delegate void NotifyEvent(Object Sender);


    public class TabNo
    {
        public string Tab;
        public string FIO;

        public void FillObject(DataRow ARow)
        {
            Tab = scv.GetStringNull("Tab", ARow);
            FIO = scv.GetStringNull("FIO", ARow);
        }

    }

    public class SuperVisor
    {
        public SuperVisorsForUser Owner;

        public int Supervisor_id;
        public string Supervisor;
        public DateTime AssignDate;

        public void FillObject(DataRow ARow)
        {
            Supervisor_id = scv.GetIntNull("int,hidden - Supervisor", ARow);
            Supervisor = scv.GetStringNull("txt - Супервайзер", ARow);
            AssignDate = scv.GetDateTimeNull("dat - с даты", ARow);
        }

        public bool Save()
        {
            string Q = "PC_LinkOperSuperInsert";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Operator", Owner.User.AccountId);

                }
            }

            Q = "PC_LinkOperSuperAddNew";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Operator", Owner.User.AccountId);

                    command.Parameters.AddWithValue("@Supervisor", Supervisor_id);
                    string dt_as_str;
                    if (AssignDate.Day < 10)
                        { dt_as_str= '0' + AssignDate.Day.ToString() + '.';}
                    else
                        { dt_as_str= AssignDate.Day.ToString() + '.';}

                    if (AssignDate.Month < 10)
                        { dt_as_str = dt_as_str +'0' + AssignDate.Month.ToString() + '.'; }
                    else
                        { dt_as_str = dt_as_str + AssignDate.Month.ToString() + '.'; }

                    dt_as_str = dt_as_str + AssignDate.Year.ToString();
                      
                    command.Parameters.AddWithValue("@DateString", dt_as_str);


                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);
                }
            }









            return false;
        }
    }
    public class SuperVisorsForUser : List<SuperVisor>
    {
        public MOUser User;
        public DateTime LoadDt;
        public void Load()
        {
            string Q = "PC_ListSupervisorsOnUser";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idUser", User.AccountId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    SuperVisor sv;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        sv = new SuperVisor();
                        sv.Owner = this;
                        Add(sv);
                        sv.FillObject(ds.Tables[0].Rows[i]);
                    }
                }
            }
        } //Load()
        public SuperVisor AddSuperVisor(int ASupervisor_id, string ASupervisor, DateTime AAssignDate)
        {
            SuperVisor sv = new SuperVisor();
            sv.Owner = this;
            sv.Supervisor_id = ASupervisor_id;
            sv.Supervisor = ASupervisor;
            sv.AssignDate = AAssignDate;
            sv.Save();

            for (int i = 0; i < Count; i++) 
            {
                if (sv.AssignDate < this[i].AssignDate) { Insert(i, sv); goto to_end; }
            }
            Add(sv);

            to_end:
            return sv;
        }

    } //SuperVisorsForUser




    public class MOUser : ModelBase
    {
        //Из списка
        public int AccountId { get; set; }
        public string TabNo_caption { get; set; }
        public string Operator_caption { get; set; }
        public DateTime DismissalDate { get; set; }

        public string Login { get; set; }
        public string ArmTypeName { get; set; }
        public string CallOCallOperatorName { get; set; }
        public string CallOCallOperatorLogin { get; set; }
        public string wbRefOperatorName { get; set; }
        public string wbRefOperatorLogin { get; set; }
        public string ChatraOperatorName { get; set; }
        public string ChatraOperatorLogin { get; set; }

        //Для редактирования
        public string Name { get; set; }
        public string Surname { get; set; }
        public Int32 ARMTypeId { get; set; }
        public string md5_pass { get; set; }
        public int Filial { get; set; }
        public DateTime FiredDate { get; set; }
        public string NameForTransaction { get; set; }
        public int CallOCallUserId { get; set; }
        public int WebimOperatorId { get; set; }
        public string TabNo { get; set; }
        public string ChatraId { get; set; }
        public int IDGeneralEmployee { get; set; }
        public string email { get; set; }
        public int BlingerId { get; set; }
        public DateTime DateARM { get; set; }

        public int NeedPassChanges;

        public List<ARM_ARMType> ARMTypeList = ARM_ARMTypeFullList.Items;
        public List<FilialItem> Filials = FilialList.Items;
        public List<TabNo> TabNoItems = new List<TabNo>();
        public List<NamedItem> BasicBindings = new List<NamedItem>();

        public List<NamedItem> Call_O_CallLogins = new List<NamedItem>();
        public List<NamedItem> WebimLogins = new List<NamedItem>();
        public List<ID_strNamedItem> ChatraLogins = new List<ID_strNamedItem>();
        public List<NamedItem> BlingerLogins = new List<NamedItem>();
        public List<MOUser> FullSuperVisorList = MOUserList.Items;

        public void FillObjectForList(DataRow ARow)
        {
            AccountId = scv.GetIntNull("int - AccountId", ARow);
            TabNo_caption = scv.GetStringNull("txt - Таб. № (1С)", ARow);
            Operator_caption = scv.GetStringNull("txt - Оператор", ARow);
            DismissalDate = scv.GetDateTimeNull("dat - Уволен (1С)", ARow);
            Login = scv.GetStringNull("txt - Логин", ARow);
            ArmTypeName = scv.GetStringNull("txt - Тип АРМ", ARow);
            CallOCallOperatorName = scv.GetStringNull("txt - Call-O-Call Имя оператора", ARow);
            CallOCallOperatorLogin = scv.GetStringNull("txt - Call-O-Call Логин", ARow);
            wbRefOperatorName = scv.GetStringNull("txt - WebIM Имя оператора", ARow);
            wbRefOperatorLogin = scv.GetStringNull("txt - WebIM Логин", ARow);
            ChatraOperatorName = scv.GetStringNull("txt - Chatra Имя оператора", ARow);
            wbRefOperatorLogin = scv.GetStringNull("txt - Chatra Логин", ARow);

            ARMTypeId = scv.GetIntNull("int,hidden - id_ArmType", ARow);
        }

        public void Load()
        {
            string Q = "cc_Account_GetData";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Author", "3");
                    command.Parameters.AddWithValue("@User", "0");
                    command.Parameters.AddWithValue("@Account", AccountId.ToString());

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    DataRow ARow = ds.Tables[0].Rows[0];

                    Name = scv.GetStringNull("Name", ARow);
                    Surname = scv.GetStringNull("Surname", ARow);
                    try { ARMTypeId = Convert.ToInt32(ARow["ARMTypeId"]); } catch { ARMTypeId = 0; }
                    md5_pass = scv.GetStringNull("md5Pass", ARow);
                    Filial = Convert.ToInt32(ARow["Filial"]);
                    FiredDate = scv.GetDateTimeNull("FiredDate", ARow);
                    NameForTransaction = scv.GetStringNull("NameForTransaction", ARow);

                    try { CallOCallUserId = scv.GetIntNull("CallOCallUserId", ARow); } catch { CallOCallUserId = 0; }
                    try { WebimOperatorId = scv.GetIntNull("WebimOperatorID", ARow); } catch { WebimOperatorId = 0; }
                    TabNo = scv.GetStringNull("TabNo", ARow);
                    ChatraId = scv.GetStringNull("ChatraID", ARow);
                    IDGeneralEmployee = scv.GetIntNull("IDGeneralEmployee", ARow);
                    email = Convert.ToString(ARow["email"]);
                    try { BlingerId = scv.GetIntNull("BlingerId", ARow); } catch { BlingerId = 0; }
                }
            }
            LoadDataForComboBoxies();
        } //Load()

        public bool CheckPwd(string APwd)
        {
            //Тут будем проверять пароль
            if (md5_pass == null) { Load(); } //похоже не грузилось, грузим
            RefreshLoginData();
            if (String.Compare(APwd, md5_pass, true) == 0) { return true; } else { return false; }
        }

        public bool ChangePwd(string NewPwd)
        {
            string Q = "cc_login_PassChange";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    SqlParameter sp;
                    command.CommandType = CommandType.StoredProcedure;

                    sp = command.Parameters.Add("@login", SqlDbType.VarChar);
                    sp.Value = Login;

                    sp = command.Parameters.Add("@old_md5", SqlDbType.VarChar);
                    sp.Value = md5_pass;

                    sp = command.Parameters.Add("@new_md5", SqlDbType.VarChar);
                    sp.Value = NewPwd;

                    command.Parameters.AddWithValue("@Author", "3");
                    command.Parameters.AddWithValue("@User", AccountId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);
                }
            }
            md5_pass = NewPwd;
            NeedPassChanges = 0;

            return true;
        }


        private void RefreshLoginData()
        {
            string Q = "cc_login_PassCheck";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Login", Login);
                    command.Parameters.AddWithValue("@md5pass", md5_pass);

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    DataRow ARow = ds.Tables[0].Rows[0];

                    NeedPassChanges = scv.GetIntNull("NeedPassChanges", ARow);
                }
            }
        }

        private void LoadDataForComboBoxies()
        {
            string Q = "cc_Account_GetAvailableTabNo"; //Табельные номена (???) 
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Author", "3");
                    command.Parameters.AddWithValue("@User", "0");
                    command.Parameters.AddWithValue("@Empl", AccountId.ToString());

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    TabNo tn;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tn = new TabNo();
                        tn.FillObject(ds.Tables[0].Rows[i]);
                        TabNoItems.Add(tn);
                    }
                }
            }//Табельные номена

            NamedItem ni;
            Q = "cc_Account_GetGeneral"; //Основная привязка (???) 
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    SqlParameter sp;
                    command.CommandType = CommandType.StoredProcedure;

                    sp = command.Parameters.Add("@Surnane", SqlDbType.VarChar);
                    if (Surname != null) { sp.Value = Surname; } else { sp.Value = ""/*DBNull.Value*/; }

                    //command.Parameters.AddWithValue("@Surnane", Surname);

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);


                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ni = new NamedItem();
                        ni.FillObjectByPos(ds.Tables[0].Rows[i]);
                        BasicBindings.Add(ni);
                    }
                }
            }//Основная привязка


            Q = "cc_Account_GetAvailableLogins"; //Логины в других системах 
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmployeeID", AccountId.ToString());

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    //Call-O-Call====================================
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ni = new NamedItem();
                        ni.FillObjectByPos(ds.Tables[0].Rows[i]);
                        Call_O_CallLogins.Add(ni);
                    }

                    //Webim==========================================
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        ni = new NamedItem();
                        ni.FillObjectByPos(ds.Tables[1].Rows[i]);
                        WebimLogins.Add(ni);
                    }

                    //Chatra==========================================
                    ID_strNamedItem nis;
                    for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                    {
                        nis = new ID_strNamedItem();
                        nis.FillObjectByPos(ds.Tables[2].Rows[i]);
                        ChatraLogins.Add(nis);
                    }


                    //Blinger==========================================
                    for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    {
                        ni = new NamedItem();
                        ni.FillObjectByPos(ds.Tables[3].Rows[i]);
                        BlingerLogins.Add(ni);
                    }
                }
            }//Логины в других системах
        }

        public void SaveUser()
        {
            string Q = "cc_login_ChangeEmployee";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    SqlParameter sp;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", AccountId);

                    sp = command.Parameters.Add("@Surname", SqlDbType.VarChar);
                    if (Surname != null) { sp.Value = Surname; } else { sp.Value = DBNull.Value; }

                    sp = command.Parameters.Add("@Name", SqlDbType.VarChar);
                    if (Name != null) { sp.Value = Name; } else { sp.Value = DBNull.Value; }

                    command.Parameters.AddWithValue("@AccessLevel", ARMTypeId);

                    sp = command.Parameters.Add("@login", SqlDbType.VarChar);
                    if (Login != null) { sp.Value = Login; } else { sp.Value = DBNull.Value; }

                    sp = command.Parameters.Add("@BlockDate", SqlDbType.DateTime);
                    if (FiredDate != default) { sp.Value = FiredDate; } else { sp.Value = DBNull.Value; }

                    command.Parameters.AddWithValue("@Author", 3);

                    sp = command.Parameters.Add("@User", SqlDbType.Int);
                    sp.Value = App.CurrentUser.AccountId;

                    command.Parameters.AddWithValue("@Filial", Filial);

                    sp = command.Parameters.Add("@md5_pass", SqlDbType.VarChar);
                    if (md5_pass != null) { sp.Value = md5_pass; } else { sp.Value = DBNull.Value; }

                    sp = command.Parameters.Add("@NameForTransaction", SqlDbType.VarChar);
                    if (NameForTransaction != null) { sp.Value = NameForTransaction; } else { sp.Value = DBNull.Value; }

                    sp = command.Parameters.Add("@Milo", SqlDbType.VarChar);
                    if (email != null) { sp.Value = email; } else { sp.Value = DBNull.Value; }

                    sp = command.Parameters.Add("@CallOCallUserId", SqlDbType.Int);
#pragma warning disable CS0472 // Результат выражения всегда равен "true", поскольку значение типа "int" никогда не равно Null типа "int?"
                    if (CallOCallUserId != null) { sp.Value = CallOCallUserId; } else { sp.Value = DBNull.Value; }
#pragma warning restore CS0472 // Результат выражения всегда равен "true", поскольку значение типа "int" никогда не равно Null типа "int?"

                    sp = command.Parameters.Add("@WebImUserId", SqlDbType.Int);
#pragma warning disable CS0472 // Результат выражения всегда равен "true", поскольку значение типа "int" никогда не равно Null типа "int?"
                    if (WebimOperatorId != null) { sp.Value = WebimOperatorId; } else { sp.Value = DBNull.Value; }
#pragma warning restore CS0472 // Результат выражения всегда равен "true", поскольку значение типа "int" никогда не равно Null типа "int?"

                    sp = command.Parameters.Add("@TabNo", SqlDbType.VarChar);
                    if (TabNo != null) { sp.Value = TabNo; } else { sp.Value = DBNull.Value; }

                    sp = command.Parameters.Add("@ChatraUserId", SqlDbType.VarChar);
                    if (ChatraId != null) { sp.Value = ChatraId; } else { sp.Value = DBNull.Value; }


                    sp = command.Parameters.Add("@GeneralEmployee", SqlDbType.Int);
#pragma warning disable CS0472 // Результат выражения всегда равен "true", поскольку значение типа "int" никогда не равно Null типа "int?"
                    if (IDGeneralEmployee != null) { sp.Value = IDGeneralEmployee; } else { sp.Value = DBNull.Value; }
#pragma warning restore CS0472 // Результат выражения всегда равен "true", поскольку значение типа "int" никогда не равно Null типа "int?"


                    sp = command.Parameters.Add("@dateARM", SqlDbType.DateTime);
                    if (DateARM != default) { sp.Value = DateARM; } else { sp.Value = DBNull.Value; }

                    sp = command.Parameters.Add("@BlingerUserId", SqlDbType.Int);
#pragma warning disable CS0472 // Результат выражения всегда равен "true", поскольку значение типа "int" никогда не равно Null типа "int?"
                    if (BlingerId != null) { sp.Value = BlingerId; } else { sp.Value = DBNull.Value; }
#pragma warning restore CS0472 // Результат выражения всегда равен "true", поскольку значение типа "int" никогда не равно Null типа "int?"

                    command.Parameters.Add("@WithResult", SqlDbType.Int).Value = 1;

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    AccountId = scv.GetIntNull("id", ds.Tables[0].Rows[0]);
                }
            }
            MOUserList.OnSaveUser(this);
            RefreshUserListRow();
        } //SaveUser() 

        public void RefreshUserListRow()
        {
            string Q = "cc_login_ListOfEmployees";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AccountId", AccountId.ToString());
                    // command.Parameters.AddWithValue("@md5pass", md5_pass);

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    FillObjectForList(ds.Tables[0].Rows[0]);
                }
            }
        }
        public bool IsARMMember(int ARM_id)
        {
            //Процедура сосзана для случая если пользователь в дальнейшем сможет принадлежать 
            //НЕ только одному АРМу
            if (ARMTypeId == ARM_id) { return true; } else { return false; }

        }

        private SuperVisorsForUser FSuperVisors;
        public SuperVisorsForUser SuperVisors
        {
            get {
                if (FSuperVisors == null)
                {
                    FSuperVisors = new SuperVisorsForUser();
                    FSuperVisors.User = this;
                }
                return FSuperVisors;
            }
        }

        public string LastSuperVisor
        {
            get
            {
                if (SuperVisors.Count == 0) { return ""; }
                else { return SuperVisors[SuperVisors.Count - 1].Supervisor; }
            }
        }
        public String LastSuperVisorAssignDate
        {
            get
            {
                if (SuperVisors.Count == 0) { return ""; }
                else { return SuperVisors[SuperVisors.Count - 1].AssignDate.ToString(); }
            }
        }

        public bool AssignNewSuperVisor(int SuperVisorId, string SVName, DateTime AssignDate)
        {
            bool Res = false;

            SuperVisor NewSV = new SuperVisor();
            NewSV.Owner = SuperVisors;

            NewSV.Supervisor_id = SuperVisorId;
            NewSV.Supervisor = SVName;
            NewSV.AssignDate = AssignDate;

            Res = NewSV.Save();
            if (Res) { SuperVisors.Add(NewSV); } 

            return Res;
        }
    } //class MOUser

    public static class MOUserList
    {
        private static LoadState ls = LoadState.lsNoLoad;
        private static List<MOUser> FItems = new List<MOUser>();

        //= new List<MOUser>();
        public static List<MOUser> Items
        {
            get
            {
                if (ls == LoadState.lsNoLoad)
                {
                    Load();
                }
                return FItems;
            }
        }
        private static void Load()
        {
            ls = LoadState.lsLoading;
            string Q = "cc_login_ListOfEmployees";
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

                    MOUser mu = new MOUser();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        mu = new MOUser();
                        Items.Add(mu);
                        mu.FillObjectForList(ds.Tables[0].Rows[i]);
                    }
                }
            }
            ls = LoadState.lsLoaded;
        } //Load()
        public static MOUser NewUser()
        {
            return new MOUser();
        }
        public static void OnSaveUser(MOUser Sender)
        {
            if (GetUserById(Sender.AccountId) != null) { return; }
            Items.Add(Sender);
        }

        public static MOUser GetUserById(int UserId)
        {
            foreach (MOUser user in Items) { if (user.AccountId == UserId) { return user; } }
            return null;
        }

        public static List<string> LoginList {
            get
            {
                List<string> ls = new List<string>();
                bool inserted;
                int i;
                for (i = 0; i < Items.Count; i++)
                {
                    inserted = false;
                    for (int j = 0; j < ls.Count; j++) //Сортируем вставкой
                    {
                        if (MOUserList.Items[i].Login.CompareTo(ls[j]) < 0) //Должно стоять раньше
                        {
                            ls.Insert(j, MOUserList.Items[i].Login);
                            inserted = true;
                            break;
                        }
                    }
                    if (!inserted) { ls.Add(MOUserList.Items[i].Login); }
                }
                return ls;
            }
        }
        public static void LoadSuperVisorsForAllUsers()
        {
            if (ls != LoadState.lsLoaded) { return; } 

            string Q = "PC_ListSupervisorsOnUser";
            using (SqlConnection connection = new SqlConnection(cn.ConStr))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(Q, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
//                    command.Parameters.AddWithValue("@idUser", User.AccountId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    da = new SqlDataAdapter(command);
                    da.Fill(ds);

                    int CurUserId;
                    MOUser CurUser = null;


                    SuperVisor sv;
                    DataRow ARow;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ARow = ds.Tables[0].Rows[i];
                        CurUserId = scv.GetIntNull("Operator", ARow);
                        if (CurUser == null) 
                        {
                            CurUser = GetUserById(CurUserId);
                            if (CurUser == null) { continue; }  
                        } 
                        else if (CurUser.AccountId != CurUserId)
                        {
                            CurUser = GetUserById(CurUserId);
                            if (CurUser == null) { continue; }
                        }


                        sv = new SuperVisor();
                        CurUser.SuperVisors.Add(sv);
                        sv.Owner = CurUser.SuperVisors;
                        sv.FillObject(ARow);
                    }
                }
            }
        }
    } //class MOUserList


    public class FilialItem
    {
        public int id;
        public string Name;

        public void FillObject(DataRow ARow)
        {
            id = scv.GetIntNull("Id", ARow);
            Name = scv.GetStringNull("Name", ARow);
        }
    } //FilialItem

    public static class FilialList
    {
        public static LoadState ls = LoadState.lsNoLoad;

        public static List<FilialItem> FItems = new List<FilialItem>();
        public static List<FilialItem> Items
        {
            get
            {
                if (ls == LoadState.lsNoLoad) { LoadList(); }
                return FItems;
            }
        }

        private static void LoadList()
        {
            ls = LoadState.lsLoading;
            string Q = "cc_Ref_Filial_GetList";
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

                    FilialItem item;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        item = new FilialItem();
                        FItems.Add(item);
                        item.FillObject(ds.Tables[0].Rows[i]);
                    }
                }
            }
            ls = LoadState.lsLoaded;
        }
    } //FilialList


    public class MOUserManager : ModelBase
    {
        public List<MOUser> Items
        { get { return MOUserList.Items; } }
    }

    //пока здесь, затычка для отладки==================================
    public static class AppQqqq  
    {
        public static MOUser User = MOUserList.GetUserById(3054); //мое
    }
    //==================================================================



}



