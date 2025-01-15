using F1RacingLib.F1RacingLib;

namespace F1RacingTests;
using F1RacingLib;

[TestClass]
public class BeolvasasTests
{
    [TestMethod]
    public void FajlBeolvasasa_HelyesAdatokatOlvasBe()
    {
        (int korokSzama, List<Versenyzo> versenyzok) = Beolvasas.FajlBeolvasasa("versenyzok.txt");
        Assert.AreEqual(50, korokSzama);
        Assert.AreEqual(20, versenyzok.Count);
        Assert.AreEqual("Max Verstappen", versenyzok[0].Nev);
        Assert.AreEqual(Kategoria.Agressziv, versenyzok[0].Kategoria);
        Assert.AreEqual("RedBull", versenyzok[0].Csapat);
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void FajlBeolvasasa_FileNotFoundExceptionDobasa()
    {
        _ = Beolvasas.FajlBeolvasasa("hibas_fajl.txt");
    }
}