// See https://aka.ms/new-console-template for more information
namespace EmployeeManagementConsoleApp;

using EmployeeManagementConsoleApp.Records;

public class Program {

    public static void Main(string[] args) {
        
        Console.WriteLine("=====Welcome to Employee Management====");

        int numberOfEmployees;

        Console.Write("\nEnter number of employees to add: ");
        bool isValidNumberOfEmployees = int.TryParse(Console.ReadLine(), out numberOfEmployees);

        if (!isValidNumberOfEmployees || numberOfEmployees <= 0) {
            Console.WriteLine("Invalid number of employees. Please enter a positive integer.");
            return;
        }

        Employee[] employees = new Employee[numberOfEmployees];

        Console.WriteLine($"\nEnter {numberOfEmployees} employees to add");

        for (int i = 0; i < numberOfEmployees; i++) {
        
            Console.Write($"\nEnter employee {i + 1} name: ");
            string? name = Console.ReadLine();

            Console.Write($"Enter employee {i + 1} role: ");
            string? role = Console.ReadLine();

            Console.Write($"Enter employee {i + 1} department: ");
            string? department = Console.ReadLine();

            Console.Write($"Enter employee {i + 1} salary: ");
            decimal? salary = decimal.TryParse(Console.ReadLine(), out decimal parsedSalary) ? parsedSalary : null;

            employees[i] = new Employee(name, role, department, parsedSalary);
        }

        Console.WriteLine("\n====Employees====");
        foreach (Employee employee in employees) {

            Console.WriteLine(employee.GetDetails());
        }
        Console.WriteLine();
    }
}
