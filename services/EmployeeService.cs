using EmployeeManagementConsoleApp.Records;

namespace EmployeeManagementConsoleApp;

public class EmployeeService {

    private readonly EmployeeRepository repository;
    
    public EmployeeService(EmployeeRepository repository) {
        this.repository = repository;
    }

    public async Task AddEmployee(Employee employee) {

       Console.WriteLine("AddEmployee method");

       await repository.AddEmployee(employee);
    }
}