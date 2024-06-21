﻿using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Infra.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetByUserIdAsync(string userId);
    }
}
