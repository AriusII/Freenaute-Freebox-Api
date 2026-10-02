namespace Freenaute.Freebox.Client;

/// <summary>Utilities for constructing API paths without interpreting user input as a route.</summary>
public static class FreeboxApiPath
{
    public static string EncodeSegment(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (value is "." or ".." || value.IndexOfAny(['/', '\\']) >= 0)
        {
            throw new ArgumentException("A route parameter must be one path segment.", nameof(value));
        }

        return Uri.EscapeDataString(value);
    }

    /// <summary>Encodes an opaque parameter such as a tracker URL, preserving separators inside that parameter.</summary>
    public static string EncodeOpaqueSegment(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (value is "." or ".." || value[0] is '/' or '\\' || value.Contains('\\') || value.Any(char.IsControl) ||
            value.Split('/').Any(part => part is "." or ".."))
        {
            throw new ArgumentException("An opaque parameter cannot contain traversal or control characters.", nameof(value));
        }
        return Uri.EscapeDataString(value);
    }

    internal static string NormalizeBasePath(string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        var relative = path.StartsWith('/') ? path[1..] : path;
        ValidateRelativePath(relative);
        if (relative.Contains('?'))
        {
            throw new ArgumentException("The API base path cannot contain a query.", nameof(path));
        }

        return "/" + relative.TrimEnd('/') + "/";
    }

    internal static void ValidateRelativePath(string relativePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(relativePath);
        if (relativePath.StartsWith('/') || relativePath.Contains('\\') || relativePath.Contains('#') ||
            Uri.TryCreate(relativePath, UriKind.Absolute, out _))
        {
            throw new ArgumentException("An API path must be relative to the versioned API root.", nameof(relativePath));
        }

        var queryIndex = relativePath.IndexOf('?');
        var path = queryIndex < 0 ? relativePath : relativePath[..queryIndex];
        if (path.Length == 0)
        {
            throw new ArgumentException("An API path must contain a resource path.", nameof(relativePath));
        }

        foreach (var segment in path.Split('/'))
        {
            var decoded = Uri.UnescapeDataString(segment);
            if (decoded is "." or ".." || decoded.StartsWith('/') || decoded.Contains('\\') ||
                decoded.Split('/').Any(part => part is "." or "..") || decoded.Any(char.IsControl))
            {
                throw new ArgumentException("An API path cannot contain traversal, leading encoded separators or control characters.", nameof(relativePath));
            }
        }
    }

    internal static Uri Resolve(Uri apiAddress, string relativePath)
    {
        ValidateRelativePath(relativePath);
        var resolved = new Uri(apiAddress, relativePath);
        if (!resolved.AbsoluteUri.StartsWith(apiAddress.AbsoluteUri, StringComparison.Ordinal))
        {
            throw new ArgumentException("An API path must stay within the versioned API root.", nameof(relativePath));
        }

        return resolved;
    }
}
