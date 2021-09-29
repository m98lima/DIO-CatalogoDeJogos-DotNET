using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Services;
using Microsoft.Extensions.Configuration;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoSqlServerRepository : IJogoRepository
    {
        private readonly SqlConnection _sqlCon;

        public JogoSqlServerRepository(IConfiguration configuration)
        {
            _sqlCon = new SqlConnection(configuration.GetConnectionString("Default"));
        }
        
        public async Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            var jogos = new List<Jogo>();
            var query = $"selct * from jogos order by id offset {((pagina - 1) * quantidade)} " +
                        $"rows fetch next {quantidade} rows only";

            await  _sqlCon.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, _sqlCon);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = (Guid) sqlDataReader["Id"],
                    Nome = (string) sqlDataReader["Nome"],
                    Produtora = (string) sqlDataReader["Produtora"],
                    Preco = (double) sqlDataReader["Preco"]
                });
            }

            await _sqlCon.CloseAsync();
            return jogos;
        }

        public async Task<Jogo> Obter(Guid id)
        {
            Jogo jogo = null;
            var query = $"select * from jogos where Id = '{id}'";

            await _sqlCon.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, _sqlCon);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogo = new Jogo
                {
                    Id = (Guid) sqlDataReader["Id"],
                    Nome = (string) sqlDataReader["Nome"],
                    Produtora = (string) sqlDataReader["Produtora"],
                    Preco = (double) sqlDataReader["Preco"]
                };
            }

            await _sqlCon.CloseAsync();
            return jogo;
        }

        public async Task<List<Jogo>> Obter(string nome, string produtora)
        {
            var jogos = new List<Jogo>();
            var query = $"select * from jogos where Nome = '{nome}' and Produtora = '{produtora}'";

            await  _sqlCon.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(query, _sqlCon);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = (Guid) sqlDataReader["Id"],
                    Nome = (string) sqlDataReader["Nome"],
                    Produtora = (string) sqlDataReader["Produtora"],
                    Preco = (double) sqlDataReader["Preco"]
                });
            }

            await _sqlCon.CloseAsync();
            return jogos;
        }

        public async Task Inserir(Jogo jogo)
        {
            var comando = $"insert jogos (Id, Nome, Produtora, Preco) values ('{jogo.Id}', " +
                          $"'{jogo.Nome}', '{jogo.Produtora}', '{jogo.Preco}')";
            
            await _sqlCon.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlCon);
            sqlCommand.ExecuteNonQuery();
            await _sqlCon.CloseAsync();
        }

        public async Task Atualizar(Jogo jogo)
        {
            var comando = $"update jogos set Nome = '{jogo.Nome}', Produtora = " +
                          $"'{jogo.Produtora}', Preco = '{jogo.Preco.ToString().Replace(",", ".")} " +
                          $"where Id = '{jogo.Id}'')";
            
            await _sqlCon.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlCon);
            sqlCommand.ExecuteNonQuery();
            await _sqlCon.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from jogos where Id = '{id}'')";
            
            await _sqlCon.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlCon);
            sqlCommand.ExecuteNonQuery();
            await _sqlCon.CloseAsync();
        }
        
        public void Dispose()
        {
            _sqlCon?.Close();
            _sqlCon?.Dispose();
        }
    }
}