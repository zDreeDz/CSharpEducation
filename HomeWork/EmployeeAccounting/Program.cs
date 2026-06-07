using EmployeeAccountingApp;

EmployeeManager<Employee> manager = new();

while (true)
{
    Console.Clear();
    Console.WriteLine("СИСТЕМА УЧЕТА СОТРУДНИКОВ");
    Console.WriteLine("1. Добавить полного сотрудника");
    Console.WriteLine("2. Добавить частичного сотрудника");
    Console.WriteLine("3. Получить информацию о сотруднике");
    Console.WriteLine("4. Обновить данные сотрудника");
    Console.WriteLine("5. Выйти");
    Console.Write("Выберите действие: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            AddFullTimeEmployee(manager);
            break;
        case "2":
            AddPartTimeEmployee(manager);
            break;
        case "3":
            ShowEmployeeInfo(manager);
            break;
        case "4":
            UpdateEmployee(manager);
            break;
        case "5":
            return;
        default:
            Console.WriteLine("Неизвестная команда.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Нажмите Enter, чтобы продолжить...");
    Console.ReadLine();
}

static void AddFullTimeEmployee(EmployeeManager<Employee> manager)
{
    string name = ReadRequiredString("Введите имя сотрудника: ");
    decimal baseSalary = ReadDecimal("Введите фиксированную зарплату: ");

    TryExecute(() => manager.Add(new FullTimeEmployee(name, baseSalary)), "Полный сотрудник добавлен.");
}

static void AddPartTimeEmployee(EmployeeManager<Employee> manager)
{
    string name = ReadRequiredString("Введите имя сотрудника: ");
    decimal hourlyRate = ReadDecimal("Введите оплату за час: ");
    decimal hoursWorked = ReadDecimal("Введите количество отработанных часов: ");

    TryExecute(() => manager.Add(new PartTimeEmployee(name, hourlyRate, hoursWorked)), "Частичный сотрудник добавлен.");
}

static void ShowEmployeeInfo(EmployeeManager<Employee> manager)
{
    string name = ReadRequiredString("Введите имя сотрудника: ");

    TryExecute(() =>
    {
        Employee employee = manager.Get(name);
        PrintEmployee(employee);
    });
}

static void UpdateEmployee(EmployeeManager<Employee> manager)
{
    string name = ReadRequiredString("Введите имя сотрудника для обновления: ");

    TryExecute(() =>
    {
        Employee currentEmployee = manager.Get(name);
        UpdateEmployeeData(currentEmployee);
        manager.Update(currentEmployee);
        Console.WriteLine("Данные сотрудника обновлены.");
    });
}

static void UpdateEmployeeData(Employee employee)
{
    Console.WriteLine("Введите новые данные сотрудника.");
    employee.Name = ReadRequiredString("Имя: ");

    if (employee is FullTimeEmployee)
    {
        employee.BaseSalary = ReadDecimal("Фиксированная зарплата: ");
        return;
    }

    PartTimeEmployee partTimeEmployee = (PartTimeEmployee)employee;
    employee.BaseSalary = ReadDecimal("Оплата за час: ");
    partTimeEmployee.HoursWorked = ReadDecimal("Количество часов: ");
}

static void PrintEmployee(Employee employee)
{
    Console.WriteLine($"Имя: {employee.Name}");

    if (employee is FullTimeEmployee)
    {
        Console.WriteLine("Тип: Полный сотрудник");
        Console.WriteLine($"Базовая зарплата: {employee.BaseSalary:F2}");
    }
    else if (employee is PartTimeEmployee partTimeEmployee)
    {
        Console.WriteLine("Тип: Частичный сотрудник");
        Console.WriteLine($"Оплата за час: {employee.BaseSalary:F2}");
        Console.WriteLine($"Отработано часов: {partTimeEmployee.HoursWorked:F2}");
    }

    Console.WriteLine($"Рассчитанная зарплата: {employee.CalculateSalary():F2}");
}

static void TryExecute(Action action, string? successMessage = null)
{
    try
    {
        action();
        if (!string.IsNullOrWhiteSpace(successMessage))
        {
            Console.WriteLine(successMessage);
        }
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static string ReadRequiredString(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string? value = Console.ReadLine()?.Trim();
        if (!string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        Console.WriteLine("Значение не должно быть пустым.");
    }
}

static decimal ReadDecimal(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (decimal.TryParse(Console.ReadLine(), out decimal value))
        {
            return value;
        }

        Console.WriteLine("Введите число.");
    }
}
