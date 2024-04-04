using System;
using System.IO;
using System.Collections.Generic;

namespace CDCE.Unpacker
{
    class TigerUnpackV4
    {
        static List<TigerEntryV4> m_EntryTable = new List<TigerEntryV4>();

        public static void iDoIt(FileStream TTigerStream, TigerHeader m_Header, String m_Archive, String m_DstFolder)
        {
            m_EntryTable.Clear();
            TTigerStream.Position -= 4;

            TigerUtils.iInitLanguageCodesV4();
            var lpTable = TTigerStream.ReadBytes(m_Header.dwTotalFiles * 24);
            using (var TMemoryReader = new MemoryStream(lpTable))
            {
                for (Int32 i = 0; i < m_Header.dwTotalFiles; i++)
                {
                    UInt32 dwHash = TMemoryReader.ReadUInt32();
                    UInt32 dwLocale = TMemoryReader.ReadUInt32();
                    Int32 dwDecompressedSize = TMemoryReader.ReadInt32();
                    Int32 dwCompressedSize = TMemoryReader.ReadInt32();
                    UInt16 wTigerPart = TMemoryReader.ReadUInt16();
                    UInt16 wTigerPriority = TMemoryReader.ReadUInt16();
                    UInt32 dwOffset = TMemoryReader.ReadUInt32();

                    var Entry = new TigerEntryV4
                    {
                        dwHash = dwHash,
                        dwLocale = dwLocale,
                        dwDecompressedSize = dwDecompressedSize,
                        dwCompressedSize = dwCompressedSize,
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
                String m_FileName = String.Format(@"{0}\", TigerUtils.iGetLanguageFromLocaleID(m_Entry.dwLocale)) + TigerHashList.iGetNameFromHashListX32(m_Entry.dwHash);
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
