using System;
using System.IO;

namespace CDCE.Unpacker
{
    class TigerNFO
    {
        public static UInt32 iGetBapFromFile(String m_NFOFile)
        {
            StreamReader TNfoReader = new StreamReader(m_NFOFile);
            String m_Line = TNfoReader.ReadLine();
            TNfoReader.Dispose();

            String[] m_Data = m_Line.Split(' ');

            for (Int32 i = 0; i < m_Data.Length; i++)
            {
                if (m_Data[i] == "bap")
                {
                    return Convert.ToUInt32(m_Data[i + 1]);
                }
            }

            return 0;
        }
    }
}
