using System;
using System.IO;
using System.Text;
using System.Linq;

namespace DataCompression {
    public static class FileWrapper {
        private static string StandardPath = AppDomain.CurrentDomain.BaseDirectory;

        public static string open(string filename) {
            var path = getValidPath(filename);
            if (filename.Contains(".txt")) {
                return getContentFromFile(path);
            } else {
                var fileContent = File.ReadAllBytes(path);
                return convertToString(fileContent);
            }
        }

        public static byte[] openInBytes(string filename) {
            var path = getValidPath(filename);
            return File.ReadAllBytes(path);
        }

        public static void write(string fileContent, string filename) {
            if (filename.Contains("alice29")) {
                writeInBytes(Encoding.ASCII.GetBytes(fileContent), filename);
            } else {
                var convertedFileContent = convertToBytes(fileContent);
                writeInBytes(convertedFileContent, filename);
            }
        }

        public static void writeInBytes(byte[] fileContent, string filename) {
            try {
                using (var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write)) {
                    fileStream.Write(fileContent, 0, fileContent.Length);
                }
            } catch (Exception exception) {
                Console.WriteLine(exception.Message);
            }
        }

        private static string getValidPath(string filename) {
            string path = Path.Combine(StandardPath, filename);
            if (!File.Exists(path)) {
                throw new IOException("File not found.");
            }
            return path;
        }

        private static string convertToString(byte[] fileContent) {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in fileContent) {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        private static byte[] convertToBytes(string fileContent) {
            return Enumerable.Range(0, fileContent.Length / 8).
                Select(pos => Convert.ToByte(
                    fileContent.Substring(pos * 8, 8),
                    2)
                ).ToArray();
        }

        private static string getContentFromFile(string path) {
            var fileContent = string.Empty;
            try {
                using (var streamReader = new StreamReader(path, Encoding.GetEncoding("ISO-8859-1"))) {
                    fileContent = streamReader.ReadToEnd();
                }
            } catch (Exception exception) {
                Console.WriteLine(exception.Message);
            }
            return fileContent;
        }
    }
}
