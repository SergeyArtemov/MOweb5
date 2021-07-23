using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOweb.Infrastructure;
using MOweb.Models;
using MOweb.Models.Case;
using MOweb.Models.ViewModels;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MOweb.Controllers
{

    public class FileModel
    {
        public string contentAsBase64String { get; set; }

        public string fileName { get; set; }

    }



    public class RabbitSend
    {
        public void TestRabbitSend()
        {
            var factory = new ConnectionFactory() { HostName = "10.2.18.42", UserName = "asa", Password = "asa" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello1",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello1",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            //Console.WriteLine(" Press [enter] to exit.");
            //Console.ReadLine();
        }
    }


    class RabbitReceive
    {
        public void TestRabbitRecieve()
        {
            var factory = new ConnectionFactory() { HostName = "10.2.18.42", UserName = "asa", Password = "asa" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello1",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                string message = "XXXX";

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "hello1",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit. {0}", message);
                Console.ReadLine();
            }
        }
    }


    public class CaseController : Controller
    {
        public IActionResult Index1(string caseId, string OrderNo)
        {
            //RABBIT TEST
            RabbitSend rs = new RabbitSend();
            rs.TestRabbitSend();

            RabbitReceive rr = new RabbitReceive();
            rr.TestRabbitRecieve();


            CaseView caseMO2View;
            caseId = caseId + "";

            if (caseId != "" && caseId != "0")
            {
                CaseClasses caseMO2 = new CaseClasses(Convert.ToInt32(caseId), 0);
                caseMO2View = new CaseView(caseMO2);
            }
            else
            {
                CaseClasses caseMO2 = new CaseClasses(0, Convert.ToInt32(OrderNo));
                caseMO2View = new CaseView(caseMO2);
            }

            // RABBIT для теста



            return View("~/Views/Case/Case.cshtml", caseMO2View);

        }

        public string Get_Case_Source_list()
        {
            string js1;

            string command = $"EXEC MO.dbo.cc_Get_Case_Ref_Source @inUse = 1 ,@oneColumn = 1";
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.Fill(customDataSet, "Case_Source_list");
                connection.Close();
            }

            return JsonConvert.SerializeObject(customDataSet);
        }

        public string get_Case_TematikaOnSource_list(string source)
        {
            string js1;

            string command = $"EXEC dbo.Case_Ref_GetTematikaOnSource @SourceName='Входящий звонок'";/*$"EXEC MO.dbo.Case_Ref_GetTematikaOnSource @SourceName='Входящий звонок'"; '" +source+"'"; */
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.Fill(customDataSet, "Case_TematikaOnSource_list");
                connection.Close();
            }

            return JsonConvert.SerializeObject(customDataSet);

        }

        public string get_Case_SutOnSourceTematika_list(string source, string tematika)
        {
            string js1;
            //@SourceName="+'Входящий звонок'+", @Tematika='SPEC Возвраты. Запрос в казначейство'";
            string command = $"EXEC dbo.Case_Ref_GetSutOnSourceTematika @SourceName='" + source + "', @Tematika='" + tematika + "'";
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.Fill(customDataSet, "Case_SutOnSourceTematika_list");
                connection.Close();
            }

            return JsonConvert.SerializeObject(customDataSet);

        }

        public string get_Case_ReasonQueueOnSourceTematikaSut(string source, string tematika, string sut)
        {
            string js1;
            //@SourceName="+'Входящий звонок'+", @Tematika='SPEC Возвраты. Запрос в казначейство'";
            string command = $"EXEC dbo.Case_Ref_GetReasonQueueOnSourceTematikaSut @SourceName='" + source + "', @Tematika='" + tematika + "'" + " ,@Sut='" + sut + "'";
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.Fill(customDataSet, "Case_ReasonQueueOnSourceTematikaSut_list");
                connection.Close();
            }

            return JsonConvert.SerializeObject(customDataSet);

        }

        public string GetLinkObjects(int CaseID1, string deleted, string Reason)
        {
            string js1;
            //@SourceName="+'Входящий звонок'+", @Tematika='SPEC Возвраты. Запрос в казначейство'";
            string command = $"EXEC dbo.Case_GetLinkObjects @CaseID=" + CaseID1.ToString() + ", @deleted=0, @Reason='" + Reason + "'";
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.Fill(customDataSet, "GetLinkObjects");
                connection.Close();
            }

            return JsonConvert.SerializeObject(customDataSet);

        }

        public string Case_DeleteLink(string str) //string CaseID,string deleted,string Reason)
        {
            //str = "";
            //@SourceName="+'Входящий звонок'+", @Tematika='SPEC Возвраты. Запрос в казначейство'";
            string command = $"EXEC dbo.Case_DeleteLink @idLink = 0, @UserId =2, @deleted=1, @listOfidLink='" + str + "'";
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.Fill(customDataSet, "Case_DeleteLink");
                connection.Close();
            }

            return JsonConvert.SerializeObject(customDataSet);

        }

        public string Case_GetItemsOnOrder(long OrderNo, int Host) //string CaseID,string deleted,string Reason)
        {
            //OrderNo = 2025290319;
            Host = 1;
            string command = $"EXEC dbo.Case_GetItemsOnOrder @OrderNo = " + OrderNo.ToString() + ", @Host =" + Host.ToString();
            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.Fill(customDataSet, "ItemsOnOrder");
                connection.Close();
            }

            return JsonConvert.SerializeObject(customDataSet);

        }

        public string Case_InsertLink(long CaseId1, long OrderNo, int Host, int LineNo, int UserId)
        {
            //OrderNo = 2025290319;
            Host = 1;
            UserId = 2;
            string command = $"EXEC dbo.Case_InsertLink @CaseId = " + CaseId1.ToString()
                                                    + ", @OrderNo = " + OrderNo.ToString()
                                                    + ", @Host =" + Host.ToString()
                                                    + ", @LineNo =" + LineNo.ToString()
                                                    + ", @UserId =" + UserId.ToString();


            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.Fill(customDataSet, "InsertLink");
                connection.Close();
            }

            return JsonConvert.SerializeObject(customDataSet);

        }

        public string SendEmail(int Author, int User, int Case, string Recipients, string Subject, string Body, int Profile, int PrevEmailId, string CC)
        {

            Author = 3;
            User = 2;
            Case = 1111;
            Subject = "test";
            //Body = "";
            Recipients = "sergei.artemov@kupivip.ru";
            Profile = 1;
            PrevEmailId = 0;
            CC = "";

            string command = $"EXEC Case_Email_Send_Head @Author = " + Author.ToString()
                                                    + ", @User = " + User.ToString()
                                                    + ", @Case =" + Case.ToString()
                                                    + ", @Recipients ='" + Recipients.ToString() + "'"
                                                    + ", @Subject ='" + Subject.ToString() + "'"
                                                    + ", @Body ='" + Body.ToString() + "'"
                                                    + ", @Profile =" + Profile.ToString()
                                                    + ", @PrevEmailId =" + PrevEmailId.ToString()
                                                    + ", @CC ='" + CC.ToString() + "'";


            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.Fill(customDataSet, "Res");
                connection.Close();
            }

            return JsonConvert.SerializeObject(customDataSet);

        }

        public string SendCase(int Author, int User,
                                int CaseId, string Caption,
                                string ReasonCode, int OrderType,
                                long OrderNo, int Host,
                                long Customer, int StateId,
                                string SourceString,
                                int HowToResponse,
                                string Comment
            /*    DateTime RecallDate, 
                string RecallDescription, string Comment,
                DateTime ShippingDate,
               int GuiltyDept*/
            )
        {

            //long CaseId = 0;
            //string Caption = "";
            //int ReasonCode = 1313131;
            //int OrderType = 0;
            //long OrderNo = 0;
            //int Host = 0;
            //long Customer = 0;

            StateId = 0;  //!!!!!!!!!!!!
            OrderType = 3;  //!!!!!!!!!!!!!!
            //CaseId = 0;//!!!!!!!!!!!!!!!!!

            DateTime RecallDate = DateTime.Now;
            string RecallDescription = "";
            //string Comment = "тест тест тест4";
            DateTime ShippingDate = DateTime.Now;
            //string SourceString = "";
            //int HowToResponse = 0;
            int GuiltyDept = 0;
            Host = 1;
            if (Comment == null) { Comment = ""; }

            Caption = "";

            // Берем текущего пользователя из текущей сессии, а затем детали берем из статического класса AppInstanceList
            int accId = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).CurrentUser.AccountId;

            string command = $"EXEC[dbo].[Case_Save] " +
                                "  @Author = " + Author.ToString() +
                                ", @User = " + accId.ToString() +
                                ", @CaseId = " + CaseId.ToString() +
                                ", @Caption = '" + Caption.ToString() + "'" +
                                ", @ReasonCode = '" + ReasonCode.ToString() + "'" +
                                ", @OrderType = " + OrderType.ToString() +
                                ", @OrderNo = " + OrderNo.ToString() +
                                ", @Host = " + Host.ToString() +
                                ", @Customer = " + Customer.ToString() +
                                ", @StateId = " + StateId.ToString() +
                                ", @RecallDate = null" + //RecallDate.ToString() + "'" +  //!!!!!!!!!!
                                ", @RecallDescription = '" + RecallDescription.ToString() + "'" +
                                ", @Comment = '" + Comment.ToString() + "'" +
                                ", @ShippingDate = null" + //ShippingDate.ToString() + "'" +    //!!!!!!!!!!
                                ", @SourceString = '" + SourceString.ToString() + "'" +
                                ", @HowToResponse = " + HowToResponse.ToString() +
                                ", @GuiltyDept = " + GuiltyDept.ToString();

            DataSet customDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                //int a = 2345 / HowToResponse;
                connection.Open();

                SqlDataAdapter custAdapter = new SqlDataAdapter(command, connection);
                custAdapter.Fill(customDataSet, "Res");
                connection.Close();
            }

            return JsonConvert.SerializeObject(customDataSet);
        }


        [HttpPost]
        public ActionResult Case_FileSend22(string base64String1, string fileName1)//FileModel filemodel)
        {
            // string command = $"EXEC [dbo].[Case_Attach_Add]" +
            //                 "  @Author = 3" +//+ Author.ToString() +
            //                 ", @User = 2" + //accId.ToString() +
            //                 ", @Case = " + CaseId.ToString() +
            //                 ", @FileName = '" + FileName + "'" +
            //                 ", @File = '" + Blob1.ToString()+"'";   // надо преобразовать к image


            //var filecontent =  //filemodel.contentAsBase64String;
            //var filetype = //filemodel.contentType;
            //var filename = //filemodel.fileName;

            var bytes = Convert.FromBase64String(base64String1);


            DataSet dset = new DataSet();
            using (SqlConnection conn = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                conn.Open();
                //SqlDataAdapter adapt = new SqlDataAdapter(command, conn);
                //adapt.Fill(dset,"Res");
                //conn.Close();

                SqlCommand sql_cmnd = new SqlCommand("Case_Attach_Add", conn);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@Author", SqlDbType.Int).Value = 3;
                sql_cmnd.Parameters.AddWithValue("@User", SqlDbType.Int).Value = 2;
                sql_cmnd.Parameters.AddWithValue("@Case", SqlDbType.Int).Value = 3713042;
                sql_cmnd.Parameters.AddWithValue("@FileName", SqlDbType.VarChar).Value = fileName1;//filemodel.fileName;
                sql_cmnd.Parameters.AddWithValue("@File", SqlDbType.Image).Value = bytes;


                sql_cmnd.ExecuteNonQuery();
                conn.Close();
            }
            return null;

        }

        [HttpPost]
        public string Case_Attach_Get(long CaseId)
        {
            DataSet customDataSet = new DataSet();

            using (SqlConnection conn = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                conn.Open();


                string comm22 = $"EXEC [dbo].[Case_Attach_Get] " +
                                    "  @Author = 3" +
                                    ", @User = 2" +
                                    ", @AttachId = 0" +
                                    ", @CaseId = " + CaseId.ToString();

                Console.WriteLine("Привет!");
                Console.WriteLine(comm22);

                SqlDataAdapter custAdapter = new SqlDataAdapter(comm22, conn);
                custAdapter.Fill(customDataSet, "Res");
                conn.Close();
            }

            return JsonConvert.SerializeObject(customDataSet);
        }

    }

}
