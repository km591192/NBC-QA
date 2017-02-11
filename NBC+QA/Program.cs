using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace NBC_QA
{
    class Program
    {
        static void Main(string[] args)
        {
            NBCClass nbcc = new NBCClass();
            QA qac = new QA();
            WordStemming ws = new WordStemming();
            SS_LDFS ssldfs = new SS_LDFS();

            string str = "отели возле моря";
            Console.WriteLine(str);
            str = ws.getQuestion(str);
            Console.WriteLine(str);
            string [] fn = nbcc.nbcbuild_returnfn("person.txt", "location.txt", "date.txt", str);
            Console.WriteLine(); 
            string answer = qac.answer(fn, str);
            Console.WriteLine(answer);
            Console.ReadLine();
        }
    }
}
