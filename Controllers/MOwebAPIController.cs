using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOweb.WebAPI;

namespace MOweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MOwebAPIController : Controller
    {
        private IMOwebAPI mowebapi;
        public MOwebAPIController(IMOwebAPI mowebapi)
        {
            this.mowebapi = mowebapi;
        }

        [HttpGet("GetMRPShedule")]
        public async Task<string> GetMRPShedule(string vc, string sd, string cd)
        {
            return await mowebapi.GetMRPShedule(vc, sd, cd);
        }
    }
}