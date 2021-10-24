using System;
using System.IO;
using System.Collections.Generic;

namespace CDCE.Dumper
{
    class DRMFileV24
    {
        private static List<DRMSectionEntry> m_Sections = new List<DRMSectionEntry>();

        public static void iDumpIt(FileStream TDRMReader, String m_DrmFile, String m_GameFolder, Int32 dwVersion)
        {
            TDRMReader.Position = 0;
            DRMUtils.iInitPackageCodesV24();

            var lpDRMBuffer = CDRMFile.iRead(TDRMReader, 0);
            var m_Header = new DRMHeaderV22();

            using (var TMemoryReader = new MemoryStream(lpDRMBuffer))
            {
                m_Header.dwVersion = TMemoryReader.ReadInt32();
                m_Header.dwStringLength1 = TMemoryReader.ReadInt32();
                m_Header.dwStringLength2 = TMemoryReader.ReadInt32();
                m_Header.dwPaddingLength = TMemoryReader.ReadInt32();
                m_Header.dwFlags = TMemoryReader.ReadUInt32();
                m_Header.dwTotalSections = TMemoryReader.ReadInt32();
                m_Header.dwPrimarySection = TMemoryReader.ReadUInt32();

                var lpFirstBlock = TMemoryReader.ReadBytes(m_Header.dwTotalSections * 32);
                var TFirstBlockReader = new MemoryStream(lpFirstBlock);

                Int32 dwStringsDataSize = m_Header.dwStringLength1 + m_Header.dwStringLength2;
                Int64 dwLimitOffset = TMemoryReader.Position + dwStringsDataSize;
                if (dwStringsDataSize != 0)
                {
                    do
                    {
                        String m_LinkedResource = TMemoryReader.ReadString();
                        Utils.iSetInfo("[DRM INFO]: Linked Resource: " + m_LinkedResource);
                        //File.AppendAllText(@"LinkedResources.log", m_LinkedResource + Environment.NewLine);
                    } while (TMemoryReader.Position != dwLimitOffset);
                }


                var lpSecondBlock = TMemoryReader.ReadBytes(m_Header.dwTotalSections * 24);
                var TSecondBlockReader = new MemoryStream(lpSecondBlock);
                TMemoryReader.Dispose();

                m_Sections.Clear();
                UInt32 dwUniqueID = 0;
                Int32 dwUniqueType = 0;
                Int32 dwChunksCount = 1;
                Boolean bCheck = false;
                for (Int32 i = 0; i < m_Header.dwTotalSections; i++)
                {
                    Int32 dwSectionSize = 0;
                    do
                    {
                        Int32 dwChunkSize = TFirstBlockReader.ReadInt32(); //Uncompressed
                        Int32 dwResType = TFirstBlockReader.ReadInt32();
                        UInt64 dwBlockHash = TFirstBlockReader.ReadUInt64();
                        UInt32 dwChunkID = TFirstBlockReader.ReadUInt32();
                        UInt32 dwSectionID = TFirstBlockReader.ReadUInt32();
                        dwUniqueID = dwSectionID;
                        dwUniqueType = dwResType;

                        dwSectionSize += dwChunkSize;
                        Int64 dwTempPos = TFirstBlockReader.Position;

                        if (TFirstBlockReader.Position + 28 <= TFirstBlockReader.Length)
                        {
                            TFirstBlockReader.Seek(28, SeekOrigin.Current);
                            Int32 dwTemp = TFirstBlockReader.ReadInt32();
                            TFirstBlockReader.Seek(dwTempPos, SeekOrigin.Begin);

                            if (dwTemp == dwSectionID)
                            {
                                i++;
                                dwChunksCount++;
                                bCheck = true;
                            }
                            else
                            {
                                bCheck = false;
                            }
                        }
                        else
                        {
                            i++;
                            bCheck = false;
                        }

                        UInt32 dwUnknown3 = TFirstBlockReader.ReadUInt32(); // 0xFFFFFFFF
                        Int32 dwUnknown4 = TFirstBlockReader.ReadInt32(); // 0

                        Int32 dwUnknown5 = TSecondBlockReader.ReadInt32();
                        Int32 dwUnknown6 = TSecondBlockReader.ReadInt32(); // 0,1,2,3,4,5
                        UInt32 dwOffset = TSecondBlockReader.ReadUInt32();
                        UInt16 wPriority = TSecondBlockReader.ReadUInt16();
                        UInt16 wTigerID = TSecondBlockReader.ReadUInt16();
                        UInt32 dwSize = TSecondBlockReader.ReadUInt32();
                        UInt32 dwHash = TSecondBlockReader.ReadUInt32();

                        var TSectionEntry = new DRMSectionEntry
                        {
                            dwSectionSize = dwSectionSize,
                            dwSectionOffset = dwOffset,
                            dwSectionID = dwSectionID,
                            m_SectionName = DRMUtils.iGetResourceType(dwVersion, dwResType, dwSectionID),
                            m_SectionArchive = DRMUtils.iGetTigerArchiveByIDV24(wTigerID, wPriority),
                        };

                        m_Sections.Add(TSectionEntry);

                    }
                    while (bCheck);
                }
            }

            foreach (var TSection in m_Sections)
            {
                if (File.Exists(m_GameFolder + TSection.m_SectionArchive))
                {
                    using (FileStream TTigerStream = File.OpenRead(m_GameFolder + TSection.m_SectionArchive))
                    {
                        if (TTigerStream.Length > 56)
                        {
                            var lpBuffer = CDRMFile.iRead(TTigerStream, TSection.dwSectionOffset);

                            if (TSection.m_SectionName.Contains("DTPData"))
                            {
                                TSection.m_SectionName = DRMUtils.iCheckDTPResourceV23(lpBuffer, TSection.m_SectionName);
                            }

                            String m_FullPath = Path.GetDirectoryName(m_DrmFile) + @"\" + Path.GetFileNameWithoutExtension(m_DrmFile) + @"\" + TSection.m_SectionName;

                            Utils.iCreateDirectory(m_FullPath);
                            File.WriteAllBytes(@"\\?\" + m_FullPath, lpBuffer);

                            TTigerStream.Dispose();
                        }
                        else
                        {
                            Utils.iSetWarning("[SKIPPED]: " + TSection.m_SectionName + " -> file " + TSection.m_SectionArchive + " is empty");
                        }
                    }
                }
                else
                {
                    Utils.iSetWarning("[SKIPPED]: " + TSection.m_SectionName + " -> " + TSection.m_SectionArchive + " not found in directory - " + m_GameFolder);
                }
            }
        }
    }
}
