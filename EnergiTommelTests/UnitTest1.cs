using ETlib;

namespace EnergiTommelTests;

[TestClass]
public class UnitTest1
{
    private Elpris elpris;
    
    [TestMethod]

    [TestInitialize]    
    
    public void TestInitialize1()
    {
        elpris = new Elpris(2.0, new DateTime(2025, 5, 10, 12, 00, 00));
        // Code to run before each test
    } 
    
    [TestMethod]
    public void ElprisTest1()
    {
        Assert.AreEqual(2.0, elpris.Pris);
        Assert.IsNotNull(elpris.Tid);
        elpris.Pris = 3.5;
        Assert.AreEqual(3.5, elpris.Pris);
        elpris.Tid = new DateTime(2025, 5, 10, 13, 00, 00);
        Assert.AreEqual(new DateTime(2025, 5, 10, 13, 00, 00), elpris.Tid);
    }
}