using SharpLZW;
using System.Text;
using System.Collections;

namespace DataCompression {
    public static class Coder {
        public static void encode(string filename) {
            var fileContent = FileWrapper.open(filename);
            var encoder = new LZWEncoder();
            var lzwEncoded = encoder.Encode(fileContent);
            var deltaEncoded = Delta.encode(lzwEncoded);
            FileWrapper.writeInBytes(deltaEncoded, getEncodedFilename(filename));
        }

        public static void decode(string filename) {
            var fileContent = FileWrapper.openInBytes(filename);
            var deltaDecoded = Delta.decode(fileContent);
            var decoder = new LZWDecoder();
            var lzwDecoded = decoder.Decode(deltaDecoded);
            FileWrapper.write(lzwDecoded, getDecodedFilename(filename));
        }

        private static string getEncodedFilename(string filename) {
            return filename.Split('.')[0] + ".encoded";
        }

        private static string getDecodedFilename(string filename) {
            return filename.Split('.')[0] + ".decoded";
        }
    }
}
