using System;
using System.IO;
using System.Collections.Generic;

namespace CDCE.Unpacker
{
    class TigerHashList
    {
        static String m_Path = Utils.iGetApplicationPath() + @"\Projects\";
        static String m_ProjectFile = "";
        static Dictionary<UInt32, String> m_HashList_x32 = new Dictionary<UInt32, String>();
        static Dictionary<UInt64, String> m_HashList_x64 = new Dictionary<UInt64, String>();

        public static void iLoadProject(TigerHeader m_Header)
        {
            Int32 i = 0;
            String m_Line = null;

            switch(m_Header.dwNfoBap)
            {
                case 1200000000: m_ProjectFile = "TR2013_PC_Release.list"; break;
                case 1398709947: m_ProjectFile = "LCTO_PC_Release.list"; break;
                case 1453484307: m_ProjectFile = "ROTR_PC_Release.list"; break;
                case 1518148642: m_ProjectFile = "SOTR_PC_Release.list"; break;
                case 1737051083: m_ProjectFile = "MA_PC_Release.list"; break;
                default: m_ProjectFile = "EMPTY_PC_Release.list"; Utils.iSetWarning("[WARNING]: Unable to determinate bap ID from bigfile.000.nfo file"); break;
            }

            switch(m_Header.dwVersion)
            {
                case 3:
                case 4: m_HashList_x32.Clear(); break;
                case 5:
                case 8: m_HashList_x64.Clear(); break;
            }

            if (!File.Exists(m_Path + m_ProjectFile))
            {
                Utils.iSetWarning("[WARNING]: Unable to load project file " + m_ProjectFile);
            }

            StreamReader TProjectFile = new StreamReader(m_Path + m_ProjectFile);
            while ((m_Line = TProjectFile.ReadLine()) != null)
            {
                if (m_Header.dwVersion == 3 || m_Header.dwVersion == 4)
                {
                    UInt32 dwHash = TigerHash.iGetHashX32(m_Line);
                    if (m_HashList_x32.ContainsKey(dwHash))
                    {
                        String m_Collision = null;
                        m_HashList_x32.TryGetValue(dwHash, out m_Collision);
                        Utils.iSetError("[COLLISION]: " + m_Collision + " <-> " + m_Line);
                    }

                    m_HashList_x32.Add(dwHash, m_Line);
                    i++;
                }
                else if (m_Header.dwVersion == 5 || m_Header.dwVersion == 8)
                {
                    UInt64 dwHash = TigerHash.iGetHashX64(m_Line);
                    if (m_HashList_x64.ContainsKey(dwHash))
                    {
                        String m_Collision = null;
                        m_HashList_x64.TryGetValue(dwHash, out m_Collision);
                        Utils.iSetError("[COLLISION]: " + m_Collision + " <-> " + m_Line);
                    }

                    m_HashList_x64.Add(dwHash, m_Line);
                    i++;
                }
            }

            TProjectFile.Close();
            Utils.iSetInfo("[INFO]: Project File Loaded: " + i.ToString());
            Console.WriteLine();
        }

        public static String iGetNameFromHashListX32(UInt32 dwHash)
        {
            String m_FileName = null;

            if (m_HashList_x32.ContainsKey(dwHash))
            {
                m_HashList_x32.TryGetValue(dwHash, out m_FileName);
            }
            else
            {
                m_FileName = @"__Unknown\" + dwHash.ToString("X8");
            }

            return m_FileName;
        }

        public static String iGetNameFromHashListX64(UInt64 dwHash)
        {
            String m_FileName = null;

            if (m_HashList_x64.ContainsKey(dwHash))
            {
                m_HashList_x64.TryGetValue(dwHash, out m_FileName);
            }
            else
            {
                m_FileName = @"__Unknown\" + dwHash.ToString("X16");
            }

            return m_FileName;
        }
    }
}
