using System;
using System.IO;
using System.Collections.Generic;

namespace CDCE.Dumper
{
    class CDRMFile
    {
        private static List<CDRMChunks> m_Chunks = new List<CDRMChunks>();

        public static Byte[] iRead(Stream TDRMReader, UInt32 dwTigerOffset)
        {
            TDRMReader.Seek(dwTigerOffset, SeekOrigin.Begin);

            UInt32 dwMagic = TDRMReader.ReadUInt32(); // CDRM
            if (dwMagic != 0x4D524443)
            {
                throw new Exception("[ERROR]: Invalid magic of DRM file!");
            }

            UInt32 dwDRMType = TDRMReader.ReadUInt32(); // 0 - Index, 2 - Resource
            if (dwDRMType != 0 && dwDRMType != 2)
            {
                throw new Exception("[ERROR]: Invalid type of DRM file => " + dwDRMType.ToString() + " expected 0 or 2");
            }

            UInt32 dwTotalBlocks = TDRMReader.ReadUInt32();
            UInt32 dwPadding = TDRMReader.ReadUInt32();

            m_Chunks.Clear();
            UInt32 dwDRMSize = 0;
            UInt32 dwDRMZSize = 0;
            UInt32 dwChunkOffset = 0;

            dwChunkOffset = dwTigerOffset + 16 + DRMUtils.iAlignUInt32(dwTotalBlocks * 8, 16) + dwPadding;
            for (Int32 i = 0; i < dwTotalBlocks; i++)
            {
                UInt32 dwChunkSize = TDRMReader.ReadUInt32();
                UInt32 dwChunkZSize = TDRMReader.ReadUInt32();
                dwChunkSize >>= 8;


                var TChunk = new CDRMChunks
                {
                    dwDecompressedSize = dwChunkSize,
                    dwCompressedSize = dwChunkZSize,
                    dwOffset = dwChunkOffset,
                    bCompressed = DRMUtils.isCompressed(dwChunkSize, dwChunkZSize),
                };

                m_Chunks.Add(TChunk);

                dwDRMSize += dwChunkSize;
                dwDRMZSize += dwChunkZSize;
                dwChunkOffset += DRMUtils.iAlignUInt32(dwChunkZSize, 16);
            }

            UInt32 dwCurrentPos = 0;
            Byte[] lpDstBuffer = new Byte[dwDRMSize];

            foreach (var TChunk in m_Chunks)
            {
                TDRMReader.Seek(TChunk.dwOffset, SeekOrigin.Begin);
                var lpSrcBuffer = TDRMReader.ReadBytes((Int32)TChunk.dwCompressedSize);

                if (TChunk.bCompressed)
                {
                    var lpTempBuffer = ZLIB.iDecompress(lpSrcBuffer);
                    Array.Copy(lpTempBuffer, 0, lpDstBuffer, dwCurrentPos, TChunk.dwDecompressedSize);
                    dwCurrentPos += TChunk.dwDecompressedSize;
                }
                else
                {
                    Array.Copy(lpSrcBuffer, 0, lpDstBuffer, dwCurrentPos, TChunk.dwCompressedSize);
                    dwCurrentPos += TChunk.dwCompressedSize;
                }
            }

            m_Chunks.Clear();
            TDRMReader.Dispose();

            return lpDstBuffer;
        }
    }
}
