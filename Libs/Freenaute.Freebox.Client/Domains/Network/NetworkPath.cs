using System.Globalization;

namespace Freenaute.Freebox.Client.Domains.Network;

internal static class NetworkPath
{
    internal static string Number(long value) => value.ToString(CultureInfo.InvariantCulture);
}
