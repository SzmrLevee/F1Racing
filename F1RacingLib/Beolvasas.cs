using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1RacingLib
{
    namespace F1RacingLib
    {
        public class Beolvasas
        {
            public static (int korokSzama, List<Versenyzo> versenyzok) FajlBeolvasasa(string fileNev)
            {
                string[] sorok = File.ReadAllLines(fileNev, Encoding.UTF8);
                string[] elsoSor = sorok[0].Split(';');
                int korokSzama = int.Parse(elsoSor[0]);
                int versenyzokSzama = int.Parse(elsoSor[1]);

                List<Versenyzo> versenyzok = new List<Versenyzo>();

                for (int i = 1; i <= versenyzokSzama; i++)
                {
                    string[] adat = sorok[i].Split(';');
                    string nev = adat[0];
                    Kategoria kategoria = (Kategoria)int.Parse(adat[1]);
                    string csapatNev = adat[2];

                    versenyzok.Add(new Versenyzo(nev, csapatNev, kategoria, i - 1, 100));
                }
                return (korokSzama, versenyzok);
            }
        }
    }
}
