using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Texas_Hold_em.Loggers;

namespace Texas_Hold_em
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var activeLogger = LoggerFactory.GetActiveLogger();
            var errorLogger = LoggerFactory.GetErrorLogger();


            Console.WriteLine("Testing loggers...");
            activeLogger.Debug( "Hello from -Program-s activeLogger !!!");
            errorLogger.Debug("Hello from -Program-s errorLogger !!!");

            Console.WriteLine("Logged in the data. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
