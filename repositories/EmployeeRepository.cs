using System.Data;
using EmployeeManagementConsoleApp.Records;
using MySql.Data.MySqlClient;
using Mysqlx;

namespace EmployeeManagementConsoleApp;

public class EmployeeRepository {

    private static readonly string CONNECTION_STRING = "Server=localhost;Database=employee_management_system;Port=3306;User ID=root;Password=4332wurx;Pooling=true;Min Pool Size=0;Max Pool Size=100;";

    public async Task AddEmployee(Employee employee) {

        using var connection = new MySqlConnection(CONNECTION_STRING);

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

        using var connection = new MySqlConnection(CONNECTION_STRING);

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

    public async Task<int> TotalEmployees() {

        using var connection = new MySqlConnection(CONNECTION_STRING);

        await connection.OpenAsync();

        string QUERY = "SELECT COUNT(*) AS total_employees FROM employees";

        using var command = new MySqlCommand(QUERY, connection);

        var reader = await command.ExecuteReaderAsync();

        int totalEmployees = 0;

        while (reader.Read()) {
            totalEmployees = reader.GetInt32("total_employees");
        }

        return totalEmployees;
    }
    
    public async Task<List<Employee>> GetEmployees(int limit, int offset) {
        
        using var connection = new MySqlConnection("Server=localhost;Database=employee_management_system;Port=3306;User ID=root;Password=4332wurx;Pooling=true;Min Pool Size=0;Max Pool Size=100;");

        await connection.OpenAsync();

        string QUERY = $"SELECT * FROM employees LIMIT {limit}  OFFSET {offset}";

        using var command = new MySqlCommand(QUERY, connection);

        var reader = await command.ExecuteReaderAsync();

        while (reader.Read()) {

            int id = reader.GetInt16("id");
            string name = reader.GetString("name");
            string role = reader.GetString("role");
            string department = reader.GetString("department");
            decimal salary = reader.GetDecimal("salary");

            Console.WriteLine(new Employee(id, name, role, department, salary).GetDetails());
        }
        return [];
    }
}