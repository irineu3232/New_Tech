using MySql.Data.MySqlClient;
using System.Data;
using New_Tech.Models;
using System.Configuration;

namespace New_Tech.Repositorio
{
    public class LoginRepositorio(IConfiguration configuration)
    {
        //Cria o construtor... ele lê os campos inseridos no MySQL.
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        // Login, ele vai comprar com o MySQL e ver se são verdadeiros ou não.
        public Usuarios ObterUsuario(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new("SELECT * FROM Usuarios WHERE Email = @Email", conexao);
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = email;


                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuarios usuario = null;

                    if (dr.Read())
                    {
                        usuario = new Usuarios
                        {
                            Id = Convert.ToInt32(dr["Id"]),

                            Nome = dr["Nome"].ToString(),

                            Email = dr["Email"].ToString(),

                            Senha = dr["Senha"].ToString()

                        };

                    }
                    return usuario;
                }


            }


        }


    }
}
