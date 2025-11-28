using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : IUSuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }
        
        async Task<IEnumerable<Usuario>> IUSuarioRepository.GetAllAsync(CancellationToken ct)
        {
            return await _context.Usuarios.ToListAsync(ct);
        }

        async Task<Usuario?> IUSuarioRepository.GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id, ct);
        }

        async Task<Usuario?> IUSuarioRepository.GetByEmailAsync(string email, CancellationToken ct)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email, ct);
        }

        async Task IUSuarioRepository.AddAsync(Usuario usuario, CancellationToken ct)
        {
            await _context.Usuarios.AddAsync(usuario, ct);
        }

        Task IUSuarioRepository.UpdateAsync(Usuario usuario, CancellationToken ct)
        {
            _context.Usuarios.Update(usuario);
            return Task.CompletedTask;
        }

        Task IUSuarioRepository.RemoveAsync(Usuario usuario, CancellationToken ct)
        {
            _context.Usuarios.Remove(usuario);
            return Task.CompletedTask;
        }
        
        async Task<bool> IUSuarioRepository.EmailExistsAsync(string email, CancellationToken ct)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email, ct);
        }

        async Task<int> IUSuarioRepository.SaveChangesAsync(CancellationToken ct)
        {
            return await _context.SaveChangesAsync(ct);
        }
    }
}