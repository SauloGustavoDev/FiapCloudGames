using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository;
    public class GameRepository(ContextDb context) : IGameRepository
    {
    private readonly ContextDb _context = context;

    public async Task AddAsync(Game Game)
        {
            await _context.Game.AddAsync(Game);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var Game = await GetBy(g => g.Id.Equals(id));
            if (Game != null)
            {
                _context.Game.Attach(Game);
                _context.Game.Remove(Game);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Game
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Game?> GetBy(Expression<Func<Game, bool>> condition)
        {
            return await _context.Game
                .AsNoTracking()
                .FirstOrDefaultAsync(condition);
        }

    public async Task UpdateAsync(Game Game)
        {
            _context.Game.Update(Game);
            await _context.SaveChangesAsync();
        }
    }
