using System;
using ETlib;
using ETlib.Repository;

namespace RepoTest;


[TestClass]
public class UnitTest1
{

    private EnergyPriceRepository _repo;
    
    /*[TestInitialize]
    public void Initialize()
    {
        _repo = new EnergyPriceRepository();
        _repo.Add(new EnergyPrice()
            { DKK_per_kWh = 2, Category = "high", Id = 1, time_start = new DateTime(2026, 6, 15, 12, 00, 00) });
        _repo.Add(new EnergyPrice()
            { DKK_per_kWh = 5, Category = "medium", Id = 2, time_start = new DateTime(2026, 7, 20, 14, 30, 00) });
        _repo.Add(new EnergyPrice()
            { DKK_per_kWh = 10, Category = "low", Id = 3, time_start = new DateTime(2026, 8, 10, 10, 15, 00) });
    }
    
    
    [TestMethod]
    public void AddTest()
    {
        EnergyPrice e = new EnergyPrice()
            { DKK_per_kWh = 15, Category = "high", Id = 4, time_start = new DateTime(2026, 9, 5, 16, 45, 00) };
        Assert.AreEqual(4, _repo.Add(e).Id);
        Assert.AreEqual(4, _repo.Get().Count());
    }*/
}