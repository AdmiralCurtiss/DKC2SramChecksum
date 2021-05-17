using HyoutaUtils;
using HyoutaUtils.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKC2SramChecksum {
	class Program {
		static void Main(string[] args) {
			var data = new DuplicatableFileStream(args[0]).CopyToMemoryAndDispose();

			EndianUtils.Endianness e = EndianUtils.Endianness.LittleEndian;

			int[] saveOffsets = new int[] { 0x8, 0x2b0, 0x558 };
			for (int i = 0; i < 3; ++i) {
				ushort checksum = 0;
				ushort checkxor = 0;
				data.Position = saveOffsets[i] + 0x6;
				for (int j = 6; j != 0x2a2; j += 2) {
					ushort value = data.ReadUInt16(e);
					checksum += value;
					checkxor ^= value;
				}

				data.Position = saveOffsets[i];
				data.WriteUInt16(checksum, e);
				data.WriteUInt16(checkxor, e);
			}

			using (var fs = new FileStream(args[0], FileMode.Create)) {
				data.Position = 0;
				StreamUtils.CopyStream(data, fs);
			}

			return;
		}
	}
}
