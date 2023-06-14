using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReactCsvPeople.Data;
using ReactCsvPeople.Web.ViewModels;
using System.Globalization;
using System.Text;
using Faker;
using Faker.Resources;

namespace ReactCsvPeople.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private string _connectionString;

        public FileController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("generate/{amount}")]
        public IActionResult GenerateCsv(int amount)
        {
            var people = GeneratePeople(amount);
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            var csvString = builder.ToString();
            var fileBytes = Encoding.UTF8.GetBytes(csvString);

            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileName = $"people_{timestamp}.csv";

            System.IO.File.WriteAllBytes($"Uploads/{fileName}", fileBytes);
            return File(fileBytes, "text/csv", fileName);
        }

        [HttpGet]
        [Route("view")]
        public IActionResult View(string name)
        {
            byte[] csvData = System.IO.File.ReadAllBytes($"Uploads/{name}");
            return File(csvData, "application/octet-stream", name);
        }

        [HttpPost]
        [Route("upload")]
        public void Upload(UploadViewModel viewModel)
        {
            string base64 = viewModel.Base64.Substring(viewModel.Base64.IndexOf(",") + 1);
            byte[] csvBytes = Convert.FromBase64String(base64);
            System.IO.File.WriteAllBytes($"uploads/{viewModel.Name}", csvBytes);
            var people = GetCsvFromBytes(csvBytes);
            var repo = new PeopleRepository(_connectionString);
            repo.AddPeople(people);
        }

        [HttpGet]
        [Route("getpeople")]
        public List<Person> GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetPeople();
        }

        [HttpPost]
        [Route("deletepeople")]
        public void DeletePeople()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeletePeople();
        }

        private static List<Person> GetCsvFromBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<Person>().ToList();
        }

        private  List<Person> GeneratePeople(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ => new Person
            {
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                Age = Faker.RandomNumber.Next(20, 100),
                Email = Faker.Internet.Email(),
                Address = Faker.Address.StreetAddress()
            }).ToList();
        }

    }
}

