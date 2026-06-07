namespace EmployeeAccountingApp;

internal sealed class FullTimeEmployee : Employee
{
    public override string Name { get; set; }
    public override decimal BaseSalary { get; set; }

    public FullTimeEmployee(string name, decimal baseSalary)
    {
        Name = name;
        BaseSalary = baseSalary;
    }

    public override decimal CalculateSalary()
    {
        return BaseSalary;
    }
}
