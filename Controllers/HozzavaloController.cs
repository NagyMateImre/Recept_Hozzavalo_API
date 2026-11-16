using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using ReceptAPI.Models;
using ReceptAPI.Models.Dto;
using System.ComponentModel;

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

        [HttpGet("GetById")]

        public object GetById(int id)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            var sql = @"SELECT * FROM hozzavalo WHERE id = @id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            object? data = null;

            while (reader.Read())
            {
                data = new Hozzavalok()
                {
                    id = reader.GetInt32(0),
                    IngredientName = reader.GetString(1),
                    Quantity = reader.GetString(2),
                    Description = reader.GetString(3),
                    AddedTime = reader.GetDateTime(4),
                    UpdateTime = reader.GetDateTime(5),
                    szakcsId = reader.GetInt32(6)
                };

                if(data != null)
                {   
                    return data;
                }
            }

            connection.Close();
            return data;
        }

        [HttpPost("Post")]

        public object Post([FromBody]AddNewHozzavalo addNewHozzavalo)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            var sql = @"INSERT INTO `hozzavalo`(`ingredientName`, `quantity`, `description`, `addedTime`, `updatetime`, `szakacsId`) VALUES (@ingredientName,@quantity,@description,@addedTime,@updatetime,@szakacsId)";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@ingredientName", addNewHozzavalo.IngredientName);
            cmd.Parameters.AddWithValue("@quantity", addNewHozzavalo.Quantity);
            cmd.Parameters.AddWithValue("@description", addNewHozzavalo.Description);
            cmd.Parameters.AddWithValue("@addedTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@updateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@szakacsId", addNewHozzavalo.szakcsId);

            cmd.ExecuteNonQuery();

            connection.Clone();

            return new { result = "Sikeres Felvétel"};
        }

        [HttpPut("Put")]

        public object Put(int id,UpdateHozzavalo updateHozzavalo)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            var sql = @"UPDATE `hozzavalo` SET `ingredientName`='@ingredientName,`quantity`=@quantity,`description`=@description,`updatetime`=@updatetime,`szakacsId`=@szakacsId WHERE `id` = @id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@ingredientName", updateHozzavalo.IngredientName);
            cmd.Parameters.AddWithValue("@quantity", updateHozzavalo.Quantity);
            cmd.Parameters.AddWithValue("@description", updateHozzavalo.Description);
            cmd.Parameters.AddWithValue("@updatetime", DateTime.Now);
            cmd.Parameters.AddWithValue("@szakacsId", updateHozzavalo.szakcsId);

            cmd.ExecuteScalar();

            connection.Close();
            return new { result = "Sikeres változtatás"};
        }

        [HttpDelete("Delete")]

        public object Delete(int id)
        {
            var connection = new MySqlConnection(ConnectionString);

            connection.Open();

            var sql = @"DELETE FROM `hozzavalo` WHERE id = @id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id",id);

            cmd.ExecuteScalar();

            connection.Close();
            return new { result = "Sikeres Törlés"};
        }
    }
}
