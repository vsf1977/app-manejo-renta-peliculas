
using System.Data.SqlClient;

namespace Backend.Models
{  
    public class Usuario
    {
        SqlCommand comando;
        Database database = new Database();
        public string id { get; set; }
        public string nombre { get; set; }
        public string rol { get; set; }
        public string password { get; set; }
        public void insertar()
        {            
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "insert into usuario values (@id, @nombre, @rol, @password)";

            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.VarChar, 50);
            SqlParameter nombreParam = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar, 60);
            SqlParameter rolParam = new SqlParameter("@rol", System.Data.SqlDbType.VarChar, 15);
            SqlParameter passwordParam = new SqlParameter("@password", System.Data.SqlDbType.VarChar, 12);

            idParam.Value = id;
            nombreParam.Value = nombre;
            rolParam.Value = rol;
            passwordParam.Value = password;

            comando.Parameters.Add(idParam);
            comando.Parameters.Add(nombreParam);
            comando.Parameters.Add(rolParam);
            comando.Parameters.Add(passwordParam);

            comando.Prepare();            
            comando.ExecuteNonQuery();            
            database.Desconectar();
        }

        public void actualizar()
        {
            comando = new SqlCommand(null, database.Conectar());            
            comando.CommandText = "update usuario set nombre =  @nombre, rol = @rol, password =  @password where id=@id";

            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.VarChar, 50);
            SqlParameter nombreParam = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar, 60);
            SqlParameter rolParam = new SqlParameter("@rol", System.Data.SqlDbType.VarChar, 15);
            SqlParameter passwordParam = new SqlParameter("@password", System.Data.SqlDbType.VarChar, 12);

            idParam.Value = id;
            nombreParam.Value = nombre;
            rolParam.Value = rol;
            passwordParam.Value = password;

            comando.Parameters.Add(idParam);
            comando.Parameters.Add(nombreParam);
            comando.Parameters.Add(rolParam);
            comando.Parameters.Add(passwordParam);

            comando.Prepare();
            comando.ExecuteNonQuery();
            database.Desconectar();
        }

        public void borrar(string id)
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "delete from usuario where id=@id";

            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.VarChar, 20);

            idParam.Value = id;
            comando.Parameters.Add(idParam);
            comando.Prepare();
            comando.ExecuteNonQuery();
            database.Desconectar();
        }
    }
}