namespace EmployeeAccountingApp;

internal sealed class Employee
{
    public int Id { get; }
    public string FullName { get; private set; }
    public string Position { get; private set; }
    public decimal HourlyRate { get; private set; }
    public decimal HoursWorked { get; private set; }
    public decimal Bonus { get; private set; }

    public Employee(int id, string fullName, string position, decimal hourlyRate, decimal hoursWorked, decimal bonus)
    {
        Id = id;
        FullName = fullName;
        Position = position;
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
        Bonus = bonus;
    }

    public void Update(string fullName, string position, decimal hourlyRate, decimal hoursWorked, decimal bonus)
    {
        FullName = fullName;
        Position = position;
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
        Bonus = bonus;
    }

    public decimal CalculateSalary()
    {
        return HourlyRate * HoursWorked + Bonus;
    }
}
