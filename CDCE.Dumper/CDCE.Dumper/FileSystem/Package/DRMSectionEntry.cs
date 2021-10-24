using System;

namespace CDCE.Dumper
{
    class DRMSectionEntry
    {
        public Int32 dwSectionSize { get; set; }
        public UInt32 dwSectionOffset { get; set; }
        public UInt32 dwSectionID { get; set; }
        public String m_SectionName { get; set; }
        public String m_SectionArchive { get; set; }
    }
}
