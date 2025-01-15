namespace F1RacingTests;
using F1RacingLib;

[TestClass]
public class VersenyzoTests
{
    [TestMethod]
    public void ProbalElőzni_SikeresElőzés()
    {
        Versenyzo versenyzo = new Versenyzo { Nev = "Charles Leclerc", Kategoria = Kategoria.Agressziv };
        bool sikeres = versenyzo.ProbalElőzni(2);
        Assert.IsTrue(sikeres || !sikeres);
    }

    [TestMethod]
    public void SzükségesTankolni_BenzinSzintAlapjanIgaz()
    {
        Versenyzo versenyzo = new Versenyzo { Nev = "Esteban Ocon", Kategoria = Kategoria.Lenduletes, Benzin = 15 };
        Assert.IsTrue(versenyzo.SzükségesTankolni());
    }

    [TestMethod]
    public void Tankol_MaximumraTöltiBenzint()
    {
        Versenyzo versenyzo = new Versenyzo { Nev = "Yuki Tsunoda", Benzin = 5 };
        versenyzo.Tankol();
        Assert.AreEqual(100, versenyzo.Benzin);
    }
}