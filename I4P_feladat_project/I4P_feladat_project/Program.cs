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
            Console.WriteLine($"Kérem adja meg a kulcsot:");
            string kulcs = Console.ReadLine();
            if (kulcs.Length<uz.Length) Console.WriteLine("A kulcs rövidebb az üzenetnél");
            else 
            {
                string rejtuz = rejtjelezes(uz, kulcs);
                Console.WriteLine($"A rejtjelezett üzenet: {rejtuz}");
                Console.WriteLine();
                Console.WriteLine($"A visszafejtett üzenet: {rejtjelfejto(rejtuz,kulcs)}");
            }

            Console.ReadKey();
        }
        static string rejtjelezes(string uzenet, string kulcs)
        {
            string rejtuz = "";
            int[] t = new int[uzenet.Length];
            for (int i = 0; i < uzenet.Length; i++)
            {
                if (uzenet[i]==' ' && kulcs[i]==' ') t[i] = ((int)uzenet[i] - 6) + ((int)kulcs[i] - 6);
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
    }
}
