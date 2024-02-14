using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using OutOfConsole.Models;
using System.Reflection;

namespace OutOfConsole.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        const string CONNECTIONSTRING = "Server=localhost;Port=5433;Database=Lesson;username=postgres;Password=2345;";


        // https://localhost:7072/WeatherForecast/Get
        [HttpGet]
        public List<Experiment> Get()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTIONSTRING))
            {
                return connection.Query<Experiment>("select * from experiment;").ToList();
            }
        }

        [HttpGet]
        public List<Experiment> GetByAge(int age)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTIONSTRING))
            {
                return connection.Query<Experiment>("select * from experiment where age > @age;", new { Age = age }).ToList();
            }
        }


        [HttpGet]
        [Route("apiget/test/get/qaytar")]
        public string Get2()
        {
            return "Malumotim 2 boryapti";
        }

        [HttpPost]
        public string Create( string name, int age )
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(CONNECTIONSTRING))
            {
                string query = $"insert into experiment(name, age) values(@name, @age)";
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("age", age);

                int x = command.ExecuteNonQuery();

                if (x != 0)
                {
                    return "malumot yaratildi";
                }
                return "hech narsa qo'shilmadi";

            }
        }

        [HttpPut]
        public string Update(string malumot)
        {
            return $"update bo'lyapti : {malumot}";
        }

        [HttpDelete]
        public string Delete(int id)
        {
            return $"Malumotim boryapti usha mo'ni: {id}";
        }

        private string Delete2(int id)
        {
            return $"Malumotim boryapti usha mo'ni: {id}";
        }

        [HttpPatch]
        public string UpdateAnyItem(string malumot)
        {
            return $"Patch ishladi: {malumot}";
        }
    }
}
