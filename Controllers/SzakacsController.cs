using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace ReceptAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SzakacsController : ControllerBase
    {

        string ConnectionString = "server=localhost;uid=root;password=;database=receptek";

        [HttpGet("GetNameAndEmailById")]

        public object GetEmailAndName(int id)
        {
            var connection = new MySqlConnection(ConnectionString);
            string Name, Email;
            connection.Open();

            var sql = @"SELECT `name`,`email` FROM `szakacs` WHERE `id` = @id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            var reader = cmd.ExecuteReader();
            object? data = null;

            while (reader.Read())
            {
                Name = reader.GetString(0);
                Email = reader.GetString(1);

                data = new { Name, Email };
            }
            connection.Close();
            return data;
        }
    }
}
