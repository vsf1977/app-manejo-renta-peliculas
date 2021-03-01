using System.Data.SqlClient;

namespace Backend.Models
{
    public class Database
    {
        SqlConnection databaseConnection;
        public SqlConnection Conectar()
        {            
            string cadena = "Server=localhost;Database=VIDEOBLOCK;Trusted_Connection=True;";
            databaseConnection = new SqlConnection(cadena);
            databaseConnection.Open();
            return databaseConnection;
        }

        public void Desconectar()
        {
            databaseConnection.Close();                   
        }

    }
}