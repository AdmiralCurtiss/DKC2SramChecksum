using HyoutaPluginBase;
using HyoutaUtils.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyoutaUtils {
	public static class StreamUtils {
		public static void CopyStream(Stream input, Stream output) {
			CopyStream(input, output, input.Length - input.Position);
		}

		public static void CopyStream(Stream input, Stream output, long count) {
			CopyStream(input, output, (ulong)count);
		}

		public static void CopyStream(Stream input, Stream output, ulong count) {
			const ulong bufferSize = 4096;
			byte[] buffer = new byte[bufferSize];
			int read;

			ulong bytesLeft = count;
			while ((read = input.Read(buffer, 0, (int)Math.Min(bufferSize, bytesLeft))) > 0) {
				output.Write(buffer, 0, read);
				bytesLeft -= (ulong)read;
				if (bytesLeft <= 0) return;
			}
		}

		public static bool IsIdentical( this Stream str, Stream other, long count ) {
			for ( long i = 0; i < count; ++i ) {
				if ( str.ReadByte() != other.ReadByte() ) {
					return false;
				}
			}
			return true;
		}

		public static bool IsIdentical(this Stream str, Stream other) {
			long slen = str.Length;
			long olen = other.Length;
			if (slen != olen) {
				return false;
			}

			long spos = str.Position;
			long opos = other.Position;
			try {
				str.Position = 0;
				other.Position = 0;
				return IsIdentical(str, other, slen);
			} finally {
				other.Position = opos;
				str.Position = spos;
			}
		}

		public static ulong ReadUInt64(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			ulong b1 = (ulong)s.ReadByte();
			ulong b2 = (ulong)s.ReadByte();
			ulong b3 = (ulong)s.ReadByte();
			ulong b4 = (ulong)s.ReadByte();
			ulong b5 = (ulong)s.ReadByte();
			ulong b6 = (ulong)s.ReadByte();
			ulong b7 = (ulong)s.ReadByte();
			ulong b8 = (ulong)s.ReadByte();

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					return (ulong)(b8 << 56 | b7 << 48 | b6 << 40 | b5 << 32 | b4 << 24 | b3 << 16 | b2 << 8 | b1);
				case EndianUtils.Endianness.BigEndian:
					return (ulong)(b1 << 56 | b2 << 48 | b3 << 40 | b4 << 32 | b5 << 24 | b6 << 16 | b7 << 8 | b8);
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static ulong ReadUInt56(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			ulong b1 = (ulong)s.ReadByte();
			ulong b2 = (ulong)s.ReadByte();
			ulong b3 = (ulong)s.ReadByte();
			ulong b4 = (ulong)s.ReadByte();
			ulong b5 = (ulong)s.ReadByte();
			ulong b6 = (ulong)s.ReadByte();
			ulong b7 = (ulong)s.ReadByte();

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					return (ulong)(b7 << 48 | b6 << 40 | b5 << 32 | b4 << 24 | b3 << 16 | b2 << 8 | b1);
				case EndianUtils.Endianness.BigEndian:
					return (ulong)(b1 << 48 | b2 << 40 | b3 << 32 | b4 << 24 | b5 << 16 | b6 << 8 | b7);
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static ulong ReadUInt48(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			ulong b1 = (ulong)s.ReadByte();
			ulong b2 = (ulong)s.ReadByte();
			ulong b3 = (ulong)s.ReadByte();
			ulong b4 = (ulong)s.ReadByte();
			ulong b5 = (ulong)s.ReadByte();
			ulong b6 = (ulong)s.ReadByte();

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					return (ulong)(b6 << 40 | b5 << 32 | b4 << 24 | b3 << 16 | b2 << 8 | b1);
				case EndianUtils.Endianness.BigEndian:
					return (ulong)(b1 << 40 | b2 << 32 | b3 << 24 | b4 << 16 | b5 << 8 | b6);
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static ulong ReadUInt40(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			ulong b1 = (ulong)s.ReadByte();
			ulong b2 = (ulong)s.ReadByte();
			ulong b3 = (ulong)s.ReadByte();
			ulong b4 = (ulong)s.ReadByte();
			ulong b5 = (ulong)s.ReadByte();

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					return (ulong)(b5 << 32 | b4 << 24 | b3 << 16 | b2 << 8 | b1);
				case EndianUtils.Endianness.BigEndian:
					return (ulong)(b1 << 32 | b2 << 24 | b3 << 16 | b4 << 8 | b5);
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static uint ReadUInt32(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			int b1 = s.ReadByte();
			int b2 = s.ReadByte();
			int b3 = s.ReadByte();
			int b4 = s.ReadByte();

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					return (uint)(b4 << 24 | b3 << 16 | b2 << 8 | b1);
				case EndianUtils.Endianness.BigEndian:
					return (uint)(b1 << 24 | b2 << 16 | b3 << 8 | b4);
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static uint ReadUInt24(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			int b1 = s.ReadByte();
			int b2 = s.ReadByte();
			int b3 = s.ReadByte();

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					return (uint)(b3 << 16 | b2 << 8 | b1);
				case EndianUtils.Endianness.BigEndian:
					return (uint)(b1 << 16 | b2 << 8 | b3);
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static ushort ReadUInt16(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			int b1 = s.ReadByte();
			int b2 = s.ReadByte();

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					return (ushort)(b2 << 8 | b1);
				case EndianUtils.Endianness.BigEndian:
					return (ushort)(b1 << 8 | b2);
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static byte ReadUInt8(this Stream s) {
			return Convert.ToByte(s.ReadByte());
		}

		public static long ReadInt64(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			return (long)ReadUInt64(s, endian);
		}

		public static int ReadInt32(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			return (int)ReadUInt32(s, endian);
		}

		public static short ReadInt16(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			return (short)ReadUInt16(s, endian);
		}

		public static sbyte ReadInt8(this Stream s) {
			return (sbyte)ReadUInt8(s);
		}

		public static ulong PeekUInt64(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			long pos = s.Position;
			ulong retval = s.ReadUInt64(endian);
			s.Position = pos;
			return retval;
		}

		public static ulong PeekUInt56(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			long pos = s.Position;
			ulong retval = s.ReadUInt56(endian);
			s.Position = pos;
			return retval;
		}

		public static ulong PeekUInt48(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			long pos = s.Position;
			ulong retval = s.ReadUInt48(endian);
			s.Position = pos;
			return retval;
		}

		public static ulong PeekUInt40(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			long pos = s.Position;
			ulong retval = s.ReadUInt40(endian);
			s.Position = pos;
			return retval;
		}

		public static uint PeekUInt32(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			long pos = s.Position;
			uint retval = s.ReadUInt32(endian);
			s.Position = pos;
			return retval;
		}

		public static uint PeekUInt24(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			long pos = s.Position;
			uint retval = s.ReadUInt24(endian);
			s.Position = pos;
			return retval;
		}

		public static ushort PeekUInt16(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			long pos = s.Position;
			ushort retval = s.ReadUInt16(endian);
			s.Position = pos;
			return retval;
		}

		public static byte PeekUInt8(this Stream s) {
			long pos = s.Position;
			byte retval = s.ReadUInt8();
			s.Position = pos;
			return retval;
		}

		public static long PeekInt64(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			return (long)PeekUInt64(s, endian);
		}

		public static int PeekInt32(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			return (int)PeekUInt32(s, endian);
		}

		public static short PeekInt16(this Stream s, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			return (short)PeekUInt16(s, endian);
		}

		public static sbyte PeekInt8(this Stream s) {
			return (sbyte)PeekUInt8(s);
		}

		public static void WriteUInt64(this Stream s, ulong num, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			byte b1 = (byte)(num & 0xFF);
			byte b2 = (byte)((num >> 8) & 0xFF);
			byte b3 = (byte)((num >> 16) & 0xFF);
			byte b4 = (byte)((num >> 24) & 0xFF);
			byte b5 = (byte)((num >> 32) & 0xFF);
			byte b6 = (byte)((num >> 40) & 0xFF);
			byte b7 = (byte)((num >> 48) & 0xFF);
			byte b8 = (byte)((num >> 56) & 0xFF);

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					s.WriteByte(b1);
					s.WriteByte(b2);
					s.WriteByte(b3);
					s.WriteByte(b4);
					s.WriteByte(b5);
					s.WriteByte(b6);
					s.WriteByte(b7);
					s.WriteByte(b8);
					return;
				case EndianUtils.Endianness.BigEndian:
					s.WriteByte(b8);
					s.WriteByte(b7);
					s.WriteByte(b6);
					s.WriteByte(b5);
					s.WriteByte(b4);
					s.WriteByte(b3);
					s.WriteByte(b2);
					s.WriteByte(b1);
					return;
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static void WriteUInt56(this Stream s, ulong num, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			byte b1 = (byte)(num & 0xFF);
			byte b2 = (byte)((num >> 8) & 0xFF);
			byte b3 = (byte)((num >> 16) & 0xFF);
			byte b4 = (byte)((num >> 24) & 0xFF);
			byte b5 = (byte)((num >> 32) & 0xFF);
			byte b6 = (byte)((num >> 40) & 0xFF);
			byte b7 = (byte)((num >> 48) & 0xFF);

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					s.WriteByte(b1);
					s.WriteByte(b2);
					s.WriteByte(b3);
					s.WriteByte(b4);
					s.WriteByte(b5);
					s.WriteByte(b6);
					s.WriteByte(b7);
					return;
				case EndianUtils.Endianness.BigEndian:
					s.WriteByte(b7);
					s.WriteByte(b6);
					s.WriteByte(b5);
					s.WriteByte(b4);
					s.WriteByte(b3);
					s.WriteByte(b2);
					s.WriteByte(b1);
					return;
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static void WriteUInt48(this Stream s, ulong num, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			byte b1 = (byte)(num & 0xFF);
			byte b2 = (byte)((num >> 8) & 0xFF);
			byte b3 = (byte)((num >> 16) & 0xFF);
			byte b4 = (byte)((num >> 24) & 0xFF);
			byte b5 = (byte)((num >> 32) & 0xFF);
			byte b6 = (byte)((num >> 40) & 0xFF);

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					s.WriteByte(b1);
					s.WriteByte(b2);
					s.WriteByte(b3);
					s.WriteByte(b4);
					s.WriteByte(b5);
					s.WriteByte(b6);
					return;
				case EndianUtils.Endianness.BigEndian:
					s.WriteByte(b6);
					s.WriteByte(b5);
					s.WriteByte(b4);
					s.WriteByte(b3);
					s.WriteByte(b2);
					s.WriteByte(b1);
					return;
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static void WriteUInt40(this Stream s, ulong num, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			byte b1 = (byte)(num & 0xFF);
			byte b2 = (byte)((num >> 8) & 0xFF);
			byte b3 = (byte)((num >> 16) & 0xFF);
			byte b4 = (byte)((num >> 24) & 0xFF);
			byte b5 = (byte)((num >> 32) & 0xFF);

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					s.WriteByte(b1);
					s.WriteByte(b2);
					s.WriteByte(b3);
					s.WriteByte(b4);
					s.WriteByte(b5);
					return;
				case EndianUtils.Endianness.BigEndian:
					s.WriteByte(b5);
					s.WriteByte(b4);
					s.WriteByte(b3);
					s.WriteByte(b2);
					s.WriteByte(b1);
					return;
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static void WriteUInt32(this Stream s, uint num, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			byte b1 = (byte)(num & 0xFF);
			byte b2 = (byte)((num >> 8) & 0xFF);
			byte b3 = (byte)((num >> 16) & 0xFF);
			byte b4 = (byte)((num >> 24) & 0xFF);

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					s.WriteByte(b1);
					s.WriteByte(b2);
					s.WriteByte(b3);
					s.WriteByte(b4);
					return;
				case EndianUtils.Endianness.BigEndian:
					s.WriteByte(b4);
					s.WriteByte(b3);
					s.WriteByte(b2);
					s.WriteByte(b1);
					return;
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static void WriteUInt24(this Stream s, uint num, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			byte b1 = (byte)(num & 0xFF);
			byte b2 = (byte)((num >> 8) & 0xFF);
			byte b3 = (byte)((num >> 16) & 0xFF);

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					s.WriteByte(b1);
					s.WriteByte(b2);
					s.WriteByte(b3);
					return;
				case EndianUtils.Endianness.BigEndian:
					s.WriteByte(b3);
					s.WriteByte(b2);
					s.WriteByte(b1);
					return;
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static void WriteUInt16(this Stream s, ushort num, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			byte b1 = (byte)(num & 0xFF);
			byte b2 = (byte)((num >> 8) & 0xFF);

			switch (endian) {
				case EndianUtils.Endianness.LittleEndian:
					s.WriteByte(b1);
					s.WriteByte(b2);
					return;
				case EndianUtils.Endianness.BigEndian:
					s.WriteByte(b2);
					s.WriteByte(b1);
					return;
				default:
					throw new Exception("unknown endianness");
			}
		}

		public static void WriteUInt8(this Stream s, byte num) {
			s.WriteByte(num);
		}

		public static void WriteInt64(this Stream s, long num, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			s.WriteUInt64((ulong)num, endian);
		}

		public static void WriteInt32(this Stream s, int num, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			s.WriteUInt32((uint)num, endian);
		}

		public static void WriteInt16(this Stream s, short num, EndianUtils.Endianness endian = EndianUtils.Endianness.LittleEndian) {
			s.WriteUInt16((ushort)num, endian);
		}

		public static void WriteInt8(this Stream s, sbyte num) {
			s.WriteUInt8((byte)num);
		}

		public static void DiscardBytes(this Stream s, long count) {
			s.Position = s.Position + count;
		}

		public static void DiscardBytes(this Stream s, ulong count) {
			s.Position = (long)(((ulong)s.Position) + count);
		}

		public static void WriteZeros(this Stream s, long count) {
			for (long i = 0; i < count; ++i) {
				s.WriteByte(0);
			}
		}

		public static void WriteZeros(this Stream s, ulong count) {
			for (ulong i = 0; i < count; ++i) {
				s.WriteByte(0);
			}
		}

		public static byte[] ReadBytes( this Stream stream, long count ) {
			byte[] sd = new byte[count];
			stream.Read( sd, 0, sd.Length );
			return sd;
		}

		public static MemoryStream ReadMemorySubstream(this Stream stream, long bytecount) {
			MemoryStream ms = new MemoryStream((int)bytecount);
			ms.Write(ReadBytes(stream, bytecount));
			ms.Position = 0;
			return ms;
		}

		public static byte[] ReadBytesFromLocationAndReset(this Stream stream, long position, long count) {
			long p = stream.Position;
			stream.Position = position;
			byte[] bytes = ReadBytes(stream, count);
			stream.Position = p;
			return bytes;
		}

		public static MemoryStream ReadMemorySubstreamFromLocationAndReset(this Stream stream, long position, long bytecount) {
			long p = stream.Position;
			stream.Position = position;
			MemoryStream ms = ReadMemorySubstream(stream, bytecount);
			stream.Position = p;
			return ms;
		}

		public static byte[] ReadUInt8Array( this Stream s, long count ) {
			// TODO: Isn't this just the same as ReadBytes() except slower?
			byte[] data = new byte[count];
			for ( long i = 0; i < count; ++i ) {
				data[i] = s.ReadUInt8();
			}
			return data;
		}

		public static uint[] ReadUInt32Array( this Stream s, long count, EndianUtils.Endianness endianness = EndianUtils.Endianness.LittleEndian ) {
			uint[] data = new uint[count];
			for ( long i = 0; i < count; ++i ) {
				data[i] = s.ReadUInt32(endianness);
			}
			return data;
		}

		public static ulong ReadUInt( this Stream s, BitUtils.Bitness bits, EndianUtils.Endianness endian ) {
			switch ( bits ) {
				case BitUtils.Bitness.B8: return s.ReadUInt8();
				case BitUtils.Bitness.B16: return s.ReadUInt16(endian);
				case BitUtils.Bitness.B32: return s.ReadUInt32(endian);
				case BitUtils.Bitness.B64: return s.ReadUInt64(endian);
			}
			throw new Exception( "Reading uint not implemented for bitness " + bits.ToString() );
		}

		public static long ReadInt( this Stream s, BitUtils.Bitness bits, EndianUtils.Endianness endian ) {
			switch ( bits ) {
				case BitUtils.Bitness.B8: return s.ReadInt8();
				case BitUtils.Bitness.B16: return s.ReadInt16(endian);
				case BitUtils.Bitness.B32: return s.ReadInt32(endian);
				case BitUtils.Bitness.B64: return s.ReadInt64(endian);
			}
			throw new Exception( "Reading int not implemented for bitness " + bits.ToString() );
		}

		public static ulong PeekUInt( this Stream s, BitUtils.Bitness bits, EndianUtils.Endianness endian ) {
			switch ( bits ) {
				case BitUtils.Bitness.B8: return s.PeekUInt8();
				case BitUtils.Bitness.B16: return s.PeekUInt16(endian);
				case BitUtils.Bitness.B32: return s.PeekUInt32(endian);
				case BitUtils.Bitness.B64: return s.PeekUInt64(endian);
			}
			throw new Exception( "Peeking uint not implemented for bitness " + bits.ToString() );
		}

		public static long PeekInt( this Stream s, BitUtils.Bitness bits, EndianUtils.Endianness endian ) {
			switch ( bits ) {
				case BitUtils.Bitness.B8: return s.PeekInt8();
				case BitUtils.Bitness.B16: return s.PeekInt16(endian);
				case BitUtils.Bitness.B32: return s.PeekInt32(endian);
				case BitUtils.Bitness.B64: return s.PeekInt64(endian);
			}
			throw new Exception( "Peeking int not implemented for bitness " + bits.ToString() );
		}

		public static void WriteUInt(this Stream s, ulong num, BitUtils.Bitness bits, EndianUtils.Endianness endian) {
			switch (bits) {
				case BitUtils.Bitness.B8: s.WriteUInt8((byte)num); return;
				case BitUtils.Bitness.B16: s.WriteUInt16((ushort)num, endian); return;
				case BitUtils.Bitness.B32: s.WriteUInt32((uint)num, endian); return;
				case BitUtils.Bitness.B64: s.WriteUInt64(num, endian); return;
			}
			throw new Exception("Writing uint not implemented for bitness " + bits.ToString());
		}

		public static void WriteInt(this Stream s, long num, BitUtils.Bitness bits, EndianUtils.Endianness endian) {
			switch (bits) {
				case BitUtils.Bitness.B8: s.WriteInt8((sbyte)num); return;
				case BitUtils.Bitness.B16: s.WriteInt16((short)num, endian); return;
				case BitUtils.Bitness.B32: s.WriteInt32((int)num, endian); return;
				case BitUtils.Bitness.B64: s.WriteInt64(num, endian); return;
			}
			throw new Exception("Writing int not implemented for bitness " + bits.ToString());
		}

		public static void ReadAlign(this Stream s, long alignment, long offset = 0) {
			while ((s.Position - offset) % alignment != 0) {
				s.DiscardBytes(1);
			}
		}

		public static void WriteAlign(this Stream s, long alignment, byte paddingByte = 0, long offset = 0) {
			while ((s.Position - offset) % alignment != 0) {
				s.WriteByte(paddingByte);
			}
		}

		public static void Write( this Stream s, byte[] data ) {
			s.Write( data, 0, data.Length );
		}

		public static MemoryStream CopyToMemory( this Stream s ) {
			long p = s.Position;
			s.Position = 0;
			MemoryStream ms = new MemoryStream( (int)s.Length );
			CopyStream( s, ms, s.Length );
			s.Position = p;
			ms.Position = p;
			return ms;
		}

		public static MemoryStream CopyToMemoryAndDispose( this Stream s ) {
			long p = s.Position;
			if (p != 0) {
				s.Position = 0;
			}
			MemoryStream ms = new MemoryStream( (int)s.Length );
			CopyStream( s, ms, s.Length );
			ms.Position = p;
			s.Dispose();
			return ms;
		}

		public static byte[] CopyToByteArray( this Stream s ) {
			long p = s.Position;
			s.Position = 0;
			byte[] data = new byte[s.Length];
			s.Read(data, 0, (int)s.Length);
			s.Position = p;
			return data;
		}

		public static byte[] CopyToByteArrayAndDispose( this Stream s ) {
			if (s.Position != 0) {
				s.Position = 0;
			}
			byte[] data = new byte[s.Length];
			s.Read(data, 0, (int)s.Length);
			s.Dispose();
			return data;
		}

		public static void SwapBytes(this Stream s, long pos0, long pos1) {
			long oldp = s.Position;
			s.Position = pos0;
			byte b0 = s.ReadUInt8();
			s.Position = pos1;
			byte b1 = s.PeekUInt8();
			s.WriteByte(b0);
			s.Position = pos0;
			s.WriteByte(b1);
			s.Position = oldp;
		}
	}
}
