using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ModelLayer;

namespace DataLayer
{
    public class DataLogger : IDataLogger
    {
        public void LogRegistration(Customer c) {
            Console.WriteLine($"User {c.Username} registered successfully");
        }  
    }
}