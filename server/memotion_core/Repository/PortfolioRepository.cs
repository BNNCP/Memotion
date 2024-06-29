using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memotion_core.Data;
using memotion_core.Interfaces;
using memotion_core.Models;
using Microsoft.EntityFrameworkCore;

namespace memotion_core.Repository
{
    public class PortfolioRepository: IPortfolioRepository
    {
        private readonly ApplicationDBContext context;
        public PortfolioRepository(ApplicationDBContext _context)
        {
            context = _context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await context.Portfolios.AddAsync(portfolio);
            await context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await context.Portfolios.Where(i=>i.AppUserId == user.Id).Select(
                stock => new Stock{
                    Id = stock.StockId,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Purchase = stock.Stock.Purchase,
                    LastDiv = stock.Stock.LastDiv,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap
                }
            ).ToListAsync();
        }
    }
}