using System;

namespace SQS.NetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var sqsService = new SQSService();

            sqsService.SendMessages().Wait();
            sqsService.ReceiveMessages().Wait();
        }
    }
}
