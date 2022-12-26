using System;
using System.Text;

namespace ReelWords.CrossCutting.Helpers;

public static class StringHelpers
{
    public static string Sanitize(this string value)
    {
        if (value == null) throw new ArgumentException("Sanitized string argument shouldn't be null");

        var builder = new StringBuilder();
        foreach (var character in value.Normalize(NormalizationForm.FormD).ToLowerInvariant())
        {
            if ('a' <= character && character <= 'z') builder.Append(character);
        }

        return builder.ToString();
    }
}
