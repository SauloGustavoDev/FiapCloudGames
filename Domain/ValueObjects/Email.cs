using System.Text.RegularExpressions;

namespace Domain.ValueObjects;
    public record Email
    {
    public string? Address { get; }
    public Email(string value)
    {
        ValidateEmail(value);
        ValidateEmailFormat(value);
        Address = value;
    }

    protected Email() { } // For EF Core

    private static void ValidateEmailFormat(string value)
    {
        string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        if (!Regex.IsMatch(value, pattern))
            throw new ArgumentException("Formato de email inválido", nameof(value));
    }

    private static void ValidateEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email não pode ser vazio.", nameof(value));
    }

    public override string ToString() => Address!;
}

