using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ModelLayer;

namespace DataLayer
{
    public interface IDataLogger
    {
        void LogRegistration(Customer c);
    }   
}