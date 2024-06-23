using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using memotion_core.Data;
using memotion_core.Dtos.Comment;
using memotion_core.Interfaces;
using memotion_core.Models;
using Microsoft.EntityFrameworkCore;

namespace memotion_core.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext context;
        public CommentRepository(ApplicationDBContext _context)
        {
            context = _context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetById(int id)
        {
            return await context.Comments.FindAsync(id);
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await context.Comments.AddAsync(commentModel);
            await context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            Comment? comment = await context.Comments.FirstOrDefaultAsync(i=>i.Id == id);
            if(comment == null) return null;
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            Comment? comment = await context.Comments.FindAsync(id);
            if(comment == null) return null;

            comment.Title = commentModel.Title;
            comment.Content = commentModel.Content;
            await context.SaveChangesAsync();

            return comment;
        }
    }
}