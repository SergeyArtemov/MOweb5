using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOweb.Models;
using System.IO;
using ClosedXML.Excel;
using Newtonsoft.Json;

namespace MOweb.Controllers
{
    public class AreaEditorController : Controller
    {
        [HttpPost]
        public string LoadAreaListFromDB()
        {
            //string str = AreaEditor.LoadAreaListFromDB_XML();
            return JsonConvert.SerializeObject(AreaEditor.LoadAreaListFromDB());
        }

        [HttpPost]
        public string GetAreaIntervals(int areaId)
        {
            return JsonConvert.SerializeObject(AreaEditor.GetAreaIntervals(areaId));
        }
    }
}