using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1RacingLib
{
    public class Versenyzo
    {
        public string Nev { get; set; }
        public Kategoria Kategoria { get; set; }
        public int Benzin { get; set; } = 100;
        public int Pozicio { get; set; }
        public bool Kiesett { get; set; } = false;
        public string Csapat { get; set; }  // Csapat string típusú, így nem kell .Nev

        // Alapértelmezett konstruktor
        public Versenyzo() { }

        public Versenyzo(Versenyzo masolat)
        {
            this.Nev = masolat.Nev;
            this.Kategoria = masolat.Kategoria;
            this.Benzin = masolat.Benzin;
            this.Pozicio = masolat.Pozicio;
            this.Kiesett = masolat.Kiesett;
            this.Csapat = masolat.Csapat;
        }

        private static Random ran = new Random();

        public bool ProbalElőzni(int kor)
        {
            switch (Kategoria)
            {
                case Kategoria.Agressziv:
                    return kor % 2 == 0 && ran.Next(1, 4) == 1;
                case Kategoria.Lenduletes:
                    return kor % 5 == 0 && ran.Next(1, 3) == 1;
                case Kategoria.Veszelyes:
                    return kor % 4 == 0 && ran.Next(1, 5) == 1;
                case Kategoria.Ovatos:
                    return false;
                default:
                    return false;
            }
        }

        public void Tankol()
        {
            Benzin = 100;
        }

        public bool SzükségesTankolni()
        {
            return Kategoria switch
            {
                Kategoria.Agressziv => Benzin < 10,
                Kategoria.Lenduletes => Benzin < 20,
                Kategoria.Veszelyes => Benzin < 5,
                Kategoria.Ovatos => Benzin < 20,
                _ => false,
            };
        }
    }
}
