// See https://aka.ms/new-console-template for more information
namespace EmployeeManagementConsoleApp;

using EmployeeManagementConsoleApp.Records;

public class Program {

    public static async Task Main(string[] args) {
        
        var employeeService = new EmployeeService(new EmployeeRepository());

        await employeeService.AddEmployee(new Employee("Sample Name", "Role", "Department", 25000));
    }
}
