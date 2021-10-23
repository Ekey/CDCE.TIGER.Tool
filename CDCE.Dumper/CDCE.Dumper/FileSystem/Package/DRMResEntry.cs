using System;

namespace CDCE.Dumper
{
    class DRMResEntry
    {
        public UInt16 wUnknown1 { get; set; }
        public UInt16 wUnknown2 { get; set; }
        public UInt32 dwUnknown1 { get; set; }
        public UInt16 wTigerPart { get; set; }
        public UInt16 wTigerID { get; set; }
        public UInt32 dwOffset { get; set; }
        public Int32 dwSize { get; set; }
        public UInt32 dwHash { get; set; }
    }
}
