﻿using MySql.Data.MySqlClient;
using System.Data;
using New_Tech.Models;



namespace New_Tech.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void Cadastrar(Produtos produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO Produtos(Nome,Descricao,Preco,Quantidade) values(@Nome,@Descricao,@Preco,@Quantidade)", conexao);
                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = produto.Nome;
                cmd.Parameters.Add("@Descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                cmd.Parameters.Add("@Preco", MySqlDbType.Decimal).Value = produto.Preco;
                cmd.Parameters.Add("@Quantidade", MySqlDbType.Int32).Value = produto.Quantidade;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public bool Atualizar(Produtos produto)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();

                    MySqlCommand cmd = new MySqlCommand("Update Produtos set Nome=@Nomes, Descricao=@Descricao, Preco=@Preco, Quantidade=@Quantidade" + "where Id= @Id", conexao);

                    cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = produto.Id;
                    cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = produto.Nome;
                    cmd.Parameters.Add("@Descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                    cmd.Parameters.Add("@Preco", MySqlDbType.Decimal).Value = produto.Preco;
                    cmd.Parameters.Add("@Quantidade", MySqlDbType.Int32).Value = produto.Quantidade;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }

            }

            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar cliente: {ex.Message}");
                return false;
            }

        }

        public IEnumerable<Produtos> TodosProdutos()
        {
            List<Produtos> ListaProdutos = new List<Produtos>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * from Produtos", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ListaProdutos.Add(
                        new Produtos
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = ((string)dr["Nome"]),
                            Descricao = ((string)dr["Descricao"]),
                            Preco = Convert.ToDecimal(dr["Preco"]),
                            Quantidade = Convert.ToInt32(dr["Quantidade"]),
                        });
                }
                return ListaProdutos;
            }

        }

        public Produtos ObterProduto(int id)
        {

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Select * from Produtos where Id=@Id ");
                cmd.Parameters.AddWithValue("@Íd", id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                MySqlDataReader dr;

                Produtos produto = new Produtos();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    produto.Id = Convert.ToInt32(dr["Id"]);
                    produto.Nome = ((string)dr["Nome"]);
                    produto.Descricao = ((string)dr["Descricao"]);
                    produto.Preco = Convert.ToDecimal(dr["Preco"]);
                    produto.Quantidade = Convert.ToInt32(dr["Quantidade"]);

                }
                return produto;
            }

        }

        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from Produtos where Id=@Id", conexao);

                cmd.Parameters.AddWithValue("@Id", id);
                int i = cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }

    }
}
