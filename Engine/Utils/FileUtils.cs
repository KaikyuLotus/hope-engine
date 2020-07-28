using Android.Renderscripts;
using Android.Util;
using System.IO;
using System.Reflection;

namespace HopeEngine.Engine.Utils
{
    public static class FileUtils
    {
        public static byte[] ReadResourceBytes(string fileName, System.Type assemblyType = null)
        {
            Log.Info("Hope", $"Reading resource file named {fileName}");

            var assembly = IntrospectionExtensions.GetTypeInfo(assemblyType ?? typeof(FileUtils)).Assembly;

            using Stream stream = assembly.GetManifestResourceStream(fileName);
            using MemoryStream ms = new MemoryStream();

            stream.CopyTo(ms);
            return ms.ToArray();
        }

    }
}