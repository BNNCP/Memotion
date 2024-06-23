using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memotion_core.Data;
using memotion_core.Dtos.Comment;
using memotion_core.Interfaces;
using memotion_core.Mappers;
using memotion_core.Models;
using Microsoft.AspNetCore.Mvc;

namespace memotion_core.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly ICommentRepository commentRepository;
        private readonly IStockRepository stockRepository;

        public CommentController(ICommentRepository _commentRepository, IStockRepository _stockRepository)
        {
            commentRepository = _commentRepository;
            stockRepository = _stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            List<Comment> comments = await commentRepository.GetAllAsync();
            List<CommentDto> commentDtos = comments.Select(i=>i.ToCommentDto()).ToList();
            return Ok(commentDtos);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            Comment? comment = await commentRepository.GetById(id);
            if(comment == null) return NotFound("Comment not found");
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto commentDto){
            if(!await stockRepository.StockExist(stockId)) return BadRequest("Stock does not exist.");

            Comment comment = commentDto.ToCommentFromCreateDto(stockId);
            await commentRepository.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById),new {id = comment.Id}, comment.ToCommentDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto){
            Comment? comment = await commentRepository.UpdateAsync(id, commentDto.ToCommentFromUpdateDto());
            if(comment == null) return NotFound("Comment not found");

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id){
            Comment? comment = await commentRepository.DeleteAsync(id);
            if(comment == null) return NotFound("Comment not found");

            return NoContent();
        }
    }
}