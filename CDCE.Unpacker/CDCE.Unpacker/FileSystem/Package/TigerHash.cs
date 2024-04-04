using System;

namespace CDCE.Unpacker
{
    class TigerHash
    {
        public static UInt32 iGetHashX32(string m_String)
        {
            UInt32 dwHash = 0xFFFFFFFFu;
            foreach (Char bByte in m_String)
            {
                dwHash ^= (UInt32)bByte << 24;

                for (Int32 j = 0; j < 8; j++)
                {
                    if ((dwHash & 0x80000000) != 0)
                    {
                        dwHash = (dwHash << 1) ^ 0x04C11DB7u;
                    }
                    else
                    {
                        dwHash <<= 1;
                    }
                }
            }
            return ~dwHash;
        }

        public static UInt64 iGetHashX64(string lpString)
        {
            UInt64 dwHash = 0xCBF29CE484222325;
            UInt64 dwPrime = 0x100000001B3;

            for (int i = 0; i < lpString.Length; i++)
            {
                dwHash ^= lpString[i];
                dwHash *= dwPrime;
            }

            return dwHash;
        }
    }
}
