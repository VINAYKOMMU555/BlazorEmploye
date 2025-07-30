namespace BlazorEmployee.Data
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployesAsync();
        Task<Employee> GetEmployeeAsync(Guid id);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Guid id);
    }
}
