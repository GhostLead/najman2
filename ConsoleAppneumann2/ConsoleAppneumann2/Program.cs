using ConsoleAppneumann2;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;

namespace ConsoleAppneumann2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Telepules> telepulesek = File.ReadAllLines("telepules.txt").Select(x => new Telepules(x)).ToList();
            List<Megyek> megyek = File.ReadAllLines("megyek.txt").Select(x => new Megyek(x)).ToList();
            Dictionary<string, int> megyeklakosai = new Dictionary<string, int>();

            // 2a)
            List<Telepules> telepulesekRendezveLakosokAlapjan = telepulesek.OrderBy(x => x.LakosokSzama).ToList();
            telepulesekRendezveLakosokAlapjan.RemoveAt(0);
            string megyenev = megyek.Find(x => x.Kod == telepulesekRendezveLakosokAlapjan[0].MegyeAzonosito).Nev;
            string keresettmegye = "";
            foreach (var telepules in telepulesek)
            {
                keresettmegye = megyek.Find(x => x.Kod == telepules.MegyeAzonosito).Nev;
                if (megyeklakosai.ContainsKey(keresettmegye))
                {
                    continue;
                }
                else
                {
                    megyeklakosai.Add(keresettmegye, 0);
                }

            }
            int lakosszam = 0;
            foreach (var item in megyeklakosai)
            {
                foreach (var telepules in telepulesek)
                {
                    keresettmegye = megyek.Find(x => x.Kod == telepules.MegyeAzonosito).Nev;
                    if (keresettmegye == item.Key)
                    {
                        megyeklakosai[keresettmegye] += telepules.LakosokSzama;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            Console.WriteLine($"2a) {megyeklakosai.OrderBy(x => x.Value).ElementAt(1).Key}-{megyeklakosai.OrderBy(x => x.Value).ElementAt(1).Value}");


            // 2b)
            Console.WriteLine($"2b) {telepulesek.MaxBy(x => x.Hosszusag).TelepulesNev}");

            // 2c)
            int db = 0;
            foreach (var telepues in telepulesek)
            {
                if (telepues.TavolsagKecskemettol <= 50 && telepues.TavolsagSzegedtol <= 50)
                {
                    db++;
                }
            }
            Console.WriteLine($"2c) {db}");

            // 2d)
            List<Telepules> megfeleloTelepulesek = new List<Telepules>();
            foreach (var telepules in telepulesek)
            {
                if (telepules.Szelesseg > 47.3 && telepules.Szelesseg < 47.4)
                {
                    megfeleloTelepulesek.Add(telepules);
                }
            }
            megfeleloTelepulesek.OrderBy(x => x.Hosszusag);
            string megoldas = "";
            double legnagyobbteruletkulonbseg = 0;
            for (int i = 0; i < megfeleloTelepulesek.Count; i++)
            {
                try
                {
                    if (Math.Abs(megfeleloTelepulesek[i + 1].Terulet - megfeleloTelepulesek[i].Terulet) > legnagyobbteruletkulonbseg)
                    {
                        legnagyobbteruletkulonbseg = Math.Abs(megfeleloTelepulesek[i + 1].Terulet - megfeleloTelepulesek[i].Terulet);
                        megoldas = $"{megfeleloTelepulesek[i].TelepulesNev}--{megfeleloTelepulesek[i + 1].TelepulesNev}--{legnagyobbteruletkulonbseg}";
                    }
                    
                }
                catch (ArgumentOutOfRangeException)
                {

                    continue;
                }
            }
            Console.WriteLine($"2d) {megoldas}");

            // 2e)
            List<string> budak = new List<string>();
            foreach (var telepules in telepulesek.OrderBy(x => x.Szelesseg))
            {
                if (telepules.TelepulesNev.ToLower().Contains("buda"))
                {
                    budak.Add(telepules.TelepulesNev);
                }
            }
            Console.WriteLine(budak[2]);

        }
    }
}