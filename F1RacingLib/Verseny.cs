using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1RacingLib
{
    public class Verseny
    {
        private List<Versenyzo> versenyzok;
        private List<Versenyzo> kiesettVersenyzok = new List<Versenyzo>();
        private int korokSzama;

        private Dictionary<int, List<string>> korTortenesek = new Dictionary<int, List<string>>();
        private Dictionary<int, List<Versenyzo>> kiesettVersenyzokKoronkent = new Dictionary<int, List<Versenyzo>>();
        private int seed = 12345;

        public Verseny(int korokSzama, List<Versenyzo> versenyzok)
        {
            this.korokSzama = korokSzama;
            this.versenyzok = versenyzok;
        }

        public void Szimulacio()
        {
            int aktualisKor = 0;
            Random korRng = new Random(seed);
            Dictionary<int, List<Versenyzo>> korSorrendek = new Dictionary<int, List<Versenyzo>>();
            List<Versenyzo> osszesKiesettVersenyzo = new List<Versenyzo>();

            while (aktualisKor < korokSzama)
            {
                Console.Clear();
                Console.WriteLine($"--- {aktualisKor + 1}. kör eredményei ---\n");

                Console.WriteLine("Kiesett versenyzők:");
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

                        if (versenyzo.SzuksegesTankolni())
                        {
                            tortenesek.Add($"{versenyzo.Nev} tankol.");
                            versenyzo.Tankol();
                            int ujPozicio = Math.Min(versenyzok.Count - 1, versenyzo.Pozicio + 5);
                            versenyzo.Pozicio = ujPozicio;
                            continue;
                        }

                        if (versenyzo.ProbalElozni(aktualisKor + 1))
                        {
                            versenyzo.Benzin -= 4;
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

                                        (versenyzok[celPozicio], versenyzok[i]) = (versenyzok[i], versenyzok[celPozicio]);

                                        versenyzok[celPozicio].Pozicio = celPozicio;
                                        versenyzok[i].Pozicio = i;
                                    }
                                }
                            }
                        }
                    }

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

                Console.WriteLine("\nTovább a következő körhöz: Enter... \nVissza az előző körhöz: Backspace...");
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
        public void EredmenyKiiras()
        {
            Console.Clear();
            versenyzok = versenyzok.OrderBy(x => x.Pozicio).ToList();
            Console.WriteLine("--- Verseny végeredmény ---");
            Console.WriteLine($"1. hely: {versenyzok[0].Nev} | Csapat: {versenyzok[0].Csapat} | Stílus: {versenyzok[0].Kategoria} | Benzin: {versenyzok[0].Benzin}%");
            Console.WriteLine($"2. hely: {versenyzok[1].Nev} | Csapat: {versenyzok[1].Csapat} | Stílus: {versenyzok[1].Kategoria} | Benzin: {versenyzok[1].Benzin}%");
            Console.WriteLine($"3. hely: {versenyzok[2].Nev} | Csapat: {versenyzok[2].Csapat} | Stílus: {versenyzok[2].Kategoria} | Benzin: {versenyzok[2].Benzin}%");
        }
    }
}
