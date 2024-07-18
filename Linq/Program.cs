using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
             //Prvi();

            //Drugi();


           peti();
            Console.ReadLine();
        }

        private static void peti()
        {
            Skladiste n1 = new Skladiste(1, "Zagreb", 300);
            Skladiste n2 = new Skladiste(2, "Zagreb", 200);
            Skladiste n3 = new Skladiste(3, "Bjelovar", 200);
            Proizvod p1 = new Proizvod(104, "Pivo", n1);
            Proizvod p2 = new Proizvod(100, "Voda", n2);
            Proizvod p3 = new Proizvod(10, "Kruh", n1);
            Proizvod p4 = new Proizvod(70, "Limun", n2);
            Proizvod p5 = new Proizvod(101234, "Boca", n1);
            Proizvod p6 = new Proizvod(10110, "Laptop", n2);

            List<Proizvod> proizvodi = new List<Proizvod>() { p1, p2, p3, p4, p5, p6 };

            var grupirano = proizvodi.GroupBy(p => p.Skladiste1).ToList();

            foreach (var grupa in grupirano)
            {
                Console.WriteLine($"Skladiste: {grupa.Key}");
                foreach (var proizvod in grupa)
                {
                    Console.WriteLine($"{proizvod.Name}");
                }
            }
            Console.ReadLine();
        }

        private static void Drugi()
        {
            ArrayList arrayList = new ArrayList()
            {
                new Skladiste(1,"Zagreb", 300),
                new Skladiste(2, "Zagreb", 200),
                new Skladiste(3, "Bjelovar", 200),
                new Proizvod(104, "Pivo", 24, "Pice"),
                new Proizvod(100, "Voda", 124, "Pice"),
            };

            var filtriranaSkladista = arrayList.OfType<Skladiste>().ToList();
            foreach (var skl in filtriranaSkladista)
            {
                Console.WriteLine($"Lokacija: {skl.Lokacija}, Kapacitet: {skl.Kapacitet}");
            }


            ///treci
            var sortiraniUpit = filtriranaSkladista.OrderBy(skl => skl.Lokacija).ThenBy(n => n.Kapacitet).ToList();
            Console.WriteLine("\nSORTIRANA SKLADISTA\n");
            foreach (var skl in sortiraniUpit)
            {
                Console.WriteLine($"Lokacija: {skl.Lokacija}, Kapacitet: {skl.Kapacitet}");
            }

      
        }

        private static void Prvi()
        {
            var proizvodi = new List<Proizvod>()
            {
                new Proizvod(104, "Pivo", 24, "Pice"),
                new Proizvod(100, "Voda", 124, "Pice"),
                new Proizvod(213, "Sok",32, "Pice"),
                new Proizvod(974, "Kruh")

            };
            var upit = (from proizvod in proizvodi where proizvod.Stanje > 0 select new { proizvod.Sifra, proizvod.Name });

            foreach (var proizvod in upit)
            {
                Console.WriteLine($"Ime: {proizvod.Name}, Sifra: {proizvod.Sifra}");
            }

            //treci zadatak
            var sortiraniUpit = upit.OrderBy(p => p.Sifra).ThenBy(n => n.Name).ToList();
            Console.WriteLine("\nSORTIRANI PROZIVODI\n");
            foreach (var proizvod in sortiraniUpit)
            {
                Console.WriteLine($"Ime: {proizvod.Name}, Sifra: {proizvod.Sifra}");
            }
        }    
    }

    public class Proizvod
    {
       
        public Proizvod(int sifra, string naziv, int stanje = 0, string kategorija = "Hrana")
        {
            Sifra = sifra;
            Name = naziv;
            Stanje = stanje;
            Kategorija = kategorija;
        }
        public Proizvod(int sifra, string naziv, Skladiste skladiste, int stanje = 0, string kategorija = "Hrana")
        {
            Sifra = sifra;
            Name = naziv;
            Stanje = stanje;
            Skladiste1 = skladiste;

        }
        
        public int Sifra { get; set; }
        public string Name { get; set; }
        public long Barkod { get; set; }

        public int Stanje { get; set; }
        public string Kategorija { get; set; }
        public Skladiste Skladiste1 { get; set; } 
        public override string ToString()
        {
            return $"Sifra: {Sifra}, Name: {Name}, Skladiste: {Skladiste1.Lokacija}";
        }
    }
    public class Skladiste
    {
        public int Id { get; set; }

        public string Lokacija { get; set; }
        public int Kapacitet { get; set; }

        public Skladiste(int id, string lokacija, int kapacitet)
        {
            Id = id;
            Lokacija = lokacija;
            Kapacitet = kapacitet;
        }

        public override string ToString()
        {
            return $"{Id} Lokacija: {Lokacija}, Kapacitet: {Kapacitet}";
        }
    }
}
