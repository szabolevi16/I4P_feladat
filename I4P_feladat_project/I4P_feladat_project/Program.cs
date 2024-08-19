using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

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

            string rejtuz1 = rejtjelezes(s1, titkulcs);
            string rejtuz2 = rejtjelezes(s2, titkulcs);

            Console.WriteLine($"1. rejtuz: {rejtuz1}, 2. rejtuz: {rejtuz2}");
            Console.WriteLine("A lehetséges kulcsok:");

            var kulcsok = kulcsfejto(rejtuz1, rejtuz2);
            foreach (string item in kulcsok)
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
            string kezdoKulcs = new string('a', Math.Max(uz1.Length, uz2.Length));
            HashSet<string> szotar = new HashSet<string>(l);
            object locker = new object();

            Parallel.ForEach(GenerateAllKeys(kezdoKulcs), kulcs =>
            {
                bool lehetJoKulcs = true;
                string uzenet1 = rejtjelfejto(uz1, kulcs);
                string uzenet2 = rejtjelfejto(uz2, kulcs);

                
                if (!MindenSzoEgyezikReszben(uzenet1, szotar) || !MindenSzoEgyezikReszben(uzenet2, szotar))
                {
                    lehetJoKulcs = false;
                }

                if (lehetJoKulcs && MindenSzoEgyezik(uzenet1, szotar) && MindenSzoEgyezik(uzenet2, szotar))
                {
                    lock (locker)
                    {
                        kulcsok.Add(kulcs);
                    }
                }
            });

            return kulcsok;
        }

        private static bool MindenSzoEgyezikReszben(string uzenet, HashSet<string> szotar)
        {
            string[] szavak = uzenet.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var szo in szavak)
            {
                if (!szotar.Contains(szo) && !szotar.Any(word => word.StartsWith(szo)))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool MindenSzoEgyezik(string uzenet, HashSet<string> szotar)
        {
            string[] szavak = uzenet.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var szo in szavak)
            {
                if (!szotar.Contains(szo))
                {
                    return false;
                }
            }

            return true;
        }

        static IEnumerable<string> GenerateAllKeys(string kezdoKulcs)
        {
            char[] kulcs = kezdoKulcs.ToCharArray();

            while (true)
            {
                yield return new string(kulcs);

                int index = kulcs.Length - 1;
                while (index >= 0)
                {
                    if (kulcs[index] == 'z')
                    {
                        kulcs[index] = ' ';
                        index--;
                    }
                    else if (kulcs[index] == ' ')
                    {
                        kulcs[index] = 'a';
                        break;
                    }
                    else
                    {
                        kulcs[index]++;
                        break;
                    }
                }

                if (index < 0) yield break;
            }
        }
    }
}
