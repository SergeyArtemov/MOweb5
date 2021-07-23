using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOUserClasses;
using SuperVisorToManager;
using AppClasses;


namespace MOweb.Controllers
{
    public class MOUserController : Controller
    {
        public MOUserManager UM = new MOUserManager();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MOUserList()
        {
            UM.ctrl = this;
            return View("MOUserList", UM);
        }

        [HttpGet]
        public IActionResult MOUserForm(int UserId)
        {
            MOUser MU;
            if (UserId == 0) { MU = new MOUser(); } 
            else
            {
                MU = MOUserClasses.MOUserList.GetUserById(UserId);
                MU.Load();
            }

            MU.ctrl = this;
            return View(MU);
        }

        [HttpPost]
        public IActionResult MOUserFormOk(MOUser MU)
        {
            MU.ctrl = this;
            MU.SaveUser();
            MOUserClasses.MOUserList.GetUserById(MU.AccountId).RefreshUserListRow();

            return MOUserList();
        }

        [HttpGet]
        public IActionResult MOUserFormCancel()
        {
            return MOUserList();
        }

    }

    public class SuperVisorToManagerFormController : Controller
    {
        //Назначение супервайзеров менеджерам

        public IActionResult SuperVisorToManagerForm()
        {
            ModelBase mb = new ModelBase();
            mb.ctrl = this;

            SuperVisorToManagerFormClass SV2M = mb.App.GetSuperVisorToManagerFormClass();
            SV2M.ctrl = this;
            return View("SuperVisorToManagerForm", SV2M);
        }
        public IActionResult SetCheckValueForARM(int id, bool Value)
        {
            ModelBase mb = new ModelBase();
            mb.ctrl = this;
            SuperVisorToManagerFormClass SV2M = mb.App.GetSuperVisorToManagerFormClass();
            SV2M.ARMTypeList.SetCheckValueForARM(id, Value);

            return SuperVisorToManagerForm();
            //return View("SuperVisorToManagerForm", SV2M);
        }


        public IActionResult SuperVisorListForUserForm(int UserId)
        {
            MOUser MU;
            if (UserId == 0) { MU = new MOUser(); }
            else
            {
                MU = MOUserClasses.MOUserList.GetUserById(UserId);
                MU.Load();
            }

            MU.ctrl = this;
            return View("SuperVisorListForUserForm", MU);
        }

        public IActionResult AssignSuperVisorToUserForm(int UserId)
        {
            
            AssignSuperVisorToUserForm_class AVUF = new AssignSuperVisorToUserForm_class();
            AVUF.ctrl = this;
            AVUF.UserId = UserId;
            return View("AssignSuperVisorToUserForm", AVUF);

        }

        public IActionResult AssignSuperVisorFormOk(int UserId, int SelectedSV_id, DateTime deSVDate)
        {
            MOUser MU;
            MU = MOUserClasses.MOUserList.GetUserById(UserId);

            AssignSuperVisorToUserForm_class AVUF = new AssignSuperVisorToUserForm_class();
            AVUF.ctrl = this;
            AVUF.UserId = UserId;

            MOUser NewSV = MOUserClasses.MOUserList.GetUserById(SelectedSV_id);

            MU.SuperVisors.AddSuperVisor(SelectedSV_id, NewSV.Operator_caption, deSVDate);


            //return View("AssignSuperVisorToUserForm", AVUF);

            return SuperVisorListForUserForm(AVUF.UserId);

        }



    }

}