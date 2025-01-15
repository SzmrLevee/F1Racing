namespace F1RacingTests;
using F1RacingLib;

[TestClass]
public class VersenyTests
{
    [TestMethod]
    public void Szimulacio_VersenySorrenfrissites()
    {
        List<Versenyzo> versenyzok = new List<Versenyzo>
        {
            new Versenyzo { Nev = "Lewis Hamilton", Kategoria = Kategoria.Lenduletes, Csapat = "Mercedes", Pozicio = 1 },
            new Versenyzo { Nev = "Max Verstappen", Kategoria = Kategoria.Agressziv, Csapat = "RedBull", Pozicio = 2 }
        };
        Verseny verseny = new Verseny(5, versenyzok);

        verseny.Szimulacio();

        Assert.AreEqual(2, versenyzok.Count);
        Assert.IsTrue(versenyzok.Exists(v => v.Nev == "Max Verstappen"));
        Assert.AreNotEqual(1, versenyzok[1].Pozicio);
    }

    [TestMethod]
    public void EredmenyKiiras_HelyesElsoHaromHelyezett()
    {
        List<Versenyzo> versenyzok = new List<Versenyzo>
        {
            new Versenyzo { Nev = "Carlos Sainz", Kategoria = Kategoria.Veszelyes, Csapat = "Ferrari", Pozicio = 1 },
            new Versenyzo { Nev = "Fernando Alonso", Kategoria = Kategoria.Ovatos, Csapat = "AstonMartin", Pozicio = 2 },
            new Versenyzo { Nev = "Lando Norris", Kategoria = Kategoria.Lenduletes, Csapat = "McLaren", Pozicio = 3 }
        };
        Verseny verseny = new Verseny(3, versenyzok);

        verseny.EredmenyKiiras();

        Assert.AreEqual("Carlos Sainz", versenyzok[0].Nev);
        Assert.AreEqual("Fernando Alonso", versenyzok[1].Nev);
        Assert.AreEqual(3, versenyzok.Count);
    }
}