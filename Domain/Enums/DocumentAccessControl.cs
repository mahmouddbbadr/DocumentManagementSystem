using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    [Flags]
    public enum DocumentAccessControl : byte
    {
        Read = 0b_0000_1000, Write = 0b_0000_0100, execute = 0b_0000_0010, Delete = 0b_0000_0001, RootUser = 0b_0000_1111
    }
}
