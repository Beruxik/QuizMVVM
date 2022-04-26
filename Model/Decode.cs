using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizMVVM.Model
{
    static public class Decode
    {
        static public string Decoding(string fileName, int n)
        {
            string fileString = File.ReadAllText(fileName);
            string fileStringDecrypted = "";
            for (int i = 0; i < fileString.Length; i++)
            {
                int charIndex = fileString[i];
                fileStringDecrypted += Convert.ToChar(charIndex - n);
            }
            return fileStringDecrypted;
        }
    }
}
