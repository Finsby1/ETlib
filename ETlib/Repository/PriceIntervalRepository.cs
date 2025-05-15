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

        private int nextId = 1;

        public PriceInterval Add(PriceInterval interval) {
            interval.Id = nextId++;
            _context.PriceInterval.Add(interval);
            _context.SaveChanges();
            return interval;
        }

        public PriceInterval GetById(int id) {
            return _context.PriceInterval.Find(id);
        }

        public void Update(PriceInterval interval) {
            _context.PriceInterval.Update(interval);
            _context.SaveChanges();
        }
        public void Delete(int id) {
            var interval = GetById(id);
            if (interval != null) {
                _context.PriceInterval.Remove(interval);
                _context.SaveChanges();
            }
        }
        public IEnumerable<PriceInterval> GetAll() {
            return _context.PriceInterval.ToList();
        }
    }
}
