using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ТЗ2
{
    public class Decrypt
    {
        private long secretKey;
        public long openKey;
        public long funcN;
        public long function;

        internal Decrypt(long openKey, long function, long funcN)
        {
            this.openKey = openKey;
            this.funcN = funcN;
            this.function = function;
        }

        private void SearchOfSecretKey()
        {
            int count = 2;

            while ((function * count + 1) % openKey != 0)
            {
                count++;
            }

            secretKey = (function * count + 1) / openKey;
        }

        public string Decode(string cipherData)
        {
            SearchOfSecretKey();

            string decipher = "";
            for (int i = 0; i < cipherData.Length - 1; i += 2)
            {
                long res = 1;
                long elementRemain = (long)cipherData[i];
                long elementDevide = (long)cipherData[i + 1];
                long codOfElement = (openKey * elementDevide) + elementRemain;
                long addSecretKey = secretKey;
                while (addSecretKey != 1)
                {
                    if (addSecretKey % 2 == 0 && codOfElement < funcN)
                    {
                        codOfElement = codOfElement * codOfElement;
                        addSecretKey /= 2;
                    }
                    else if (addSecretKey % 2 != 0)
                    {
                        res *= codOfElement;
                        res %= funcN;
                        addSecretKey--;
                    }
                    codOfElement %= funcN;
                }
                long decodOfElement = (res * (codOfElement % funcN)) % funcN;
                decipher += (char)decodOfElement;
            }
            return decipher;
        }
    }
}
