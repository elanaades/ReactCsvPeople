using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactCsvPeople.Data
{
    public class PeopleRepository
    {
        public string _connectionString { get; set; }

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddPeople(List<Person> people)
        {
            using var context = new PeopleDataContext(_connectionString);
            context.People.AddRange(people);
            context.SaveChanges();
        }
        public List<Person> GetPeople()
        {
            using var context = new PeopleDataContext(_connectionString);
            return context.People.ToList();
        }
        public void DeletePeople()
        {
            using var context = new PeopleDataContext(_connectionString);
            var peopleToRemove = context.People.ToList();
            context.People.RemoveRange(peopleToRemove);
            context.SaveChanges();
        }
    }
}
