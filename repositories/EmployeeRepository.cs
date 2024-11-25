using EmployeeManagementConsoleApp.Records;
using MySql.Data.MySqlClient;
using Mysqlx;

namespace EmployeeManagementConsoleApp;

public class EmployeeRepository {

    
    public async Task AddEmployee(Employee employee) {

        using var connection = new MySqlConnection("Server=localhost;Database=employee_management_system;Port=3306;User ID=root;Password=4332wurx;Pooling=true;Min Pool Size=0;Max Pool Size=100;");

        try {
            await connection.OpenAsync();

            // Create a query to execute
            string QUERY = "INSERT INTO employees (name, role, department, salary) VALUES (@name, @role, @department, @salary)";

            // Create a command object
            using var command = new MySqlCommand(QUERY, connection);

            // Add parameters to the command
            command.Parameters.AddWithValue("@name", employee.Name);
            command.Parameters.AddWithValue("@role", employee.Role);
            command.Parameters.AddWithValue("@department", employee.Department);
            command.Parameters.AddWithValue("@salary", employee.Salary);

            int rowsAffected = await command.ExecuteNonQueryAsync();
        }
        catch(MySqlException e) {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    public async Task Login(string username, string password) {

        using var connection = new MySqlConnection("Server=localhost;Database=employee_management_system;Port=3306;User ID=root;Password=4332wurx;Pooling=true;Min Pool Size=0;Max Pool Size=100;");

        await connection.OpenAsync();

        string QUERY = "SELECT * FROM users WHERE username = @username";

        using var command = new MySqlCommand(QUERY, connection);
        command.Parameters.AddWithValue("@username", username);

        var reader = await command.ExecuteReaderAsync();

        if (reader.HasRows) {
            while (reader.Read()) {
                string? storedPassword = reader["password"].ToString();
                
                if (storedPassword != null && !storedPassword.Equals(password)) {
                    throw new Exception("Invalid Credentials");
                }
            }
        } 
        else {
            throw new Exception("Invalid Credentials");
        }        
    }
}