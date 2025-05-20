using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETlib.Models;
using Microsoft.EntityFrameworkCore;

namespace ETlib.Repository {
    public class PriceIntervalRepository {
        
        private readonly finsby_dk_db_viberContext _context;
        public PriceIntervalRepository(finsby_dk_db_viberContext context) {
            _context = context;
        }

        public PriceInterval Add(PriceInterval interval) {
            
            _context.PriceInterval.Add(interval);
            _context.SaveChanges();
            return interval;
        }

        public PriceInterval? GetById(int id) {
            return _context.PriceInterval.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public PriceInterval? Update(PriceInterval interval) {
            _context.PriceInterval.Update(interval);
            _context.SaveChanges();
            return GetById(2); 
        }
        public PriceInterval Delete(int id) {
            var interval = GetById(id);

            try
            {
                if (interval != null)
                {
                    _context.PriceInterval.Remove(interval);
                    _context.SaveChanges();
                }

            }
            catch (Exception e)
            {
                throw new ArgumentNullException("Cannot delete PriceInterval because it was not found", e);
            }
            return interval;
        }
        public IEnumerable<PriceInterval> GetAll() {
            return _context.PriceInterval.ToList();
        }
    }
}
