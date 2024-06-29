using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memotion_core.Extensions;
using memotion_core.Interfaces;
using memotion_core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace memotion_core.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController: ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IStockRepository stockRepository;
        private readonly IPortfolioRepository portfolioRepository;
        public PortfolioController(UserManager<AppUser> _userManager, IStockRepository _stockRepository, IPortfolioRepository _portfolioRepository)
        {
            userManager = _userManager;
            stockRepository = _stockRepository;
            portfolioRepository = _portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio(){
            var userName = User.GetUsername();
            var AppUser = await userManager.FindByNameAsync(userName);
            var userPortfolio = await portfolioRepository.GetUserPortfolio(AppUser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol){
            var userName = User.GetUsername();
            var AppUser = await userManager.FindByNameAsync(userName);
            var stock = await stockRepository.GetBySymbolAsync(symbol);

            if(stock == null) return BadRequest("Stock not found");

            var userPortfolio = await portfolioRepository.GetUserPortfolio(AppUser);

            if(userPortfolio.Any(i=>i.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");

            var portfolioModel = new Portfolio{
                StockId = stock.Id,
                AppUserId = AppUser.Id
            };

            await portfolioRepository.CreateAsync(portfolioModel);

            if(portfolioModel == null){
                return StatusCode(500,"Create failed");
            }
            else{
                return Created();
            }
        }
    }
}