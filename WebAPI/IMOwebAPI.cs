using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOweb.WebAPI
{
    public interface IMOwebAPI
    {
        Task<string> GetMRPShedule(string vc, string sd, string cd);
    }
}
