using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memotion_core.Dtos.Comment;
using memotion_core.Models;

namespace memotion_core.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel){
            return new CommentDto{
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto commentDto, int stockId){
            return new Comment{
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdateDto(this UpdateCommentRequestDto updateDto){
            return new Comment{
                Title = updateDto.Title,
                Content = updateDto.Content,
            };
        }
    }
}