using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ARMClasses;


namespace MOweb.Controllers
{
    public class ARMController : Controller
    {
        ARM_Manager ARMManager = new ARM_Manager();



        public IActionResult ARMForm()
        {
            ARMManager.ctrl = this;
            return View(ARMManager);
        }

        [HttpPost]
        public IActionResult ARMForm_cb_change(ARM_Manager ARMM)
        {

            /*
                        if (ARMM.NewARMName != null)
                        { ARMM.CurARMTypeId = ARM_ARMTypeFullList.AddARMType(ARMM.NewARMName).id; ARMM.NewARMName = null; }
                        else if (ARMM.EditingARMName != null)
                        { ARMM.CurARMType.Name = ARMM.EditingARMName;   ARMM.EditingARMName = null; }
                        */

            ARMM.ctrl = this;
            return View("ARMForm", ARMM);
        }
    }
}