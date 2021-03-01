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
    [RoutePrefix("")]
    public class UsuarioController : ApiController
    {
        Database database = new Database();

        SqlCommand comando;
        SqlDataReader cursor;
        public List<Usuario> Get()
        {
            comando = new SqlCommand("select * from usuario", database.Conectar());
            cursor = comando.ExecuteReader();
            List<Usuario> usuarios = new List<Usuario>();
            if (cursor.HasRows)
            {
                while (cursor.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.id = cursor.GetString(0);
                    usuario.nombre = cursor.GetString(1);
                    usuario.rol = cursor.GetString(2);
                    usuarios.Add(usuario);
                }
            }
            else
            {
                usuarios = null;
            }
            database.Desconectar();
            return usuarios;
        }

        public Usuario Get(string id, string password)
        {
            comando = new SqlCommand(null, database.Conectar());
            comando.CommandText = "select * from usuario where id = @id and password = @password";

            SqlParameter idParam = new SqlParameter("@id",System.Data.SqlDbType.VarChar, 50);
            SqlParameter passwordParam = new SqlParameter("@password", System.Data.SqlDbType.VarChar, 12);

            idParam.Value = id;
            passwordParam.Value = password;

            comando.Parameters.Add(idParam);
            comando.Parameters.Add(passwordParam);
            comando.Prepare();
            cursor = comando.ExecuteReader();
            Usuario usuario = new Usuario();
            if (cursor.HasRows)
            {
                cursor.Read();
                usuario.id = cursor.GetString(0);
                usuario.nombre = cursor.GetString(1);
                usuario.rol = cursor.GetString(2);
            }
            else
                usuario = null;
            database.Desconectar();
            return usuario;            
        }

        public HttpResponseMessage Post([FromBody] Usuario data)
        {
            Usuario usuario = new Usuario();
            usuario.id = data.id;
            usuario.nombre = data.nombre;
            usuario.rol = data.rol;
            usuario.password = data.password;
            try
            {
                usuario.insertar();
                return new HttpResponseMessage(HttpStatusCode.Created);                
            }
            catch (SqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public HttpResponseMessage Put(string id, [FromUri] Usuario data)
        {
            Usuario usuario = new Usuario();
            usuario.id = id;
            usuario.nombre = data.nombre;
            usuario.rol = data.rol;
            try
            {
                usuario.actualizar();
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (SqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        public HttpResponseMessage Delete(string id)
        {
            Usuario usuario = new Usuario();
            try
            {
                usuario.borrar(id);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (SqlException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
    }
}
