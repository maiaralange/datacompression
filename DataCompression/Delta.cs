using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace DataCompression {
    public static class Delta {
        public static char first;

        public static byte[] encode(string fileContent) {
            var encodedContent = populateListWithDeltaAlgorithm(fileContent);
            var encodedContentInBytes = getByteArray(encodedContent);
            var allEncodedContent = addFirstCharacter(encodedContentInBytes);
            return allEncodedContent;
        }

        private static byte[] addFirstCharacter(byte[] encodedContent) {
            var firstEncoded = (int)first;
            var result = new byte[encodedContent.Length + 1];
            result[0] = (byte)firstEncoded;
            for (int i = 1; i < result.Length; i++) {
                result[i] = encodedContent[i - 1];
            }
            return result;
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
            var bits = new BitArray(fileContent);
            // We get the first character
            var decodedContent = ((char)fileContent[0]).ToString();
            for (int i = 8; i < bits.Length; i++) {
                if (hasChanged(bits[i])) {
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
                decodedContent += "1";
            } else {
                decodedContent += "0";
            }
            return decodedContent;
        }
        
        private static char getEqualValue(string decodedContent) {
            return decodedContent[decodedContent.Length - 1];
        }
    }
}
