using Microsoft.Data.SqlClient;
namespace BlazorEmployee.Data
{
    public class EmployeeService : IEmployeeService
    {
        private readonly string _connectionString;

        public EmployeeService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Employee");
        }

        public async Task<List<Employee>> GetEmployesAsync()
        {
            var employees = new List<Employee>();
            using SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", conn);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                employees.Add(new Employee
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Job_title = reader.GetString(2),
                    Work_phone = reader.GetString(3),
                    Cell_phone = reader.GetString(4),
                    Department = reader.GetString(5),
                    Manager = reader.GetString(6)
                });
            }
            return employees;
        }

        public async Task<Employee> GetEmployeeAsync(Guid id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Employee
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Job_title = reader.GetString(2),
                    Work_phone = reader.GetString(3),
                    Cell_phone = reader.GetString(4),
                    Department = reader.GetString(5),
                    Manager = reader.GetString(6)
                };
            }
            return null;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            using SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using SqlCommand cmd = new SqlCommand(@"INSERT INTO Employee (Id, Name,Job_title, Work_phone, Cell_phone, Department, Manager)
                                                   VALUES (@Id, @Name,@Job_title, @Work_phone, @Cell_phone, @Department, @Manager)", conn);
            cmd.Parameters.AddWithValue("@Id", employee.Id);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Job_title", employee.Job_title);
            cmd.Parameters.AddWithValue("@Work_phone", employee.Work_phone);
            cmd.Parameters.AddWithValue("@Cell_phone", employee.Cell_phone);
            cmd.Parameters.AddWithValue("@Department", employee.Department);
            cmd.Parameters.AddWithValue("@Manager", employee.Manager);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using SqlCommand cmd = new SqlCommand(@"UPDATE Employee SET
                                                        Name = @Name,
                                                        Job_title = @Job_title,
                                                        Work_phone = @Work_phone,
                                                        Cell_phone = @Cell_phone,
                                                        Department = @Department,
                                                        Manager = @Manager
                                                    WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", employee.Id);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Job_title", employee.Job_title);
            cmd.Parameters.AddWithValue("@Work_phone", employee.Work_phone);
            cmd.Parameters.AddWithValue("@Cell_phone", employee.Cell_phone);
            cmd.Parameters.AddWithValue("@Department", employee.Department);
            cmd.Parameters.AddWithValue("@Manager", employee.Manager);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}