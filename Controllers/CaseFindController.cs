using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOweb.Models;

namespace MOweb.Controllers
{
    public class CaseFindController : Controller
    {
        public IActionResult Index()
        { // asa
            // ПОКА ЭТО ПРОСТО ЗАГЛУШКА!!!!
            string strQuery = String.Format($"EXEC MO.[dbo].[Case_GetCaseData] "
                   + $"@CaseId= 0, "
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
            //int Id = Convert.ToInt64(customDataSet.Tables["Case_GetCaseData"].Rows[0]["CaseId"].ToString()); //Rows[0].ItemArray[0].ToString());
            int Author = Convert.ToInt16(customDataSet.Tables["Case_GetCaseData"].Rows[0]["Author"].ToString());
            int User = Convert.ToInt16(customDataSet.Tables["Case_GetCaseData"].Rows[0]["User"].ToString());

            return View(Author/*ЗАГЛУШКА!!!*/);
        }
    }
}