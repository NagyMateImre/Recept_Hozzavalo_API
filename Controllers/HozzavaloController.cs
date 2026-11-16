using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ReceptAPI.Models;

namespace ReceptAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HozzavaloController : ControllerBase
    {
        string ConnectionString = "server=localhost;uid=root;password=;database=receptek";

        [HttpGet("Get")]

        public object Get()
        {
            List<Hozzavalok> hozzavalo = new List<Hozzavalok>();

            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            var sql = @"SELECT * FROM hozzavalo";

            var cmd = new MySqlCommand(sql, connection);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Hozzavalok hozzavalok = new Hozzavalok()
                {
                    id = reader.GetInt32(0),
                    IngredientName = reader.GetString(1),
                    Quantity = reader.GetString(2),
                    Description = reader.GetString(3),
                    AddedTime = reader.GetDateTime(4),
                    UpdateTime = reader.GetDateTime(5),
                    szakcsId = reader.GetInt32(6)
                };

                hozzavalo.Add(hozzavalok);
            }

            connection.Close();
            return hozzavalo;
        }
    }
}
