using System;
using System.Data.SqlClient;

namespace Backend.Models
{
    public class Reserva
    {
        SqlCommand comando;
        Database database = new Database();
        public string id_usuario { get; set; }
        public string id_pelicula { get; set; }
        public string id_reserva { get; set; }
        public DateTime fecha_reserva { get; set; }
        public string entregado { get; set; }
        public void insertar()
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "insert into reserva values (@id_reserva, @id_pelicula, @id_usuario, @fecha_reserva, @entregado)";

            SqlParameter id_reservaParam = new SqlParameter("@id_reserva", System.Data.SqlDbType.VarChar, 20);
            SqlParameter id_peliculaParam = new SqlParameter("@id_pelicula", System.Data.SqlDbType.VarChar, 20);
            SqlParameter id_usuarioParam = new SqlParameter("@id_usuario", System.Data.SqlDbType.VarChar, 20);
            SqlParameter fecha_reservaParam = new SqlParameter("@fecha_reserva", System.Data.SqlDbType.Date);
            SqlParameter entregadoParam = new SqlParameter("@entregado", System.Data.SqlDbType.VarChar, 5);


            id_reservaParam.Value = id_reserva;
            id_peliculaParam.Value = id_pelicula;
            id_usuarioParam.Value = id_usuario;
            fecha_reservaParam.Value = fecha_reserva;
            entregadoParam.Value = entregado;

            comando.Parameters.Add(id_reservaParam);
            comando.Parameters.Add(id_peliculaParam);
            comando.Parameters.Add(id_usuarioParam);
            comando.Parameters.Add(fecha_reservaParam);
            comando.Parameters.Add(entregadoParam);

            comando.Prepare();
            comando.ExecuteNonQuery();
            database.Desconectar();
        }

        public void actualizar()
        {
            comando = new SqlCommand(null, database.Conectar());            
            comando.CommandText = "update reserva set id_pelicula =  @id_pelicula, id_usuario = @id_usuario, fecha_reserva = @fecha_reserva, entregado = @entregado where id_reserva=@id_reserva";
            
            SqlParameter id_reservaParam = new SqlParameter("@id_reserva", System.Data.SqlDbType.VarChar, 20);
            SqlParameter id_peliculaParam = new SqlParameter("@id_pelicula", System.Data.SqlDbType.VarChar, 20);
            SqlParameter id_usuarioParam = new SqlParameter("@id_usuario", System.Data.SqlDbType.VarChar, 20);
            SqlParameter fecha_reservaParam = new SqlParameter("@fecha_reserva", System.Data.SqlDbType.Date);
            SqlParameter entregadoParam = new SqlParameter("@entregado", System.Data.SqlDbType.VarChar, 5);


            id_reservaParam.Value = id_reserva;
            id_peliculaParam.Value = id_pelicula;
            id_usuarioParam.Value = id_usuario;
            fecha_reservaParam.Value = fecha_reserva;
            entregadoParam.Value = entregado;

            comando.Parameters.Add(id_reservaParam);
            comando.Parameters.Add(id_peliculaParam);
            comando.Parameters.Add(id_usuarioParam);
            comando.Parameters.Add(fecha_reservaParam);
            comando.Parameters.Add(entregadoParam);

            comando.Prepare();
            comando.ExecuteNonQuery();
            database.Desconectar();
        }

        public void borrar(string id_reserva)
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "delete from reserva where id_reserva=@id_reserva";
            SqlParameter id_reservaParam = new SqlParameter("@id_reserva", System.Data.SqlDbType.VarChar, 20);

            id_reservaParam.Value = id_reserva;

            comando.Parameters.Add(id_reservaParam);
            comando.Prepare();
            comando.ExecuteNonQuery();
            database.Desconectar();
        }
    }
}