using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCompression {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("\nPressione qualquer tecla para iniciar a compressáo.");
            Console.ReadLine();

            Coder.encode("alice29.txt");
            Console.WriteLine("Arquivo comprimido com sucesso.");
            Console.WriteLine("Pressione qualquer tecla para iniciar a descompressáo.");
            Console.ReadLine();

            Coder.decode("alice29.encoded");
            Console.WriteLine("Arquivo descomprimido com sucesso.");
            Console.WriteLine("Pressione qualquer tecla para encerrar.");
            Console.ReadLine();

        }
    }
}
