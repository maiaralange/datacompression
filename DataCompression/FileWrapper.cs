using System;
using System.IO;
using System.Text;

namespace DataCompression {
    public static class FileWrapper {
        private static string StandardPath = AppDomain.CurrentDomain.BaseDirectory;

        public static string open(string filename) {
            var path = getValidPath(filename);        
            return getContentFromFile(path);
        }

        public static byte[] openInBytes(string filename) {
            var path = getValidPath(filename);
            return File.ReadAllBytes(path);
        }

        public static void write(string fileContent, string filename) {
            writeInBytes(Encoding.ASCII.GetBytes(fileContent), filename);
        }

        public static void writeInBytes(byte[] fileContent, string filename) {
            try {
                using (var fileStream = new FileStream(filename, FileMode.Create)) {
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

        private static string getContentFromFile(string path) {
            var fileContent = string.Empty;
            try {
                using (var streamReader = new StreamReader(path)) {
                    fileContent = streamReader.ReadToEnd();
                }
            } catch (Exception exception) {
                Console.WriteLine(exception.Message);
            }
            return fileContent;
        }
    }
}
