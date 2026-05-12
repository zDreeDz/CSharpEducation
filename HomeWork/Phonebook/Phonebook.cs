using System.Text;

namespace PhonebookApp;

internal sealed class Phonebook
{
    private static readonly Lazy<Phonebook> LazyInstance = new(() => new Phonebook());
    private readonly List<Abonent> _abonents = [];
    private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "phonebook.txt");

    public static Phonebook Instance => LazyInstance.Value;

    private Phonebook()
    {
        LoadFromFile();
    }

    public IReadOnlyCollection<Abonent> GetAll()
    {
        return _abonents.AsReadOnly();
    }

    public bool AddAbonent(string phoneNumber, string name, out string errorMessage)
    {
        phoneNumber = phoneNumber.Trim();
        name = name.Trim();

        if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(name))
        {
            errorMessage = "Имя и номер телефона не должны быть пустыми.";
            return false;
        }

        bool exists = _abonents.Any(a =>
            string.Equals(a.PhoneNumber, phoneNumber, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase));

        if (exists)
        {
            errorMessage = "Такой абонент уже записан.";
            return false;
        }

        _abonents.Add(new Abonent(phoneNumber, name));
        SaveToFile();
        errorMessage = string.Empty;
        return true;
    }

    public bool RemoveByPhoneNumber(string phoneNumber)
    {
        Abonent? abonent = GetByPhoneNumber(phoneNumber);
        if (abonent is null)
        {
            return false;
        }

        _abonents.Remove(abonent);
        SaveToFile();
        return true;
    }

    public Abonent? GetByPhoneNumber(string phoneNumber)
    {
        return _abonents.FirstOrDefault(a =>
            string.Equals(a.PhoneNumber, phoneNumber.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    public Abonent? GetByName(string name)
    {
        return _abonents.FirstOrDefault(a =>
            string.Equals(a.Name, name.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    public void SaveToFile()
    {
        List<string> lines = [];
        foreach (Abonent abonent in _abonents)
        {
            lines.Add($"{Escape(abonent.PhoneNumber)}|{Escape(abonent.Name)}");
        }

        File.WriteAllLines(_filePath, lines, Encoding.UTF8);
    }

    private void LoadFromFile()
    {
        if (!File.Exists(_filePath))
        {
            return;
        }

        foreach (string line in File.ReadAllLines(_filePath, Encoding.UTF8))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = SplitLine(line);
            if (parts.Length != 2)
            {
                continue;
            }

            string phoneNumber = parts[0].Trim();
            string name = parts[1].Trim();
            if (string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(name))
            {
                continue;
            }

            bool exists = _abonents.Any(a =>
                string.Equals(a.PhoneNumber, phoneNumber, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase));

            if (!exists)
            {
                _abonents.Add(new Abonent(phoneNumber, name));
            }
        }
    }

    private static string Escape(string value)
    {
        return value.Replace("\\", "\\\\").Replace("|", "\\|");
    }

    private static string[] SplitLine(string line)
    {
        List<string> parts = [];
        StringBuilder current = new();
        bool escape = false;

        foreach (char ch in line)
        {
            if (escape)
            {
                current.Append(ch);
                escape = false;
                continue;
            }

            if (ch == '\\')
            {
                escape = true;
                continue;
            }

            if (ch == '|')
            {
                parts.Add(current.ToString());
                current.Clear();
                continue;
            }

            current.Append(ch);
        }

        parts.Add(current.ToString());
        return parts.ToArray();
    }
}
