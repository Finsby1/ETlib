using ETlib;

namespace EnergiTommelTests;

[TestClass]
public class UnitTest1
{
    private EnergyPrice _energyPrice;
    

    [TestInitialize]    
    public void TestInitialize()
    {
        _energyPrice = new EnergyPrice(2, new DateTime(2025, 5, 12, 12, 00, 00), "high");
    } 

    [TestMethod]
    public void PriceTest()
    {
        Assert.AreEqual(2, _energyPrice.DkkPrice);
        
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _energyPrice.DkkPrice = 0);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _energyPrice.DkkPrice = 20);
    }
    
    [TestMethod]
    public void DateTest()
    {
        Assert.AreEqual(new DateTime(2025, 5, 12, 12, 00, 00), _energyPrice.Time);
        
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _energyPrice.Time = new DateTime(2025, 1, 1, 0, 00, 00) );
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _energyPrice.Time = new DateTime(2125, 1, 1, 00, 00, 00));
    }
    
    [TestMethod]
    public void CategoryTest()
    {
        
        Assert.AreEqual("high", _energyPrice.Category);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _energyPrice.Category = "ab");
        Assert.ThrowsException<ArgumentNullException>(() => _energyPrice.Category = null);
    }
}