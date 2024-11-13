namespace EmployeeManagementConsoleApp.Records;

public record Employee (string Name, string Role, string Department, decimal Salary)
{
    public decimal ComputeTaxDeduction() => 0;
    public decimal ComputeAnnualSalary() => Salary * 12;

    public string GetDetails() => $"\nName:{Name}\nRole: {Role}\nDepartment: {Department}\nSalary: {Salary}";
}

