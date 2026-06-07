using EmployeeAccountingApp;

EmployeeRegistry registry = EmployeeRegistry.Instance;

while (true)
{
    Console.Clear();
    Console.WriteLine("СИСТЕМА УЧЕТА СОТРУДНИКОВ");
    Console.WriteLine("1. Добавить сотрудника");
    Console.WriteLine("2. Обновить сотрудника");
    Console.WriteLine("3. Получить сотрудника по ID");
    Console.WriteLine("4. Рассчитать зарплату сотрудника");
    Console.WriteLine("5. Показать всех сотрудников");
    Console.WriteLine("0. Выход");
    Console.Write("Выберите действие: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            AddEmployee(registry);
            break;
        case "2":
            UpdateEmployee(registry);
            break;
        case "3":
            ShowEmployee(registry);
            break;
        case "4":
            CalculateSalary(registry);
            break;
        case "5":
            ShowAllEmployees(registry);
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Неизвестная команда.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Нажмите Enter, чтобы продолжить...");
    Console.ReadLine();
}

static void AddEmployee(EmployeeRegistry registry)
{
    string fullName = ReadRequiredString("Введите ФИО: ");
    string position = ReadRequiredString("Введите должность: ");
    decimal hourlyRate = ReadDecimal("Введите почасовую ставку: ");
    decimal hoursWorked = ReadDecimal("Введите количество отработанных часов: ");
    decimal bonus = ReadDecimal("Введите премию: ");

    Employee employee = registry.AddEmployee(fullName, position, hourlyRate, hoursWorked, bonus);
    Console.WriteLine($"Сотрудник добавлен. ID: {employee.Id}");
}

static void UpdateEmployee(EmployeeRegistry registry)
{
    int id = ReadInt("Введите ID сотрудника: ");
    Employee? employee = registry.GetById(id);
    if (employee is null)
    {
        Console.WriteLine("Сотрудник не найден.");
        return;
    }

    string fullName = ReadRequiredString("Введите новое ФИО: ");
    string position = ReadRequiredString("Введите новую должность: ");
    decimal hourlyRate = ReadDecimal("Введите новую почасовую ставку: ");
    decimal hoursWorked = ReadDecimal("Введите новое количество часов: ");
    decimal bonus = ReadDecimal("Введите новую премию: ");

    registry.UpdateEmployee(id, fullName, position, hourlyRate, hoursWorked, bonus);
    Console.WriteLine("Информация о сотруднике обновлена.");
}

static void ShowEmployee(EmployeeRegistry registry)
{
    int id = ReadInt("Введите ID сотрудника: ");
    Employee? employee = registry.GetById(id);
    if (employee is null)
    {
        Console.WriteLine("Сотрудник не найден.");
        return;
    }

    PrintEmployee(employee);
}

static void CalculateSalary(EmployeeRegistry registry)
{
    int id = ReadInt("Введите ID сотрудника: ");
    Employee? employee = registry.GetById(id);
    if (employee is null)
    {
        Console.WriteLine("Сотрудник не найден.");
        return;
    }

    Console.WriteLine($"Зарплата сотрудника {employee.FullName}: {employee.CalculateSalary():F2}");
}

static void ShowAllEmployees(EmployeeRegistry registry)
{
    IReadOnlyCollection<Employee> employees = registry.GetAll();
    if (employees.Count == 0)
    {
        Console.WriteLine("Список сотрудников пуст.");
        return;
    }

    foreach (Employee employee in employees)
    {
        PrintEmployee(employee);
        Console.WriteLine();
    }
}

static void PrintEmployee(Employee employee)
{
    Console.WriteLine($"ID: {employee.Id}");
    Console.WriteLine($"ФИО: {employee.FullName}");
    Console.WriteLine($"Должность: {employee.Position}");
    Console.WriteLine($"Почасовая ставка: {employee.HourlyRate:F2}");
    Console.WriteLine($"Отработано часов: {employee.HoursWorked:F2}");
    Console.WriteLine($"Премия: {employee.Bonus:F2}");
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

static int ReadInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int value))
        {
            return value;
        }

        Console.WriteLine("Введите целое число.");
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
