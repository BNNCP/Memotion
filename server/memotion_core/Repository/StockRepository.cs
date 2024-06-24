using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memotion_core.Data;
using memotion_core.Dtos.Stock;
using memotion_core.Helpers;
using memotion_core.Interfaces;
using memotion_core.Models;
using Microsoft.EntityFrameworkCore;

namespace memotion_core.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext context;
        
        public StockRepository(ApplicationDBContext _context)
        {
            context = _context;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            IQueryable<Stock> stocks =  context.Stocks.Include(c=>c.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(query.CompanyName)) stocks = stocks.Where(i=>i.CompanyName.Contains(query.CompanyName));
            if(!string.IsNullOrWhiteSpace(query.Symbol)) stocks = stocks.Where(i=>i.Symbol.Contains(query.Symbol));

            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(i=>i.Id == id);
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await context.Stocks.AddAsync(stockModel);
            await context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            Stock? stockModel = await context.Stocks.FirstOrDefaultAsync(i=>i.Id == id);
            if(stockModel==null) return null;
            context.Stocks.Remove(stockModel);
            await context.SaveChangesAsync();
            return stockModel;
        }
        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            Stock? stockModel = await context.Stocks.FirstOrDefaultAsync(i=>i.Id == id);
            if(stockModel==null) return null;

            stockModel.Symbol = stockDto.Symbol;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.Industry = stockDto.Industry;
            stockModel.MarketCap = stockDto.MarketCap;
            await context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<bool> StockExist(int id){
            return await context.Stocks.AnyAsync(i=> i.Id == id);
        }
    }
}