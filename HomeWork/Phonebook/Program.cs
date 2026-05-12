using PhonebookApp;

Phonebook phonebook = Phonebook.Instance;

while (true)
{
    Console.Clear();
    Console.WriteLine("PHONEBOOK");
    Console.WriteLine("1. Добавить абонента");
    Console.WriteLine("2. Удалить абонента по номеру");
    Console.WriteLine("3. Найти абонента по номеру");
    Console.WriteLine("4. Найти номер по имени");
    Console.WriteLine("5. Показать всех абонентов");
    Console.WriteLine("0. Выход");
    Console.Write("Выберите действие: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            AddAbonent(phonebook);
            break;
        case "2":
            RemoveAbonent(phonebook);
            break;
        case "3":
            FindByPhoneNumber(phonebook);
            break;
        case "4":
            FindByName(phonebook);
            break;
        case "5":
            ShowAll(phonebook);
            break;
        case "0":
            phonebook.SaveToFile();
            return;
        default:
            Console.WriteLine("Неизвестная команда.");
            break;
    }

    Console.WriteLine();
    Console.WriteLine("Нажмите Enter, чтобы продолжить...");
    Console.ReadLine();
}

static void AddAbonent(Phonebook phonebook)
{
    Console.Write("Введите номер телефона: ");
    string phoneNumber = Console.ReadLine() ?? string.Empty;

    Console.Write("Введите имя абонента: ");
    string name = Console.ReadLine() ?? string.Empty;

    if (phonebook.AddAbonent(phoneNumber, name, out string errorMessage))
    {
        Console.WriteLine("Абонент добавлен.");
    }
    else
    {
        Console.WriteLine(errorMessage);
    }
}

static void RemoveAbonent(Phonebook phonebook)
{
    Console.Write("Введите номер телефона для удаления: ");
    string phoneNumber = Console.ReadLine() ?? string.Empty;

    if (phonebook.RemoveByPhoneNumber(phoneNumber))
    {
        Console.WriteLine("Абонент удален.");
    }
    else
    {
        Console.WriteLine("Абонент с таким номером не найден.");
    }
}

static void FindByPhoneNumber(Phonebook phonebook)
{
    Console.Write("Введите номер телефона: ");
    string phoneNumber = Console.ReadLine() ?? string.Empty;

    Abonent? abonent = phonebook.GetByPhoneNumber(phoneNumber);
    if (abonent is null)
    {
        Console.WriteLine("Абонент не найден.");
        return;
    }

    Console.WriteLine($"Имя: {abonent.Name}");
}

static void FindByName(Phonebook phonebook)
{
    Console.Write("Введите имя абонента: ");
    string name = Console.ReadLine() ?? string.Empty;

    Abonent? abonent = phonebook.GetByName(name);
    if (abonent is null)
    {
        Console.WriteLine("Абонент не найден.");
        return;
    }

    Console.WriteLine($"Номер телефона: {abonent.PhoneNumber}");
}

static void ShowAll(Phonebook phonebook)
{
    IReadOnlyCollection<Abonent> abonents = phonebook.GetAll();
    if (abonents.Count == 0)
    {
        Console.WriteLine("Телефонная книга пуста.");
        return;
    }

    foreach (Abonent abonent in abonents)
    {
        Console.WriteLine($"{abonent.Name} - {abonent.PhoneNumber}");
    }
}
