using Microsoft.Data.SqlClient;


namespace BlazorEmployee.Services

{
    public class DatabaseService

    {
       
       private readonly string _connectionString;

            public DatabaseService(IConfiguration configuration)
            {
                _connectionString = configuration.GetConnectionString("Employee");
            }

            public async Task CreateTableAsync()
            {
                string createTableQuery = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Employee' AND xtype='U')
            BEGIN
                CREATE TABLE Employee(
                    Id UNIQUEIDENTIFIER PRIMARY KEY,
                    Name VARCHAR(20) NOT NULL,
                    Job_title VARCHAR(20) NOT NULL,
                    Work_phone VARCHAR(10) NOT NULL,
                    Cell_phone VARCHAR(10) NOT NULL,
                    Department VARCHAR(20) NOT NULL,
                    Manager VARCHAR(20) NOT NULL
                );
            END";

                using SqlConnection connection = new SqlConnection(_connectionString);
                   
                 await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(createTableQuery, connection);
                await command.ExecuteNonQueryAsync();
            }
       
     }
    
}


