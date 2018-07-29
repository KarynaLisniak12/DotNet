using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ТЗ2
{
    public class Encrypt
    {
        private long firstSimpleNum = 50263;
        private long secondSimpleNum = 51151;
        public long openKey = 54919;
        
        public long FuncN
        {
            get
            {
                return firstSimpleNum * secondSimpleNum;
            }
        }

        public long Function
        {
            get
            {
                return (firstSimpleNum - 1) * (secondSimpleNum - 1);
            }
        }

        public string Encode(string inputData)
        {
            string encodedData = "";

            for (int i = 0; i < inputData.Length; i++)
            {
                long element = (long)inputData[i];
                long res = 1;
                long addOpenKey = openKey;

                while (addOpenKey != 1)
                {
                    if (addOpenKey % 2 == 0 && element < FuncN)
                    {
                        element = element * element;
                        addOpenKey /= 2;
                    }
                    else if (addOpenKey % 2 != 0)
                    {
                        res *= element;
                        res %= FuncN;
                        addOpenKey--;
                    }

                    element %= FuncN;
                }

                long codOfElement = (res * (element % FuncN)) % FuncN;
                encodedData += (char)(codOfElement % openKey);
                encodedData += (char)(codOfElement / openKey);
            }

            return encodedData;
        }

        public string Encode(int inputData)
        {
            string encodedData = "";

            for (int i = 0; i < inputData.ToString().Length; i++)
            {
                long element = (long)inputData.ToString()[i];
                long res = 1;
                long addOpenKey = openKey;
                while (addOpenKey != 1)
                {
                    if (addOpenKey % 2 == 0 && element < FuncN)
                    {
                        element = element * element;
                        addOpenKey /= 2;
                    }
                    else if (addOpenKey % 2 != 0)
                    {
                        res *= element;
                        res %= FuncN;
                        addOpenKey--;
                    }

                    element %= FuncN;
                }

                long codOfElement = (res * (element % FuncN)) % FuncN;
                encodedData += (char)(codOfElement % openKey);
                encodedData += (char)(codOfElement / openKey);
            }

            return encodedData;
        }

        public string Encode(double inputData)
        {
            string encodedData = "";

            for (int i = 0; i < inputData.ToString().Length; i++)
            {
                long element = (long)inputData.ToString()[i];
                long res = 1;
                long addOpenKey = openKey;

                while (addOpenKey != 1)
                {
                    if (addOpenKey % 2 == 0 && element < FuncN)
                    {
                        element = element * element;
                        addOpenKey /= 2;
                    }
                    else if (addOpenKey % 2 != 0)
                    {
                        res *= element;
                        res %= FuncN;
                        addOpenKey--;
                    }
                    element %= FuncN;
                }

                long codOfElement = (res * (element % FuncN)) % FuncN;
                encodedData += (char)(codOfElement % openKey);
                encodedData += (char)(codOfElement / openKey);
            }

            return encodedData;
        }
    }
}
