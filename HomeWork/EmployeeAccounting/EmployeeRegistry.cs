namespace EmployeeAccountingApp;

internal sealed class EmployeeRegistry
{
    private static readonly Lazy<EmployeeRegistry> LazyInstance = new(() => new EmployeeRegistry());
    private readonly List<Employee> _employees = [];
    private int _nextId = 1;

    public static EmployeeRegistry Instance => LazyInstance.Value;

    private EmployeeRegistry()
    {
    }

    public IReadOnlyCollection<Employee> GetAll()
    {
        return _employees.AsReadOnly();
    }

    public Employee AddEmployee(string fullName, string position, decimal hourlyRate, decimal hoursWorked, decimal bonus)
    {
        Employee employee = new(_nextId++, fullName, position, hourlyRate, hoursWorked, bonus);
        _employees.Add(employee);
        return employee;
    }

    public Employee? GetById(int id)
    {
        return _employees.FirstOrDefault(e => e.Id == id);
    }

    public bool UpdateEmployee(int id, string fullName, string position, decimal hourlyRate, decimal hoursWorked, decimal bonus)
    {
        Employee? employee = GetById(id);
        if (employee is null)
        {
            return false;
        }

        employee.Update(fullName, position, hourlyRate, hoursWorked, bonus);
        return true;
    }
}
