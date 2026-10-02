using System.Globalization;

namespace Freenaute.Freebox.Client.Domains.Services;

internal static class ServicesPath
{
    internal static string Id(long id) => id >= 0 ? id.ToString(CultureInfo.InvariantCulture)
        : throw new ArgumentOutOfRangeException(nameof(id));
    internal static string Segment(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (value is "." or "..") throw new ArgumentException("A resource identifier cannot be a relative path segment.", nameof(value));
        return Uri.EscapeDataString(value);
    }
}
