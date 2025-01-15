using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1RacingLib
{
    public class Verseny
    {
        private List<Versenyzo> versenyzok;  // Aktív versenyzők listája
        private List<Versenyzo> kiesettVersenyzok = new List<Versenyzo>();  // Kiesett versenyzők
        private int korokSzama;  // Verseny hossza
        private static Random ran = new Random();

        private Dictionary<int, List<string>> korTortenesek = new Dictionary<int, List<string>>();
        private Dictionary<int, List<Versenyzo>> kiesettVersenyzokKoronkent = new Dictionary<int, List<Versenyzo>>();
        private int seed = 12345;  // Alapértelmezett seed az azonos véletlen számokhoz

        public Verseny(int korokSzama, List<Versenyzo> versenyzok)
        {
            this.korokSzama = korokSzama;
            this.versenyzok = versenyzok;
        }

        // Verseny szimulációja
        public void Szimulacio()
        {
            int aktualisKor = 0;
            Random korRng = new Random(seed);  // Véletlenszám-generátor ismételhetőséghez
            Dictionary<int, List<Versenyzo>> korSorrendek = new Dictionary<int, List<Versenyzo>>();
            List<Versenyzo> osszesKiesettVersenyzo = new List<Versenyzo>();  // Összes kiesett versenyző listája

            while (aktualisKor < korokSzama)
            {
                Console.Clear();
                Console.WriteLine($"--- {aktualisKor + 1}. kör ---\n");

                // Kiesett versenyzők kiírása
                Console.WriteLine("Kiesett versenyzők addig:");
                if (osszesKiesettVersenyzo.Count > 0)
                {
                    foreach (var kiesett in osszesKiesettVersenyzo)
                    {
                        Console.WriteLine($"- {kiesett.Nev}");
                    }
                }
                else
                {
                    Console.WriteLine("- Nincs kieső.");
                }
                Console.WriteLine();

                List<string> tortenesek;

                if (korTortenesek.ContainsKey(aktualisKor))
                {
                    tortenesek = korTortenesek[aktualisKor];
                    versenyzok = korSorrendek[aktualisKor].Select(v => new Versenyzo(v)).ToList();
                }
                else
                {
                    tortenesek = new List<string>();
                    korTortenesek[aktualisKor] = tortenesek;

                    for (int i = versenyzok.Count - 1; i >= 0; i--)
                    {
                        Versenyzo versenyzo = versenyzok[i];
                        versenyzo.Benzin -= 5;

                        if (versenyzo.SzükségesTankolni())
                        {
                            tortenesek.Add($"{versenyzo.Nev} tankol.");
                            versenyzo.Tankol();
                            int ujPozicio = Math.Min(versenyzok.Count - 1, versenyzo.Pozicio + 5);
                            versenyzo.Pozicio = ujPozicio;
                            continue;
                        }

                        if (versenyzo.ProbalElőzni(aktualisKor + 1))
                        {
                            versenyzo.Benzin -= 4;  // Előzés miatti benzincsökkenés
                            int celPozicio = versenyzo.Pozicio - 1;

                            if (celPozicio >= 0)
                            {
                                Versenyzo celVersenyzo = versenyzok[celPozicio];

                                if (versenyzo.Csapat == celVersenyzo.Csapat)
                                {
                                    tortenesek.Add($"{versenyzo.Nev} sikeresen megelőzte csapattársát ({celVersenyzo.Nev})!");
                                }
                                else
                                {
                                    double balesetEsely = versenyzo.Kategoria == Kategoria.Veszelyes ? 0.08 : 0.04;
                                    if (korRng.NextDouble() < balesetEsely)
                                    {
                                        tortenesek.Add($"{versenyzo.Nev} és {celVersenyzo.Nev} kiestek baleset miatt.");
                                        versenyzo.Kiesett = true;
                                        celVersenyzo.Kiesett = true;
                                        versenyzok.RemoveAll(v => v.Kiesett);
                                        osszesKiesettVersenyzo.Add(versenyzo);
                                        osszesKiesettVersenyzo.Add(celVersenyzo);
                                        continue;
                                    }
                                    else
                                    {
                                        tortenesek.Add($"{versenyzo.Nev} sikeresen megelőzte {celVersenyzo.Nev}-t!");

                                        // Versenyzők cseréje a listában
                                        (versenyzok[celPozicio], versenyzok[i]) = (versenyzok[i], versenyzok[celPozicio]);

                                        // Pozíciók frissítése
                                        versenyzok[celPozicio].Pozicio = celPozicio;
                                        versenyzok[i].Pozicio = i;
                                    }
                                }
                            }
                        }
                    }

                    // Sorrend frissítése az adott kör végén
                    versenyzok = versenyzok.OrderBy(v => v.Pozicio).ToList();
                    for (int j = 0; j < versenyzok.Count; j++)
                    {
                        versenyzok[j].Pozicio = j;
                    }

                    korSorrendek[aktualisKor] = versenyzok.Select(v => new Versenyzo(v)).ToList();
                }

                Console.WriteLine("Történések:");
                foreach (var tortenes in tortenesek)
                {
                    Console.WriteLine(tortenes);
                }

                Console.WriteLine("\nJelenlegi sorrend:");
                for (int poz = 0; poz < versenyzok.Count; poz++)
                {
                    Console.WriteLine($"{poz + 1}. {versenyzok[poz].Nev} | Csapat: {versenyzok[poz].Csapat} | Stílus: {versenyzok[poz].Kategoria} | Benzin: {versenyzok[poz].Benzin}%");
                }

                Console.WriteLine("\nTovább a következő körhöz: Enter... Vissza az előző körhöz: Backspace...");
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.Enter)
                {
                    aktualisKor++;
                }
                else if (key == ConsoleKey.Backspace && aktualisKor > 0)
                {
                    aktualisKor--;
                }
            }

            Console.Clear();
            EredmenyKiiras();
        }

        // Verseny végeredményének kiírása
        public void EredmenyKiiras()
        {
            Console.Clear();
            versenyzok = versenyzok.OrderBy(x => x.Pozicio).ToList();
            Console.WriteLine("--- Verseny végeredmény ---");
            Console.WriteLine($"1. hely: {versenyzok[0].Nev}");
            Console.WriteLine($"2. hely: {versenyzok[1].Nev}");
            Console.WriteLine($"3. hely: {versenyzok[2].Nev}");
        }
    }
}
