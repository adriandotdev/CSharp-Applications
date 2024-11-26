using System.Security.Cryptography.X509Certificates;

namespace EmployeeManagementConsoleApp.Records;

public record Employee (int? ID, string Name, string Role, string Department, decimal Salary)
{

    public decimal ComputeAnnualIncome() => this.Salary * 12;

    public string GetDetails() => $@"
ID: {ID}
Name: {Name}
Role: {Role}
Department: {Department}
Monthly Salary: {Salary:C}
Annual Salary: {this.ComputeAnnualIncome():C} 
";
}

