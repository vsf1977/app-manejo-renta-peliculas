using Backend.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http.Cors;

namespace Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReservaController : ApiController
    {
        Database database = new Database();

        SqlCommand comando;
        SqlDataReader cursor;
        public List<Reserva> Get()
        {
            comando = new SqlCommand("select * from reserva", database.Conectar());
            cursor = comando.ExecuteReader();
            List<Reserva> reservas = new List<Reserva>();
            if (cursor.HasRows)
            {
                while (cursor.Read())
                {
                    Reserva reserva = new Reserva();
                    reserva.id_reserva = cursor.GetString(0);
                    reserva.id_pelicula = cursor.GetString(1);
                    reserva.id_usuario = cursor.GetString(2);
                    reserva.fecha_reserva = cursor.GetDateTime(3);
                    reserva.entregado = cursor.GetString(4);
                    reservas.Add(reserva);
                }
            }
            else
            {
                reservas = null;
            }
            database.Desconectar();
            return reservas;
        }

        public Reserva Get(string id)
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "select * from reserva where id_reserva = @id_reserva";

            SqlParameter id_reservaParam = new SqlParameter("@id_reserva",System.Data.SqlDbType.VarChar, 20);

            id_reservaParam.Value = id;

            comando.Parameters.Add(id_reservaParam);
            comando.Prepare();
            cursor = comando.ExecuteReader();
            Reserva reserva = new Reserva();
            if (cursor.HasRows)
            {
                cursor.Read();
                reserva.id_reserva = cursor.GetString(0);
                reserva.id_pelicula = cursor.GetString(1);
                reserva.id_usuario = cursor.GetString(2);
                reserva.fecha_reserva = cursor.GetDateTime(3);
                reserva.entregado = cursor.GetString(4);
            }
            else
                reserva = null;
            database.Desconectar();
            return reserva;
        }

        public HttpResponseMessage Post([FromBody] Reserva data)
        {
            Reserva reserva = new Reserva();
            reserva.id_reserva = data.id_reserva;
            reserva.id_pelicula = data.id_pelicula;
            reserva.id_usuario = data.id_usuario;
            reserva.fecha_reserva = data.fecha_reserva;
            reserva.entregado = data.entregado;
            try
            {
                reserva.insertar();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (SqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public HttpResponseMessage Put([FromBody] Reserva data)
        {
            Reserva reserva = new Reserva();
            reserva.id_reserva = data.id_reserva;
            reserva.id_pelicula = data.id_pelicula;
            reserva.id_usuario = data.id_usuario;
            reserva.fecha_reserva = data.fecha_reserva;
            reserva.entregado = data.entregado;
            try
            {
                reserva.actualizar();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (SqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
        public HttpResponseMessage Delete(string id_reserva)
        {
            Reserva reserva = new Reserva();
            try
            {
                reserva.borrar(id_reserva);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (SqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
