using MySql.Data.MySqlClient;
using System.Data;
using New_Tech.Models;


namespace New_Tech.Repositorio
{
    public class LoginRepositorio(IConfiguration configuration)
    {
      
        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");

        
        public Usuarios ObterUsuario(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new("SELECT * from Usuarios where Email = @Email", conexao);
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
