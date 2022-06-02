using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelSaber.Main.Parser
{
    public static class UnityBundle
    {
        public static UnityHeader GetUnityHeader(Stream stream)
        {
            var reader = new BinaryReader(stream, Encoding.BigEndianUnicode);
            var type = GetUnityString(reader);
            var typeVersion = GetUnityInt(reader);
            var minVersion = GetUnityString(reader);
            var buildVersion = GetUnityString(reader);
            return new UnityHeader(type, typeVersion, minVersion, buildVersion);
        }

        public static string GetUnityString(BinaryReader reader)
        {
            var pos = reader.BaseStream.Position;
            while (reader.ReadByte() != 0)
            {
                //find the magic number
            }
            var count = (int)(reader.BaseStream.Position - pos);
            var buffer = new byte[count];
            reader.BaseStream.Seek(pos, SeekOrigin.Begin);
            var _ = reader.Read(buffer, 0, count);
            return Encoding.UTF8.GetString(buffer.SkipLast(1).ToArray()).Trim();
        }

        public static int GetUnityInt(BinaryReader reader)
        {
            var buffer = new byte[4];
            reader.Read(buffer, 0, 4);
            return BitConverter.ToInt32(buffer.Reverse().ToArray(), 0);
        }
    }
}
