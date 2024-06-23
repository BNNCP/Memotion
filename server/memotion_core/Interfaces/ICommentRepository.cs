using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memotion_core.Dtos.Comment;
using memotion_core.Models;

namespace memotion_core.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetById(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id, Comment commentModel);
        Task<Comment?> DeleteAsync(int id);
    }
}