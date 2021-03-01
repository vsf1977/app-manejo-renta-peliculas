using System.Data.SqlClient;
namespace Backend.Models
{
    public class Pelicula
    {
        SqlCommand comando;
        Database database = new Database();
        public string id { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string director { get; set; }
        public string costo_alquiler { get; set; }
        public string cantidad { get; set; }
        public string actores { get; set; }

        public void insertar()
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "insert into pelicula values (@id, @titulo, @descripcion, @director, @costo_alquiler, @cantidad, @actores)";

            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.VarChar, 20);
            SqlParameter tituloParam = new SqlParameter("@titulo", System.Data.SqlDbType.VarChar, 50);
            SqlParameter descParam = new SqlParameter("@descripcion", System.Data.SqlDbType.VarChar, 100);
            SqlParameter directorParam = new SqlParameter("@director", System.Data.SqlDbType.VarChar, 50);
            SqlParameter costo_alquilerParam = new SqlParameter("@costo_alquiler", System.Data.SqlDbType.VarChar, 10);
            SqlParameter cantidadParam = new SqlParameter("@cantidad", System.Data.SqlDbType.VarChar, 5);
            SqlParameter actoresParam = new SqlParameter("@actores", System.Data.SqlDbType.VarChar, 100);

            idParam.Value = id;
            tituloParam.Value = titulo;
            descParam.Value = descripcion;
            directorParam.Value = director;
            costo_alquilerParam.Value = costo_alquiler;
            cantidadParam.Value = cantidad;
            actoresParam.Value = actores;

            comando.Parameters.Add(idParam);
            comando.Parameters.Add(tituloParam);
            comando.Parameters.Add(descParam);
            comando.Parameters.Add(directorParam);
            comando.Parameters.Add(costo_alquilerParam);
            comando.Parameters.Add(cantidadParam);
            comando.Parameters.Add(actoresParam);

            comando.Prepare();
            comando.ExecuteNonQuery();
            database.Desconectar();
        }

        public void actualizar()
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "update pelicula set titulo =  @titulo, descripcion = @descripcion, director = @director, costo_alquiler= @costo_alquiler, cantidad = @cantidad, actores=@actores where id=@id";

            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.VarChar, 20);
            SqlParameter tituloParam = new SqlParameter("@titulo", System.Data.SqlDbType.VarChar, 50);
            SqlParameter descParam = new SqlParameter("@descripcion", System.Data.SqlDbType.VarChar, 100);
            SqlParameter directorParam = new SqlParameter("@director", System.Data.SqlDbType.VarChar, 50);
            SqlParameter costo_alquilerParam = new SqlParameter("@costo_alquiler", System.Data.SqlDbType.VarChar, 10);
            SqlParameter cantidadParam = new SqlParameter("@cantidad", System.Data.SqlDbType.VarChar, 5);
            SqlParameter actoresParam = new SqlParameter("@actores", System.Data.SqlDbType.VarChar, 100);

            idParam.Value = id;
            tituloParam.Value = titulo;
            descParam.Value = descripcion;
            directorParam.Value = director;
            costo_alquilerParam.Value = costo_alquiler;
            cantidadParam.Value = cantidad;
            actoresParam.Value = actores;

            comando.Parameters.Add(idParam);
            comando.Parameters.Add(tituloParam);
            comando.Parameters.Add(descParam);
            comando.Parameters.Add(directorParam);
            comando.Parameters.Add(costo_alquilerParam);
            comando.Parameters.Add(cantidadParam);
            comando.Parameters.Add(actoresParam);

            comando.Prepare();
            comando.ExecuteNonQuery();
            database.Desconectar();            
        }

        public void borrar(string id)
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "delete from pelicula where id=@id";

            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.VarChar, 20);

            idParam.Value = id;
            comando.Parameters.Add(idParam);
            comando.Prepare();
            comando.ExecuteNonQuery();
            database.Desconectar();
        }

    }
}