using System;

namespace DataCompression {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("\nCompressão de dados:");
            Console.WriteLine("\t1 - alice29.txt");
            Console.WriteLine("\t2 - sum");
            Console.Write("Digite a opção desejada: ");
            var option = Console.ReadLine();

            if (option.Equals("1")) {
                encodeAndDecode("alice29", ".txt");
            } else if (option.Equals("2")) {
                encodeAndDecode("sum", "");
            }
        }

        private static void encodeAndDecode(string filename, string extension) {
            Console.WriteLine("\nComprimindo...");
            Coder.encode(filename + extension);
            successEncode();
            Coder.decode(filename + ".encoded");
            successDecode();
        }

        private static void successEncode() {
            Console.WriteLine("Arquivo comprimido com sucesso.");
            Console.WriteLine("Pressione qualquer tecla para iniciar a descompressáo.");
            Console.ReadLine();
            Console.WriteLine("Descomprimindo...");
        }

        private static void successDecode() {
            Console.WriteLine("Arquivo descomprimido com sucesso.");
            Console.WriteLine("Pressione qualquer tecla para encerrar.");
            Console.ReadLine();
        }
    }
}
