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

            var interval = _context.PriceInterval.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (interval == null) {
                throw new ArgumentException($"PriceInterval with ID {id} not found.");
            }
            return interval;
        }

        public PriceInterval? Update(PriceInterval interval) {

            _context.PriceInterval.Update(interval);
            _context.SaveChanges();
            return GetById(2); 
        }
    }
}
