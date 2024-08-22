using DotNet.Testcontainers.Builders;
using FluentAssertions;
using MySql.Data.MySqlClient;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;
using Testcontainers.MySql;

namespace Playwright_Project.Steps
{
    [Binding]
    public class TestContainersTestStepDefinitions
    {
        DotNet.Testcontainers.Containers.IContainer container;
        string guid;
        int port;
        string myConnectionString;
        MySql.Data.MySqlClient.MySqlConnection connection;

        [Given(@"I run the mySql test container")]
        public async Task GivenIRunTheMySqlTestContainer()
        {
            container = new MySqlBuilder()
              .WithImage("mysql:8.0")
              .WithDatabase("credentials")
              .WithPassword("test")
              .WithUsername("test")
              .WithPortBinding(3306)
              .WithExposedPort(3306)
              .Build();
            await container.StartAsync();
            port = container.GetMappedPublicPort(3306);
            myConnectionString = "server=127.0.0.1;port=" + port + ";uid=test;pwd=test;database=credentials";
            connection = new MySqlConnection(myConnectionString);
            connection.Open();
        }

        [Given(@"I store credentials")]
        public async Task GivenIStoreCredentials()
        {
            
            var command = new MySqlCommand();
            command.Connection = connection;

            // Create users table
            command.CommandText = @"CREATE TABLE Users (
                User varchar(255),
                Pass varchar(255)
            );";
            command.ExecuteNonQuery();

            // Insert user and pass
            command.CommandText = @"INSERT INTO Users(User,Pass) VALUES(@user,@pass);";
            command.Parameters.AddWithValue("@user", "admin");
            command.Parameters.AddWithValue("@pass", "password123");
            command.ExecuteNonQuery();
            
        }
        
        [When(@"I read credentials")]
        public async Task WhenIReadCredentials()
        {
            var command = new MySqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Users;";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0));
                Console.WriteLine(reader.GetString(1));
            }
            
        }

        [Then(@"I verify the user and password and close the connection")]
        public void ThenIObtainAValidUUIDInsideTheResponse()
        {
            connection.Close();
        }
    }
}
