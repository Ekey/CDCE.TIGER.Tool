using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace CDCE.Unpacker
{
    class TigerUnpackV8
    {
        static List<TigerEntryV8> m_EntryTable = new List<TigerEntryV8>();

        public static void iDoIt(FileStream TTigerStream, TigerHeader m_Header, String m_Archive, String m_DstFolder)
        {
            m_EntryTable.Clear();
            TTigerStream.Position -= 4;

            TigerUtils.iInitLanguageCodesV8();
            TigerUtils.iInitPackageCodesV8();
            String m_BaseName = Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(m_Archive));

            Int32 dwUnknown = TTigerStream.ReadInt32(); // 65535
            Int32 dwTotalLanguages = TTigerStream.ReadInt32();

            for (Int32 i = 0; i < dwTotalLanguages; i++)
            {
                Int32 dwLanguageCode = TTigerStream.ReadInt32();
                Int32 dwFlag = TTigerStream.ReadInt32(); // 0, 1
                String m_Language = Encoding.ASCII.GetString(TTigerStream.ReadBytes(16)).TrimEnd('\0');

                //TigerUtils.m_LanguagesIds.Add((UInt64)dwLanguageCode, m_Language);
            }

            var lpTable = TTigerStream.ReadBytes(m_Header.dwTotalFiles * 24);
            using (var TMemoryReader = new MemoryStream(lpTable))
            {
                for (Int32 i = 0; i < m_Header.dwTotalFiles; i++)
                {
                    UInt64 dwHash = TMemoryReader.ReadUInt64();
                    UInt32 dwLocale = TMemoryReader.ReadUInt32();
                    Int32 dwDecompressedSize = TMemoryReader.ReadInt32();
                    UInt32 dwOffset = TMemoryReader.ReadUInt32();
                    UInt16 wTigerPriority = TMemoryReader.ReadUInt16();
                    UInt16 wTigerPart = TMemoryReader.ReadUInt16();

                    var Entry = new TigerEntryV8
                    {
                        dwHash = dwHash,
                        dwLocale = dwLocale,
                        dwDecompressedSize = dwDecompressedSize,
                        dwOffset = dwOffset,
                        wTigerPriority = wTigerPriority,
                        wTigerPart = wTigerPart,
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

                String m_ArchiveFile = Path.GetDirectoryName(m_Archive) + @"\" + m_BaseName + TigerUtils.iGetPackageNameFromIDV8(m_Entry.wTigerPart);

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
