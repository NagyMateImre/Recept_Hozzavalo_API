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

        [HttpGet("GetNameAndInformations")]

        public object GetNameAndInformations(int id)
        {
            List<object> informations = new List<object>();

            var connection = new MySqlConnection(ConnectionString);
            string ingredientName,quantity;
            connection.Open();

            var sql = @"SELECT `name`, hozzavalo.ingredientName, hozzavalo.quantity FROM `szakacs` INNER JOIN hozzavalo ON hozzavalo.szakacsId = szakacs.id WHERE szakacs.id = @id;";

            var cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            string name = "";
            while (reader.Read())
            {
                name = reader.GetString(0);
                ingredientName = reader.GetString("ingredientName");
                quantity = reader.GetString("quantity");
                informations.Add(new { ingredientName,quantity});
            }

            connection.Clone();
            return new { result = "Sikeres Lekérés", name, informations };
        }

        [HttpGet("GetRecordsBySzakacsID")]

        public object GetRecords(int id)
        {
            var connection = new MySqlConnection(ConnectionString);
            int Records = 0;
            connection.Open();

            var sql = @"SELECT count(*) FROM `szakacs` INNER JOIN `hozzavalo` ON hozzavalo.szakacsId = szakacs.id WHERE szakacs.id = @id;";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            var number = cmd.ExecuteScalar();
            Records = Convert.ToInt32(number);

            connection.Close();
            return new { result = "Sikeres Lekérés", Records};
        }
    }
}
