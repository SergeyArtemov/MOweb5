using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MOweb.Models;
using Orders;
using CustomerFinder;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace MOweb
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Thread myThread = new Thread(new ThreadStart(Count));
            myThread.Start(); // запускаем поток

            //CreateWebHostBuilder(args).Build().Run();
            CreateHostBuilder(args).Build().Run();

            //OrderList OD = OrdersMapper.OrderListCreate("");//OrderListCreate();
        }
        public static void Count()
        {

            Thread.Sleep(5000);
            for (;;)
            {
                //Console.WriteLine("Второй поток:");
                //Console.WriteLine(i * i);



                OrdersMapper.updRealtimeCache2();  //21042021
                Thread.Sleep(10000);   //21042021
                OrdersMapper.ToDoServerLog();  //21042021

            }
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
                    webBuilder.UseStartup<Startup>();
                });

    }


}
