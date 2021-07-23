using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARMClasses;
using AppClasses;
using MOUserClasses;


namespace SuperVisorToManager
{
    public class ARMTypeWithCheck
    {
        public ARM_ARMType ARMTypeEntry;
        public bool Check;

    } //ARMTypeWithCheck


    public class ARMTypeWithCheckList : List<ARMTypeWithCheck>
    {
        public SuperVisorToManagerFormClass Owner;
        public ARMTypeWithCheckList() { ReFill(); }
        public void ReFill()
        {
            ARMTypeWithCheck Item;
            for (int i = 0; i < ARM_ARMTypeFullList.Items.Count; i++)
            {
                Item = new ARMTypeWithCheck();
                Item.ARMTypeEntry = ARM_ARMTypeFullList.Items[i];



                if (Item.ARMTypeEntry.id == 3) Item.Check = true; //Оператор



                Add(Item);
            }
        } // ReFill()

        public void SetCheckValueForARM(int id, bool Value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].ARMTypeEntry.id == id)
                {
                    this[i].Check = Value;
                    Owner.RefillUserList();
                    break;
                }
            }
            return;
        } //SetCheckValueForARM
    } //ARMTypeWithCheckList

    public class SuperVisorToManagerFormClass : ModelBase
    {
        public SuperVisorToManagerFormClass()
        {
            MOUserList.LoadSuperVisorsForAllUsers();
        }
        private ARMTypeWithCheckList FARMTypeList;
        public ARMTypeWithCheckList ARMTypeList
        {
            get
            {
                if (FARMTypeList == null)
                {
                    FARMTypeList = new ARMTypeWithCheckList();
                    FARMTypeList.Owner = this;

                    RefillUserList();
                }
                return FARMTypeList;
            }
        }


        public List<MOUser> UserList = new List<MOUser>(); 
        public void RefillUserList()
        {
            UserList.Clear();
            for (int i = 0; i < ARMTypeList.Count; i++)
            {
                if (ARMTypeList[i].Check)
                {
                    for (int j = 0; j < MOUserList.Items.Count; j++)
                    {
                        if (MOUserList.Items[j].IsARMMember(ARMTypeList[i].ARMTypeEntry.id))
                        {
                            //Игнорирование дублей и сортировка вставкой
                            for (int k = 0; k < UserList.Count; k++)
                            {
                                if (MOUserList.Items[j].AccountId == UserList[k].AccountId)
                                {
                                    goto to_next_item;
                                }
                                else if (String.Compare(MOUserList.Items[j].Name, UserList[k].Name) < 0)
                                {
                                    UserList.Insert(k, MOUserList.Items[j]);
                                    goto to_next_item;
                                }
                            }
                            UserList.Add(MOUserList.Items[j]);
                        }
                        to_next_item:;
                    }
                }
            }
        } //RefillUserList
    } //SuperVisorToManagerFormClass


    public class AssignSuperVisorToUserForm_class : ModelBase
    {
        public MOUser User;
        public int UserId
        {
            get { if (User == null){ return 0; } else return User.AccountId; }
            set { User = MOUserList.GetUserById(value); }
        }
        public List<MOUser> SuperVisorList = MOUserList.Items;
        public int SelectedSV_id { get; set; }
        public DateTime deSVDate { get; set; }

    }

}
