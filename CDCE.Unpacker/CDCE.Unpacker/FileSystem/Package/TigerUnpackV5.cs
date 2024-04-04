using System;
using System.IO;
using System.Collections.Generic;

namespace CDCE.Unpacker
{
    class TigerUnpackV5
    {
        static List<TigerEntryV5> m_EntryTable = new List<TigerEntryV5>();

        public static void iDoIt(FileStream TTigerStream, TigerHeader m_Header, String m_Archive, String m_DstFolder)
        {
            m_EntryTable.Clear();

            TigerUtils.iInitLanguageCodesV5();
            var lpTable = TTigerStream.ReadBytes(m_Header.dwTotalFiles * 32);
            using (var TMemoryReader = new MemoryStream(lpTable))
            {
                for (Int32 i = 0; i < m_Header.dwTotalFiles; i++)
                {
                    UInt64 dwHash = TMemoryReader.ReadUInt64();
                    UInt64 dwLocale = TMemoryReader.ReadUInt64();
                    Int32 dwDecompressedSize = TMemoryReader.ReadInt32();
                    Int32 dwUnknown = TMemoryReader.ReadInt32();
                    UInt16 wTigerPart = TMemoryReader.ReadUInt16();
                    UInt16 wTigerPriority = TMemoryReader.ReadUInt16();
                    UInt32 dwOffset = TMemoryReader.ReadUInt32();

                    var Entry = new TigerEntryV5
                    {
                        dwHash = dwHash,
                        dwLocale = dwLocale,
                        dwDecompressedSize = dwDecompressedSize,
                        dwUnknown = dwUnknown,
                        wTigerPart = wTigerPart,
                        wTigerPriority = wTigerPriority,
                        dwOffset = dwOffset,
                    };

                    m_EntryTable.Add(Entry);
                }
                TMemoryReader.Dispose();
                TTigerStream.Dispose();
            }

            foreach (var m_Entry in m_EntryTable)
            {
                String m_FileName = String.Format(@"{0}\", TigerUtils.iGetLanguageFromLocaleID(m_Entry.dwLocale)) + TigerHashList.iGetNameFromHashListX64(m_Entry.dwHash);
                String m_FullPath = m_DstFolder + m_FileName;

                Utils.iSetInfo("[UNPACKING]: " + m_FileName);
                Utils.iCreateDirectory(m_FullPath);

                String m_ArchiveFile = Path.GetDirectoryName(m_Archive) + @"\" + Path.GetFileName(m_Archive).Replace("000.tiger", "") + String.Format("{0:D3}.tiger", m_Entry.wTigerPart);

                if (File.Exists(m_ArchiveFile))
                {
                    using (FileStream TArchiveStream = File.OpenRead(m_ArchiveFile))
                    {
                        TArchiveStream.Seek(m_Entry.dwOffset, SeekOrigin.Begin);
                        var lpBuffer = TArchiveStream.ReadBytes(m_Entry.dwDecompressedSize);

                        File.WriteAllBytes(@"\\?\" + m_FullPath, lpBuffer);

                        TArchiveStream.Dispose();
                    }
                }
                else
                {
                    Utils.iSetWarning("[SKIPPED]: " + m_FileName + " -> " + m_ArchiveFile + " not found");
                }
            }
        }
    }
}
