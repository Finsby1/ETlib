using ETlib;
using ETlib.Models;
using ETlib.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;
using System;

namespace RepoTest;


[TestClass]
public class UnitTest1
{
    private DbContextOptions<finsby_dk_db_viberContext> CreateInMemoryDatabaseOptions()
    {
        // Create a unique database name to avoid conflicts between tests
        return new DbContextOptionsBuilder<finsby_dk_db_viberContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    [TestMethod()]
    public void TestInMemmory()
    {
        // Arrange
        var options = CreateInMemoryDatabaseOptions();

        // Act
        using (var context = new finsby_dk_db_viberContext(options))
        {

            // Assert
            Assert.IsNotNull(context);
        }
    }
    EnergyPriceRepository _repo;



    [TestInitialize]
    public void Initialize()
    {
        var options = CreateInMemoryDatabaseOptions();
        var context = new finsby_dk_db_viberContext(options);
        var priceIntervalRepo = new PriceIntervalRepository(context);
        var testData = new PriceInterval
        {
            Id = 2, 
            High = 0.8,
            Low = 0.2,
        };
        priceIntervalRepo.Add(testData);

        _repo = new EnergyPriceRepository(priceIntervalRepo);
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
        //arrange
        var testData = new PriceInterval
        {
            Id = 2,
            High = 0.8,
            Low = 0.2,
        };
        EnergyPrice e = new EnergyPrice()
            { DKK_per_kWh = 0.5, Category = "high", Id = 4, time_start = new DateTimeOffset(2026, 9, 5, 16, 45, 00, TimeSpan.FromHours(2)) };
        _repo.Add(e, 1);
        _repo.Add(e,2);
        Assert.AreEqual(4, _repo.GetSavedPrices(1).Count());
        Assert.AreEqual(1, _repo.GetSavedPrices(2).Count());
        //ser om den ændre category til det rigtige (medium)
        Assert.AreEqual("medium", _repo.GetSavedPrices(1).Last().Category);

        Assert.ThrowsException<ArgumentException>(() => _repo.Add(e, 99));
    }
    
    [TestMethod]
    public void GetByHourTest()
    {
        Assert.AreEqual(1, _repo.GetByHour(14, 1).Id);
        Assert.AreEqual("high", _repo.GetByHour(10, 1).Category);
        //forkert zone
        Assert.ThrowsException<ArgumentException>(() =>_repo.GetByHour(10, 3));
        //forkert time i zone 2
        Assert.ThrowsException<ArgumentException>(() =>_repo.GetByHour(11, 2));
        //forkert time i zone 1
        Assert.ThrowsException<ArgumentException>(() => _repo.GetByHour(0, 1));
        //forkert zone og time
        Assert.ThrowsException<ArgumentException>(() => _repo.GetByHour(0, 99));
    }

    [TestMethod]
    public void GetSavedPricesTest()
    {
        Assert.AreEqual(3, _repo.GetSavedPrices(1).Count());
        Assert.ThrowsException<ArgumentException>(() => _repo.GetSavedPrices(3));
        
        List<EnergyPrice> prices = _repo.GetSavedPrices(1).ToList();
        Assert.AreEqual("high", prices[2].Category);
        Assert.AreEqual(2, prices[2].Id);
    }

    [TestMethod]
    public void RestartTest()
    {
        _repo.Restart();
        Assert.AreEqual(0, _repo.GetSavedPrices(1).Count());
        Assert.AreEqual(0, _repo.GetSavedPrices(2).Count());
    }

    [TestMethod]
    public void SetCatTest()
    {
        //tester om high er sat korrekt
        var energyPriceHigh = new EnergyPrice { DKK_per_kWh = 1.0 };

        var resultHigh = _repo.SetCategory(energyPriceHigh);

        Assert.AreEqual("high", resultHigh.Category);
        //tester om medium er sat korrekt

        var energyPriceMid = new EnergyPrice { DKK_per_kWh = 0.1 };

        var resultmedium = _repo.SetCategory(energyPriceMid);

        Assert.AreEqual("low", resultmedium.Category);

        //tester om low er sættes korrekt

        var energyPriceLow = new EnergyPrice { DKK_per_kWh = 0.5 };

        var resultLow = _repo.SetCategory(energyPriceLow);

        Assert.AreEqual("medium", resultLow.Category);


        //tester om den sætter exception hvis den ikke kan finde intervallet


    }

    [TestMethod]
    public void SetCatNoId()
    {
        //tester en hvor den kun har en med id 1 og ingen med 2
        var options = CreateInMemoryDatabaseOptions();
        var context = new finsby_dk_db_viberContext(options);
        var priceIntervalRepo = new PriceIntervalRepository(context);

        priceIntervalRepo.Add(new PriceInterval
        {
            Id = 1,
            High = 1.5,
            Low = 0.5
        });

        var No2Repo = new EnergyPriceRepository(priceIntervalRepo);
        var energyPrice = new EnergyPrice { DKK_per_kWh = 1.0 };

        var result = No2Repo.SetCategory(energyPrice);

        Assert.AreEqual("medium", result.Category);

    }
} 