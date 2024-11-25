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
}