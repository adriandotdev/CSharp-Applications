// See https://aka.ms/new-console-template for more information
namespace EmployeeManagementConsoleApp;

using EmployeeManagementConsoleApp.Records;

public class Program {

    public static async Task Main(string[] args) {
        
        var employeeService = new EmployeeService(new EmployeeRepository());

        var loginResult = await ShowLogin(employeeService);

        if (loginResult.Equals("OK")) {

            ShowAppMenu();
            bool isValidChoice = int.TryParse(Console.ReadLine(), out int choice);


            switch(choice) {
                case 1:
                    Console.Write("\nEnter employee name: ");
                    string? employeeName = Console.ReadLine();

                    Console.Write("Enter employee role: ");
                    string? role = Console.ReadLine();

                    Console.Write("Enter employee department: ");
                    string? department = Console.ReadLine();

                    Console.Write("Enter employee salary: ");
                    bool isValidSalary = decimal.TryParse(Console.ReadLine(), out decimal salary);

                    if (isValidSalary) {
                        await employeeService.AddEmployee(new Employee(employeeName, role, department, salary));
                        Console.WriteLine($"Employee {employeeName} added successfully.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public static async Task<string> ShowLogin(EmployeeService employeeService) {
        
        int loginAttempts = 3;

        Console.WriteLine("Employee Management Console App");
        Console.WriteLine("==================================");
        while (loginAttempts > 0) {

            try {
                
                Console.Write("\nEnter your username: ");
                string? username = Console.ReadLine();

                Console.Write("Enter your password: ");
                string? password = Console.ReadLine();

                await employeeService.Login(username, password);

                return "OK";
            }
            catch(Exception e) {
                Console.WriteLine($"Error: {e.Message}");
                loginAttempts--;
            }
        }

        return "Failed";
    }

    public static void ShowAppMenu() {

        Console.WriteLine("Employee Management Console App");
        Console.WriteLine("==================================");
        Console.WriteLine("1. Add Employee");
        Console.WriteLine("2. Update Employee");
        Console.WriteLine("3. Delete Employee");
        Console.WriteLine("4. List Employees");
        Console.WriteLine("5. Exit");
        Console.Write("Enter your choice: ");
    }
}
