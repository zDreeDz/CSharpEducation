namespace PhonebookApp;

internal sealed class Abonent
{
    public string PhoneNumber { get; }
    public string Name { get; }

    public Abonent(string phoneNumber, string name)
    {
        PhoneNumber = phoneNumber;
        Name = name;
    }
}
