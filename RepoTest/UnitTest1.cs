using System;
using ETlib;
using ETlib.Repository;

namespace RepoTest;


[TestClass]
public class UnitTest1
{

    private EnergyPriceRepository _repo;
    
    private PriceIntervalRepository _priceIntervalRepository;
    
    [TestInitialize]
    public void Initialize()
    {
        _repo = new EnergyPriceRepository(_priceIntervalRepository);
        _repo.Add(new EnergyPrice()
            { DKK_per_kWh = 2, Category = "high", Id = 1, time_start = new DateTime(2026, 6, 15, 12, 00, 00) }, 1);
        _repo.Add(new EnergyPrice()
            { DKK_per_kWh = 5, Category = "medium", Id = 2, time_start = new DateTime(2026, 7, 20, 14, 30, 00) }, 1);
        _repo.Add(new EnergyPrice()
            { DKK_per_kWh = 10, Category = "low", Id = 3, time_start = new DateTime(2026, 8, 10, 10, 15, 00) }, 1);
    }
    
    
    [TestMethod]
    public void AddTest()
    {
        EnergyPrice e = new EnergyPrice()
            { DKK_per_kWh = 15, Category = "high", Id = 4, time_start = new DateTimeOffset(2026, 9, 5, 16, 45, 00, TimeSpan.FromHours(2)) };
        _repo.Add(e, 1);
        Assert.AreEqual(4, _repo.GetAllForTest(1).Count());
    }
    
    [TestMethod]
    public void GetByHourTest()
    {
        Assert.AreEqual(1, _repo.GetByHourForTest(14, 1).Id);
        Assert.AreEqual("low", _repo.GetByHourForTest(10, 1).Category);
        Assert.ThrowsException<ArgumentException>(() =>_repo.GetByHourForTest(10, 3));
        Assert.ThrowsException<ArgumentException>(() =>_repo.GetByHourForTest(11, 2));
    }

    [TestMethod]
    public void GetSavedTest()
    {
        Assert.AreEqual(3, _repo.GetAllForTest(1).Count());
        Assert.ThrowsException<ArgumentException>(() => _repo.GetAllForTest(3));
        
        List<EnergyPrice> prices = _repo.GetAllForTest(1).ToList();
        Assert.AreEqual("low", prices[2].Category);
    }

    [TestMethod]
    public void RestartTest()
    {
        _repo.Restart();
        Assert.AreEqual(0, _repo.GetAllForTest(1).Count());
        Assert.AreEqual(0, _repo.GetAllForTest(2).Count());
    }
} 