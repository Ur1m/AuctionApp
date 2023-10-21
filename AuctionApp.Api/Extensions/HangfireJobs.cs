using Hangfire;
using System.Threading;
using System;
using System.Threading.Tasks;
using AuctionApp.Business.AuctionServices;

namespace AuctionApp.Api.Extensions
{
    public static class HangfireJobs
    {


        public static void Run()
        {
            Thread.Sleep(5000);
            Console.WriteLine("MyJob is running...");
        }

        [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public static void RecurringJobs()
        {
            Task.Run(() => RecurringJob.AddOrUpdate<IAuctionService>(x => x.CompleteAuctions(), Cron.Minutely)).ConfigureAwait(false);
        }
    }

}