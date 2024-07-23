using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] gradovi = { "Varaždin", "Zagreb", "Osijek", "Sisak", "Split", "Slavonski Brod" };

            // Query sintaksa upita
            var query = from grad in gradovi
                        where grad.StartsWith("S")
                        select grad;

            // Selektor sintaksa upita
            var query2 = gradovi
                .Where(grad => grad.StartsWith("S"));


            //enumerator

            var e = query.GetEnumerator();
            e.MoveNext();
            var clan = e.Current;
            //e.Reset();

            foreach (var izbor in query)
            {
                Console.WriteLine(izbor);
            }

            var artikli = new List<Artikl>()
            {
                new Artikl(104, "Pivo", 32432432432, 24, 1),
                new Artikl(204, "Sok", 455435435435, 10, 1),
                new Artikl(412, "Kruh", 543543543534, 0),
                new Artikl(320, "Mlijeko", 3243243266, 0),
                new Artikl(422, "Deo", 543543543534, 45, 4),
                new Artikl(234, "Sapun", 3243243266, 220, 4),
                new Artikl(444, "Šampon", 2131243543534, 220, 4)
            };

            artikli.ForEach(x => Console.WriteLine(x));

            // Query sintaksa

            var upit = (from artikl in artikli
                        where artikl.StanjeNaSkladistu > 0
                        select new { artikl.Sifra, artikl.Naziv }).ToList();

            // Selector sintaksa

            var upit2 = artikli // List<Artikl>
                .Where(artikl => artikl.StanjeNaSkladistu > 0) // IEnumerable<Artikl>
                .Select(x => new { x.Barkod, x.Naziv }) // IEnumerable<anonym>
                .ToList(); // List<anonym>

            // artikli.Sort();

            Console.WriteLine();

            var sortirano = (from artikl in artikli
                             orderby artikl.Naziv
                             select artikl
                             ).ToList();

            //var sortirano = from artikl in artikli
            //                orderby artikl.Naziv
            //                select artikl;

            sortirano.ForEach(x => Console.WriteLine(x));

            Console.WriteLine();

            // kompleksniji upit

            var osnovniUpit = from artikl in artikli
                              where artikl.StanjeNaSkladistu > 0 && artikl.StanjeNaSkladistu <= int.MaxValue
                              select artikl;

            var rezultirajuciUpit = osnovniUpit
                .OrderBy(x => x.Naziv)
                .Take(3);

            foreach (var stavka in rezultirajuciUpit)
                Console.WriteLine(stavka );

            var lista = from artikl in artikli
                        group artikl by artikl.Vrsta into rezultat
                        select rezultat;




            //UPDATE

            var y = from artikl in artikli select artikl;

            var ima = y.ElementAt(0);
            var nema = y.ElementAtOrDefault(10);

            var provjera = rezultirajuciUpit.SequenceEqual(osnovniUpit);

            if(y.Any(_ => _.Naziv == "Vino"))
            {
                Console.Write("Parrrty");
            }

            if (y.All(_ => _.StanjeNaSkladistu > 0))
            {
                Console.Write("Svi su tu!");
            }

            if(y.Contains(new Artikl(320, "Mlijeko", 3243243266, 0)))
            {
                Console.Write("POTOJIIIII");
            }

            ///filter i projekcija


            var filtrirano = (from artikl in artikli
                              where artikl.StanjeNaSkladistu > 0
                              select new { artikl.Barkod, artikl.Naziv });

            filtrirano = artikli.Where(artikl => artikl.StanjeNaSkladistu > 0)
                .Select(x => new { x.Barkod, x.Naziv });

            //sortiranjee by rule


            var abecedno = from a in artikli //query sintaksa
                           orderby a.Naziv 
                           select a;


             abecedno = artikli.OrderBy(_ => _.Naziv); //metodna

            var abecednoSilazno = from a in artikli
                                  orderby a.Naziv descending
                                  select a;

            abecednoSilazno = artikli.OrderByDescending(_ => _.Naziv);

            abecedno = from a in artikli
                       orderby a.StanjeNaSkladistu, a.Naziv
                       select a;

            abecedno = artikli.OrderBy(a => a.StanjeNaSkladistu).ThenBy(a => a.Naziv);


            //grupiranje by rule

            var grupirano = from artikl in artikli
                        group artikl by artikl.Vrsta;

             //grupirano = artikli.GroupBy(a => a.Vrsta);
             grupirano = artikli.ToLookup(a => a.Vrsta);//izvrsi se odma
            foreach (var grupa in grupirano)
            {
                Console.WriteLine("Vrsta artikla " + grupa.Key);
                foreach(var value in grupa)
                {
                    Console.WriteLine(value);
                }
            }


            //join

            var voce = new List<string>()
            {
                "Jabuka",
                "Banana",
                "Kruska",
                "Sljiva"
            };

            var voce2 = new List<string>()
            {
                "Naranca",
                "Limun",
                "Banana",
                "Jabuka",
                "Grejp"
            };

            voce.SequenceEqual(voce2);

            var z = voce.SequenceEqual(voce2);

            var podudarnosti = voce.Join(voce2,
                niz1 => niz1,
                niz2 => niz2,
            (niz1, niz2) => niz1);



            foreach (var podudar in podudarnosti)
            {
                Console.WriteLine(podudar);
            }


            var proizvodi1 = new List<Artikl>()
            {
                new Artikl(104, "Pivo", 32432432432, 24, 0),
                new Artikl(204, "Sok", 455435435435, 10, 1),
                new Artikl(412, "Kruh", 543543543534, 0),
                new Artikl(320, "Mlijeko", 3243243266, 0),
                new Artikl(444, "Šampon", 2131243543534, 220, 2)
            };

            var proizvodi2 = new List<Artikl>()
            {
                new Artikl(104, "Pivo", 32432432432, 24, 0),
                new Artikl(204, "Sok", 123, 10, 0),
                new Artikl(412, "Kruh", 512, 0),
                new Artikl(320, "Mlijeko", 3243243266, 0),
                new Artikl(422, "Deo", 543543543534, 45, 4),
                new Artikl(234, "Sapun", 3241233243266, 220, 4),
                new Artikl(444, "Šampon", 1, 10, 4)
            };

            var matches = proizvodi1.Join(proizvodi2,
                p1 => p1.Sifra,
                p2 => p2.Sifra,
              (p1, p2) => new
              {
                  p1.Naziv,
                  Barkod = p2.Barkod,
                  Stanje1 = p1.StanjeNaSkladistu,
                  Stanje2 = p2.StanjeNaSkladistu
              });

            var vrste = new List<VrstaArtikla>()
            {
                new VrstaArtikla() {Sifra = 1, Naziv = "Hrana"},
                 new VrstaArtikla() {Sifra = 0, Naziv = "Pice"},
                  new VrstaArtikla() {Sifra = 4, Naziv = "Kozmetika"},
                   new VrstaArtikla() {Sifra = 3, Naziv = "Ne znam"},
                   new VrstaArtikla() {Sifra = 2, Naziv = "Random vrsta"}
            };

            var spoj = proizvodi2.Join(vrste,
                proizvod => proizvod.Vrsta,
                vrsta => vrsta.Sifra,
                (proizvod, vrsta) => new
                {
                    SifraProizvoda = proizvod.Sifra,
                    Vrsta = vrsta.Naziv,
                    proizvod.Naziv,
                    proizvod.Barkod,
                    proizvod.StanjeNaSkladistu
                });

            spoj = from proizvod in proizvodi2
                   join vrsta in vrste
                   on proizvod.Vrsta equals vrsta.Sifra 
                   select new
                   {
                       SifraProizvoda = proizvod.Sifra,
                       Vrsta = vrsta.Naziv,
                       proizvod.Naziv,
                       proizvod.Barkod,
                       proizvod.StanjeNaSkladistu
                   };

            foreach (var podudar in spoj)
            {
                Console.WriteLine(podudar);
            }

            Console.ReadKey();


        }
    }

    public class Artikl
    {
        public Artikl(int sifra, string naziv, long barkod, int stanje, int vrsta = 1)
        {
            Sifra = sifra;
            Naziv = naziv;
            Barkod = barkod;
            StanjeNaSkladistu = stanje;
            Vrsta = vrsta;
        }

        public int Sifra { get; set; }

        public string Naziv { get; set; }

        public int Vrsta { get; set; }

        public long Barkod { get; set; }

        public int StanjeNaSkladistu { get; set; }

        public override string ToString()
        {
            return $"Sifra: {Sifra}, Naziv: {Naziv}, Barkod: {Barkod}, Stanje: {StanjeNaSkladistu}";
        }
    }

    public class VrstaArtikla
    {

        public int Sifra { get; set; }
        public string Naziv { get; set; }
        
    }
}