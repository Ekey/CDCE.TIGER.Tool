using System;
using System.IO;
using System.Text;

namespace CDCE.Unpacker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("CDC Engine TIGER Unpacker");
            Console.WriteLine("(c) 2021 Ekey (h4x0r) / v{0}\n", Utils.iGetApplicationVersion());
            Console.ResetColor();

            if (args.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[Usage]");
                Console.WriteLine("    CDCE.Unpacker <m_File> <m_Directory>\n");
                Console.WriteLine("    m_File - Source of 000.TIGER archive file");
                Console.WriteLine("    m_Directory - Destination directory\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Examples]");
                Console.WriteLine("    CDCE.Unpacker E:\\Games\\ROTR\\bigfile.000.tiger D:\\Unpacked");
                Console.ResetColor();
                return;
            }

            String m_TigerFile = args[0];
            String m_Output = Utils.iCheckArgumentsPath(args[1]);

            if (!Path.GetFileName(m_TigerFile).Contains("000.tiger"))
            {
                Utils.iSetError("[ERROR]: Input TIGER file is not the main archive, you need to select files which ending by 000.tiger in name");
                return;
            }

            if (!File.Exists(m_TigerFile))
            {
                Utils.iSetError("[ERROR]: Input TIGER file -> " + m_TigerFile + " <- does not exist");
                return;
            }

            var m_Header = new TigerHeader();
            using (FileStream TTigerStream = File.OpenRead(m_TigerFile))
            {
                if (TTigerStream.Length <= 56)
                {
                    Utils.iSetWarning("[WARNING]: TIGER archive file is empty");
                    return;
                }

                var lpHeader = TTigerStream.ReadBytes(56);
                using (var THeaderReader = new MemoryStream(lpHeader))
                {
                    m_Header.dwMagic = THeaderReader.ReadUInt32();
                    m_Header.dwVersion = THeaderReader.ReadInt32();
                    m_Header.dwPartsCount = THeaderReader.ReadInt32();
                    m_Header.dwTotalFiles = THeaderReader.ReadInt32();
                    m_Header.dwTigerID = THeaderReader.ReadInt32();

                    if (m_Header.dwVersion == 4 || m_Header.dwVersion == 5 || m_Header.dwVersion == 8)
                    {
                        m_Header.dwNfoBap = TigerNFO.iGetBapFromFile(Path.GetDirectoryName(m_TigerFile) + @"\" + "bigfile.000.nfo");
                    }
                    else
                    {
                        m_Header.dwNfoBap = 1200000000;
                    }

                    if (m_Header.dwVersion == 5)
                    {
                        THeaderReader.Position += 4;
                    }

                    m_Header.m_Platform = Encoding.ASCII.GetString(THeaderReader.ReadBytes(32)).TrimEnd('\0');

                    if (m_Header.dwMagic != 0x53464154)
                    {
                        Utils.iSetError("[ERROR]: Invalid magic of TIGER archive file");
                        return;
                    }

                    if (m_Header.dwVersion != 3 && m_Header.dwVersion != 4 && m_Header.dwVersion != 5 && m_Header.dwVersion != 8)
                    {
                        Utils.iSetError("[ERROR]: Invalid version of TIGER archive file -> " + m_Header.dwVersion.ToString() + ", expected 3, 4, 5 or 8");
                        return;
                    }

                    THeaderReader.Dispose();
                }

                TigerHashList.iLoadProject(m_Header);
                switch (m_Header.dwVersion)
                {
                    case 3: TigerUnpackV3.iDoIt(TTigerStream, m_Header, m_TigerFile, m_Output); break;
                    case 4: TigerUnpackV4.iDoIt(TTigerStream, m_Header, m_TigerFile, m_Output); break;
                    case 5: TigerUnpackV5.iDoIt(TTigerStream, m_Header, m_TigerFile, m_Output); break;
                    case 8: TigerUnpackV8.iDoIt(TTigerStream, m_Header, m_TigerFile, m_Output); break;
                }
            }
        }
    }
}
