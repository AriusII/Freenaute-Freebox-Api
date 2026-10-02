using System.Globalization;
using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Client.Domains.Files;

internal static class FilesPath
{
    public static string Id(long id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        return id.ToString(CultureInfo.InvariantCulture);
    }

    public static string Opaque(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (value is "." or ".." || value.Any(char.IsControl)) throw new ArgumentException("Invalid opaque segment.", nameof(value));
        return Uri.EscapeDataString(value);
    }

    public static string Listing(EncodedFreeboxPath path, FileListingOptions? options)
    {
        var resource = $"fs/ls/{path.ToEscapedSegment()}";
        if (options is null) return resource;
        List<KeyValuePair<string, string>> values = [];
        Add(values, "onlyFolder", options.OnlyFolders);
        Add(values, "countSubFolder", options.CountSubFolders);
        Add(values, "removeHidden", options.RemoveHidden);
        if (options.ExifMode is not null)
        {
            if (options.ExifMode is not "light" and not "full" and not "base64")
                throw new ArgumentException("EXIF mode must be light, full or base64.", nameof(options));
            values.Add(new("exifMode", options.ExifMode));
        }
        if (options.Limit is not null)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(options.Limit.Value);
            values.Add(new("limit", options.Limit.Value.ToString(CultureInfo.InvariantCulture)));
        }
        if (options.Cursor is not null) values.Add(new("cursor", options.Cursor));
        return Query(resource, values);
    }

    public static string Advice(long diskId, long? partitionId, bool? dedicatedDisk)
    {
        List<KeyValuePair<string, string>> values = [];
        if (partitionId is not null) values.Add(new("partition_id", Id(partitionId.Value)));
        Add(values, "dedicated_disk", dedicatedDisk);
        return Query($"storage/disk/{Id(diskId)}/fsadvice", values);
    }

    private static void Add(List<KeyValuePair<string, string>> values, string name, bool? value)
    {
        if (value is not null) values.Add(new(name, value.Value ? "true" : "false"));
    }

    private static string Query(string path, IEnumerable<KeyValuePair<string, string>> values)
    {
        var query = string.Join('&', values.Select(pair => $"{Uri.EscapeDataString(pair.Key)}={Uri.EscapeDataString(pair.Value)}"));
        return query.Length == 0 ? path : path + '?' + query;
    }
}
