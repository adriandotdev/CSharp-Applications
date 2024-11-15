using EmployeeManagementConsoleApp.Records;
using MySql.Data.MySqlClient;

namespace EmployeeManagementConsoleApp;

public class EmployeeRepository {

    
    public async Task AddEmployee(Employee employee) {

        Console.WriteLine("AddEmployee Repository");

        using var connection = new MySqlConnection("Server=localhost;Database=employee_management_system;Port=3306;User ID=root;Password=4332wurx;Pooling=true;Min Pool Size=0;Max Pool Size=100;");

        try {
            await connection.OpenAsync();

            // Create a query to execute
            string QUERY = "INSERT INTO employees (role, salary) VALUES (@role, @salary)";

            // Create a command object
            using var command = new MySqlCommand(QUERY, connection);

            // Add parameters to the command
            command.Parameters.AddWithValue("@role", employee.Role);
            command.Parameters.AddWithValue("@salary", employee.Salary);

            int rowsAffected = await command.ExecuteNonQueryAsync();
        }
        catch(MySqlException e) {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}