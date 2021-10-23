using System;
using System.IO;
using System.Collections.Generic;

namespace CDCE.Dumper
{
    class DRMFileV22
    {
        private static List<DRMResInfoV22> m_ResourceInfo = new List<DRMResInfoV22>();
        private static List<DRMResEntry> m_ResourceEntry = new List<DRMResEntry>();

        public static void iDumpIt(FileStream TDRMReader, String m_DrmFile, String m_GameFolder, Int32 dwVersion)
        {
            var m_Header = new DRMHeaderV22();
            var lpHeader = TDRMReader.ReadBytes(28);
            using (var THeaderReader = new MemoryStream(lpHeader))
            {
                m_Header.dwStringLength1 = THeaderReader.ReadInt32();
                m_Header.dwStringLength2 = THeaderReader.ReadInt32();
                m_Header.dwPaddingLength = THeaderReader.ReadInt32();
                m_Header.dwDRMSize = THeaderReader.ReadInt32();
                m_Header.dwFlags = THeaderReader.ReadUInt32();
                m_Header.dwTotalSections = THeaderReader.ReadInt32();
                m_Header.dwPrimarySection = THeaderReader.ReadUInt32();
                THeaderReader.Dispose();
            }

            using (var TResInfoReader = new MemoryStream(TDRMReader.ReadBytes(m_Header.dwTotalSections * 20)))
            {
                for (Int32 i = 0; i < m_Header.dwTotalSections; i++)
                {
                    var TResInfo = new DRMResInfoV22
                    {
                        dwUnknown1 = TResInfoReader.ReadInt32(),
                        dwResourceType = TResInfoReader.ReadInt32(),
                        dwUnknown2 = TResInfoReader.ReadInt32(),
                        dwHash = TResInfoReader.ReadUInt32(),
                        dwLocale = TResInfoReader.ReadUInt32(),
                    };

                    m_ResourceInfo.Add(TResInfo);
                }
            }

            Int32 dwStringsDataSize = m_Header.dwStringLength1 + m_Header.dwStringLength2;
            Int64 dwLimitOffset = TDRMReader.Position + dwStringsDataSize;
            if (dwStringsDataSize != 0)
            {
                do
                {
                    String m_LinkedResource = TDRMReader.ReadString();
                    Utils.iSetInfo("[DRM INFO]: Linked Resource: " + m_LinkedResource);
                    //File.AppendAllText(@"LinkedResources.log", m_LinkedResource + Environment.NewLine);
                } while (TDRMReader.Position != dwLimitOffset);
            }

            Boolean bOldStructure = false;
            Int32 dwResEntrySize = m_Header.dwTotalSections * 24;
            if (dwResEntrySize > TDRMReader.Length - TDRMReader.Position)
            {
                bOldStructure = true;
                dwResEntrySize = m_Header.dwTotalSections * 16;
            }

            using (var TResEntryReader = new MemoryStream(TDRMReader.ReadBytes(dwResEntrySize)))
            {
                for (Int32 i = 0; i < m_Header.dwTotalSections; i++)
                {
                    if (!bOldStructure)
                    {
                        UInt16 wUnknown1 = TResEntryReader.ReadUInt16();
                        UInt16 wUnknown2 = TResEntryReader.ReadUInt16();
                        UInt32 dwUnknown1 = TResEntryReader.ReadUInt32();
                        UInt16 wTigerPart = TResEntryReader.ReadUInt16();
                        UInt16 wTigerID = TResEntryReader.ReadUInt16();
                        UInt32 dwOffset = TResEntryReader.ReadUInt32();
                        Int32 dwSize = TResEntryReader.ReadInt32();
                        UInt32 dwHash = TResEntryReader.ReadUInt32();

                        var TResEntry = new DRMResEntry
                        {
                            wUnknown1 = wUnknown1,
                            wUnknown2 = wUnknown2,
                            dwUnknown1 = dwUnknown1,
                            wTigerPart = wTigerPart,
                            wTigerID = wTigerID,
                            dwOffset = dwOffset,
                            dwSize = dwSize,
                            dwHash = dwHash,
                        };

                        m_ResourceEntry.Add(TResEntry);
                    }
                    else
                    {
                        UInt16 wUnknown1 = TResEntryReader.ReadUInt16();
                        UInt16 wUnknown2 = TResEntryReader.ReadUInt16();
                        UInt32 dwOffset = TResEntryReader.ReadUInt32();
                        Int32 dwSize = TResEntryReader.ReadInt32();
                        UInt32 dwHash = TResEntryReader.ReadUInt32();

                        var TResEntry = new DRMResEntry
                        {
                            wUnknown1 = wUnknown1,
                            wUnknown2 = wUnknown2,
                            wTigerPart = (UInt16)(dwOffset & 0xF),
                            dwOffset = dwOffset & 0xFFFFF800,
                            dwSize = dwSize,
                            dwHash = dwHash,
                        };

                        m_ResourceEntry.Add(TResEntry);
                    }
                }
            }

            for (Int32 i = 0; i < m_Header.dwTotalSections; i++)
            {
                String m_FileName = DRMUtils.iGetResourceType(dwVersion, m_ResourceInfo[i].dwResourceType, m_ResourceInfo[i].dwHash);
                String m_TigerName = DRMUtils.iGetTigerArchiveByID(dwVersion, m_ResourceEntry[i]);
                String m_TigerArhive = m_GameFolder + @"\" + m_TigerName;

                Utils.iSetInfo("[UNPACKING]: " + m_TigerName + " - " + m_FileName);

                if (File.Exists(m_TigerArhive))
                {
                    using (FileStream TTigerStream = File.OpenRead(m_TigerArhive))
                    {
                        var lpBuffer = CDRMFile.iRead(TTigerStream, m_ResourceEntry[i].dwOffset);

                        if (m_FileName.Contains("DTPData"))
                        {
                            m_FileName = DRMUtils.iCheckDTPResourceV22(lpBuffer, m_FileName);
                        }

                        String m_FullPath = Path.GetDirectoryName(m_DrmFile) + @"\" + Path.GetFileNameWithoutExtension(m_DrmFile) + @"\" + m_FileName;

                        Utils.iCreateDirectory(m_FullPath);
                        File.WriteAllBytes(@"\\?\" + m_FullPath, lpBuffer);

                        TTigerStream.Dispose();
                    }
                }
                else
                {
                    Utils.iSetWarning("[SKIPPED]: " + m_FileName + " -> " + m_TigerName + " not found in directory - " + m_GameFolder);
                }
            }
        }
    }
}
