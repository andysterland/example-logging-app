using LoggingExampleApp.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Diagnostics;
using System.Threading;

namespace LoggingExampleApp.Controllers
{
    public class HomeController : Controller
    {
        private Stopwatch _stopwatch = new Stopwatch();
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            _stopwatch.Start();
            Log.Information("Starting: {Uri}", "/home");
            
            Random random = new Random();
            int randomNumber = random.Next(500);
            Thread.Sleep(randomNumber);

            int luckyTicket = random.Next(10);

            switch(luckyTicket)
            {
                case int n when luckyTicket == 0:
                    try
                    {
                        throw new Exception("uh oh");
                    }
                    catch(Exception ex)
                    {
                        Log.Error(ex, "Exception in /index/");
                    }
                    break;

                default:
                    break;
            }
            
            _stopwatch.Stop();
            if (_stopwatch.ElapsedMilliseconds > 300)
            {
                Log.Warning("Finished {Uri} taking {Duration}ms which is too long!", "/home", _stopwatch.ElapsedMilliseconds);
            }
            else
            {
                Log.Information("Finished {Uri} taking {Duration}ms", "/home", _stopwatch.ElapsedMilliseconds);
            }

            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
