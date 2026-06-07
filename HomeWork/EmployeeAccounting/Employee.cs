namespace EmployeeAccountingApp;

internal abstract class Employee
{
    public abstract string Name { get; set; }
    public abstract decimal BaseSalary { get; set; }

    public abstract decimal CalculateSalary();
}
