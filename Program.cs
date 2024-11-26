// See https://aka.ms/new-console-template for more information
namespace EmployeeManagementConsoleApp;

using EmployeeManagementConsoleApp.Records;
using Google.Protobuf.WellKnownTypes;

public class Program {

    public static async Task Main(string[] args) {
        
        var employeeService = new EmployeeService(new EmployeeRepository());

        var loginResult = await ShowLogin(employeeService);

        if (loginResult.Equals("OK")) {

            int choice = 0;

            ShowAppMenu();
            bool isValidChoice = int.TryParse(Console.ReadLine(), out choice);


            while(isValidChoice && choice >= 1 && choice <= 3) {
                
                switch(choice) {
                    case 1:

                        Console.WriteLine("\nNew Employee");
                        Console.WriteLine("==================================");

                        Console.Write("\nEnter employee name: ");
                        string? employeeName = Console.ReadLine();

                        Console.Write("Enter employee role: ");
                        string? role = Console.ReadLine();

                        Console.Write("Enter employee department: ");
                        string? department = Console.ReadLine();

                        Console.Write("Enter employee salary: ");
                        bool isValidSalary = decimal.TryParse(Console.ReadLine(), out decimal salary);

                        if (isValidSalary) {
                            await employeeService.AddEmployee(new Employee(0, employeeName, role, department, salary));
                            Console.WriteLine($"Employee {employeeName} added successfully.\n");
                        }
                        break;
                    case 2:
                        Console.WriteLine("\nList of Employees");
                        Console.WriteLine("==================================");

                        int page = 1;
                        int limit = 5 ;
                        int offset = (page - 1) * limit;

                        var totalEmployees = await employeeService.GetEmployees(limit, offset);
        
                        while (totalEmployees > (limit * page)) {
                            Console.Write("Show More? (Type \"yes\" to confirm): ");
                            string? nextPageInput = Console.ReadLine();

                            if (!string.IsNullOrEmpty(nextPageInput) && nextPageInput.ToLower().Equals("yes")) {
                                page++;
                                offset = (page - 1) * limit;
                                totalEmployees = await employeeService.GetEmployees(limit, offset);
                            }
                            else {
                                break;
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("Exit");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                ShowAppMenu();
                isValidChoice = int.TryParse(Console.ReadLine(), out choice);
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

                if (username is null) {
                    Console.WriteLine("Error: Username cannot be empty.");
                    continue;
                }

                if (password is null) {
                    Console.WriteLine("Error: Password cannot be empty.");
                    continue;
                }

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
        Console.WriteLine("2. Show Employees");
        Console.WriteLine("3. Exit");
        Console.Write("Enter your choice: ");
    }
}
