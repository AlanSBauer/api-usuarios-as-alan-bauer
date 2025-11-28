using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUSuarioRepository _repository;

        public UsuarioService(IUSuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UsuarioReadDto>> ListarAsync(CancellationToken ct)
        {
            var usuarios = await _repository.GetAllAsync(ct);

            return usuarios.Select(u => new UsuarioReadDto(
                u.Id,
                u.Nome,
                u.Email,
                u.DataNascimento,
                u.Telefone,
                u.Ativo,
                u.DataCriacao
            ));
        }

        public async Task<UsuarioReadDto?> ObterAsync(int id, CancellationToken ct)
        {
            var usuario = await _repository.GetByIdAsync(id, ct);

            if(usuario is null) 
                return null;

            return new UsuarioReadDto(
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.DataNascimento,
                usuario.Telefone,
                usuario.Ativo,
                usuario.DataCriacao
            );
        }

        public async Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct)
        {
            return await _repository.EmailExistsAsync(email, ct);
        }

        public async Task<UsuarioReadDto> CriarAsync(UsuarioCreateDto dto, CancellationToken ct)
        {
            if(await _repository.EmailExistsAsync(dto.Email, ct))
                throw new Exception("Email já cadastrado.");
            
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha,
                DataNascimento = dto.DataNascimento,
                Telefone = dto.Telefone,
                Ativo = true,
                DataCriacao = DateTime.Now
            };

            await _repository.AddAsync(usuario, ct);
            await _repository.SaveChangesAsync(ct);

            return new UsuarioReadDto(
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.DataNascimento,
                usuario.Telefone,
                usuario.Ativo,
                usuario.DataCriacao
            );
        }

        public async Task<UsuarioReadDto> AtualizarAsync(int id, UsuarioUpdateDto dto, CancellationToken ct)
        {
            var usuario = await _repository.GetByIdAsync(id, ct);

            if(usuario is null)
                throw new Exception("Usuário não encontrado.");

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.DataNascimento = dto.DataNascimento;
            usuario.Telefone = dto.Telefone;
            usuario.Ativo = dto.Ativo;

            await _repository.UpdateAsync(usuario, ct);
            await _repository.SaveChangesAsync(ct);

            return new UsuarioReadDto(
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.DataNascimento,
                usuario.Telefone,
                usuario.Ativo,
                usuario.DataCriacao
            );
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken ct)
        {
            var usuario = await _repository.GetByIdAsync(id, ct);

            if(usuario is null) 
                return false;

            await _repository.RemoveAsync(usuario, ct);
            await _repository.SaveChangesAsync(ct);

            return true;
        }
    }
}