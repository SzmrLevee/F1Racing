using F1RacingLib;
using F1RacingLib.F1RacingLib;

Console.OutputEncoding = System.Text.Encoding.UTF8;

//TODO: drs, ers, rosszul kapta el a rajtot - kipörgés vagy törés, kicsúszás, tömegkarambol, benzin kevesebb - nagyobb százalékban tud előzni, több - kisebb százalék - lassabb, megjelenítés javítása
//moodle, pdf követelmények

// Console.Write("Adja meg a bemeneti fájl nevét: ");
// string fileNev = Console.ReadLine();
// (int korokSzama, List<Versenyzo> versenyzok) = Beolvasas.FajlBeolvasasa(fileNev);

(int korokSzama, List<Versenyzo> versenyzok) = Beolvasas.FajlBeolvasasa("versenyzok.txt");
Verseny verseny = new Verseny(korokSzama, versenyzok);

verseny.Szimulacio();
verseny.EredmenyKiiras();
