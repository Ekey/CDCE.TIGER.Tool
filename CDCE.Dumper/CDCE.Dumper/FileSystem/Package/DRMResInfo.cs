using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCE.Dumper
{
    class DRMResInfoV22
    {
        public Int32 dwUnknown1 { get; set; }
        public Int32 dwResourceType { get; set; }
        public Int32 dwUnknown2 { get; set; }
        public UInt32 dwHash { get; set; }
        public UInt32 dwLocale { get; set; }
    }

    class DRMResInfoV23
    {
        public Int32 dwUnknown1 { get; set; }
        public Int32 dwResourceType { get; set; }
        public Int32 dwUnknown2 { get; set; }
        public UInt32 dwHash { get; set; }
        public UInt64 dwLocale { get; set; }

    }
}
