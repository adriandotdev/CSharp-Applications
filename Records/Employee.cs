namespace EmployeeManagementConsoleApp.Records;

public record Employee (string Name, string Role, string Department, decimal Salary)
{
    public decimal ComputeTaxDeduction() => 0;
    public decimal ComputeAnnualSalary() => Salary * 12;

    public string GetDetails() => $@"
Name: {Name}
Role: {Role}
Department: {Department}
Salary: {Salary}
Annual Salary: {this.ComputeAnnualSalary()}
";
}

