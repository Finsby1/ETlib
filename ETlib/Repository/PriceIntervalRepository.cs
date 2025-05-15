using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETlib.Models;

namespace ETlib.Repository {
    public class PriceIntervalRepository {
        private readonly finsby_dk_db_viberContext _context;
        public PriceIntervalRepository(finsby_dk_db_viberContext context) {
            _context = context;
        }

        public void Add(PriceInterval category) {
            _context.PriceInterval.Add(category);
            _context.SaveChanges();
        }

        public PriceInterval GetById(int id) {
            return _context.PriceInterval.Find(id);
        }

        public void Update(PriceInterval category) {
            _context.PriceInterval.Update(category);
            _context.SaveChanges();
        }
        public void Delete(int id) {
            var category = GetById(id);
            if (category != null) {
                _context.PriceInterval.Remove(category);
                _context.SaveChanges();
            }
        }
        public IEnumerable<PriceInterval> GetAll() {
            return _context.PriceInterval.ToList();
        }
    }
}
