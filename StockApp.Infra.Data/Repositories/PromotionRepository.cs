using StockApp.Infra.Data.Context;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Infra.Data.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        ApplicationDbContext _context;
        public PromotionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Promotion promotion)
        {
            _context.Promotions.Add(promotion);
            _context.SaveChanges();
        }

        public Promotion Get(int id)
        {
            return _context.Promotions.Find(id);
        }

        public IEnumerable<Promotion> GetAll()
        {
            return _context.Promotions.ToList();
        }

        public void Delete(int id) {
            var promotion = _context.Promotions.Find(id);
            if (promotion != null) {
                _context.Promotions.Remove(promotion);
                _context.SaveChanges();
            }
        }

        public void Update(Promotion promotion) {
            _context.Promotions.Update(promotion);
            _context.SaveChanges();
        }
    }
}
