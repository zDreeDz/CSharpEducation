namespace EmployeeAccountingApp;

internal sealed class PartTimeEmployee : Employee
{
    public override string Name { get; set; }
    public override decimal BaseSalary { get; set; }
    public decimal HoursWorked { get; set; }

    public PartTimeEmployee(string name, decimal hourlyRate, decimal hoursWorked)
    {
        Name = name;
        BaseSalary = hourlyRate;
        HoursWorked = hoursWorked;
    }

    public override decimal CalculateSalary()
    {
        return BaseSalary * HoursWorked;
    }
}
