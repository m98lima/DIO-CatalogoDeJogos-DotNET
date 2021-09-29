using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Services;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("b48a0e77-9e75-4618-8dc5-1a7944d4a967"), new Jogo {Id = Guid.Parse("b48a0e77-9e75-4618-8dc5-1a7944d4a967"), Nome = "Darksiders", Produtora = "THQ", Preco = 35}},
            {Guid.Parse("6f3765ba-8030-4c94-8c60-4f162249b9f6"), new Jogo {Id = Guid.Parse("6f3765ba-8030-4c94-8c60-4f162249b9f6"), Nome = "Hollow Knight", Produtora = "Team Cherry", Preco = 30}},
            {Guid.Parse("985f206e-b081-407c-beae-00b3327b419a"), new Jogo {Id = Guid.Parse("985f206e-b081-407c-beae-00b3327b419a"), Nome = "The Witcher 3", Produtora = "CD Projekt Red", Preco = 80}},
            {Guid.Parse("552e40e1-eb0d-490f-b8e1-9ef4bb94f3df"), new Jogo {Id = Guid.Parse("552e40e1-eb0d-490f-b8e1-9ef4bb94f3df"), Nome = "Hades", Produtora = "Supergiant Games", Preco = 50}},
            {Guid.Parse("ee34d0d1-c524-43f2-ad96-3aff04a55807"), new Jogo {Id = Guid.Parse("ee34d0d1-c524-43f2-ad96-3aff04a55807"), Nome = "Portal 2", Produtora = "Valve", Preco = 20}}
        };

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade)
                .Take(quantidade).ToList());
        }

        public async Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return null;
            
            return jogos[id];
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values
                .Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }
        
        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}