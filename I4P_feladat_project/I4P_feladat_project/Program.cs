using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4P_feladat_project
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Üdvözlöm a rejtjelezési programban!");
            Console.WriteLine("Kérem adja meg a rejtjelezni kívánt üzenetet az angol abc kisbetűivel:");
            string uz = Console.ReadLine();
            string kulcs = kulcsgenerator(uz.Length);
            Console.WriteLine($"A legenerált rejtjelezési kulcs: {kulcs}");
            string rejtuz = rejtjelezes(uz, kulcs);
            Console.WriteLine($"A rejtjelezett üzenet: {rejtuz}");
            Console.ReadKey();
        }
        static string kulcsgenerator(int hossz)
        {
            Random r = new Random();
            char[] c = new char[hossz];
            string kulcs = "";
            for (int i = 0; i < hossz; i++)
            {
                c[i] = (char)r.Next(97, 123);
            }
            for (int i = 0;i < c.Length; i++)
            {
                kulcs= kulcs + c[i];
            }
            return kulcs;
        }
        static string rejtjelezes(string uzenet, string kulcs)
        {
            string rejtuz = "";
            int[] t = new int[uzenet.Length];
            for (int i = 0; i < uzenet.Length; i++)
            {
                if (uzenet[i]==' ') t[i] = ((int)uzenet[i] - 6) + ((int)kulcs[i] - 97);
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
    }
}
