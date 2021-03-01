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
    public class PeliculaController : ApiController
    {
        Database database = new Database();

        SqlCommand comando;
        SqlDataReader cursor;
        public List<Pelicula> Get()
        {
            comando = new SqlCommand("select * from pelicula", database.Conectar());
            cursor = comando.ExecuteReader();
            List<Pelicula> peliculas = new List<Pelicula>();
            if (cursor.HasRows)
            {
                while (cursor.Read())
                {
                    Pelicula pelicula = new Pelicula();
                    pelicula.id = cursor.GetString(0);
                    pelicula.titulo = cursor.GetString(1);
                    pelicula.descripcion = cursor.GetString(2);
                    pelicula.director = cursor.GetString(3);
                    pelicula.costo_alquiler = cursor.GetString(4);
                    pelicula.cantidad = cursor.GetString(5);
                    pelicula.actores = cursor.GetString(6);
                    peliculas.Add(pelicula);
                }
            }
            else
            {
                peliculas = null;
            }
            database.Desconectar();
            return peliculas;
        }

        
        public Pelicula Get(string id)
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "select * from pelicula where id = @id";

            SqlParameter idParam = new SqlParameter("@id",System.Data.SqlDbType.VarChar, 20);

            idParam.Value = id;
            comando.Parameters.Add(idParam);
            comando.Prepare();
            cursor = comando.ExecuteReader();
            Pelicula pelicula = new Pelicula();
            if (cursor.HasRows)
            {
                cursor.Read();
                pelicula.id = cursor.GetString(0);
                pelicula.titulo = cursor.GetString(1);
                pelicula.descripcion = cursor.GetString(2);
                pelicula.director = cursor.GetString(3);
                pelicula.costo_alquiler = cursor.GetString(4);
                pelicula.cantidad = cursor.GetString(5);
                pelicula.actores = cursor.GetString(6);
            }
            else
                pelicula = null;
            database.Desconectar();
            return pelicula;
        }

        public HttpResponseMessage Post([FromBody] Pelicula data)
        {
            Pelicula pelicula = new Pelicula();
            pelicula.id = data.id;
            pelicula.titulo = data.titulo;
            pelicula.descripcion = data.descripcion;
            pelicula.director = data.director;
            pelicula.costo_alquiler = data.costo_alquiler;
            pelicula.cantidad = data.cantidad;
            pelicula.actores = data.actores;
            try
            {
                pelicula.insertar();
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (SqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public HttpResponseMessage Put([FromBody] Pelicula data)
        {
            Pelicula pelicula = new Pelicula();
            pelicula.id = data.id;
            pelicula.titulo = data.titulo;
            pelicula.descripcion = data.descripcion;
            pelicula.director = data.director;
            pelicula.costo_alquiler = data.costo_alquiler;
            pelicula.cantidad = data.cantidad;
            pelicula.actores = data.actores;
            try
            {
                pelicula.actualizar();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (SqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public HttpResponseMessage Delete(string id)
        {
            Pelicula pelicula = new Pelicula();
            try
            {
                pelicula.borrar(id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (SqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}