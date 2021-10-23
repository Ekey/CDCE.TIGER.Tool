using System;

namespace CDCE.Unpacker
{
    class TigerHeader
    {
        public UInt32 dwMagic { get; set; } // 0x53464154 (TAFS)
        public Int32 dwVersion { get; set; } // 3, 4, 5 or 8
        public Int32 dwPartsCount { get; set; }
        public Int32 dwTotalFiles { get; set; }
        public Int32 dwTigerID { get; set; }
        public String m_Platform { get; set; }
        public UInt32 dwNfoBap { get; set; }

        //Baps
        //1200000000 - Tomb Raider 2013
        //1398709947 - Lara Croft and the Temple of Osiris
        //1453484307 - Rise of the Tomb Raider
        //1518148642 - Shadow of the Tomb Raider
        //1737051083 - Marvels Avengers
    }
}
