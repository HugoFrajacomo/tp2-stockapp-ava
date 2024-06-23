using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Include(o => o.Products)
                .Where(order => order.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetRecentOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Products)
                .OrderByDescending(o => o.OrderDate)
                .Take(10) 
                .ToListAsync();
        }
    }
}
