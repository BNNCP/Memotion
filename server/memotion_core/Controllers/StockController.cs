using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memotion_core.Data;
using memotion_core.Dtos.Stock;
using memotion_core.Interfaces;
using memotion_core.Mappers;
using memotion_core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace memotion_core.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController:ControllerBase
    {
        private readonly IStockRepository stockRepository;
        public StockController( IStockRepository _stockRepository)
        {
            stockRepository = _stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            List<Stock> stocks = await stockRepository.GetAllAsync();
            List<StockDto> stocksDto = stocks.Select(s=>s.ToStockDto()).ToList();

            return Ok(stocksDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id){
            var stocks = await stockRepository.GetByIdAsync(id);

            if(stocks == null) return NotFound();

            return Ok(stocks.ToStockDto());
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto){
            Stock stockModel = stockDto.ToStockFromCreateDTO();
            await stockRepository.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id} ,stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto){
            Stock? stockModel = await stockRepository.UpdateAsync(id, stockDto);
            if(stockModel==null) return NotFound();
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            Stock? stockModel = await stockRepository.DeleteAsync(id);
            if(stockModel == null) return NotFound();

            return NoContent();
        }
    }
}