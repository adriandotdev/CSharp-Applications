namespace EmployeeManagementConsoleApp.Records;

public record Employee (int? ID, string Name, string Role, string Department, decimal Salary)
{
    public decimal ComputeTaxDeduction() => 0;
    public decimal ComputeAnnualSalary() => Salary * 12;

    public string GetDetails() => $@"
ID: {ID}
Name: {Name}
Role: {Role}
Department: {Department}
Salary: {Salary}
Annual Salary: {this.ComputeAnnualSalary()}
";
}

