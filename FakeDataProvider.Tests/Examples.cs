using System.Collections.Generic;
using System.Data;
using System.Linq;
using NUnit.Framework;

namespace FakeDataProvider.Tests
{
    [TestFixture]
    public class Examples
    {
        [Test]
        public void SimpleExample()
        {
            // This is the result we want to return when execute reader is executed
            var fakeDataReader = new FakeDataReader(0,"UserId","Name");
            fakeDataReader.AddRow(1, "Smith");
            fakeDataReader.AddRow(2, "John");

            var result = new List<User>();
            using (var connection = new FakeDbConnection("ConnectionString", dbCommand => fakeDataReader))
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Users";

                    IDbDataParameter dbDataParameter = command.CreateParameter();
                    dbDataParameter.ParameterName = "ParameterName";
                    dbDataParameter.DbType = DbType.Int32;
                    dbDataParameter.Value = 0;

                    command.Parameters.Add(dbDataParameter);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                       
                        while (reader.Read())
                        {
                            result.Add(new User { UserId = reader.GetInt32(0), Name = reader.GetString(1)});
                        }
                    }
                }
            }

            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result.ElementAt(0).UserId, Is.EqualTo(1));
            Assert.That(result.ElementAt(0).Name, Is.EqualTo("Smith"));

            Assert.That(result.ElementAt(1).UserId, Is.EqualTo(2));
            Assert.That(result.ElementAt(1).Name, Is.EqualTo("John"));
        }

        private class User
        {
            public int UserId { get; set; }
            public string Name { get; set; }
        }
    }
}
