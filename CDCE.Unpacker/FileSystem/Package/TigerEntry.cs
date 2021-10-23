using System;

namespace CDCE.Unpacker
{
    class TigerEntryV3
    {
        public UInt32 dwHash { get; set; }
        public UInt32 dwLocale { get; set; }
        public Int32 dwDecompressedSize { get; set; }
        public UInt32 dwOffset { get; set; }
        public UInt16 wTigerPart { get; set; }
    }

    class TigerEntryV4
    {
        public UInt32 dwHash { get; set; }
        public UInt32 dwLocale { get; set; }
        public Int32 dwDecompressedSize { get; set; }
        public Int32 dwCompressedSize { get; set; }
        public UInt16 wTigerPart { get; set; }
        public UInt16 wTigerPriority { get; set; }
        public UInt32 dwOffset { get; set; }
    }

    class TigerEntryV5
    {
        public UInt64 dwHash { get; set; }
        public UInt64 dwLocale { get; set; }
        public Int32 dwDecompressedSize { get; set; }
        public Int32 dwUnknown { get; set; }
        public UInt16 wTigerPart { get; set; }
        public UInt16 wTigerPriority { get; set; }
        public UInt32 dwOffset { get; set; }
    }

    class TigerEntryV8
    {
        public UInt64 dwHash { get; set; }
        public UInt32 dwLocale { get; set; }
        public Int32 dwDecompressedSize { get; set; }
        public UInt32 dwOffset { get; set; }
        public UInt16 wTigerPriority { get; set; }
        public UInt16 wTigerPart { get; set; }
    }
}
