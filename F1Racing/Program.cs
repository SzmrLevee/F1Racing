using F1RacingLib;
using F1RacingLib.F1RacingLib;  // A névtér importálása
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Console.Write("Adja meg a bemeneti fájl nevét: ");
// string fileNev = Console.ReadLine();
// (int korokSzama, List<Versenyzo> versenyzok) = Beolvasas.FajlBeolvasasa(fileNev);

(int korokSzama, List<Versenyzo> versenyzok) = Beolvasas.FajlBeolvasasa("versenyzok.txt");  // Explicit típusok
Verseny verseny = new Verseny(korokSzama, versenyzok);

verseny.Szimulacio();
verseny.EredmenyKiiras();
