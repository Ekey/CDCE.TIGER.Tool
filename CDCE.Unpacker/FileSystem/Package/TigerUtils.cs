using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDCE.Unpacker
{
    class TigerUtils
    {
        private static Dictionary<UInt16, String> m_PackageIdsV8 = new Dictionary<UInt16, String>();
        private static Dictionary<UInt64, String> m_LanguagesIds = new Dictionary<UInt64, String>();
        public static Dictionary<Int32, String> m_LanguagesListV8 = new Dictionary<Int32, String>();

        public static void iInitPackageCodesV8()
        {
            m_PackageIdsV8.Add(0, "_english.000.tiger");
            m_PackageIdsV8.Add(1, "_french.000.tiger");
            m_PackageIdsV8.Add(2, "_german.000.tiger");
            m_PackageIdsV8.Add(3, "_italian.000.tiger");
            m_PackageIdsV8.Add(4, "_latamspanish.000.tiger");
            m_PackageIdsV8.Add(5, "_iberspanish.000.tiger");
            m_PackageIdsV8.Add(6, "_japanese.000.tiger");
            m_PackageIdsV8.Add(7, "_portuguese.000.tiger");
            m_PackageIdsV8.Add(8, "_polish.000.tiger");
            m_PackageIdsV8.Add(9, "_russian.000.tiger");
            m_PackageIdsV8.Add(13, "_simplechinese.000.tiger");
            m_PackageIdsV8.Add(14, "_arabic.000.tiger");
            m_PackageIdsV8.Add(63, ".000.tiger");
            m_PackageIdsV8.Add(127, ".001.tiger");
            m_PackageIdsV8.Add(191, ".002.tiger");
            m_PackageIdsV8.Add(255, ".003.tiger");
            m_PackageIdsV8.Add(319, ".004.tiger");
            m_PackageIdsV8.Add(383, ".005.tiger");
            m_PackageIdsV8.Add(447, ".006.tiger");
            m_PackageIdsV8.Add(511, ".007.tiger");
            m_PackageIdsV8.Add(575, ".008.tiger");
            m_PackageIdsV8.Add(639, ".009.tiger");
            m_PackageIdsV8.Add(703, ".010.tiger");
        }

        public static void iInitLanguageCodesV3()
        {
            m_LanguagesIds.Add(0xFFFFFFFF, "default");
            m_LanguagesIds.Add(0xFFFF0001, "english");
            m_LanguagesIds.Add(0xFFFF0002, "french");
            m_LanguagesIds.Add(0xFFFF0004, "german");
            m_LanguagesIds.Add(0xFFFF0008, "italian");
            m_LanguagesIds.Add(0xFFFF0010, "latamspanish");
            m_LanguagesIds.Add(0xFFFF0020, "iberspanish");
            m_LanguagesIds.Add(0xFFFF0040, "japanese");
            m_LanguagesIds.Add(0xFFFF0080, "portuguese");
            m_LanguagesIds.Add(0xFFFF0100, "polish");
            m_LanguagesIds.Add(0xFFFF0200, "russian");
            m_LanguagesIds.Add(0xFFFF2000, "simplechinese");
            m_LanguagesIds.Add(0xFFFF4000, "arabic");
        }

        public static void iInitLanguageCodesV4()
        {
            m_LanguagesIds.Add(0xFFFFFFFF, "default");
            m_LanguagesIds.Add(0xFFFFC001, "english");
            m_LanguagesIds.Add(0xFFFFC002, "french");
            m_LanguagesIds.Add(0xFFFFC004, "german");
            m_LanguagesIds.Add(0xFFFFC008, "italian");
            m_LanguagesIds.Add(0xFFFFC010, "latamspanish");
            m_LanguagesIds.Add(0xFFFFC020, "iberspanish");
            m_LanguagesIds.Add(0xFFFFC040, "japanese");
            m_LanguagesIds.Add(0xFFFFC080, "portuguese");
            m_LanguagesIds.Add(0xFFFFC100, "polish");
            m_LanguagesIds.Add(0xFFFFC200, "russian");
            m_LanguagesIds.Add(0xFFFFC400, "dutch");
            m_LanguagesIds.Add(0xFFFFC800, "korean");
            m_LanguagesIds.Add(0xFFFFD000, "chinese");
            m_LanguagesIds.Add(0xFFFFE000, "simplechinese");
        }

        public static void iInitLanguageCodesV5()
        {
            m_LanguagesIds.Add(0xFFFFFFFFFFFFFFFF, "default");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF0401, "english");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF0402, "french");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF0404, "german");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF0408, "italian");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF0410, "latamspanish");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF0420, "iberspanish");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF0480, "portuguese");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF0500, "polish");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF0600, "russian");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF0C00, "korean");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF1400, "chinese");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF2400, "simplechinese");
            m_LanguagesIds.Add(0xFFFFFFFFFFFF8400, "arabic");
        }

        public static void iInitLanguageCodesV8()
        {
            m_LanguagesIds.Add(0xFFFFFFFF, "default");
            m_LanguagesIds.Add(0xFFFF0001, "english");
            m_LanguagesIds.Add(0xFFFF0002, "french");
            m_LanguagesIds.Add(0xFFFF0004, "german");
            m_LanguagesIds.Add(0xFFFF0008, "italian");
            m_LanguagesIds.Add(0xFFFF0010, "latamspanish");
            m_LanguagesIds.Add(0xFFFF0020, "iberspanish");
            m_LanguagesIds.Add(0xFFFF0040, "japanese");
            m_LanguagesIds.Add(0xFFFF0080, "portuguese");
            m_LanguagesIds.Add(0xFFFF0100, "polish");
            m_LanguagesIds.Add(0xFFFF0200, "russian");
            m_LanguagesIds.Add(0xFFFF2000, "simplechinese");
            m_LanguagesIds.Add(0xFFFF4000, "arabic");
        }

        public static String iGetPackageNameFromIDV8(UInt16 wTigerID)
        {
            String m_PackageName = null;
            if (m_PackageIdsV8.ContainsKey(wTigerID))
            {
                m_PackageIdsV8.TryGetValue(wTigerID, out m_PackageName);
            }
            else
            {
                Utils.iSetError("[ERROR]: Unknown package ID " + wTigerID.ToString());
            }
            return m_PackageName;
        }

        public static String iGetLanguageFromLocaleID(UInt64 dwLocale)
        {
            String m_Language = null;
            if (m_LanguagesIds.ContainsKey(dwLocale))
            {
                m_LanguagesIds.TryGetValue(dwLocale, out m_Language);
            }
            else
            {
                return "unknown_" + dwLocale.ToString("X8");
            }
            return m_Language;
        }
    }
}
