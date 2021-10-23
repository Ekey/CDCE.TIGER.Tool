using System;
using System.IO;
using System.Text;

namespace CDCE.Dumper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("CDC Engine DRM Dumper");
            Console.WriteLine("(c) 2021 Ekey (h4x0r) / v{0}\n", Utils.iGetApplicationVersion());
            Console.ResetColor();

            if (args.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[Usage]");
                Console.WriteLine("    CDCE.Dumper <m_File> <m_GameDirectory>\n");
                Console.WriteLine("    m_File - Source of DRM file");
                Console.WriteLine("    m_GameDirectory - Game folder with TIGER files\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Examples]");
                Console.WriteLine("    CDCE.Dumper E:\\live_cd_enm_handgun.drm E:\\Games\\ROTR");
                Console.ResetColor();
                return;
            }

            String m_DrmFile = args[0];
            String m_GameFolder = Utils.iCheckArgumentsPath(args[1]);

            if (!File.Exists(m_DrmFile))
            {
                throw new Exception("[ERROR]: Input file does not exist " + m_DrmFile);
            }

            FileStream TDRMReader = new FileStream(m_DrmFile, FileMode.Open);
            Int32 dwVersion = TDRMReader.ReadInt32();

            switch (dwVersion)
            {
                case 22: DRMFileV22.iDumpIt(TDRMReader, m_DrmFile, m_GameFolder, dwVersion); break;
                case 23: DRMFileV23.iDumpIt(TDRMReader, m_DrmFile, m_GameFolder, dwVersion); break;
            }
        }
    }
}
