using EmployeeManagementConsoleApp.Records;

namespace EmployeeManagementConsoleApp;

public class EmployeeService {

    private readonly EmployeeRepository repository;
    
    public EmployeeService(EmployeeRepository repository) {
        this.repository = repository;
    }

    public async Task AddEmployee(Employee employee) {

       await repository.AddEmployee(employee);
    }

    public async Task Login(string username, string password) {

        await this.repository.Login(username, password);
    }

    public async Task<int> GetEmployees(int limit, int offset) {

        var totalEmployees = await this.repository.TotalEmployees();

        await repository.GetEmployees(limit, offset);

        return totalEmployees;
    }
}