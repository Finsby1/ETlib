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
        
        private static CategoryRepository _categoryRepository;


        [ClassInitialize]
        public static void InitOnce(TestContext context) {

            var options = new DbContextOptionsBuilder<finsby_dk_db_viberContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _categoryRepository = new CategoryRepository(new finsby_dk_db_viberContext(options));
        }
        [TestMethod()]
        public void UpdateTest()
        {
            _categoryRepository.Add(new Category() { Id = 1, High = 0.6 , Low = 0.3});
            Assert.AreEqual(1, _categoryRepository.GetAll().Count());
        }
    }
}