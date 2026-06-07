namespace EmployeeAccountingApp;

internal sealed class EmployeeManager<T> : IEmployeeManager<T> where T : Employee
{
    private readonly List<T> _employees = [];

    public void Add(T employee)
    {
        if (_employees.Any(existing => string.Equals(existing.Name, employee.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("Сотрудник с таким именем уже существует.");
        }

        _employees.Add(employee);
    }

    public T Get(string name)
    {
        T? employee = _employees.FirstOrDefault(existing =>
            string.Equals(existing.Name, name, StringComparison.OrdinalIgnoreCase));

        if (employee is null)
        {
            throw new InvalidOperationException("Сотрудник не найден.");
        }

        return employee;
    }

    public void Update(T employee)
    {
        int index = _employees.IndexOf(employee);
        if (index == -1)
        {
            throw new InvalidOperationException("Сотрудник не найден.");
        }

        bool hasDuplicateName = _employees.Any(existing =>
            !ReferenceEquals(existing, employee) &&
            string.Equals(existing.Name, employee.Name, StringComparison.OrdinalIgnoreCase));

        if (hasDuplicateName)
        {
            throw new InvalidOperationException("Сотрудник с таким именем уже существует.");
        }

        _employees[index] = employee;
    }

    public IReadOnlyCollection<T> GetAll()
    {
        return _employees.AsReadOnly();
    }
}
