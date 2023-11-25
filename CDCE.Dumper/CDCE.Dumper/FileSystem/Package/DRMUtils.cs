using System;
using System.Collections.Generic;

namespace CDCE.Dumper
{
    class DRMUtils
    {
        private static Dictionary<UInt16, String> m_PackageCodes = new Dictionary<UInt16, String>();

        public static String iGetTigerArchiveByIDV22(UInt16 wTigerID, UInt16 wTigerPart)
        {
            if (wTigerID == 0 || wTigerID == 1) { return String.Format("bigfile.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 4) { return String.Format("DLC\\PACK4.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 5) { return String.Format("DLC\\PACK5.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 6) { return String.Format("DLC\\PACK6.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 7) { return String.Format("DLC\\PACK7.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 8) { return String.Format("DLC\\PACK8.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 10) { return String.Format("bigfile.update1.000.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 20) { return String.Format("bigfile.update2.000.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 30) { return String.Format("bigfile.update3.000.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 64) { return String.Format("title.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 65) { return String.Format("patch.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 67) { return String.Format("patch2.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 69) { return String.Format("patch3.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 150) { return String.Format("bigfile.dlc.mode.endurance.000.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 175) { return String.Format("bigfile.dlc.story.babayaga.000.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 200) { return String.Format("bigfile.dlc.mode.colddarkness.000.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 225) { return String.Format("bigfile.dlc.mode.bloodties.000.{0:D3}.tiger", wTigerPart); }
            else { throw new Exception("[ERROR]: Unable to determinate TIGER id -> " + wTigerID.ToString()); }   
        }

        public static String iGetTigerArchiveByIDV23(UInt16 wTigerID, UInt16 wTigerPart)
        {
            if (wTigerID == 1) { return String.Format("bigfile.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 10) { return String.Format("bigfile.update1.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 11) { return String.Format("bigfile.update2.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 12) { return String.Format("bigfile.update3.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 13) { return String.Format("bigfile.update4.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 103) { return String.Format("bigfile.dlc.outfitpack.1.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 104) { return String.Format("bigfile.dlc.outfitpack.2.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 105) { return String.Format("bigfile.dlc.outfitpack.3.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 106) { return String.Format("bigfile.dlc.weaponpack.1.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 107) { return String.Format("bigfile.dlc.weaponpack.2.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 108) { return String.Format("bigfile.dlc.weaponpack.3.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 109) { return String.Format("bigfile.dlc.skillboosterpack.1.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 117) { return String.Format("bigfile.dlc1.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 118) { return String.Format("bigfile.dlc.outfit.10.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 119) { return String.Format("bigfile.dlc.weapon.10.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 121) { return String.Format("bigfile.dlc.outfit.4.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 122) { return String.Format("bigfile.dlc.outfit.5.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 127) { return String.Format("bigfile.dlc.weapon.4.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 128) { return String.Format("bigfile.dlc.weapon.5.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 133) { return String.Format("bigfile.dlc2.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 139) { return String.Format("bigfile.full.dlc3.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 140) { return String.Format("bigfile.full.dlc4.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 141) { return String.Format("bigfile.full.dlc5.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 142) { return String.Format("bigfile.full.dlc6.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 143) { return String.Format("bigfile.weapon.outfit.dlc3.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 144) { return String.Format("bigfile.weapon.outfit.dlc4.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 145) { return String.Format("bigfile.weapon.outfit.dlc5.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 146) { return String.Format("bigfile.weapon.outfit.dlc6.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 147) { return String.Format("bigfile.weapon.outfit.dlc7.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 148) { return String.Format("bigfile.full.dlc7.002.{0:D3}.tiger", wTigerPart); }
            else if (wTigerID == 149) { return String.Format("bigfile.full.dlc8.002.{0:D3}.tiger", wTigerPart); }
            else { throw new Exception("[ERROR]: Unable to determinate TIGER id -> " + wTigerID.ToString()); }
        }

        public static void iInitPackageCodesV24()
        {
            m_PackageCodes.Add(16, "_ultra.000.tiger");
            m_PackageCodes.Add(63, ".000.tiger");
            m_PackageCodes.Add(80, "_ultra.001.tiger");
            m_PackageCodes.Add(127, ".001.tiger");
            m_PackageCodes.Add(144, "_ultra.002.tiger");
            m_PackageCodes.Add(191, ".002.tiger");
            m_PackageCodes.Add(208, "_ultra.003.tiger");
            m_PackageCodes.Add(255, ".003.tiger");
            m_PackageCodes.Add(272, "_ultra.004.tiger");
            m_PackageCodes.Add(319, ".004.tiger");
            m_PackageCodes.Add(336, "_ultra.005.tiger");
            m_PackageCodes.Add(383, ".005.tiger");
            m_PackageCodes.Add(400, "_ultra.006.tiger");
            m_PackageCodes.Add(447, ".006.tiger");
            m_PackageCodes.Add(511, ".007.tiger");
            m_PackageCodes.Add(575, ".008.tiger");
            m_PackageCodes.Add(639, ".009.tiger");
            m_PackageCodes.Add(703, ".010.tiger");
        }

        public static String iGetTigerArchiveByIDV24(UInt16 wTigerID, UInt16 wTigerPart)
        {
            String m_PackageName = null;
            if (m_PackageCodes.ContainsKey(wTigerID))
            {
                m_PackageCodes.TryGetValue(wTigerID, out m_PackageName);
            }
            else { throw new Exception("[ERROR]: Unknown TIGER ID -> " + wTigerID.ToString()); }

            switch (wTigerPart)
            {
                case 1: m_PackageName = "bigfile" + m_PackageName; break;
                case 10: m_PackageName = "bigfile.update1.000" + m_PackageName; break;
                case 11: m_PackageName = "bigfile.update2.000" + m_PackageName; break;
                default: throw new Exception("[ERROR]: Unable to determinate TIGER ID " + wTigerID.ToString() + " with " + wTigerPart.ToString() + " priority!");
            }

            return m_PackageName;
        }

        public static String iGetTigerArchiveByID(Int32 dwVersion, DRMResEntry m_ResourceEntry)
        {
            if (dwVersion == 22) { return DRMUtils.iGetTigerArchiveByIDV22(m_ResourceEntry.wTigerID, m_ResourceEntry.wTigerPart); }
            else if (dwVersion == 23) { return DRMUtils.iGetTigerArchiveByIDV23(m_ResourceEntry.wTigerID, m_ResourceEntry.wTigerPart); }
            else { throw new Exception("[ERROR]: Unable to determinate TIGER version -> " + dwVersion.ToString()); }
        }

        public static String iGetResourceType(Int32 dwVersion, Int32 dwResourceType, UInt32 dwHash)
        {
            dwResourceType &= 0xFF;
            if (dwResourceType == 0) { return String.Format("Generic\\Section_{0}.data", dwHash); }
            else if (dwResourceType == 1) { return String.Format("Empty\\Section_{0}.empty", dwHash); }
            else if (dwResourceType == 2) { return String.Format("Animation\\Section_{0}.anim", dwHash); }
            else if (dwResourceType == 3) { return String.Format("Unused\\Section_{0}.unused", dwHash); }
            else if (dwResourceType == 4) { return String.Format("PSDResource\\Section_{0}.psdres", dwHash); }
            else if (dwResourceType == 5 && dwVersion == 22) { return String.Format("Texture\\Section_{0}.tr2pcd", dwHash); }
            else if (dwResourceType == 5 && dwVersion == 23) { return String.Format("Texture\\Section_{0}.tr11pcd", dwHash); }
            else if (dwResourceType == 5 && dwVersion == 24) { return String.Format("Texture\\Section_{0}.mapcd", dwHash); }
            else if (dwResourceType == 6) { return String.Format("Sound\\Section_{0}.sound", dwHash); }
            else if (dwResourceType == 7) { return String.Format("DTPData\\Section_{0}.dtp", dwHash); }
            else if (dwResourceType == 8) { return String.Format("Script\\Section_{0}.script", dwHash); }
            else if (dwResourceType == 9) { return String.Format("ShaderLib\\Section_{0}.shader", dwHash); }
            else if (dwResourceType == 10) { return String.Format("Material\\Section_{0}.material", dwHash); }
            else if (dwResourceType == 11) { return String.Format("Object\\Section_{0}.object", dwHash); }
            else if (dwResourceType == 12 && dwVersion == 22) { return String.Format("RenderMesh\\Section_{0}.tr2mesh", dwHash); }
            else if (dwResourceType == 12 && dwVersion == 23) { return String.Format("RenderMesh\\Section_{0}.tr11mesh", dwHash); }
            else if (dwResourceType == 12 && dwVersion == 24) { return String.Format("RenderMesh\\Section_{0}.mamesh", dwHash); }
            else if (dwResourceType == 13 && dwVersion == 22) { return String.Format("CollisionMesh\\Section_{0}.tr2cmesh", dwHash); }
            else if (dwResourceType == 13 && dwVersion == 23) { return String.Format("CollisionMesh\\Section_{0}.tr11cmesh", dwHash); }
            else if (dwResourceType == 13 && dwVersion == 24) { return String.Format("CollisionMesh\\Section_{0}.macmesh", dwHash); }
            else if (dwResourceType == 14) { return String.Format("StreamGroupList\\Section_{0}.grplist", dwHash); }
            else if (dwResourceType == 15) { return String.Format("TriggerData\\Section_{0}.trigger", dwHash); }
            else if (dwResourceType == 17 && dwVersion == 24) { return String.Format("Locale\\Section_{0}.malocale", dwHash); }
            else { return String.Format("Unknown\\Section_{0}.trunknown_{1}", dwHash, dwResourceType); }
        }

        public static String iCheckDTPResourceV22(Byte[] lpBuffer, String m_FileName)
        {
            if (lpBuffer.Length > 28)
            {
                Int64 dwROTRSkl = BitConverter.ToInt64(lpBuffer, 0);
                if (dwROTRSkl == 6)
                {
                    return m_FileName.Replace(".dtp", ".skl");
                }

                Int32 dwGfxCheck = BitConverter.ToInt32(lpBuffer, 16);
                dwGfxCheck *= 4;
                dwGfxCheck += 24;

                if (dwGfxCheck < lpBuffer.Length)
                {
                    UInt32 dwMagic = BitConverter.ToUInt32(lpBuffer, (Int32)dwGfxCheck);
                    if (dwMagic == 0xA584647)
                    {
                        return m_FileName.Replace(".dtp", ".gfx");
                    }
                }
                else { return m_FileName; }
            }

            return m_FileName;
        }

        public static String iCheckDTPResourceV23(Byte[] lpBuffer, String m_FileName)
        {
            if (lpBuffer.Length > 28)
            {
                Int64 dwSOTRSkl = BitConverter.ToInt64(lpBuffer, 24);
                if (dwSOTRSkl == 0x1000000088)
                {
                    return m_FileName.Replace(".dtp", ".skl");
                }

                UInt32 dwGfxCheck = BitConverter.ToUInt32(lpBuffer, 16);
                dwGfxCheck *= 4;
                dwGfxCheck += 24;

                if (dwGfxCheck < lpBuffer.Length)
                {
                    UInt32 dwMagic = BitConverter.ToUInt32(lpBuffer, (Int32)dwGfxCheck);
                    if (dwMagic == 0xA584647)
                    {
                        return m_FileName.Replace(".dtp", ".gfx");
                    }
                }
                else { return m_FileName; }
            }

            return m_FileName;
        }

        public static UInt32 iAlignUInt32(UInt32 dwValue, UInt32 dwAlignSize)
        {
            if (dwValue == 0)
            {
                return dwValue;
            }

            return dwValue + ((dwAlignSize - (dwValue % dwAlignSize)) % dwAlignSize);
        }

        public static Boolean isCompressed(UInt32 dwDecompressedSize, UInt32 dwCompressedSize)
        {
            if (dwDecompressedSize != dwCompressedSize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
