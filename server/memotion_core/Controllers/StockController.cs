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
            if(!ModelState.IsValid) return BadRequest(ModelState);
            List<Stock> stocks = await stockRepository.GetAllAsync();
            List<StockDto> stocksDto = stocks.Select(s=>s.ToStockDto()).ToList();

            return Ok(stocksDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id){
            if(!ModelState.IsValid) return BadRequest(ModelState);
            Stock? stocks = await stockRepository.GetByIdAsync(id);

            if(stocks == null) return NotFound();

            return Ok(stocks.ToStockDto());
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto){
            if(!ModelState.IsValid) return BadRequest(ModelState);
            Stock stockModel = stockDto.ToStockFromCreateDTO();
            await stockRepository.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id} ,stockModel.ToStockDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto){
            if(!ModelState.IsValid) return BadRequest(ModelState);
            Stock? stockModel = await stockRepository.UpdateAsync(id, stockDto);
            if(stockModel==null) return NotFound();
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            if(!ModelState.IsValid) return BadRequest(ModelState);
            Stock? stockModel = await stockRepository.DeleteAsync(id);
            if(stockModel == null) return NotFound();

            return NoContent();
        }
    }
}