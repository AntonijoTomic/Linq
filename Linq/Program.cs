using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //PrviSat();

            Console.ReadLine();

        }

        private static void PrviSat()
        {
            string[] gradovi = { "Varazdin", "Zagreb", "Osijek", "Sisak", "Sibenik" };


            //query
            var query = from grad in gradovi
                        where grad.StartsWith("S")
                        select grad;

            // query.GetEnumerator();

            //selektor
            var kveri = gradovi
                .Where(x => x.StartsWith("S"));

            foreach (var grad in query)
            {
                Console.WriteLine(grad);
            }

            var artikli = new List<Artikl>()
            {
                new Artikl(104, "Pivo", 24),
                new Artikl(100, "Voda", 124),
                new Artikl(213, "Sok"),
                new Artikl(974, "Kruh")

            };



            artikli.ForEach(x => Console.WriteLine(x));

            var upit = (from artikl in artikli where artikl.Stanje > 0 select new { artikl.Sifra }).ToList();



            var upit2 = artikli.Where(artikl => artikl.Stanje > 0).Select(y => new { y.Barkod, y.Sifra });


            var sortirano = (from artikl in artikli orderby artikl.Name select artikl).ToList();


            //komplikacije

            var osnovni = from artikl in artikli
                          where artikl.Stanje > 0
                          select artikl;

            var rezultirajuciUpit = osnovni.OrderBy(x => x.Name).Take(2);
        }
    }

    public class Artikl
    {
        public Artikl(int sifra, string naziv, int stanje =0)
        {
            Sifra = sifra;  
            Name = naziv;
            Stanje = stanje;
        }
        public int Sifra { get; set; }
        public string Name { get; set; }
        public long Barkod { get; set; }

        public int Stanje { get; set; }
    }
}
