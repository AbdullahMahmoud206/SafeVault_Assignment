using System.Net;
using System.Text.RegularExpressions;

namespace SafeVaultDemo.Services;

public static class InputSanitizer
{
    public static string Sanitize(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        input = input.Trim();

        // Remove script tags
        input = Regex.Replace(
            input,
            @"<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>",
            string.Empty,
            RegexOptions.IgnoreCase);

        // Encode remaining HTML
        return WebUtility.HtmlEncode(input);
    }

    public static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(
            email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public static bool IsValidUsername(string username)
    {
        return Regex.IsMatch(
            username,
            @"^[a-zA-Z0-9_]{3,20}$");
    }
}