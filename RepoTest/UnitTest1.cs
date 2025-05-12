using System;
using ETlib;
using ETlib.Repository;

namespace RepoTest;


[TestClass]
public class UnitTest1
{

    private EnergyPriceRepository _repo;
    
    [TestInitialize]
    public void Initialize()
    {
        _repo = new EnergyPriceRepository();
        _repo.Add(new EnergyPrice()
            { DkkPrice = 2, Category = "high", Id = 1, Time = new DateTime(2026, 6, 15, 12, 00, 00) });
        _repo.Add(new EnergyPrice()
            { DkkPrice = 5, Category = "medium", Id = 2, Time = new DateTime(2026, 7, 20, 14, 30, 00) });
        _repo.Add(new EnergyPrice()
            { DkkPrice = 10, Category = "low", Id = 3, Time = new DateTime(2026, 8, 10, 10, 15, 00) });
    }
    
    
    [TestMethod]
    public void AddTest()
    {
        EnergyPrice e = new EnergyPrice()
            { DkkPrice = 15, Category = "high", Id = 4, Time = new DateTime(2026, 9, 5, 16, 45, 00) };
        Assert.AreEqual(4, _repo.Add(e).Id);
        Assert.AreEqual(4, _repo.Get().Count());
    }
}