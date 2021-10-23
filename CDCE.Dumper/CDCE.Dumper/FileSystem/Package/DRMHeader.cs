using System;

namespace CDCE.Dumper
{
    class DRMHeaderV22
    {
        public Int32 dwVersion { get; set; }
        public Int32 dwStringLength1 { get; set; }
        public Int32 dwStringLength2 { get; set; }
        public Int32 dwPaddingLength { get; set; }
        public Int32 dwDRMSize { get; set; }
        public UInt32 dwFlags { get; set; }
        public Int32 dwTotalSections { get; set; }
        public UInt32 dwPrimarySection { get; set; }
    }

    class DRMHeaderV23
    {
        public Int32 dwVersion { get; set; }
        public Int32 dwStringLength1 { get; set; }
        public Int32 dwStringLength2 { get; set; }
        public Int32 dwPaddingLength { get; set; }
        public Int32 dwDRMSize { get; set; }
        public UInt32 dwFlags { get; set; }
        public Int32 dwTotalSections { get; set; }
        public UInt32 dwPrimarySection { get; set; }
        public UInt64 dwLocale { get; set; }
    }
}
