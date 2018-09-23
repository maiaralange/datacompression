using System.Collections;
using System.Collections.Generic;

namespace DataCompression {
    public static class Delta {
        public static char first;

        public static byte[] encode(string fileContent) {
            var encodedContent = populateListWithDeltaAlgorithm(fileContent);
            return getByteArray(encodedContent);
        }

        private static List<bool> populateListWithDeltaAlgorithm(string fileContent) {
            var encodedContent = new List<bool>();
            var last = first = fileContent[0];
            foreach (var current in fileContent) {
                if (current == last) {
                    addEqualsValue(encodedContent);
                } else {
                    addChangedValue(encodedContent);
                }
                last = current;
            }
            // The first one is separated.
            encodedContent.RemoveAt(0);
            return encodedContent;
        }

        private static void addChangedValue(List<bool> encodedContent) {
            encodedContent.Add(true);
        }

        private static void addEqualsValue(List<bool> encodedContent) {
            encodedContent.Add(false);
        }

        private static byte[] getByteArray(List<bool> encodedContent) {
            var bits = new BitArray(encodedContent.ToArray());
            var returnBits = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(returnBits, 0);
            return returnBits;
        }

        public static string decode(byte[] fileContent) {
            var decodedContent = first.ToString();
            var bits = new BitArray(fileContent);
            foreach (var bit in bits) {
                if (hasChanged(bit)) {
                    decodedContent = getChangedValue(decodedContent);
                } else {
                    decodedContent += getEqualValue(decodedContent);
                }
            }
            return decodedContent;
        }

        private static bool hasChanged(object bit) {
            return (bool)bit;
        }

        private static string getChangedValue(string decodedContent) {
            if (decodedContent[decodedContent.Length - 1].Equals('0')) {
                decodedContent += 1;
            } else {
                decodedContent += 0;
            }
            return decodedContent;
        }
        
        private static char getEqualValue(string decodedContent) {
            return decodedContent[decodedContent.Length - 1];
        }
    }
}
