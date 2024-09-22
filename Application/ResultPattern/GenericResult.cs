using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Services.ResultPattern
{
    public class GenericResult
    {
        public bool Success { get; set; }
        public object Body { get; set; }
        public  string Message { get; set; }
    }
}
