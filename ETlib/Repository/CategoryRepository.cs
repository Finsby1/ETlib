using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETlib.Models;

namespace ETlib.Repository {
    public class CategoryRepository {
        private readonly finsby_dk_db_viberContext _context;
        public CategoryRepository(finsby_dk_db_viberContext context) {
            _context = context;
        }

        public void Add(Category category) {
            _context.Category.Add(category);
            _context.SaveChanges();
        }

        public Category GetById(int id) {
            return _context.Category.Find(id);
        }

        public void Update(Category category) {
            _context.Category.Update(category);
            _context.SaveChanges();
        }
        public void Delete(int id) {
            var category = GetById(id);
            if (category != null) {
                _context.Category.Remove(category);
                _context.SaveChanges();
            }
        }
        public IEnumerable<Category> GetAll() {
            return _context.Category.ToList();
        }
    }
}
