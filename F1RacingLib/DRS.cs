using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1RacingLib
{
    public class DRS
    {
        public bool Aktiv { get; set; }   // A DRS aktív állapota
        public bool Palyaszakasz { get; set; } // Jelzi, hogy a versenyző DRS-zónában van-e

        public DRS()
        {
            Aktiv = false;
            Palyaszakasz = false;
        }

        public void Kapcsol(bool aktiv)
        {
            if (Palyaszakasz)
            {
                Aktiv = aktiv;
            }
        }
    }
}
