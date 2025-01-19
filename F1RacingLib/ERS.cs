using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1RacingLib
{
    public class ERS
    {
        public int Eroforras { get; set; } // Az ERS aktuális energiája (0-100 közötti érték)
        public bool Aktiv { get; set; }   // Az ERS aktív állapotát jelzi

        public ERS()
        {
            Eroforras = 100; // Alapértelmezett érték
            Aktiv = false;   // Alapértelmezett inaktív állapot
        }

        public void Hasznal(int mennyiseg)
        {
            Eroforras = Math.Max(0, Eroforras - mennyiseg);
        }

        public void Toltes(int mennyiseg)
        {
            Eroforras = Math.Min(100, Eroforras + mennyiseg);
        }

        public void Kapcsol(bool aktiv)
        {
            Aktiv = aktiv;
        }
    }
}
