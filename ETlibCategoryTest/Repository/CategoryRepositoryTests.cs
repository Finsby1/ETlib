using Microsoft.VisualStudio.TestTools.UnitTesting;
using ETlib.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using ETlib.Models;

namespace ETlib.Repository.Tests {

    [TestClass()]
    public class CategoryRepositoryTests {

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
        [TestMethod()]
        public void AddDataTest()
        {
            //Arrange
            var options = CreateInMemoryDatabaseOptions();
            var testData = new PriceInterval
            {
                Id = 2, // This should be reset to 0 by the repository
                High = 0.8,
                Low = 0.2,
            };
            // Act
            using (var context = new finsby_dk_db_viberContext(options))
            {
                var repository = new PriceIntervalRepository(context);
                var result = repository.Add(testData);
                
                Assert.AreEqual(testData.Id, result.Id); 
                Assert.AreEqual(testData.High, result.High);

            }
            // Assert - se om det er ens i databasen
            using (var context = new finsby_dk_db_viberContext(options))
            {
                Assert.AreEqual(1, context.PriceInterval.Count());
                var savedData = context.PriceInterval.First();
                Assert.AreEqual(testData.Id, savedData.Id); 
                Assert.AreEqual(testData.High, savedData.High);
                Assert.AreEqual(testData.Low, savedData.Low);
            }
        }
        [TestMethod()]
        public void updateDataTest()
        {
            var options = CreateInMemoryDatabaseOptions();
            var testData = new PriceInterval
            {
                Id = 2,
                High = 0.8,
                Low = 0.2,
            };

            // Act
            using (var context = new finsby_dk_db_viberContext(options))
            {
                //tilføjer data for at kunne opdatere det
                var repository = new PriceIntervalRepository(context);
                repository.Add(testData);


            }
            using (var context = new finsby_dk_db_viberContext(options))
            {
                var updatedData = new PriceInterval
                {
                    Id = 2,
                    High = 0.5,
                    Low = 0.0,
                };
                var repository = new PriceIntervalRepository(context);
                var result = repository.Update(updatedData);
                Assert.AreEqual(updatedData.Id, result.Id); 
                Assert.AreEqual(updatedData.High, result.High);
            }
            // Assert - se om det er ens i databasen
            using (var context = new finsby_dk_db_viberContext(options))
            {
                Assert.AreEqual(1, context.PriceInterval.Count());
                var savedData = context.PriceInterval.First();
                Assert.AreEqual(2, savedData.Id); 
                Assert.AreEqual(0.5, savedData.High);
                Assert.AreEqual(0.0, savedData.Low);

            }
        }
        [TestMethod()]
        public void getbyId()
        {
            var options = CreateInMemoryDatabaseOptions();
            var testData = new PriceInterval
            {
                Id = 2,
                High = 0.8,
                Low = 0.2,
            };
            // Act
            using (var context = new finsby_dk_db_viberContext(options))
            {
                //tilføjer data for at kunne opdatere det
                var repository = new PriceIntervalRepository(context);
                repository.Add(testData);
            }
            using (var context = new finsby_dk_db_viberContext(options))
            {
                var repository = new PriceIntervalRepository(context);
                var result = repository.GetById(2);
                Assert.AreEqual(testData.Id, result.Id); 
                Assert.AreEqual(testData.High, result.High);
                Assert.AreEqual(testData.Low, result.Low);

                Assert.ThrowsException<ArgumentException>(() => repository.GetById(3), "PriceInterval with ID 3 not found.");
            }
        }
    }
}