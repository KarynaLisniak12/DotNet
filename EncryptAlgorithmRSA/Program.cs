using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ТЗ2
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputData = Console.ReadLine();

            Encrypt encrypt = new Encrypt();
            string encodeData = encrypt.Encode(inputData);

            using (StreamWriter outputFile = new StreamWriter(Environment.CurrentDirectory.ToString() + @"/EncryptedData.txt"))
            {
                outputFile.Write(encodeData);
            }

            Decrypt decrypt = new Decrypt(encrypt.openKey, encrypt.Function, encrypt.FuncN);
            string decodeData = decrypt.Decode(encodeData);

            Console.WriteLine(decodeData);

            Console.ReadKey();
        }
    }
}
