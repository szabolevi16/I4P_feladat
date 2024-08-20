using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace I4P_feladat_project
{
    internal class Program
    {
        public static List<string> l = new List<string>();
        static void Main(string[] args)
        {
            string titkulcs;
            
            Console.WriteLine("Üdvözlöm a rejtjelezési programban!");
            Console.WriteLine("Kérem adja meg a rejtjelezni kívánt üzenetet az angol abc kisbetűivel:");
            string uz = Console.ReadLine();
            Console.WriteLine($"Kérem adja meg a kulcsot:");
            string kulcs = Console.ReadLine();
            if (kulcs.Length < uz.Length) Console.WriteLine("A kulcs rövidebb az üzenetnél");
            else
            {
                string rejtuz = rejtjelezes(uz, kulcs);
                Console.WriteLine($"A rejtjelezett üzenet: {rejtuz}");
                Console.WriteLine();
                Console.WriteLine($"A visszafejtett üzenet: {rejtjelfejto(rejtuz, kulcs)}");
            }
            beolv();
            Console.WriteLine("Kérem adjon meg egy rejtjelezni kívánt üzenetet:");
            string s1 = Console.ReadLine();
            Console.WriteLine("Kérem adjon meg még egy rejtjelezni kívánt üzenetet:");
            string s2 = Console.ReadLine();
            if (s2.Length > s1.Length) titkulcs = kulcsgenerator(s2.Length);
            else titkulcs = kulcsgenerator(s1.Length);
            Console.WriteLine($"A használt kulcs:{titkulcs}");
            string rejtuz1 = rejtjelezes(s1, titkulcs);
            string rejtuz2 = rejtjelezes(s2, titkulcs);
            Console.WriteLine($"1. rejtuz: {rejtuz1}, 2. rejtuz: {rejtuz2}");
            Console.WriteLine("A lehetséges kulcsok:");
            foreach (string item in kulcsfejto(rejtuz1, rejtuz2))
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static string rejtjelezes(string uzenet, string kulcs)
        {
            string rejtuz = "";
            int[] t = new int[uzenet.Length];
            for (int i = 0; i < uzenet.Length; i++)
            {
                if (uzenet[i] == ' ' && kulcs[i] == ' ') t[i] = ((int)uzenet[i] - 6) + ((int)kulcs[i] - 6);
                else if (uzenet[i] == ' ' && kulcs[i] != ' ') t[i] = ((int)uzenet[i] - 6) + ((int)kulcs[i] - 97);
                else if (uzenet[i] != ' ' && kulcs[i] == ' ') t[i] = ((int)uzenet[i] - 97) + ((int)kulcs[i] - 6);
                else t[i] = ((int)uzenet[i] - 97) + ((int)kulcs[i] - 97);
            }
            for (int j = 0; j < t.Length; j++)
            {
                if (t[j] > 26) t[j] = t[j] % 27;
                if (t[j] == 26) t[j] = t[j] + 6;
                else if (t[j] != 26) t[j] = t[j] + 97;
                rejtuz = rejtuz + (char)t[j];
            }
            return rejtuz;
        }
        static string rejtjelfejto(string rejtuz, string kulcs)
        {
            string uzenet = "";
            int[] t = new int[rejtuz.Length];
            for (int i = 0; i < rejtuz.Length; i++)
            {
                if (rejtuz[i] == ' ' && kulcs[i] == ' ') t[i] = ((int)rejtuz[i] - 6) - ((int)kulcs[i] - 6);
                else if (rejtuz[i] == ' ' && kulcs[i] != ' ') t[i] = ((int)rejtuz[i] - 6) - ((int)kulcs[i] - 97);
                else if (rejtuz[i] != ' ' && kulcs[i] == ' ') t[i] = ((int)rejtuz[i] - 97) - ((int)kulcs[i] - 6);
                else t[i] = ((int)rejtuz[i] - 97) - ((int)kulcs[i] - 97);
            }
            for (int j = 0; j < t.Length; j++)
            {
                if (t[j] < 0) t[j] = t[j] + 27;
                if (t[j] == 26) t[j] = t[j] + 6;
                else if (t[j] != 26) t[j] = t[j] + 97;
                uzenet = uzenet + (char)t[j];
            }
            return uzenet;
        }
        static string kulcsgenerator(int hossz)
        {
            Random r = new Random();
            int[] t = new int[hossz];
            string s = "";
            for (int i = 0; i < hossz; i++)
            {
                t[i] = r.Next(0, 27);
                if (t[i] < 0) t[i] = t[i] + 27;
                if (t[i] == 26) t[i] = t[i] + 6;
                else if (t[i] != 26) t[i] = t[i] + 97;
                s = s + (char)t[i];
            }
            return s;
        }
        static void beolv()
        {
            StreamReader sr = new StreamReader("words.txt");
            while (!sr.EndOfStream)
            {
                l.Add(sr.ReadLine());
            }
            sr.Close();
        }
        public static List<string> kulcsfejto(string uz1, string uz2)
        {
            List<string> kulcsok = new List<string>();
            string kulcs = "";
            int hossz;
            if (uz1.Length > uz2.Length)
            {
                hossz = uz1.Length;
            }
            else hossz = uz2.Length;
            for (int i = 0; i < hossz; i++)
            {
                kulcs = kulcs + 'a';
            }
            while (kulcs!="vege")
            {
                string uzenet1 = rejtjelfejto(uz1,kulcs);
                string uzenet2 = rejtjelfejto(uz2,kulcs);
                bool duplaspace = true;
                bool elsospace = true;
                for (int i = 0; i < uzenet1.Length-1; i++)
                {
                    if (uzenet1[i]==' ' && uzenet1[i+1]== ' ')
                    {
                        duplaspace = false;
                    }
                }
                for (int i = 0; i < uzenet2.Length-1; i++)
                {
                    if (uzenet2[i] == ' ' && uzenet2[i+1] == ' ')
                    {
                        duplaspace = false;
                    }
                }
                if (uzenet1[0] == ' ' || uzenet2[0] == ' ') elsospace = false;
                if (duplaspace && elsospace)
                {
                    string[] tu1 = uzenet1.Split(' ');
                    string[] tu2 = uzenet2.Split(' ');
                    int test1 = 0;
                    int test2 = 0;
                    for (int i = 0; i < l.Count; i++)
                    {
                        for (int j = 0; j < tu1.Length; j++)
                        {
                            if (l[i] == tu1[j])
                            {
                                test1++;
                            }
                        }
                        for (int k = 0; k < tu2.Length; k++)
                        {
                            if (l[i] == tu2[k])
                            {
                                test2++;
                            }
                        }
                    }
                    if (test1 == tu1.Length && test2 == tu2.Length)
                    {
                        kulcsok.Add(kulcs);
                    }
                }
                kulcs = kulcslepteto(kulcs);
            }
            return kulcsok;
        }
        static string kulcslepteto(string kulcs)
        {
            string _kulcs = "";
            int[] t = new int[kulcs.Length];
            for (int i = 0; i < kulcs.Length; i++)
            {
                if (kulcs[i] == ' ') t[i] = (int)kulcs[i] - 6;
                else if((int)kulcs[i]>96 && (int)kulcs[i]<123) t[i] = (int)kulcs[i] - 97;
            }
            t[t.Length - 1] = t[t.Length - 1] + 1;
            for (int i = t.Length-1; i >= 0; i--)
            {
                if (t[i]==27 && t[0]!=27)
                {
                    t[i] = 0;
                    t[i - 1] = t[i - 1] + 1;
                }
                else if(t[0] == 27) _kulcs = "vege";
            }
            if (_kulcs!="vege")
            {
                for (int i = 0; i < t.Length; i++)
                {
                    if (t[i] == 26) t[i] = t[i] + 6;
                    else t[i] = t[i] + 97;
                    _kulcs = _kulcs + (char)t[i];
                }
            }
            return _kulcs;
        }
    }
}
