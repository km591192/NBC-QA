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
    class QA
    {
        SortedDictionary<string, string> ansdictionary = new SortedDictionary<string, string>();
        WordStemming ws = new WordStemming();
        
        public string keystring (string s)
        {
            string ans = "";
            int i = 0, j = s.Length;
            while (i < j && s[i] != '/')
            { ans += s[i]; i++; }
            ans = ws.Stemword(ans);
           return ans;
        }
        public static string getfilename(string s)
        {
            string ans = "";
            int i = 0, j = s.Length;
            while (i < j && s[i] != '.')
            { ans += s[i]; i++; }
            return ans;
        }
        public static string valuestring(string s)
        {
            string ans = "";
            int i = 0, j = s.Length,k=1;
            while (i < j && s[i] != '/')
                i++;
            k = i + 1;
           while (k < j)
            { ans += s[k]; k++; }
            return ans;
        }

        public static string keywordindictionary(string s)
        {
            string ans = "";
            int i = 0, j = s.Length, k = 1;
            while (i < j && s[i] != '_')
                i++;
            k = i + 1;
            while (k < j)
            { ans += s[k]; k++; }
            return ans;
        }

        public  void filetodic(SortedDictionary<string, string> ansdictionary,string filename)
        {
            string s = "";
            string skey = "";
            string svalue = "";
            string fn = "answer" + filename;
            string fninput = getfilename(filename);
            StreamReader sr = new StreamReader(fn, Encoding.Default);
            while (sr.EndOfStream != true)
            {
                s = sr.ReadLine() + '\n';
                skey = fninput + '_' + keystring(s);
                svalue = valuestring(s);
                ansdictionary.Add(skey.Trim(),svalue.Trim());
            }
        }
        public static string getkey(SortedDictionary<string, string> ansdictionary, string str)
        {
            string keystr = "",sss;
            string[] words = str.Split(' ');
            SortedDictionary<string, string>.KeyCollection keyColl = ansdictionary.Keys;
            foreach (string ss in keyColl)
            {
                sss = keywordindictionary(ss.ToLower().Trim());
                if (str.Contains(sss))
                    keystr += ss + " ";
            }
            if (keystr.Length < 2) 
            foreach (string ss in keyColl)
            {
                sss = keywordindictionary(ss.ToLower().Trim());
                for (int i = 0; i < words.Length;i++)
                if (sss.Contains(words[i]))
                    keystr += ss + " ";
            }
            return keystr;
        }


        public static string getanswer(SortedDictionary<string, string> ansdictionary, string str)
        {
            string keystr = "",answer="";
            keystr = getkey(ansdictionary, str);
            ansdictionary.TryGetValue(keystr.Trim(), out answer);
            return answer;
        }

        public string answer(string [] fn,string str)
        {
            string answer = "";
            for (int i = 0; i < fn.Length; i++)
            {
                filetodic(ansdictionary, fn[i]);
                answer += getanswer(ansdictionary, str) + "\n";
                ansdictionary.Clear();
            }
            if (answer.Length < 2) answer = "Попробуйте задать вопрос по-другому (уточнив вопрос или перефразировать). Ответа на Ваш вопрос нет.";
                return answer;
        }
    }
}
